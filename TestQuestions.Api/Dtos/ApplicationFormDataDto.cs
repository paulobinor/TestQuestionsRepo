
namespace TestQuestions.Api.Dtos;
public class ApplicationFormDataDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Nationality { get; set; }
    public string CurrentResidence { get; set; }
    public string IDNumber { get; set; }
    public string DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string About { get; set; }
    public List<string> MultipleChoice { get; set; }
    public bool IsRejected { get; set; } = false;
    public string YearsofExperience { get; set; }
}
