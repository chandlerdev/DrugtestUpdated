using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    public class iThreeScreensController : Controller
    {
        // GET: iThreeScreens


        public ActionResult iThreeScreen()
        {
            List<iThreeScreen> listthreescreen = new List<iThreeScreen>();
            iThreeScreen threescreen = new iThreeScreen();


            listthreescreen.Add(threescreen);

            return View(listthreescreen);
        }
    }
}