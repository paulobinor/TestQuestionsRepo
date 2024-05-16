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

[Route("api/Application")]
[ApiController]
public class CandidateController : ControllerBase
{
   // private readonly CosmosDBRepository _repository;
    private readonly ITestQuestionsService _testAppService;
    private readonly IMapper _mapper;
    //private readonly ABC.TestQuestions.AppService.Interfaces.IDBRepo _repository;

    public CandidateController(ITestQuestionsService testAppService)
    {
        _testAppService = testAppService;
        _mapper = MapperConfig.TestQuestionMapper();
    }


    [HttpPost("submit")]
    public async Task<ActionResult<TestQuestionDto>> SubmitForm([FromBody] ApplicationFormDataDto applicationFormData)
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
        var formData = _mapper.Map<ApplicationFormData>(applicationFormData);
        await _testAppService.SubmitApplicationForm(formData);
        return Ok("Submitted successfully");

       // var question = _mapper.Map<TestQuestion>(questionDto);  
       // var res = await _testAppService.AddNewQuestion(question);
       //// await _repository.AddAsync(questionDto);
       // return Ok(_mapper.Map<TestQuestionDto>(res));
    }
}
