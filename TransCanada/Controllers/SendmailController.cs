using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    public class SendmailController : Controller
    {
        // GET: Sendmail
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Email email)
        {
            return View();
        }
    }
}