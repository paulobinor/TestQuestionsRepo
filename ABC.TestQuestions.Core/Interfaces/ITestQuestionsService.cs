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
        Task AddNewQuestion(TestQuestion question);
        Task<TestQuestion> GetQuestionById(string id);
        Task<IEnumerable<TestQuestion>> GetQuestions();
        Task<IEnumerable<TestQuestion>> GetQuestionsByType(string questionType);
        Task<TestQuestion> UpdateQuestionAsync(TestQuestion question);
    }
}
