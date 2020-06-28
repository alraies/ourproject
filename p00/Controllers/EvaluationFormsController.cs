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
    public class EvaluationFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EvaluationForms
        public ActionResult Index()
        {
            return View(db.EvaluationForm.ToList());
        }
        public ActionResult into(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
            var R = from b in db.EvaluaationFormtoSections
                    select new
                    {
                        b.EvaluationFormID,
                        b.SectionsID,
                    };
            var R2 = from b in db.SectionstoTopics select new
                    {
                        b.TopicsID,
                        b.SectionsID,
                    };
            var R3 = from b in db.Teachers
                     select new
                     {
                         b.Id
                     };
            var lasit = new List<TopicEV>();
            var lasit2 = new List<EvaluaationFormtoSections>();
            var lasit3 = new List<SectionstoTopics>();
            foreach (var item in R)
            {
                lasit2.Add(new EvaluaationFormtoSections { EvaluationFormID = item.EvaluationFormID, SectionsID = item.SectionsID });
            }
            foreach (var item in R2)
            {
                lasit3.Add(new SectionstoTopics {SectionsID = item.SectionsID,TopicsID=item.TopicsID });
            }
            foreach (var item in lasit2)
            {
                if (item.EvaluationFormID == id)
                {
                    foreach (var item2 in lasit3)
                    {
                        if (item2.SectionsID == item.SectionsID)
                        {
                            foreach (var item3 in R3) 
                            { 
                                lasit.Add(new TopicEV {EvaluationFormId = item.EvaluationFormID, SectionsId = item.SectionsID, TopicsId = item2.TopicsID, TeacherId = item3.Id });
                             db.TopicEVs.Add(new TopicEV { EvaluationFormId = item.EvaluationFormID, SectionsId = item.SectionsID, TopicsId = item2.TopicsID, TeacherId = item3.Id });
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
            return View(lasit);
        }
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult into(int? id,EvaluationForm evaluationForm)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

          //  EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
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
            Sections sections =new Sections();
            Topics topics = new Topics();
            Teacher teacher = new Teacher();
            var R3 = from b in db.Teachers
                     select new
                     {
                         b.Id
                     };
        foreach (var item in R)
            {
                sections = db.Sections.Find(item.Id);
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
                                         where (ab.SectionsID ==item.Id) & (ab.TopicsID == b.Id)
                                         select ab).Count() > 0)
                         };
                
                foreach (var item2 in R2)
                {
                    topics = db.Topics.Find(item2.Id);
                    foreach (var item3 in R3)
                    {
                      //  teacher = db.Teachers.Find(item.Id);
                      //  db.TopicEVs.Add(new TopicEV { EvaluationFormId = evaluationForm.id, SectionsId = sections.Id, TopicsId = topics.Id, TeacherId =teacher.Id });
                        db.TopicEVs.Add(new TopicEV { EvaluationFormId =id.Value, SectionsId = item.Id,TopicsId=item2.Id,TeacherId=item3.Id});
                   
                    }
                }
               // db.SaveChanges();
            }
            db.SaveChanges();
                return View();
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
                        if (item.Id == item2.SectionsID&&evaluationForm.id==item2.EvaluationFormID)
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
