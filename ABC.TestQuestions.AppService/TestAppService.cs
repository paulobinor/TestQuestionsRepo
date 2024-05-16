using TestQuestions.AppService.Interfaces;
using TestQuestions.Core.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using TestQuestions.Core.Interfaces;

namespace TestQuestions.AppService
{
   
    public class TestAppService : ITestQuestionsService
    {
        private readonly IDBRepo _dBRepo;
        public TestAppService(IDBRepo dBRepo)
        {
            _dBRepo = dBRepo;
        }
        public async Task<TestQuestion> AddNewQuestion(TestQuestion question)
        {
           return await _dBRepo.AddTestQuestionAsync(question);
            // await _container.CreateItemAsync(question);
        }

        public async Task<TestQuestion> GetTestQuestionById(string id)
        {
            return await _dBRepo.GetTestQuestionByIdAsync(id); // _container.ReadItemAsync<TestQuestion>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<TestQuestion>> GetTestQuestionsByType(string questionType)
        {
            return await _dBRepo.GetQuestionsByTypeAsync(questionType);
        }

        public async Task<IEnumerable<TestQuestion>> GetTestQuestions()
        {
            var results = await _dBRepo.GetAllQuestionsAsync();
            return results;
        }
        public async Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(string questionTypeName)
        {
            return await _dBRepo.GetQuestionsByTypeAsync(questionTypeName);
        }

        public async Task<TestQuestion> UpdateTestQuestionAsync(TestQuestion question)
        {
            var res = await _dBRepo.UpdateQuestionAsync(question); // _container.ReplaceItemAsync(question, question.Id, new PartitionKey(question.Id));
            return res;
        }

        //Question types
        public async Task<QuestionType> GetTestQuestionType(string questionType)
        {
            var res = await _dBRepo.GetQuestionTypeAsync(questionType); // _container.ReplaceItemAsync(question, question.Id, new PartitionKey(question.Id));
            return res;
        }

        public async Task<QuestionType> AddNewQuestionTypeAsync(QuestionType questionType)
        {
            var response = await _dBRepo.AddNewQuestionTypeAsync(questionType);
            return response;
        }

        public async Task<QuestionType> UpdateQuestionTypeAsync(QuestionType questionType)
        {
            return await _dBRepo.UpdateQuestionTypeAsync(questionType);
        }

        public async Task DeleteQuestionTypeAsync(string id)
        {
            await _dBRepo.DeleteQuestionTypeAsync(id);
        }

        public async Task<QuestionType> GetQuestionTypeByIdAsync(string id)
        {
            try
            {
                var response = await _dBRepo.GetQuestionTypeByIdAsync(id);
                return response;
            }
            catch (CosmosException ex) //when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<QuestionType>> GetAllQuestionTypesAsync()
        {
            return await _dBRepo.GetAllQuestionTypesAsync();
        }

        //Submit form data
        public async Task SubmitApplicationForm(ApplicationFormData applicationFormData)
        {
            await _dBRepo.SubmitFormDataAsync(applicationFormData);
        }
    }
}
