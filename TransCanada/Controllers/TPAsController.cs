using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    public class TPAsController : Controller
    {
        // GET: TPAs
        public ActionResult Index()
        {
            TPAs tpas = new TPAs();

            return View(tpas);
        }
    }
}