// CosmosDBRepository.cs

using TestQuestions.Core.Models;

namespace TestQuestions.AppService.Interfaces;

public interface IDBRepo
{
    Task<QuestionType> AddNewQuestionTypeAsync(QuestionType questionType);
    Task AddTestQuestionAsync(TestQuestion question);
    Task DeleteQuestionAsync(string id);
    Task DeleteQuestionTypeAsync(string id);
    Task<IEnumerable<TestQuestion>> GetAllQuestionsAsync();
    Task<IEnumerable<QuestionType>> GetAllQuestionTypesAsync();
    Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(string questionTypeName);
    Task<QuestionType> GetQuestionTypeByIdAsync(string id);
    Task<TestQuestion> GetTestQuestionByIdAsync(string id);
    Task<TestQuestion> UpdateQuestionAsync(TestQuestion question);
    Task UpdateQuestionTypeAsync(QuestionType questionType);
}