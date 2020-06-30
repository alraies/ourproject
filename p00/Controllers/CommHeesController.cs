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
    public class CommHeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommHees
        public ActionResult Index()
        {
            var commHees = db.CommHees.Include(c => c.CommitHees);
            return View(commHees.ToList());
        }

        // GET: CommHees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHee commHee = db.CommHees.Find(id);
            if (commHee == null)
            {
                return HttpNotFound();
            }
            return View(commHee);
        }

        // GET: CommHees/Create
        public ActionResult Create()
        {
            ViewBag.CommitHeesid = new SelectList(db.CommitHees, "id", "comitname");
            return View();
        }

        // POST: CommHees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,AcdYea,head,CommitHeesid")] CommHee commHee)
        {
            if (ModelState.IsValid)
            {
                db.CommHees.Add(commHee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommitHeesid = new SelectList(db.CommitHees, "id", "comitname", commHee.CommitHeesid);
            return View(commHee);
        }

        // GET: CommHees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHee commHee = db.CommHees.Find(id);
            if (commHee == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommitHeesid = new SelectList(db.CommitHees, "id", "comitname", commHee.CommitHeesid);
            return View(commHee);
        }

        // POST: CommHees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,AcdYea,head,CommitHeesid")] CommHee commHee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commHee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommitHeesid = new SelectList(db.CommitHees, "id", "comitname", commHee.CommitHeesid);
            return View(commHee);
        }

        // GET: CommHees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHee commHee = db.CommHees.Find(id);
            if (commHee == null)
            {
                return HttpNotFound();
            }
            return View(commHee);
        }

        // POST: CommHees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommHee commHee = db.CommHees.Find(id);
            db.CommHees.Remove(commHee);
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
