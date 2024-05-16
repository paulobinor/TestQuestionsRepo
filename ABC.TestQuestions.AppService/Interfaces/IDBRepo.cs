// CosmosDBRepository.cs

using TestQuestions.Core.Models;

namespace TestQuestions.AppService.Interfaces;

public interface IDBRepo
{
    Task<TestQuestion> AddTestQuestionAsync(TestQuestion question);
    Task<TestQuestion> UpdateQuestionAsync(TestQuestion question);
    Task DeleteQuestionAsync(string id);
    Task<IEnumerable<TestQuestion>> GetAllQuestionsAsync();
    Task<IEnumerable<QuestionType>> GetAllQuestionTypesAsync();
    Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(string questionTypeName);
    Task<QuestionType> GetQuestionTypeAsync(string questionType);
    Task<QuestionType> GetQuestionTypeByIdAsync(string id);
    Task<TestQuestion> GetTestQuestionByIdAsync(string id);
    Task<QuestionType> AddNewQuestionTypeAsync(QuestionType questionType);
    Task<QuestionType> UpdateQuestionTypeAsync(QuestionType questionType);
    Task DeleteQuestionTypeAsync(string id);
    Task SubmitFormDataAsync(ApplicationFormData applicationFormData);
}