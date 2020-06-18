using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using p00.Models;

namespace p00.Controllers
{
    public class TopicEVsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TopicEVs
        public ActionResult Index()
        {
            // var topicEVs = db.TopicEVs.Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
            return View();//topicEVs.ToList());
        }
        public ActionResult into(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
            var R = from b in db.Sections
                    select new
                    {
                        b.Id,
                        b.SectionName,
                        b.Description,
                        b.TotalPoints,

                        Checked = ((from ab in db.EvaluaationFormtoSections
                                    where (ab.EvaluationFormID == id) & (ab.SectionsID == b.Id)
                                    select ab).Count() > 0)
                    };
            Sections sections = new Sections();
            Topics topics = new Topics();
            Teacher teacher = new Teacher();
            //var R3 = from b in db.Teachers
            //         select new
            //         {
            //             b.Id
            //         };
            var lasit = new List<TopicEV>();
            foreach (var item in R)
            {
                // sections = db.Sections.Find(item.Id);
                var R2 = from b in db.Topics
                         select new
                         {
                             b.Id,
                             b.TopicName,
                             b.Description,
                             b.TotalPoints,
                             b.ReqDoc,
                             b.DocPoints,
                             Checked = ((from ab in db.SectionstoTopics
                                         where (ab.SectionsID == item.Id) & (ab.TopicsID == b.Id)
                                         select ab).Count() > 0)
                         };
                if (item.Checked)
                    foreach (var item2 in R2)
                    {
                        if (item2.Checked)
                            lasit.Add(new TopicEV { EvaluationFormId = evaluationForm.id, SectionsId = item.Id, TopicsId = item2.Id, TeacherId = 1 });
                        //    topics = db.Topics.Find(item2.Id);
                        //    foreach (var item3 in R3)
                        //    {
                        //        //  teacher = db.Teachers.Find(item.Id);
                        //        //  db.TopicEVs.Add(new TopicEV { EvaluationFormId = evaluationForm.id, SectionsId = sections.Id, TopicsId = topics.Id, TeacherId =teacher.Id });
                        //       lasit.Add(new TopicEV { EvaluationFormId = id.Value, SectionsId = item.Id, TopicsId = item2.Id, TeacherId = item3.Id });

                        //    }
                    }
                // db.SaveChanges();
            }
            return View(lasit);
        }
        // GET: TopicEVs/Details/5
        public ActionResult Details(int? id)
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
            return View(topicEV);
        }

        // GET: TopicEVs/Create
        public ActionResult Create()
        {
            ViewBag.EvaluationFormId = new SelectList(db.EvaluationForm, "id", "id");
            ViewBag.SectionsId = new SelectList(db.Sections, "Id", "SectionName");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName");
            ViewBag.TopicsId = new SelectList(db.Topics, "Id", "TopicName");
            return View();
        }

        // POST: TopicEVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EvaluationFormId,SectionsId,TopicsId,TeacherId,Points,Approved")] TopicEV topicEV)
        {
            if (ModelState.IsValid)
            {
                db.TopicEVs.Add(topicEV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvaluationFormId = new SelectList(db.EvaluationForm, "id", "id", topicEV.EvaluationFormId);
            ViewBag.SectionsId = new SelectList(db.Sections, "Id", "SectionName", topicEV.SectionsId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName", topicEV.TeacherId);
            ViewBag.TopicsId = new SelectList(db.Topics, "Id", "TopicName", topicEV.TopicsId);
            return View(topicEV);
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
        public ActionResult Edit([Bind(Include = "Id,EvaluationFormId,SectionsId,TopicsId,TeacherId,Points,Approved")] TopicEV topicEV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topicEV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvaluationFormId = new SelectList(db.EvaluationForm, "id", "id", topicEV.EvaluationFormId);
            ViewBag.SectionsId = new SelectList(db.Sections, "Id", "SectionName", topicEV.SectionsId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "FullName", topicEV.TeacherId);
            ViewBag.TopicsId = new SelectList(db.Topics, "Id", "TopicName", topicEV.TopicsId);
            return View(topicEV);
        }

        // GET: TopicEVs/Delete/5
        public ActionResult Delete(int? id)
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
            return View(topicEV);
        }

        // POST: TopicEVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TopicEV topicEV = db.TopicEVs.Find(id);
            db.TopicEVs.Remove(topicEV);
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
