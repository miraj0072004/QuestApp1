using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuestionRevisedApi.Dtos;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,UserForClientDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Question, QuestionForListDto>();
            CreateMap<Question, QuestionForDetailDto>();
            CreateMap<QuestionCreateDto, Question>();
            CreateMap<QuestionUpdateDto, Question>();
        }
    }
}
