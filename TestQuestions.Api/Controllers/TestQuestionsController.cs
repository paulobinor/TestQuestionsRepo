// TestQuestionsController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestQuestions.Api;
using TestQuestions.AppService;
using TestQuestions.Core.Models;

[Route("api/[controller]")]
[ApiController]
public class TestQuestionsController : ControllerBase
{
   // private readonly CosmosDBRepository _repository;
    private readonly TestAppService _testAppService;
    private readonly IMapper _mapper;
    //private readonly ABC.TestQuestions.AppService.Interfaces.IDBRepo _repository;

    public TestQuestionsController(TestAppService testAppService)
    {
        _testAppService = testAppService;
        _mapper = MapperConfig.TestQuestionMapper();
    }

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<TestQuestionDto>>> Get()
    //{
    //    var questions = await _repository.GetAllAsync();
    //    return Ok(questions);
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<TestQuestionDto>> GetQuestion(string id)
    {
        var question = await _testAppService.GetQuestionById(id); // _repository.GetByIdAsync(id);
       // var question =  _repository.GetByIdAsync(id);
        if (question == null)
            return NotFound();

        var questionDto = _mapper.Map<TestQuestionDto>(question);
        return Ok(questionDto);
    }

    [HttpPost]
    public async Task<ActionResult<TestQuestionDto>> Post(TestQuestionDto questionDto)
    {
        var question = _mapper.Map<TestQuestion>(questionDto);  
        await _testAppService.AddNewQuestion(question);
       // await _repository.AddAsync(questionDto);
        return CreatedAtAction(nameof(GetQuestion), new { id = questionDto.Id }, questionDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion(string id, TestQuestionDto question)
    {
        var existingQuestion = await _testAppService.GetQuestionById(id);
        if (existingQuestion == null)
        {
            return NotFound();
        }

        existingQuestion.Question = question.Question;
        existingQuestion.QuestionTypeId = question.QuestionTypeId;

        await _testAppService.UpdateQuestionAsync(existingQuestion);

        return NoContent();
    }
}
