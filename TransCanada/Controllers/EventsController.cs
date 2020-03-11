using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    public class EventsController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            Events_Model events = new Events_Model();

            return View(events);
        }
    }
}