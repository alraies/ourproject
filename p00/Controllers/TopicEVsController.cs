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
    [Authorize]
    public class TopicEVsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TopicEVs
        public ActionResult Index()
        {
            //var topicEVs = db.TopicEVs.Include(t => t.Document).Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
            //return View(topicEVs.ToList());
            return View();
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
            ViewBag.Id = new SelectList(db.Documents, "Id", "Name");
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
        public ActionResult Create([Bind(Include = "Id,EvaluationFormId,SectionsId,TopicsId,TeacherId,Points,Approved,Nameproved")] TopicEV topicEV)
        {
            if (ModelState.IsValid)
            {
                db.TopicEVs.Add(topicEV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Documents, "Id", "Name", topicEV.Id);
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
        public ActionResult Edit([Bind(Include = "Id,EvaluationFormId,SectionsId,TopicsId,TeacherId,Points,Approved,Nameproved")] TopicEV topicEV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topicEV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Documents, "Id", "Name", topicEV.Id);
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
