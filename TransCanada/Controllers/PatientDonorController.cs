using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    public class PatientDonorController : Controller
    {
        // GET: PatientDonor
        public ActionResult Index()
        {
            PatientDonor_Model patientdonor = new PatientDonor_Model();

            return View(patientdonor);
        }
    }
}