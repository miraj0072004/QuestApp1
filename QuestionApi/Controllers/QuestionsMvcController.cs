using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuestionApi.Models;

namespace QuestionApi.Controllers
{
   
    public class QuestionsMvcController : Controller
    {
        private QuestionsContext db = new QuestionsContext();

        // GET: QuestionsMvc
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: QuestionsMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: QuestionsMvc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionsMvc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuestionText,AnswerDescription,UserId,Answers")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.UserId = "tempUserId";

               
                //db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Answers ON");
                //question.Id = db.Questions.Max(q => q.Id) + 1;

                for (int i = 1; i <= question.Answers.Count; i++)
                {
                    question.Answers[i - 1].QuestionAnswerId = i;
                    //question.Answers[i - 1].Id = db.Answers.Max(a=>a.Id) + 1;
                }
                //db.Database.Connection.Open();
                //db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Questions ON");
                //question.Id = db.Questions.Max(q => q.Id) + 1;
                db.Questions.Add(question);
                
                //db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Answers OFF");
                db.SaveChanges();
                //db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Questions OFF");
                //db.Database.Connection.Close();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: QuestionsMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: QuestionsMvc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuestionText,AnswerDescription,UserId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: QuestionsMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: QuestionsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
