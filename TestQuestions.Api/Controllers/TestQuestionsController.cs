// TestQuestionsController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestQuestions.Api;
using TestQuestions.Api.Dtos;
using TestQuestions.AppService;
using TestQuestions.Core.Interfaces;
using TestQuestions.Core.Models;

[Route("api/TestQuestions")]
[ApiController]
public class TestQuestionsController : ControllerBase
{
   // private readonly CosmosDBRepository _repository;
    private readonly ITestQuestionsService _testAppService;
    private readonly IMapper _mapper;
    //private readonly ABC.TestQuestions.AppService.Interfaces.IDBRepo _repository;

    public TestQuestionsController(ITestQuestionsService testAppService)
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

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<TestQuestionDto>> GetQuestion(string id)
    {
        var question = await _testAppService.GetTestQuestionById(id); // _repository.GetByIdAsync(id);
       // var question =  _repository.GetByIdAsync(id);
        if (question == null)
            return NotFound();

       // var questionDto = ;
        return Ok(_mapper.Map<TestQuestionDto>(question));
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<TestQuestionDto>>> GetQuestions(string? questionTypeId)
    {
         IEnumerable<TestQuestion> questions = null;
        if (questionTypeId == null)
        {
             questions = (await _testAppService.GetTestQuestions());
          //  questions = _mapper.Map<List<TestQuestionDto>>(res.ToList());
        }
        else
        {
            questions = await _testAppService.GetQuestionsByTypeAsync(questionTypeId);             
        }

        if (questions.Count() == 0)
            return NotFound("no items found");

        return Ok(_mapper.Map<IEnumerable<TestQuestionDto>>(questions));
    }

    //
    //public async Task<ActionResult<List<TestQuestionDto>>> GetAllQuestions()
    //{
    //    var questions = await _testAppService.GetTestQuestions();
    //    if (questions.Count() == 0)
    //        return NotFound("No test question where found");

    //    var questionDto = _mapper.Map<List<TestQuestionDto>>(questions.ToList());
    //    return Ok(questionDto);
    //}

    [HttpPost("Add")]
    public async Task<ActionResult<TestQuestionDto>> Post([FromBody] CreateTestQuestionDto questionDto)
    {
        if (!ModelState.IsValid)
        {
            var errorList = new List<string>();
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
            }
            return BadRequest(new { Errors = errorList });
        }

        var updateQuestionType = await _testAppService.GetQuestionTypeByIdAsync(questionDto.QuestionTypeId);
        if (updateQuestionType == null)
        {
            return NotFound("invalid questionTypeId provided");
        }

        var question = _mapper.Map<TestQuestion>(questionDto);  
        var res = await _testAppService.AddNewQuestion(question);
       // await _repository.AddAsync(questionDto);
        return Ok(_mapper.Map<TestQuestionDto>(res));
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<TestQuestionDto>> UpdateQuestion(string id, [FromBody] CreateTestQuestionDto testQuestionDto )
    {
        if (!ModelState.IsValid)
        {
            var errorList = new List<string>();
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
            }
            return BadRequest(new { Errors = errorList });
        }

        var existingQuestion = await _testAppService.GetTestQuestionById(id);
        if (existingQuestion == null)
        {
            return NotFound("The question you are trying to modify was not found or does not exist");
        }

        var updateQuestionType = await _testAppService.GetQuestionTypeByIdAsync(id);
        if (updateQuestionType == null)
        {
            return NotFound("invalid questionTypeId provided");
        }

        existingQuestion.Question = testQuestionDto.Question;
        existingQuestion.QuestionTypeId = testQuestionDto.QuestionTypeId;

        var res = await _testAppService.UpdateTestQuestionAsync(existingQuestion);

        return Ok(_mapper.Map<TestQuestionDto>(res));
    }
}
