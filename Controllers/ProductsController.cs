using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF_DBFirst.Models;
namespace EF_DBFirst.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        EFFirstDatabaseEntities1 db = new EFFirstDatabaseEntities1();
        public ActionResult Index(string search = "", string SortColumn =
            "ProductId",string IconClass="Fa-sort-asc",string searchCategory="", int page=1)
        {

                EFFirstDatabaseEntities1 db = new EFFirstDatabaseEntities1();
         
            List<Product> products = db.Products.Where(row => row.name.Contains(search)).ToList();
           
            if (searchCategory.Length > 0)
            {
              
                products = products.Where(row => row.Category.name.Contains(searchCategory)).ToList();
                ViewBag.searchCate = searchCategory;
                if (SortColumn.ToUpper().Trim() == "ProductId".ToUpper().Trim())
                {
                    if (IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(row => row.id).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(row => row.id).ToList();
                    }

                }
                else if (SortColumn == "ProductName")
                {
                    if (IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(row => row.name).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(row => row.name).ToList();
                    }
                }
                else if (SortColumn == "Price")
                {
                    if (IconClass == "fa-sort-asc")
                    {
                        products = products.OrderBy(row => row.price).ToList();
                    }
                    else
                    {
                        products = products.OrderByDescending(row => row.price).ToList();
                    }
                }
            }
       
            // List<Product> category = db.Products.Where(row => row.Category.name.Contains (searchCategory)).ToList();

            ViewBag.search = search;
            //sort
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            
            int NoOfRecordPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages= NoOfPages;
            products= products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(products);
        }
        public ActionResult Detail(int id)
        {
            EFFirstDatabaseEntities1 db = new EFFirstDatabaseEntities1();
            Product pro = db.Products.Where(row => row.id == id).FirstOrDefault();

            return View(pro);
        }
        public ActionResult Create()
        {
            EFFirstDatabaseEntities1 db
                 = new EFFirstDatabaseEntities1();
            List<Category> categories = db.Categories.ToList();
            ViewBag.CategoryList = categories;
            List<Brand> brands = db.Brands.ToList();
            ViewBag.BrandLists = brands;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            EFFirstDatabaseEntities1 db = new EFFirstDatabaseEntities1();
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id) {
            Product product = db.Products.Where(row=>row.id == id).FirstOrDefault();
            List<Category> categories = db.Categories.ToList();
            ViewBag.CategoryList1 = categories;
            List<Brand> brands = db.Brands.ToList();
            ViewBag.BrandLists1 = brands;
            var selectedCategory = db.Categories.FirstOrDefault(c => c.id == id);
            ViewBag.SelectedCategoryName = selectedCategory?.name;
            ViewBag.SelectedCategoryId = product.category_id;
            ViewBag.SelectedBrandId= product.brand_id;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product pro)
        {
            Product product = db.Products.Where(row => row.id == pro.id).FirstOrDefault();
           product.name = pro.name;
            product.price = pro.price;
            product.brand_id = pro.brand_id;
            product.category_id=pro.category_id;
            db.SaveChanges();
            return Redirect("Index");
        }
        public ActionResult Delete(int id) { 
            Product product = db.Products.Where(row=> row.id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            Product product = db.Products.Where(row => row.id == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index","Products");
        }
    }
}