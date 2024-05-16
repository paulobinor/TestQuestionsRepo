using AutoMapper;
using System;
using TestQuestions.Api.Dtos;

namespace TestQuestions.Api
{
    public class MapperConfig : Profile
    {
        public static IMapper TestQuestionMapper()
        {
            //Provide all the Mapping Configuration
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TestQuestions.Core.Models.TestQuestion, TestQuestionDto>().ReverseMap();
                cfg.CreateMap<TestQuestions.Core.Models.TestQuestion, CreateTestQuestionDto>().ReverseMap();
                cfg.CreateMap<TestQuestions.Core.Models.QuestionType, QuestionTypeDto>().ReverseMap();
                cfg.CreateMap<TestQuestions.Core.Models.ApplicationFormData, ApplicationFormDataDto>().ReverseMap();
            }).CreateMapper();
           
            return mapper;
        }
    }
}
