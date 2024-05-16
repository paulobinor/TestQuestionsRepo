// TestQuestionsController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestQuestions.Api;
using TestQuestions.AppService;
using TestQuestions.Core.Interfaces;
using TestQuestions.Core.Models;

[Route("api/[controller]")]
[ApiController]
public class TestQuestionsTypeController : ControllerBase
{
   // private readonly CosmosDBRepository _repository;
    private readonly ITestQuestionsService _testAppService;
    private readonly IMapper _mapper;
    //private readonly ABC.TestQuestions.AppService.Interfaces.IDBRepo _repository;

    public TestQuestionsTypeController(ITestQuestionsService testAppService)
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
    public async Task<ActionResult<TestQuestionDto>> GetQuestionType(string id)
    {
        var question = await _testAppService.GetTestQuestionById(id); // _repository.GetByIdAsync(id);
       // var question =  _repository.GetByIdAsync(id);
        if (question == null)
            return NotFound();

        var questionDto = _mapper.Map<TestQuestionDto>(question);
        return Ok(questionDto);
    }

    [HttpGet("{questionTypeName}")]
    public async Task<IActionResult> GetByQuestionTypeName(string questionTypeName)
    {
        var questions = await _testAppService.GetTestQuestionType(questionTypeName);
        if (questions == null)
        {
            return NotFound();
        }
        return Ok(questions);
    }

    [HttpPost]
    public async Task<ActionResult<QuestionTypeDto>> AddNew(string questionTypeName)
    {
        var existingQuestion = await _testAppService.GetTestQuestionType(questionTypeName);
        if (existingQuestion != null)
        {
            return BadRequest("QuestionType already exists");
        }
        //var questionType = _mapper.Map<QuestionType>(new crea);  
        var res = await _testAppService.AddNewQuestionTypeAsync(new QuestionType { Name = questionTypeName });
       // await _repository.AddAsync(questionDto);
        return Ok(_mapper.Map<QuestionTypeDto>(res));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestionType(string id, TestQuestionDto question)
    {
        var existingQuestion = await _testAppService.GetTestQuestionById(id);
        if (existingQuestion == null)
        {
            return NotFound();
        }

        existingQuestion.Question = question.Question;
        existingQuestion.QuestionTypeId = question.QuestionTypeId;

        await _testAppService.UpdateTestQuestionAsync(existingQuestion);

        return NoContent();
    }
}
