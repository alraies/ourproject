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
            if (Session["message"] != null)
            {
                ViewBag.Message = Session["message"].ToString();
                Session["message"] = null;
            }
            return View(db.EvaluationForm.ToList());
        }
        public ActionResult into(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            EvaluationForm evaluationForm = db.EvaluationForm.Find(id);
            //var R = from b in db.EvaluaationFormtoSections
            //        select new
            //        {
            //            b.EvaluationFormID,
            //            b.SectionsID,
            //        };
            var te = db.TopicEVs.Where(a => a.EvaluationFormId == evaluationForm.id);
            if (evaluationForm.iscurent==true&&te.Count()<=0)
            {
                var R = db.EvaluaationFormtoSections.Where(a => a.EvaluationFormID == evaluationForm.id);
                var R2 = from b in db.SectionstoTopics select new
                {
                    b.TopicsID,
                    b.SectionsID,
                };
                var R3 = from b in db.Teachers
                         select new
                         {
                             b.Id,
                             b.Vacation
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
                    lasit3.Add(new SectionstoTopics { SectionsID = item.SectionsID, TopicsID = item.TopicsID });
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
                                    if (item3.Vacation==false) 
                                    {
                                        // lasit.Add(new TopicEV { EvaluationFormId = item.EvaluationFormID, SectionsId = item.SectionsID, TopicsId = item2.TopicsID, TeacherId = item3.Id });
                                        db.TopicEVs.Add(new TopicEV { EvaluationFormId = item.EvaluationFormID, SectionsId = item.SectionsID, TopicsId = item2.TopicsID, TeacherId = item3.Id });
                                    }
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Session["message"]=" هذا الاستماره مفعله سابقا ذا كنت تريد تفعيل نفس الاستماره يجب ان تعمل نسخه لها وتفعيلها";
            }
            return RedirectToAction("Index");
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
        public ActionResult LateForSubmission()
        {
            if (Session["message"] != null)
            {
                ViewBag.Message = Session["message"].ToString();
                Session["message"] = null;
            }
            var evaluation = db.EvaluationForm.Where(a => a.iscurent == true).SingleOrDefault();
            var R = from b in db.Teachers
                    select new
                    {
                        b.Id,
                        b.FullName,
                        b.University,
                        b.College,
                        b.Department,
                        b.ScientificTitle,
                        b.Vacation,
                        Checked = ((from ab in db.TopicEVs
                                    where (ab.EvaluationFormId == evaluation.id) & (ab.TeacherId == b.Id)
                                    select ab).Count() <= 0)
                    };
            var teacher = new List<Teacher>();
            foreach(var item in R)
            {
                if (item.Checked&&item.Vacation==false)
                    teacher.Add(new Teacher {Id=item.Id,FullName=item.FullName,University=item.University,College=item.College,Department=item.Department,ScientificTitle=item.ScientificTitle});
            }
            return View(teacher);
        }

        public ActionResult RequestForEvaluation(int? id)
        {
            if(id==null)
            {
                return HttpNotFound();
            }
            var evalu = db.EvaluationForm.Where(a => a.iscurent == true).SingleOrDefault();
            var evaltosa = db.EvaluaationFormtoSections.Where(a => a.EvaluationFormID == evalu.id);
            var topic = db.SectionstoTopics.ToList();
            foreach (var item in evaltosa )
            {   
                foreach(var item2 in topic)
                {
                    if(item.SectionsID==item2.SectionsID)
                    db.TopicEVs.Add(new TopicEV { EvaluationFormId = evalu.id, SectionsId = item.SectionsID, TopicsId = item2.TopicsID, TeacherId = id.Value });
                }
            }
            db.SaveChanges();
            Session["message"] = "تم اضافة هذا الاستاذ لتقيم";
            return RedirectToAction("LateForSubmission");
        }
        public ActionResult AllRequestForEvaluation(List<Teacher> teachers)
        {
            if (teachers == null)
            {
                return HttpNotFound();
            }
            
            var evalu = db.EvaluationForm.Where(a => a.iscurent == true).SingleOrDefault();
            var evaltosa = db.EvaluaationFormtoSections.Where(a => a.EvaluationFormID == evalu.id);
            var topic = db.SectionstoTopics.ToList();
            var R = from b in db.Teachers
                    select new
                    {
                        b.Id,
                        b.FullName,
                        b.University,
                        b.College,
                        b.Department,
                        b.ScientificTitle,
                        b.Vacation,
                        Checked = ((from ab in db.TopicEVs
                                    where (ab.EvaluationFormId == evalu.id) & (ab.TeacherId == b.Id)
                                    select ab).Count() <= 0)
                    };
            var R2 = R.ToList();
            foreach (var item in evaltosa)
            {
                foreach (var item2 in topic)
                {
                    if (item.SectionsID == item2.SectionsID)
                    {
                        foreach(var item3 in R2)
                        {
                            if(item3.Checked&&item3.Vacation==false)
                            db.TopicEVs.Add(new TopicEV { EvaluationFormId = evalu.id, SectionsId = item.SectionsID, TopicsId = item2.TopicsID, TeacherId =item3.Id });
                        }
                    }    
                       
                }
            }
            db.SaveChanges();
            Session["message"] = "تم اضافة  الاساتذه لتقيم";
            return RedirectToAction("LateForSubmission");
        }
        public ActionResult CopyEvaluation(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var evaluation = db.EvaluationForm.Find(id);
            var evalutiontosections = db.EvaluaationFormtoSections.Where(a => a.EvaluationFormID == evaluation.id);
            var lastev = db.EvaluationForm.ToList();
            int x = lastev[lastev.Count() - 1].id;
            db.EvaluationForm.Add(new EvaluationForm {id=x+1,year=DateTime.Now,iscurent=true});
           
            foreach(var item in evalutiontosections)
            {
                db.EvaluaationFormtoSections.Add(new EvaluaationFormtoSections{EvaluationFormID=x+1,SectionsID=item.SectionsID });
            }
            db.SaveChanges();
            Session["message"] = "تم نسخ الاستماره وضيفة عل تاريخ اليوم";
            return RedirectToAction("Index");
        }
    }
}
