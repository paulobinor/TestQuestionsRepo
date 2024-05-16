using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

using Microsoft.VisualStudio.TestPlatform.TestHost;
namespace TestQuestions.Tests
{
    public class TestQuestionsUnitTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public TestQuestionsUnitTest()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [Fact]
        public async Task Post_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newQuestion =  new { Question = "Test question", QuestionTypeId = "test101" };
            var content = new StringContent(JsonConvert.SerializeObject(newQuestion), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/TestQuestions", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        //[Fact]
        //public async Task AddQuestionType_ReturnsSuccessStatusCode()
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();
        //   // var newQuestion = new QuestionTypeDto { Question = "New Test question" };
        //    var content = new StringContent(JsonConvert.SerializeObject(new { Name = "Paragraph"}), Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await client.PostAsync("/api/QuestionTypes/Add", content);

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}


        [Fact]
        public async Task Put_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newQuestion = new  { Question = "Updated Test question", QuestionTypeId = "c5e2f06a-291b-4d5e-85eb-08f897ab5871" };
            var content = new StringContent(JsonConvert.SerializeObject(newQuestion), Encoding.UTF8, "application/json");
            var postResponse = await client.PostAsync("/api/TestQuestions", content);
            var jsonQuestion = await postResponse.Content.ReadAsStringAsync();
            var createdQuestion = JsonConvert.DeserializeObject<dynamic>(jsonQuestion);

            // Update the question
            var updatedQuestion = new 
            {
                Id = Convert.ToString(createdQuestion.Id),
                Question = "Updated Test question",
                QuestionTypeId = "ty001" // Change the type
            };
            var updateContent = new StringContent(JsonConvert.SerializeObject(updatedQuestion), Encoding.UTF8, "application/json");

            // Act
            var putResponse = await client.PutAsync($"/api/TestQuestions/update/{createdQuestion.Id}", updateContent);

            // Assert
            putResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
        }

        //[Fact]
        //public async Task Get_ReturnsSuccessStatusCode()
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();
        //   // var httpClient = new TestHttpClient(client);
        //    var newQuestion = new TestQuestion { Question = "Test question", QuestionType = "MultipleChoice" };

        //    // Create the question
        //    var createdQuestion = await client.PostQuestionAsync(newQuestion);

        //    // Act
        //    var response = await client.GetAsync($"/api/TestQuestions/{createdQuestion.Id}");

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //    // Verify the returned question
        //    var returnedQuestion = await response.Content.ReadAsAsync<TestQuestion>();
        //    Assert.NotNull(returnedQuestion);
        //    Assert.Equal(createdQuestion.Id, returnedQuestion.Id);
        //    Assert.Equal(createdQuestion.Question, returnedQuestion.Question);
        //    Assert.Equal(createdQuestion.QuestionType, returnedQuestion.QuestionType);
        //}
    }
}