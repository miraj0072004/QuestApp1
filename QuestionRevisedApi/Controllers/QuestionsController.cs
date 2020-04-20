using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionRevisedApi.Data;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private IQuestionsRepository _questionsRepository;


        public QuestionsController(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository; 
        }

        // GET: api/Questions
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _questionsRepository.GetQuestions();
        }

        [Authorize(Roles = Role.Admin)]
        // GET: api/Questions/5
        [HttpGet("{id}", Name = "Get")]
        public Question Get(int id)
        {
            return _questionsRepository.GetQuestion(id);
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
