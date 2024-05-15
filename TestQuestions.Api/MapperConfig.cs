using AutoMapper;
using System;

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
            }).CreateMapper();
           
            return mapper;
        }
    }
}
