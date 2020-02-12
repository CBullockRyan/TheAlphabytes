using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FODfinder.Models;

namespace FODfinder.Controllers
{
    public class FODMAPIngredientsController : Controller
    {
        private FFDBContext db = new FFDBContext();

        // GET: FODMAPIngredients
        public ActionResult Browse()
        {
            return View(db.FODMAPIngredients.ToList());
        }

        // GET: FODMAPIngredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FODMAPIngredient fODMAPIngredient = db.FODMAPIngredients.Find(id);
            if (fODMAPIngredient == null)
            {
                return HttpNotFound();
            }
            return View(fODMAPIngredient);
        }

        // GET: FODMAPIngredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FODMAPIngredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Aliases")] FODMAPIngredient fODMAPIngredient)
        {
            if (ModelState.IsValid)
            {
                db.FODMAPIngredients.Add(fODMAPIngredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fODMAPIngredient);
        }

        // GET: FODMAPIngredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FODMAPIngredient fODMAPIngredient = db.FODMAPIngredients.Find(id);
            if (fODMAPIngredient == null)
            {
                return HttpNotFound();
            }
            return View(fODMAPIngredient);
        }

        // POST: FODMAPIngredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Aliases")] FODMAPIngredient fODMAPIngredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fODMAPIngredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fODMAPIngredient);
        }

        // GET: FODMAPIngredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FODMAPIngredient fODMAPIngredient = db.FODMAPIngredients.Find(id);
            if (fODMAPIngredient == null)
            {
                return HttpNotFound();
            }
            return View(fODMAPIngredient);
        }

        // POST: FODMAPIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FODMAPIngredient fODMAPIngredient = db.FODMAPIngredients.Find(id);
            db.FODMAPIngredients.Remove(fODMAPIngredient);
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
