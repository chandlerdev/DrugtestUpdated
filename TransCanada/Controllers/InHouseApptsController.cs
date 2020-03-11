using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    public class InHouseApptsController : Controller
    {
        // GET: InHouseAppts
        public ActionResult Index()
        {
            InHouseAppts_Model inhouseappts = new InHouseAppts_Model();
                        
            return View(inhouseappts);
        }
    }
}