
using Newtonsoft.Json;

namespace TestQuestions.Core.Models;
public class TestQuestion
{

    [JsonProperty("id")]
    public string Id { get; set; }

    //[JsonProperty("myid")]
    //public string myid { get; set; }
    //[JsonProperty("Question")]
    public string Question { get; set; }
    [JsonProperty("QuestionType")]
    public string QuestionTypeId { get; set; }
}
