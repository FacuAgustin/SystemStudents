using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SystemStudents.Models;

namespace SystemStudents.Controllers
{
    public class RegistationsController : Controller
    {
        private StudentSystemEntities db = new StudentSystemEntities();

        // GET: Registations
        public ActionResult Index()
        {
            var registations = db.Registations.Include(r => r.Batch).Include(r => r.Course);
            return View(registations.ToList());
        }

        // GET: Registations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registation registation = db.Registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // GET: Registations/Create
        public ActionResult Create()
        {
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Batch1");
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");
            return View();
        }

        // POST: Registations/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,CourseId,BatchId,Tel")] Registation registation)
        {
            if (ModelState.IsValid)
            {
                db.Registations.Add(registation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Batch1", registation.BatchId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", registation.CourseId);
            return View(registation);
        }

        // GET: Registations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registation registation = db.Registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Batch1", registation.BatchId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", registation.CourseId);
            return View(registation);
        }

        // POST: Registations/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,CourseId,BatchId,Tel")] Registation registation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Batch1", registation.BatchId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", registation.CourseId);
            return View(registation);
        }

        // GET: Registations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registation registation = db.Registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // POST: Registations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registation registation = db.Registations.Find(id);
            db.Registations.Remove(registation);
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
