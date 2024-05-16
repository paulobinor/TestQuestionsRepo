using TestQuestions.AppService.Interfaces;
using TestQuestions.Core.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TestQuestions.Repo
{
    public class CosmosDbRepo : IDBRepo
    {
        private readonly Microsoft.Azure.Cosmos.Container _questionsContainer;
        private readonly Microsoft.Azure.Cosmos.Container _questionTypeContainer;
        private readonly Microsoft.Azure.Cosmos.Container _candidateFormDataContainer;

        public CosmosDbRepo(IConfiguration config)
        {
            var cosmosSettings = config.GetSection("CosmosSettings");
            var cosmosClient = new CosmosClient(cosmosSettings["EndpointUri"], cosmosSettings["PrimaryKey"]);
            var database = cosmosClient.GetDatabase(cosmosSettings["DatabaseName"]);
            _questionsContainer = database.GetContainer(cosmosSettings["QuestionsContainerName"]);
            _questionTypeContainer = database.GetContainer(cosmosSettings["QuestionTypeContainerName"]);
            _candidateFormDataContainer = database.GetContainer(cosmosSettings["CandidateFormDataContainerName"]);
            
        }

        public async Task<TestQuestion> AddTestQuestionAsync(TestQuestion question)
        {
            try
            {
               return await _questionsContainer.CreateItemAsync(question);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TestQuestion> GetTestQuestionByIdAsync(string id)
        {
            try
            {
                return await _questionsContainer.ReadItemAsync<TestQuestion>(id, new PartitionKey(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(string questionTypeId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.QuestionTypeId =@questionTypeId ").WithParameter("@questionTypeId", questionTypeId);

            var queryResult = _questionsContainer.GetItemQueryIterator<TestQuestion>(query);

            List<TestQuestion> results = new List<TestQuestion>();
            while (queryResult.HasMoreResults)
            {
                var currentResultSet = await queryResult.ReadNextAsync();
                results.AddRange(currentResultSet);
            }

            return results;
        }

        public async Task<IEnumerable<TestQuestion>> GetAllQuestionsAsync()
        {
            try
            {
                var query = _questionsContainer.GetItemQueryIterator<TestQuestion>();
                var results = new List<TestQuestion>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response.ToList());
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TestQuestion> UpdateQuestionAsync(TestQuestion question)
        {
            try
            {
                var res = await _questionsContainer.ReplaceItemAsync<TestQuestion>(question, question.Id, new PartitionKey(question.Id));
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteQuestionAsync(string id)
        {
            await _questionTypeContainer.DeleteItemAsync<TestQuestion>(id, new PartitionKey(id));
        }

        //Question Types
        public async Task<QuestionType> AddNewQuestionTypeAsync(QuestionType questionType)
        {
           // questionType.questionTypeId = questionType.Id;
            var response = await _questionTypeContainer.CreateItemAsync(questionType);
            return response.Resource;
        }

        public async Task<QuestionType> UpdateQuestionTypeAsync(QuestionType questionType)
        {
            try
            {
                
                var res = await _questionTypeContainer.ReplaceItemAsync<QuestionType>(questionType, questionType.Id, new PartitionKey(questionType.Id));
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteQuestionTypeAsync(string id)
        {
            await _questionTypeContainer.DeleteItemAsync<QuestionType>(id, new PartitionKey(id));
        }


        public async Task<QuestionType> GetQuestionTypeAsync(string questionType)
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.Name = @name").WithParameter("@name", questionType);

                var queryResultSetIterator = _questionTypeContainer.GetItemQueryIterator<QuestionType>(query);

                if (queryResultSetIterator.HasMoreResults)
                {
                    var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    return currentResultSet.FirstOrDefault();
                }
                return null;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<QuestionType> GetQuestionTypeByIdAsync(string id)
        {
            try
            {
                //var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", id);

                //var queryResultSetIterator = _questionTypeContainer.GetItemQueryIterator<QuestionType>(query);
                //if (queryResultSetIterator.HasMoreResults)
                //{
                //    var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                //    return currentResultSet.FirstOrDefault();
                //}
                var response = await _questionTypeContainer.ReadItemAsync<QuestionType>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex)  when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<QuestionType>> GetAllQuestionTypesAsync()
        {
            // var query = new QueryDefinition("SELECT * FROM c");
            var queryResultSetIterator = _questionTypeContainer.GetItemQueryIterator<QuestionType>();

            List<QuestionType> results = new List<QuestionType>();
            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                results.AddRange(currentResultSet);
            }

            return results;
        }

        public async Task SubmitFormDataAsync(ApplicationFormData applicationFormData)
        {
            await _candidateFormDataContainer.CreateItemAsync(applicationFormData);
           // return response.Resource;
        }
    }
}
