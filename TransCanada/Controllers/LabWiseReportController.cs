using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    public class LabWiseReportController : Controller
    {
        // GET: LabWiseReport
        public ActionResult Index()
        {

            List<LabWiseReport_Model> listlabwisereport = new List<LabWiseReport_Model>();

            if (User.Identity.IsAuthenticated)
             {

                if (User.IsInRole("Admin"))
                {
                    LabWiseReport_Model labwisereport = new LabWiseReport_Model();


                    listlabwisereport.Add(labwisereport);

                }

            }
            return View(listlabwisereport);
        }
    }
}