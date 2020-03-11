using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult List()
        {
            List<AccountsModel> List_AccountsModel = new List<AccountsModel>();

            AccountsModel accountsmodel;

            accountsmodel = new AccountsModel();
            accountsmodel.AccountId = "101";
            accountsmodel.AccountName = "TransCanada";
            List_AccountsModel.Add(accountsmodel);

            accountsmodel = new AccountsModel();
            accountsmodel.AccountId = "102";
            accountsmodel.AccountName = "BP";
            List_AccountsModel.Add(accountsmodel);

            accountsmodel = new AccountsModel();
            accountsmodel.AccountId = "103";
            accountsmodel.AccountName = "Reliant Energy";
            List_AccountsModel.Add(accountsmodel);
            
            return View(List_AccountsModel);

        }

        public ActionResult Client_Redirect(string Id)
        {
            return RedirectToAction("List", "Client", new { @id = Id });
        }

            
    }
}