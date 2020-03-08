﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FODfinder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FODfinder.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private FFDBContext db = new FFDBContext();
        public ActionResult Index()
        {
            List<ApplicationUser> users = context.Users.ToList();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            users.RemoveAll(x => userManager.IsInRole(x.Id, "Admin") || userManager.IsInRole(x.Id, "SuperAdmin"));

            return View(users);
        }

        public ActionResult Delete(string id)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == id).FirstOrDefault();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            if (userManager.IsInRole(id, "SuperAdmin") || userManager.IsInRole(id, "Admin"))
            {
                return new HttpUnauthorizedResult();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(ApplicationUser userToDelete)
        {
            ApplicationUser user = context.Users.Where(x => x.Id == userToDelete.Id).FirstOrDefault();
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public enum Message
        {
            AddFodmapSuccess,
            FodmapExists,
            AddFodmapFailure
        }

        public ActionResult AddFodmap(Message? message)
        {
            ViewBag.StatusMessage =
                message == Message.AddFodmapSuccess ? "Successfully added new high FODMAP ingredient"
                : message == Message.FodmapExists ? "Ingredient already exists in database"
                : message == Message.AddFodmapFailure ? "Failed to add new entry to database"
                : "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFodmap([Bind(Include = "Name,Aliases")] FODMAPIngredient newIngredient)
        {
            if (ModelState.IsValid)
            {
                if (db.FODMAPIngredients.FirstOrDefault(x => x.Name.Contains(newIngredient.Name)) != null)
                {
                    return RedirectToAction("AddFodmap", new { Message = Message.FodmapExists });
                }
                db.FODMAPIngredients.Add(newIngredient);
                db.SaveChanges();
                return RedirectToAction("AddFodmap", new { Message = Message.AddFodmapSuccess });
            }
            return RedirectToAction("AddFodmap", new { Message = Message.AddFodmapFailure });
        }
    }
}