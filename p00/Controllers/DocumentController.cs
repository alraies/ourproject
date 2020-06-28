using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using p00.Models;
using WebApplication2.Models;

namespace p00.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        // GET: Document
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Document/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Document/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Document/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase upload)
        {
            //try
            //{
            // TODO: Add insert logic here
            //  document.Name = upload.FileName;
                //  document.Id = 1;
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                db.Documents.Add(new Document {Name=upload.FileName });
                db.SaveChanges();
                //  return RedirectToAction("/Home/Index");
                return View();
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Document/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
