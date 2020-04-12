using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using FODfinder.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace FODfinder.Controllers
{
    [Authorize]
    public class SavedFoodsController : Controller
    {
        private FFDBContext db = new FFDBContext();

        public int countSavedFoods(List<SavedFood> savedFoodsList)
        {
            int count = savedFoodsList.Count();
            return count;
        }

        // GET: SavedFoods
        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            List<SavedFood> savedFoodsList = db.SavedFoods.Where(sf => sf.userID == uid).ToList();
            ViewBag.TotalSavedFoods = countSavedFoods(savedFoodsList);
            return View(savedFoodsList);
        }

        // GET: SavedFoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedFood savedFood = db.SavedFoods.Find(id);
            if (savedFood == null)
            {
                return HttpNotFound();
            }
            return View(savedFood);
        }

        // POST: SavedFoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ContentResult Create(int usdaFoodID, string brandOwner, string upc, string description)
        {
            string userID = User.Identity.GetUserId();
            if (userID != null)
            {
                SavedFood savedFood = new SavedFood(usdaFoodID, userID, brandOwner, upc, description);
                try
                {
                    db.SavedFoods.Add(savedFood);
                    db.SaveChanges();
                    var jsonData = new
                    {
                        success = true,
                        message = "Food has been saved.",
                        redirect = false
                    };

                    var result = JObject.FromObject(jsonData);
                    return Content(result.ToString(), "Application/json");
                }
                catch
                {
                    var jsonData2 = new
                    {
                        success = false,
                        message = "Food has already been saved.",
                        redirect = false
                    };

                    var result2 = JObject.FromObject(jsonData2);
                    return Content(result2.ToString(), "Application/json");
                }
            }

            var jsonData3 = new
            {
                success = false,
                message = "User not logged in.",
                redirect = true
            };

            var result3 = JObject.FromObject(jsonData3);
            return Content(result3.ToString(), "Application/json");
        }

        // GET: SavedFoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedFood savedFood = db.SavedFoods.Find(id);
            if (savedFood == null)
            {
                return HttpNotFound();
            }
            return View(savedFood);
        }

        // POST: SavedFoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usdaFoodID,userID")] SavedFood savedFood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savedFood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(savedFood);
        }

        // GET: SavedFoods/Delete/5
        public ActionResult Delete(int? usdaFoodID, string userID)
        {
            if (usdaFoodID == null || userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavedFood savedFood = db.SavedFoods.Find(usdaFoodID, userID);
            if (savedFood == null)
            {
                return HttpNotFound();
            }
            return View(savedFood);
        }

        // POST: SavedFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int usdaFoodID, string userID)
        {
            SavedFood savedFood = db.SavedFoods.Find(usdaFoodID, userID);
            db.SavedFoods.Remove(savedFood);
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
