// TestQuestionsController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestQuestions.Api;
using TestQuestions.AppService;
using TestQuestions.Core.Interfaces;
using TestQuestions.Core.Models;

[Route("api/QuestionTypes")]
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

    //[HttpGet("get/{id}")]
    //public async Task<ActionResult<TestQuestionDto>> GetQuestionType(string id)
    //{
    //    var question = await _testAppService.GetTestQuestionById(id); // _repository.GetByIdAsync(id);
    //   // var question =  _repository.GetByIdAsync(id);
    //    if (question == null)
    //        return NotFound();

    //    var questionDto = _mapper.Map<TestQuestionDto>(question);
    //    return Ok(questionDto);
    //}

    [HttpGet("get/{questionTypeName}")]
    public async Task<IActionResult> GetByQuestionTypeName(string questionTypeName)
    {
        var questions = await _testAppService.GetTestQuestionType(questionTypeName);
        if (questions == null)
        {
            return NotFound();
        }
        return Ok(questions);
    }
    [HttpGet("list")]
    public async Task<ActionResult<List<QuestionTypeDto>>> GetQuestionTypes()
    {
        var questionTypes = await _testAppService.GetAllQuestionTypesAsync(); 
        if (questionTypes.Count() == 0)
            return NotFound("No Question types where found");

        var questionDto = _mapper.Map<List<QuestionTypeDto>>(questionTypes.ToList());
        return Ok(questionDto);
    }
    [HttpPost("add")]
    public async Task<ActionResult<QuestionTypeDto>> AddNew(string questionTypeName)
    {
        if (string.IsNullOrEmpty(questionTypeName))
        {
            return BadRequest("questionTypeName is required");

        }
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

    [HttpPut("update/{id}")]
    public async Task<ActionResult<QuestionTypeDto>> UpdateQuestionType(string id, [FromBody] string questionTypeName)
    {
        if (string.IsNullOrEmpty(questionTypeName))
        {
            return BadRequest("questionTypeName is required");

        }
        var updateQuestionType = await _testAppService.GetQuestionTypeByIdAsync(id);
        if (updateQuestionType == null)
        {
            return NotFound();
        }

        updateQuestionType.Name = questionTypeName;

        var res = await _testAppService.UpdateQuestionTypeAsync(updateQuestionType);

        return Ok(_mapper.Map<QuestionTypeDto>(res));
    }
}
