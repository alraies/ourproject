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
    public class CommitHeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommitHees
        public ActionResult Index()
        {
            return View(db.CommitHees.ToList());
        }

        // GET: CommitHees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitHees commitHees = db.CommitHees.Find(id);
            if (commitHees == null)
            {
                return HttpNotFound();
            }
            return View(commitHees);
        }

        // GET: CommitHees/Create
        public ActionResult Create()
        {
            var list1 = new List<string>() { "مؤقته", "دائما" };
            ViewBag.list1 = list1;
            var list2 = new List<string>() { "كيان", "لجنه" };
            ViewBag.list2 = list2;
            return View();
        }

        // POST: CommitHees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,comitname,comitDecipt,comitpriod,comittype,isActive")] CommitHees commitHees)
        {
            if (ModelState.IsValid)
            {
                db.CommitHees.Add(commitHees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commitHees);
        }

        // GET: CommitHees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitHees commitHees = db.CommitHees.Find(id);
            if (commitHees == null)
            {
                return HttpNotFound();
            }
            return View(commitHees);
        }

        // POST: CommitHees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,comitname,comitDecipt,comitpriod,comittype,isActive")] CommitHees commitHees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commitHees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commitHees);
        }

        // GET: CommitHees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitHees commitHees = db.CommitHees.Find(id);
            if (commitHees == null)
            {
                return HttpNotFound();
            }
            return View(commitHees);
        }

        // POST: CommitHees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommitHees commitHees = db.CommitHees.Find(id);
            db.CommitHees.Remove(commitHees);
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
