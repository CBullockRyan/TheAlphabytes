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

        public ActionResult Browse()
        {
            return View(db.FODMAPIngredients.ToList());
        }

        [HttpGet]
        public ActionResult Search()
        {
            string search = Request.QueryString["search"];
            if (!String.IsNullOrEmpty(search))
            {
                var matchAlias = db.FODMAPIngredients.Where(p => p.Aliases.Contains(search));
                List<FODMAPIngredient> allMatches = db.FODMAPIngredients.Where(p => p.Name.Contains(search)).Union(matchAlias).ToList();
                ViewBag.Success = true;
                if (!allMatches.Any())
                {
                    ViewBag.Success = false;
                }
                return View(allMatches);
            }
            return View();
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
