using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionRevisedApi.Data;
using QuestionRevisedApi.Dtos;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private IQuestionsRepository _questionsRepository;

        private readonly IMapper _mapper;

        public QuestionsController(IQuestionsRepository questionsRepository,
                                    IMapper mapper)
        {
            _mapper = mapper;
            _questionsRepository = questionsRepository;
        }

        // GET: api/Questions
        [HttpGet]
        public IActionResult GetQuestions()
        {
            var questionsFromRepo = _questionsRepository.GetQuestions();
            var questions = _mapper.Map<IEnumerable<QuestionForListDto>>(questionsFromRepo);

            return Ok(questions);

        }

        [Authorize(Roles = Role.Admin)]
        // GET: api/Questions/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetQuestion(int id)
        {
            var questionFromRepo = _questionsRepository.GetQuestion(id);
            var question = _mapper.Map<QuestionForDetailDto>(questionFromRepo);
            return Ok(question);
        }

        // POST: api/Questions
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
