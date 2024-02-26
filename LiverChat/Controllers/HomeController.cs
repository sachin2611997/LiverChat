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
            var list = db.liver.GroupBy(x => x.STATE).Select(x => new LiverModel() { State = x.Key, Statecount = x.Count(), Percentage=Math.Round(x.Count() * 100.0 / db.liver.Select(y => y.STATE).Count(),1)}).ToList();
          //  ViewBag.objectname = db.liver.GroupBy(x => x.TMH_RX).Select(y => new { y.Key, Count = Math.Round(y.Count() * 100.0 ,1) }).ToList();
          var obj = db.liver.GroupBy(x => x.TMH_RX).Select(y => new { y.Key, Count = y.Count() }).ToList();
            ViewBag.objectname= obj.Where(x=>x.Key!=null);
            ViewBag.TotalCount = db.liver.Select(x => x.STATE).Count();
            ViewBag.per = Math.Round(list.Sum(x => x.Percentage));
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

        public ActionResult BarChart()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            dataPoints.Add(new DataPoint("Economics", 1));
            dataPoints.Add(new DataPoint("Physics", 2));
            dataPoints.Add(new DataPoint("Literature", 4));
            dataPoints.Add(new DataPoint("Chemistry", 4));
            dataPoints.Add(new DataPoint("Literature", 9));
            dataPoints.Add(new DataPoint("Physiology or Medicine", 11));
            dataPoints.Add(new DataPoint("Peace", 13));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View(dataPoints);
        }
    }
}