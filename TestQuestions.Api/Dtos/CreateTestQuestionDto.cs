
using System.ComponentModel.DataAnnotations;
namespace TestQuestions.Api.Dtos;

public class CreateTestQuestionDto
{
    [Required( ErrorMessage ="Question is required")]
    public string Question { get; set; }

    [Required(ErrorMessage = "QuestionTypeId is required")]
    public string QuestionTypeId { get; set; }
}
