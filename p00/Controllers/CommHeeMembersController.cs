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
    public class CommHeeMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CommHeeMembers
        public ActionResult Index()
        {
            var commHeeMembers = db.CommHeeMembers.Include(c => c.CommHee).Include(c => c.Teacher);
            return View(commHeeMembers.ToList());
        }

        // GET: CommHeeMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHeeMembers commHeeMembers = db.CommHeeMembers.Find(id);
            if (commHeeMembers == null)
            {
                return HttpNotFound();
            }
            return View(commHeeMembers);
        }

        // GET: CommHeeMembers/Create
        public ActionResult Create()
        {
            ViewBag.CommHeeid = new SelectList(db.CommHees, "id", "AcdYea");
          //  ViewBag.CommitHeesid = new SelectList(db.CommitHees, "id", "comitname");
            ViewBag.Teacherid = new SelectList(db.Teachers, "Id", "FullName");
            return View();
        }

        // POST: CommHeeMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Role,CommHeeid,Teacherid")] CommHeeMembers commHeeMembers)
        {
            if (ModelState.IsValid)
            {
                db.CommHeeMembers.Add(commHeeMembers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CommHeeid = new SelectList(db.CommHees, "id", "AcdYea", commHeeMembers.CommHeeid);
            ViewBag.Teacherid = new SelectList(db.Teachers, "Id", "FullName", commHeeMembers.Teacherid);
            return View(commHeeMembers);
        }

        // GET: CommHeeMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHeeMembers commHeeMembers = db.CommHeeMembers.Find(id);
            if (commHeeMembers == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommHeeid = new SelectList(db.CommHees, "id", "AcdYea", commHeeMembers.CommHeeid);
            ViewBag.Teacherid = new SelectList(db.Teachers, "Id", "FullName", commHeeMembers.Teacherid);
            return View(commHeeMembers);
        }

        // POST: CommHeeMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Role,CommHeeid,Teacherid")] CommHeeMembers commHeeMembers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commHeeMembers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CommHeeid = new SelectList(db.CommHees, "id", "AcdYea", commHeeMembers.CommHeeid);
            ViewBag.Teacherid = new SelectList(db.Teachers, "Id", "FullName", commHeeMembers.Teacherid);
            return View(commHeeMembers);
        }

        // GET: CommHeeMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommHeeMembers commHeeMembers = db.CommHeeMembers.Find(id);
            if (commHeeMembers == null)
            {
                return HttpNotFound();
            }
            return View(commHeeMembers);
        }

        // POST: CommHeeMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommHeeMembers commHeeMembers = db.CommHeeMembers.Find(id);
            db.CommHeeMembers.Remove(commHeeMembers);
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
