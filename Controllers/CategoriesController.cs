
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF_DBFirst.Models;
namespace EF_DBFirst.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            EFFirstDatabaseEntities1 db = new EFFirstDatabaseEntities1();
            List<Category> categories =db.Categories.ToList();
            return View(categories);

        }
    }
}