using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestQuestions.Core.Models;

namespace TestQuestions.Core.Interfaces
{
    public interface ITestQuestionsService
    {
        Task<TestQuestion> AddNewQuestion(TestQuestion question);
        Task<QuestionType> AddNewQuestionTypeAsync(QuestionType questionType);
        Task DeleteQuestionTypeAsync(string id);
        Task<IEnumerable<QuestionType>> GetAllQuestionTypesAsync();
        Task<IEnumerable<TestQuestion>> GetQuestionsByTypeAsync(string questionTypeName);
        Task<QuestionType> GetQuestionTypeByIdAsync(string id);
        Task<TestQuestion> GetTestQuestionById(string id);
        Task<IEnumerable<TestQuestion>> GetTestQuestions();
        Task<IEnumerable<TestQuestion>> GetTestQuestionsByType(string questionType);
        Task<QuestionType> GetTestQuestionType(string questionType);
        Task SubmitApplicationForm(ApplicationFormData applicationFormData);
        Task<QuestionType> UpdateQuestionTypeAsync(QuestionType questionType);
        Task<TestQuestion> UpdateTestQuestionAsync(TestQuestion question);
    }
}
