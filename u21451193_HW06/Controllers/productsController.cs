using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u21451193_HW06.Models;
using PagedList;
using Newtonsoft.Json;

namespace u21451193_HW06.Views
{
    public class productsController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        int numberElementsPerPage = 10;


        // Index without searchText and with searchText
        public ActionResult Index(int pageIndex = 1)
        {
            var products = db.products.Include(p => p.brand).Include(p => p.category);
            return View(products.ToList().ToPagedList(pageIndex, numberElementsPerPage));
        }
        [HttpPost]
        public ActionResult Index(string searchText, int pageIndex = 1)
        { // Add searchtext thing
            var products = db.products.Include(p => p.brand).Include(p => p.category);

            return View(products.Where(x => x.product_name.Contains(searchText)).ToList().ToPagedList(pageIndex, numberElementsPerPage));
        }

        // Display Modals
        public ActionResult Add()
        {
            ViewBag.brand = db.brands;
            ViewBag.category = db.categories;
            return PartialView("_Create");
        }

        public ActionResult Details(int? id)
        { 
            var product = db.products.FirstOrDefault(x => x.product_id == id);

            return PartialView("_Details", product);
        } // No touchy


        [HttpPost]
        public ActionResult Edit(int? id)
        {
            var product = db.products.FirstOrDefault(x => x.product_id == id);
            ViewBag.Products = product;
            ViewBag.brand = db.brands;
            ViewBag.category = db.categories;
            return PartialView("_Edit", product);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var product = db.products.FirstOrDefault(x => x.product_id == id);
            ViewBag.Products = product;
            return PartialView("_Delete", product);
        }

        // CRUD Modals

        [HttpPost]
        public ActionResult AddProduct(product sentProduct)
        {
            db.products.Add(sentProduct);
            db.SaveChanges();
            return Json(sentProduct);
        }
    }
}
