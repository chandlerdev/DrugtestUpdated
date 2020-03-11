using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    [Authorize]
    public class ClientWiseReportController : Controller
    {
        // GET: ClientWiseReport
        public ActionResult Index()
        {

            List<ClientWiseReport_Model> listclientwisereport = new List<ClientWiseReport_Model>();
            ClientWiseReport_Model clientwisereport = new ClientWiseReport_Model();


            listclientwisereport.Add(clientwisereport);

            return View(listclientwisereport);
        }
    }
}