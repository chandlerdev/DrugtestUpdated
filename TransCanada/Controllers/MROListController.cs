using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    public class MROListController : Controller
    {
        // GET: MROList
        public ActionResult Index()
        {
            MROList mrolist = new MROList();

            return View(mrolist);
        }
    }
}