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

namespace u21451193_HW06.Controllers
{
    public class order_itemsController : Controller
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        int numberElementsPerPage = 10;
        // GET: order_items
        public ActionResult Index(int pageIndex = 1)
        {
            return View(db.order_items.ToList().ToPagedList(pageIndex, numberElementsPerPage));
        }

        [HttpPost]
        public ActionResult Index(DateTime? datePicker, int pageIndex = 1)
        {
            if (datePicker.Equals(""))
            {
                return View(db.order_items.ToList());
            }
            else
            {
                return View(db.order_items.Where(x => x.order.order_date == datePicker).ToList().ToPagedList(pageIndex, numberElementsPerPage));
            }
        }
    }
}
