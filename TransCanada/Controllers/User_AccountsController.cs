using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TransCanada.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace TransCanada.Controllers
{
    public class User_AccountsController : Controller
    {
        // GET: User_Accounts

        string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectAccounts()
        {
            UserAccounts_Model Accounts = new UserAccounts_Model();
            List<UserAccounts_Model> select = new List<UserAccounts_Model>();

            using (SqlConnection con = new SqlConnection(TransConnString))
            {

                string query = "Select AccountId, LogoImage from AspNetAccounts";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Accounts.Id = new List<UserAccounts_Model>();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserAccounts_Model Acc = new UserAccounts_Model();
                    Acc.AccountId = dt.Rows[i]["AccountId"].ToString();
                    Acc.LogoImage = dt.Rows[i]["LogoImage"].ToString();

                    select.Add(Acc);
                }
                Accounts.Id = select;


                //SqlConnection con1 = new SqlConnection(TransConnString);

                List<User_Mdl> Select1 = new List<User_Mdl>();
                string query1 = "Select Id, UserName from  AspNetUsers";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);

                Accounts.Users = new List<User_Mdl>();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    User_Mdl user = new User_Mdl();
                    user.Id = dt1.Rows[i]["Id"].ToString();
                    user.UserName = dt1.Rows[i]["UserName"].ToString();

                    Select1.Add(user);
                }
                Accounts.Users = Select1;
            }
            return View(Accounts);

        }

        [HttpPost]
        public ActionResult SelectAccounts(UserAccounts_Model UserAccounts)
        {
            SqlConnection con1 = new SqlConnection(TransConnString);
            string query = "Insert into AspNetUserRoles ";

            return View();
        }

    }
}