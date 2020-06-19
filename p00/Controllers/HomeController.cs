using p00.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var topicEVs = db.TopicEVs.Include(t => t.Document).Include(t => t.EvaluationForm).Include(t => t.Sections).Include(t => t.Teacher).Include(t => t.Topics);
            return View(topicEVs.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: Document/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Document/Create
        [HttpPost]
        public ActionResult Create( int id,HttpPostedFileBase upload)
        {

            // TODO: Add insert logic here
            //if (id != null)
            //{

            
            db.Documents.Add(new Document {Id=id,Name = upload.FileName });
            db.SaveChanges();
            string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
            upload.SaveAs(path);
            //}
            //else
            //    return HttpNotFound();

            return RedirectToAction("Index");
           
        }
    }
}