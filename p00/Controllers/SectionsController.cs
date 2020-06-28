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
    public class SectionsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sections
        public ActionResult Index()
        {
            return View(db.Sections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sections sections = db.Sections.Find(id);
            if (sections == null)
            {
                return HttpNotFound();
            }
            var R = from b in db.Topics
                    select new
                    {
                        b.Id,
                        b.TopicName,
                        b.Description,
                        b.TotalPoints,
                        b.ReqDoc,
                        b.DocPoints,
                        Checked = ((from ab in db.SectionstoTopics
                                    where (ab.SectionsID == id) & (ab.TopicsID == b.Id)
                                    select ab).Count() > 0)
                    };
            var Myviewmodel = new SectionsViewModel();
            Myviewmodel.Id = id.Value;
            Myviewmodel.SectionName = sections.SectionName;
            Myviewmodel.Description = sections.Description;
            Myviewmodel.TotalPoints = sections.TotalPoints;


            var Myviewmodel2 = new List<TopicsViewModel>();
            foreach (var item in R)
            {
                Myviewmodel2.Add(new TopicsViewModel { Id = item.Id, TopicName = item.TopicName, Description = item.Description, TotalPoints = item.TotalPoints, ReqDoc = item.ReqDoc, DocPoints = item.DocPoints, Checked = item.Checked });
            }
            Myviewmodel.Topics = Myviewmodel2;

            return View(Myviewmodel);
        }

        // GET: Sections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SectionName,Description,TotalPoints")] Sections sections)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(sections);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sections);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sections sections = db.Sections.Find(id);
            if (sections == null)
            {
                return HttpNotFound();
            }
            var R=from b in db.Topics
                select new
                {
                    b.Id,
                    b.TopicName,
                    b.Description,
                    b.TotalPoints,
                    b.ReqDoc,
                    b.DocPoints,
                    Checked=((from ab in db.SectionstoTopics
                              where(ab.SectionsID==id)&(ab.TopicsID== b.Id)
                              select ab).Count()>0)
                };
            var Myviewmodel = new SectionsViewModel();
            Myviewmodel.Id = id.Value;
            Myviewmodel.SectionName = sections.SectionName;
            Myviewmodel.Description = sections.Description;
            Myviewmodel.TotalPoints = sections.TotalPoints;


            var Myviewmodel2 = new List<TopicsViewModel>();
            foreach(var item in R)
            {
                Myviewmodel2.Add(new TopicsViewModel { Id = item.Id, TopicName = item.TopicName,Description=item.Description,TotalPoints=item.TotalPoints,ReqDoc=item.ReqDoc,DocPoints=item.DocPoints ,Checked = item.Checked });
            }
            Myviewmodel.Topics = Myviewmodel2;

            return View(Myviewmodel);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionsViewModel sections)
        {

            if (ModelState.IsValid)
            {
                var Mysection = db.Sections.Find(sections.Id);
                Mysection.SectionName = sections.SectionName;
                Mysection.Description = sections.Description;
                Mysection.TotalPoints = sections.TotalPoints;

                foreach (var item in db.SectionstoTopics)
                {
                    if (item.SectionsID == sections.Id)
                    {
                        foreach (var item2 in sections.Topics)
                        {

                            if (item.TopicsID == item2.Id)
                            {
                                if (item2.Checked)
                                {

                                }
                                else
                                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            }

                        }
                    }
                }
                bool A = true;
                foreach (var item in sections.Topics)
                {
                    if (item.Checked)
                    {
                        foreach (var item2 in db.SectionstoTopics)
                        {
                            if (item.Id == item2.TopicsID&&sections.Id==item2.SectionsID)
                            {
                                A = false;
                            }

                        }
                        if (A)
                            db.SectionstoTopics.Add(new SectionstoTopics { SectionsID = sections.Id, TopicsID = item.Id });
                        A = true;
                    }
                  
                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sections);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sections sections = db.Sections.Find(id);
            if (sections == null)
            {
                return HttpNotFound();
            }
            return View(sections);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sections sections = db.Sections.Find(id);
            db.Sections.Remove(sections);
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
