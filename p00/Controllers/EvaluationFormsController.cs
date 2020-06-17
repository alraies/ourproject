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
    public class EvaluationFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EvaluationForms
        public ActionResult Index()
        {
            return View(db.EvaluationForm.ToList());
        }

        // GET: EvaluationForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
            if (evaluationForm == null)
            {
                return HttpNotFound();
            }
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
            var Myviewmodel = new EvaluationFormViewModel();
            Myviewmodel.id = id.Value;
            Myviewmodel.year = evaluationForm.year;
            Myviewmodel.iscurent = evaluationForm.iscurent;



            var Myviewmodel2 = new List<CheckSectionsViewModel>();
            foreach (var item in R)
            {
                Myviewmodel2.Add(new CheckSectionsViewModel { Id = item.Id, SectionName = item.SectionName, Description = item.Description, TotalPoints = item.TotalPoints, Checked = item.Checked });
            }
            Myviewmodel.Sections = Myviewmodel2;

            return View(Myviewmodel);
        }

        // GET: EvaluationForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvaluationForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,year,iscurent")] EvaluationForm evaluationForm)
        {
            if (ModelState.IsValid)
            {
                db.EvaluationForm.Add(evaluationForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evaluationForm);
        }

        // GET: EvaluationForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
            if (evaluationForm == null)
            {
                return HttpNotFound();
            }
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
            var Myviewmodel = new EvaluationFormViewModel();
            Myviewmodel.id = id.Value;
            Myviewmodel.year =evaluationForm.year;
            Myviewmodel.iscurent = evaluationForm.iscurent;



            var Myviewmodel2 = new List<CheckSectionsViewModel>();
            foreach (var item in R)
            {
                Myviewmodel2.Add(new CheckSectionsViewModel { Id = item.Id, SectionName = item.SectionName, Description = item.Description, TotalPoints = item.TotalPoints, Checked = item.Checked });
            }
            Myviewmodel.Sections = Myviewmodel2;

            return View(Myviewmodel);
        }

        // POST: EvaluationForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvaluationFormViewModel evaluationForm)
        {
            //if (ModelState.IsValid)
            //{
                var Mysection = db.EvaluationForm.Find(evaluationForm.id);
                Mysection.year = evaluationForm.year;
                Mysection.iscurent = evaluationForm.iscurent;


            foreach (var item in db.EvaluaationFormtoSections)
            {
                if (item.EvaluationFormID == evaluationForm.id)
                {
                    foreach (var item2 in evaluationForm.Sections)
                    {

                        if (item.SectionsID == item2.Id)
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
            foreach (var item in evaluationForm.Sections)
            {
                if (item.Checked)
                {
                    foreach (var item2 in db.EvaluaationFormtoSections)
                    {
                        if (item.Id == item2.SectionsID)
                        {
                            A = false;
                        }

                    }
                    if (A)
                        db.EvaluaationFormtoSections.Add(new EvaluaationFormtoSections { EvaluationFormID = evaluationForm.id, SectionsID = item.Id });
                    A = true;
                }
            }


            db.SaveChanges();
                return RedirectToAction("Index");
          //  }
            return View(evaluationForm);
        }

        // GET: EvaluationForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
            if (evaluationForm == null)
            {
                return HttpNotFound();
            }
            return View(evaluationForm);
        }

        // POST: EvaluationForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
            db.EvaluationForm.Remove(evaluationForm);
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
