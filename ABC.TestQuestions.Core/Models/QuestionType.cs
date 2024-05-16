
using Newtonsoft.Json;

namespace TestQuestions.Core.Models;
// QuestionType.cs
public class QuestionType
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    //public string questionTypeId { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }
}