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

        public CosmosDbRepo(IConfiguration config)
        {
            var cosmosSettings = config.GetSection("CosmosSettings");
            var cosmosClient = new CosmosClient(cosmosSettings["EndpointUri"], cosmosSettings["PrimaryKey"]);
            var database = cosmosClient.GetDatabase(cosmosSettings["DatabaseName"]);
            _questionsContainer = database.GetContainer(cosmosSettings["QuestionsContainerName"]);
            _questionTypeContainer = database.GetContainer(cosmosSettings["TestQuestionTypeContainer"]);
        }

        public async Task AddTestQuestionAsync(TestQuestion question)
        {
            try
            {
                var res = await _questionsContainer.CreateItemAsync(question);

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

        public async Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(string questionTypeName)
        {
            var query = new QueryDefinition(
             "SELECT q.* FROM TestQuestions q JOIN QuestionTypes qt ON q.QuestionTypeId = qt.Id " +
             "WHERE qt.Name = @questionTypeName").WithParameter("@questionTypeName", questionTypeName);

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
                var res = await _questionsContainer.ReplaceItemAsync(question, question.Id, new PartitionKey(question.Id));
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
            var response = await _questionTypeContainer.CreateItemAsync(questionType, new PartitionKey(questionType.Id));
            return response.Resource;
        }

        public async Task UpdateQuestionTypeAsync(QuestionType questionType)
        {
            await _questionTypeContainer.ReplaceItemAsync(questionType, questionType.Id, new PartitionKey(questionType.Id));
        }

        public async Task DeleteQuestionTypeAsync(string id)
        {
            await _questionTypeContainer.DeleteItemAsync<QuestionType>(id, new PartitionKey(id));
        }

        public async Task<QuestionType> GetQuestionTypeByIdAsync(string id)
        {
            try
            {
                var response = await _questionTypeContainer.ReadItemAsync<QuestionType>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
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
    }
}
