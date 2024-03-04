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
           
         //  ViewBag.data = db.liver.GroupBy(x => x.N_CLINEXT).Select(y=> new { y.Key, Count = y.Count() }).ToList();

          // ViewBag.objectname = db.liver.GroupBy(x => x.N_CLINEXT).Select(y =>y.Count()).ToList();

            ViewBag.data = db.liver.GroupBy(x => x.N_CLINEXT).Select(y => new  { y.Key, Count = Math.Round(y.Count() * 100.0 / db.liver.Select(x => x.N_CLINEXT).Count(),1)}).ToList();
            var list = db.liver.GroupBy(x => x.STATE).Select(x => new LiverModel() { State = x.Key, Statecount = x.Count(), Percentage=Math.Round(x.Count() * 100.0 / db.liver.Select(y => y.STATE).Count(),1)}).ToList().OrderByDescending(r=>r.Percentage).Take(5);
          //  ViewBag.objectname = db.liver.GroupBy(x => x.TMH_RX).Select(y => new { y.Key, Count = Math.Round(y.Count() * 100.0 ,1) }).ToList();
          var obj = db.liver.GroupBy(x => x.TMH_RX).Select(y => new { y.Key, Count = y.Count() }).ToList();
            ViewBag.objectname= obj.Where(x=>x.Key!=null);
            ViewBag.TotalCount = db.liver.Select(x => x.STATE).Count();

            //    ViewBag.other = db.liver.GroupBy(x => x.STATE).OrderByDescending(x=> new LiverModel() { Statecount = x.Count() }).Skip(count:5).Count();
        
            var list1 = db.liver.GroupBy(x => x.STATE).Select(x => new LiverModel() { State = x.Key, Statecount = x.Count(), Percentage = Math.Round(x.Count() * 100.0 / db.liver.Select(y => y.STATE).Count(), 1) }).ToList().OrderByDescending(r => r.Percentage).Skip(5);

            ViewBag.other =list1.Sum(y => y.Statecount);
            ViewBag.Otherper= list1.Sum(x => x.Statecount)*100 / db.liver.Select(x => x.STATE).Count();
             ViewBag.totalper = db.liver.Select(x => x.STATE).Count() * 100 / db.liver.Select(x => x.STATE).Count();
            
            return View(list);
        }
    
        public ActionResult list()
        {
            var list = db.liver.GroupBy(x => x.STATE).Select(x => new LiverModel() { State = x.Key, Statecount = x.Count() });
            return View(list);
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