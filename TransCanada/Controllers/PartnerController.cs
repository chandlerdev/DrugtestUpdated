using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    public class PartnerController : Controller
    {
        // GET: Partner
        public ActionResult Index()
        {
            Partner partner = new Partner();

            return View(partner);
        }
    }
}