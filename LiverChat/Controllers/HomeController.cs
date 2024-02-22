using LiverChat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LiverChat.Controllers
{
    public class HomeController : Controller
    {
        DbEntity db = new DbEntity();
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            //var list= db.liver.GroupBy(x => x.N_CLINEXT).ToList();
            // ViewBag.lists = JsonConvert.SerializeObject(list);
         //  ViewBag.data = db.liver.GroupBy(x => x.N_CLINEXT).Select(y=> new { y.Key, Count = y.Count() }).ToList();

          // ViewBag.objectname = db.liver.GroupBy(x => x.N_CLINEXT).Select(y =>y.Count()).ToList();

            ViewBag.data = db.liver.GroupBy(x => x.N_CLINEXT).Select(y => new  { y.Key, Count = Math.Round(y.Count() * 100.0 / db.liver.Select(x => x.N_CLINEXT).Count(),1)}).ToList();
            var list = db.liver.GroupBy(x => x.STATE).Select(x => new LiverModel() { State = x.Key, Statecount = x.Count() });
            //    ViewBag.objectname = db.liver.GroupBy(x => x.TMH_RX).Select(y => new { y.Key, Count = Math.Round(y.Count() * 100.0 / db.liver.Select(x => x.TMH_RX).Count(), 1) }).ToList();
               ViewBag.objectname = db.liver.GroupBy(x => x.TMH_RX).Select(y => new { y.Key, Count = y.Count() }).ToList();

            return View(list);
        }

        public ActionResult list()
        {
            var list = db.liver.GroupBy(x => x.STATE).Select(x => new LiverModel() { State = x.Key, Statecount = x.Count() });
            return View(list);
        }
        


        [HttpPost]
        [ActionName("Index")]
        public JsonResult Index1()
        {
          //  var list = db.liver.GroupBy(x => x.N_CLINEXT).Select(y=>new {y.Key, Count=y.Count()}).ToList();
            // ViewBag.lists = JsonConvert.SerializeObject(list);
           
           
            DbEntity db = new DbEntity();
            return Json(db.liver.GroupBy(x => x.N_CLINEXT).Select(y => new { y.Key, Count = y.Count() }).ToList(), JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public ActionResult Index2()
        {
            ViewBag.data = db.liver.GroupBy(x => x.N_CLINEXT).Select(y => y.Count()).ToList();
            ViewBag.objectname = db.liver.GroupBy(x => x.N_CLINEXT).Select(y =>  y.Key ).ToList();
            return View();
        }
    }
}