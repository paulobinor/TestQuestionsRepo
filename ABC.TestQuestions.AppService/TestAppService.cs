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
        public async Task AddNewQuestion(TestQuestion question)
        {
            await _dBRepo.AddTestQuestionAsync(question);

            // await _container.CreateItemAsync(question);
        }

        public async Task<TestQuestion> GetQuestionById(string id)
        {

            return await _dBRepo.GetTestQuestionByIdAsync(id); // _container.ReadItemAsync<TestQuestion>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<TestQuestion>> GetQuestionsByType(string questionType)
        {

            return await _dBRepo.GetQuestionsByTypeAsync(questionType);
        }

        public async Task<IEnumerable<TestQuestion>> GetQuestions()
        {
            var results = await _dBRepo.GetAllQuestionsAsync(); 
            return results;
        }

        public async Task<TestQuestion> UpdateQuestionAsync(TestQuestion question)
        {
            var res = await _dBRepo.UpdateQuestionAsync(question); // _container.ReplaceItemAsync(question, question.Id, new PartitionKey(question.Id));
            return res;
        }

    }
}
