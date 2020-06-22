using p00.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;
using System.Reflection;

namespace WebApplication2.Controllers
{
   
        public class HomeController : Controller
        {
            private ApplicationDbContext db = new ApplicationDbContext();
            public ActionResult Index()
            {
                var topicEVs = db.TopicEVs.Include(t => t.Document).Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
                return View(topicEVs.ToList());
            }

            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            // GET: Document/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: Document/Create
            [HttpPost]
            public ActionResult Create(int id, HttpPostedFileBase upload)
            {

                // TODO: Add insert logic here
                //if (id != null)
                //{


                db.Documents.Add(new Document { Id = id, Name = upload.FileName });
                db.SaveChanges();
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                //}
                //else
                //    return HttpNotFound();

                return RedirectToAction("Index");

            }

            public ActionResult Assent()
            {
                var topicEVs = db.TopicEVs.Include(t => t.Document).Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
                return View(topicEVs.ToList());
            }
            [HttpPost]
            public ActionResult Assent(int id)
            {
                return View();
            }
            // GET: TopicEVs/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TopicEV topicEV = db.TopicEVs.Find(id);
                if (topicEV == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id = new SelectList(db.Documents, "Id", "Name", topicEV.Id);
                ViewBag.EvaluationFormId = new SelectList(db.EvaluationForm, "id", "id", topicEV.EvaluationFormId);
                ViewBag.SectionsId = new SelectList(db.Sections, "Id", "SectionName", topicEV.SectionsId);
                ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName", topicEV.TeacherId);
                ViewBag.TopicsId = new SelectList(db.Topics, "Id", "TopicName", topicEV.TopicsId);
               
            return View(topicEV);
            }

            // POST: TopicEVs/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,EvaluationFormId,SectionsId,TopicsId,TeacherId,Points,Approved,Nameproved")] TopicEV topicEV, string submitButton)
            {
            
                if (ModelState.IsValid)
                {
                if (submitButton == "موافقه")
                { 
                    topicEV.Approved = true;
                    var UserID = User.Identity.GetUserId();
                    var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
                    var CurrentTeacher = db.UserToTeachers.Where(a => a.UserID == UserID).SingleOrDefault();
                    var teacher = db.Teachers.Find(CurrentTeacher.TeacherID);
                    var topics = db.Topics.Find(topicEV.TopicsId);
                    var document = db.Documents.Find(topicEV.Id);
                    topicEV.Nameproved = teacher.FullName;
                    if (document!= null)
                    {
                        topicEV.Points = (topicEV.Points + topics.DocPoints);
                    }
                    if (topicEV.Points > topics.TotalPoints)
                        topicEV.Points = topics.TotalPoints;

                    //top = null;
                    db.Entry(topicEV).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Assent");
                }
                }
                ViewBag.Id = new SelectList(db.Documents, "Id", "Name", topicEV.Id);
                ViewBag.EvaluationFormId = new SelectList(db.EvaluationForm, "id", "id", topicEV.EvaluationFormId);
                ViewBag.SectionsId = new SelectList(db.Sections, "Id", "SectionName", topicEV.SectionsId);
                ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName", topicEV.TeacherId);
                ViewBag.TopicsId = new SelectList(db.Topics, "Id", "TopicName", topicEV.TopicsId);
                return View(topicEV);
            }
        public ActionResult DegreeOfAssessment()
        {
            var topicEVs = db.TopicEVs.Include(t => t.Document).Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
            return View(topicEVs.ToList());
        }
        public ActionResult EditProfileTeachers()
        {
            var UserID = User.Identity.GetUserId();
            var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            var CurrentTeacher = db.UserToTeachers.Where(a => a.UserID == UserID).SingleOrDefault();
            Teacher teacher = db.Teachers.Find(CurrentTeacher.TeacherID);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfileTeachers([Bind(Include = "Id,FullName,University,College,Department,Certificate,C_Date,C_Doner,GeneralSpecialization,Specialization,ScientificTitle,ST_Date,ST_Doner,Email,Phone")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult CreateProfileTeachers()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfileTeachers([Bind(Include = "Id,FullName,University,College,Department,Certificate,C_Date,C_Doner,GeneralSpecialization,Specialization,ScientificTitle,ST_Date,ST_Doner,Email,Phone")] Teacher teacher)
        {
            var UserID = User.Identity.GetUserId();
            var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.UserToTeachers.Add(new UserToTeacher { UserID = CurrentUser.Id, TeacherID = teacher.Id });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        public ActionResult TeachersLists()
        {
            return View(db.Teachers.ToList());
        }
        public ActionResult ShowAssent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

    }
}