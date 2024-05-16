
using Newtonsoft.Json;

namespace TestQuestions.Core.Models;
public class ApplicationFormData
{
    [JsonProperty("id")]
    public string id { get; set; } = Guid.NewGuid().ToString();
    [JsonProperty("FirstName")]
    public string FirstName { get; set; }
    [JsonProperty("LastName")]
    public string LastName { get; set; }
    [JsonProperty("Email")]
    public string Email { get; set; }
    [JsonProperty("Phone")]
    public string Phone { get; set; }
    [JsonProperty("Nationality")]
    public string Nationality { get; set; }
    [JsonProperty("CurrentResidence")]
    public string CurrentResidence { get; set; }
    [JsonProperty("IDNumber")]
    public string IDNumber { get; set; }
    [JsonProperty("DateOfBirth")]
    public string DateOfBirth { get; set; }
    [JsonProperty("Gender")]
    public string Gender { get; set; }
    [JsonProperty("About")]
    public string About { get; set; }
    [JsonProperty("MultipleChoice")]
    public List<string> MultipleChoice { get; set; }
    [JsonProperty("IsRejected")]
    public bool IsRejected { get; set; }
    [JsonProperty("YearsofExperience")]
    public string YearsofExperience { get; set; }
}
