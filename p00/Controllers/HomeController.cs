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
using System.Web.Helpers;

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
            public ActionResult TopicView()
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
            public ActionResult Create(int id,List<HttpPostedFileBase> uploads)
            {

            // TODO: Add insert logic here
            if (uploads!= null)
            {
                foreach(var upload in uploads)
                {
                    db.Documents.Add(new Document {TopicEVId = id, Name = upload.FileName });
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                }
                db.SaveChanges();
            }
            else
                return HttpNotFound();

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
                var topic = db.Topics.Find(topicEV.TopicsId);
                if (submitButton == "موافقه")
                { 
                    topicEV.Approved = true;
                    var UserID = User.Identity.GetUserId();
                    var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
                    var CurrentTeacher = db.UserToTeachers.Where(a => a.UserID == UserID).SingleOrDefault();
                    var teacher = db.Teachers.Find(CurrentTeacher.TeacherID);
                    var topics = db.Topics.Find(topicEV.TopicsId);
                    var document = db.Documents.Where(a=>a.TopicEVId==topicEV.Id);
                    topicEV.Nameproved = teacher.FullName;
                    var Currentcommit = db.CommHeeMembers.Where(a => a.Teacherid == CurrentTeacher.TeacherID).SingleOrDefault();
                    var Currentcommit2 = db.CommHees.Where(a => a.id == Currentcommit.CommHeeid).SingleOrDefault();
                    var Currentcommit3 = db.CommitHees.Where(a => a.id == Currentcommit2.CommitHeesid).SingleOrDefault();
                    if (document!= null)
                    {
                        topicEV.Points = (document.Count()*topics.DocPoints);
                    }
                    if (topicEV.Points > topics.TotalPoints)
                        topicEV.Points = topics.TotalPoints;
                    WebMail.SmtpServer="smtp.gmail.com";
                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;
                    WebMail.EnableSsl = true;
                    WebMail.UserName="UOB.cs.com@gmail.com";
                    WebMail.Password = "UOB.cs.com";
                    db.Notifications.Add(new Notification { RecipientID = topicEV.TeacherId, AccountontID = teacher.Id, Messagee = "تم الموافقه على فقرة " + topic.TopicName, AddedOn = DateTime.Now });
                    db.Entry(topicEV).State = EntityState.Modified;
                    db.SaveChanges();
                    var CurrentTeacher2 = db.UserToTeachers.Where(a => a.TeacherID==topicEV.TeacherId).SingleOrDefault();
                    string s = " تمت الموافقه عل فقرت "+topicEV.Topics.TopicName+" \n من قبل "+Currentcommit3.comitname+"";
                    WebMail.Send(to:CurrentTeacher2.User.Email,subject:"تقييم الاساتذه",body:s,isBodyHtml:true);
                    //top = null;
                   
                    return RedirectToAction("Assent");
                }
                else
                {
                    var UserID = User.Identity.GetUserId();
                    var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
                    var CurrentTeacher = db.UserToTeachers.Where(a => a.UserID == UserID).SingleOrDefault();
                    var teacher = db.Teachers.Find(CurrentTeacher.TeacherID);
                    var Currentcommit = db.CommHeeMembers.Where(a => a.Teacherid == CurrentTeacher.TeacherID).SingleOrDefault();
                    var Currentcommit2 = db.CommHees.Where(a => a.id == Currentcommit.CommHeeid).SingleOrDefault();
                    var Currentcommit3 = db.CommitHees.Where(a => a.id == Currentcommit2.CommitHeesid).SingleOrDefault();
                    topicEV.Nameproved = teacher.FullName;
                    topicEV.Approved = true;
                    topicEV.Points = 0;
                    db.Notifications.Add(new Notification { RecipientID = topicEV.TeacherId, AccountontID = teacher.Id, Messagee = "تم رفض فقرة "+ topic.TopicName, AddedOn = DateTime.Now });
                    db.Entry(topicEV).State = EntityState.Modified;
                    db.SaveChanges();
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "UOB.cs.com@gmail.com";
                    WebMail.Password = "UOB.cs.com";
                    var CurrentTeacher2 = db.UserToTeachers.Where(a => a.TeacherID == topicEV.TeacherId).SingleOrDefault();
                    string s = " تم ارفض فقرت " + topicEV.Topics.TopicName + " من قبل " + Currentcommit3.comitname + " وفي حالة الارفض الفقره تعطي درجة صفر ذا هناك اعتراض يرجاء مرجعة الموقع";
                    WebMail.Send(to: CurrentTeacher2.User.Email, subject: "تقييم الاساتذه", body: s, isBodyHtml: true);
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
        [HttpPost]
        public ActionResult TeachersLists(string searchName)
        {
            var teachers = db.Teachers.Where(a => a.FullName.Contains(searchName)).ToList();
            return View(teachers);
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

        //Get Topico of tecahers Edit
        public ActionResult DocumentsofTopic()
        {
            var topicEVs = db.TopicEVs.Include(t => t.Document).Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
            return View(topicEVs.ToList());
        }
        //Documentoftopic
        public ActionResult EditDocumentOfTopic(int id)
        {
            var topicEV = db.TopicEVs.Find(id);
            return View(topicEV);
        }
        //EditDocumentoftopic
        public ActionResult EditDocumentOf(int id)
        {
            var document = db.Documents.Find(id);
            return View(document);
        }

        [HttpPost]
        public ActionResult EditDocumentOf([Bind(Include = "IdDocument,Name,TopicEVId")] Document document,HttpPostedFileBase uploads)
        {

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                 document.Name = uploads.FileName;
                db.Entry(document).State = EntityState.Modified;
                string path = Path.Combine(Server.MapPath("~/Uploads"), uploads.FileName);
                    uploads.SaveAs(path);
               
                db.SaveChanges();
            }
            else
                return HttpNotFound();

            return RedirectToAction("EditDocumentOfTopic");

        }

        public ActionResult RefuseTheDocument()
        {
            var topicEVs = db.TopicEVs.Include(t => t.Document).Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
            return View(topicEVs.ToList());
        }

         public ActionResult RefuseTheDocument2(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            TopicEV topicEV = db.TopicEVs.Find(id);
                topicEV.Approved = false;
                db.Entry(topicEV).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("RefuseTheDocument");
        }
        public ActionResult NotificationView()
        {
            var UserID = User.Identity.GetUserId();
            var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            var CurrentTeacher = db.UserToTeachers.Where(a => a.UserID == UserID).SingleOrDefault();
            var notification = db.Notifications.Where(a => a.RecipientID == CurrentTeacher.TeacherID).ToList();
            return View(notification);
        }
       
        public ActionResult NotificationView2()
        {
            var UserID = User.Identity.GetUserId();
            var CurrentUser = db.Users.Where(a => a.Id == UserID).SingleOrDefault();
            var CurrentTeacher = db.UserToTeachers.Where(a => a.UserID == UserID).SingleOrDefault();
            var notification = db.Notifications.Where(a => a.RecipientID == CurrentTeacher.TeacherID&&a.issee==false);
            foreach(var itme in notification)
            {
                itme.issee = true;
                db.Entry(itme).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("NotificationView");
        }

    }
}