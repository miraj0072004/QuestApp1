﻿using System;
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
        public async Task<IActionResult> GetQuestions()
        {
            var questionsFromRepo = _questionsRepository.GetQuestions();
            var questions = _mapper.Map<IEnumerable<QuestionForListDto>>(questionsFromRepo);

            return Ok(questions);

        }

        [Authorize(Roles = Role.Admin)]
        // GET: api/Questions/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var questionFromRepo =_questionsRepository.GetQuestion(id);
            var question = _mapper.Map<QuestionForDetailDto>(questionFromRepo);
            return Ok(question);
        }

        // POST: api/Questions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QuestionCreateDto questionCreateDto)
        {
            var question = _mapper.Map<Question>(questionCreateDto);
            var createdQuestion = _questionsRepository.CreateQuestion(question);
            var questionToReturn = _mapper.Map<QuestionForDetailDto>(createdQuestion);

            return Ok(questionToReturn);
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] QuestionUpdateDto questionUpdateDto)
        {
            var questionFromRepo = _questionsRepository.GetQuestion(id);

            if(questionFromRepo == null)
            {
                return BadRequest("The question doesn't exist");
            }

            
            questionUpdateDto.Id = id;
            questionFromRepo = _mapper.Map<Question>(questionUpdateDto);
            var updatedQuestion =_questionsRepository.UpdateQuestion(questionFromRepo);
            var questionToReturn = _mapper.Map<QuestionForDetailDto>(updatedQuestion);

            return Ok(questionToReturn);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedQuestion = _questionsRepository.DeleteQuestion(id);

            if(deletedQuestion == null)
            {
                return BadRequest("The question doesn't exist");
            }

            var questionToReturn = _mapper.Map<QuestionForDetailDto>(deletedQuestion);

            return Ok(questionToReturn);


        }
    }
}
