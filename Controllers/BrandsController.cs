using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF_DBFirst.Models;
namespace EF_DBFirst.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Brands
        public ActionResult Index()
        {
            EFFirstDatabaseEntities1 db
                = new EFFirstDatabaseEntities1();
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
    }
}