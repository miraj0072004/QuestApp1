using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QuestionApi.Models;

namespace QuestionApi.Controllers
{
    public class UserPerformancesController : ApiController
    {
        private QuestionsContext db = new QuestionsContext();

        // GET: api/UserPerformances
        [Authorize]
        public IQueryable<UserPerformance> GetUserPerformances()
        {
            return db.UserPerformances;
        }

        //GET: api/UserPerformances/5
        [ResponseType(typeof(UserPerformance))]
        [Authorize]
        public IHttpActionResult GetUserPerformance(string id)
        {

            UserPerformance userPerformance = db.UserPerformances.Find(id);
            if (userPerformance == null)
            {
                return NotFound();
            }

            return Ok(userPerformance);
        }


        // GET: api/UserPerformances/MyPerformance
        [Route("api/UserPerformances/MyPerformance")]
        [ResponseType(typeof(UserPerformance))]
        [Authorize]
        public IHttpActionResult GetMyPerformance()
        {

            UserPerformance userPerformance = db.UserPerformances.Find(User.Identity.Name);
            if (userPerformance == null)
            {
                return NotFound();
            }

            return Ok(userPerformance);
        }

        // PUT: api/UserPerformances/5
        [ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult PutUserPerformance(string userEmail, UserPerformance userPerformance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userEmail != userPerformance.UserId)
            {
                return BadRequest();
            }

            UserPerformance currentPerformance= db.UserPerformances.First(x => x.UserId==userEmail);
            if (currentPerformance != null)
            {
                userPerformance.CorrectAnswerCount += currentPerformance.CorrectAnswerCount;
                userPerformance.TotalGamesCount += currentPerformance.TotalGamesCount;
                userPerformance.TotalQuestions += currentPerformance.TotalQuestions;
            }


            db.Entry(userPerformance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPerformanceExists(userPerformance.UserId))
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

        // PUT: api/UserPerformances/5
        [ResponseType(typeof(void))]
        [Route("api/UserPerformances/PutMyPerformance")]
        [Authorize]
        public IHttpActionResult PutMyPerformance(UserPerformance userPerformance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Identity.Name != userPerformance.UserId)
            {
                return BadRequest();
            }

            UserPerformance currentPerformance = db.UserPerformances.First(x => x.UserId == userPerformance.UserId);
            if (currentPerformance != null)
            {
                userPerformance.CorrectAnswerCount += currentPerformance.CorrectAnswerCount;
                userPerformance.TotalGamesCount = currentPerformance.TotalGamesCount+ 1;
                userPerformance.TotalQuestions += currentPerformance.TotalQuestions;
            }


            //db.Entry(userPerformance).State = EntityState.Modified;

            try
            {
                db.Set<UserPerformance>().AddOrUpdate(userPerformance);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPerformanceExists(userPerformance.UserId))
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

        // POST: api/UserPerformances
        [ResponseType(typeof(UserPerformance))]
        public IHttpActionResult PostUserPerformance(UserPerformance userPerformance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserPerformances.Add(userPerformance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserPerformanceExists(userPerformance.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userPerformance.UserId }, userPerformance);
        }

        // DELETE: api/UserPerformances/5
        [ResponseType(typeof(UserPerformance))]
        public IHttpActionResult DeleteUserPerformance(string id)
        {
            UserPerformance userPerformance = db.UserPerformances.Find(id);
            if (userPerformance == null)
            {
                return NotFound();
            }

            db.UserPerformances.Remove(userPerformance);
            db.SaveChanges();

            return Ok(userPerformance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserPerformanceExists(string id)
        {
            return db.UserPerformances.Count(e => e.UserId == id) > 0;
        }
    }
}