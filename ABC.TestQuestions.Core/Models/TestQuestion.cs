
using Newtonsoft.Json;

namespace TestQuestions.Core.Models;
public class TestQuestion
{

    [JsonProperty("id")]
    public string Id { get; set; } =  Guid.NewGuid().ToString();

    [JsonProperty("Question")]
    public string Question { get; set; }

    [JsonProperty("QuestionTypeId")]
    public string QuestionTypeId { get; set; }
}
