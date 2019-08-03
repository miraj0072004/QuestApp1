using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuestionApi.Models;
using Microsoft.AspNet.Identity;


namespace QuestionApi.Controllers
{
    public class QuestionsController : ApiController
    {
        private QuestionsContext db = new QuestionsContext();

        // GET: api/Questions
        [Authorize]
        public IQueryable<Question> GetQuestions()
        {
            //ICollection<Question> questions = db.Questions.ToArray<Question>();

            //foreach(Question question in questions )
            //{
            //    question.Answers = db.Answers.Where(x => x.QuestionId == question.Id);
            //}
            return db.Questions.Include("Answers");
        }

        [Authorize]
        [Route("api/Questions/Count")]
        public IHttpActionResult GetQuestionsCount()
        {
            //ICollection<Question> questions = db.Questions.ToArray<Question>();

            //foreach(Question question in questions )
            //{
            //    question.Answers = db.Answers.Where(x => x.QuestionId == question.Id);
            //}
            var count= db.Questions.Count();
            return Ok(count);
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        [Authorize]
        public IHttpActionResult GetQuestion(int id)
        {
            //var question = db.Questions.Find(id);
            var question = db.Questions.Where(x=>x.Id==id).Include(x=>x.Answers);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }

            db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Questions
        [Authorize]
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string userId = User.Identity.GetUserId();
            question.UserId = userId;
            db.Questions.Add(question);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.Questions.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(int id)
        {
            return db.Questions.Count(e => e.Id == id) > 0;
        }

        [Route("api/Questions/All")]
        public IQueryable<int> GetAllQuestionIds ()
        {
            return db.Questions.Select(x=>x.Id);
        }


    }
}