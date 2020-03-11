using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MvcBreadCrumbs;

namespace TransCanada.Controllers
{
    [Authorize]
    [BreadCrumb]
    public class HomeController : Controller
    {

        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        [BreadCrumb(Clear = true,Label ="Clients")]

        public ActionResult Account_List()
        {
            List<UserAccounts_Model> list_useraccounts = new List<UserAccounts_Model>();
            UserAccounts_Model mod_useraccounts;

            using (SqlConnection con = new SqlConnection(TransCanadaConnection))
            {

                //string query = "select AccountsId, LogoName from AspNetUsers Where UserName = @UserName";
                string query = "get_user_Accounts";
                using (SqlCommand Cmd = new SqlCommand(query, con))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("@userid", User.Identity.GetUserId());
                    using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                    {
                        
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                //ViewBag.AccountId = Convert.ToString(dt.Rows[0]["AccountId"]);

                                mod_useraccounts = new UserAccounts_Model();
                                mod_useraccounts.AccountId_PK = Convert.ToInt32(row["AccountId_PK"]);
                                mod_useraccounts.UserId = Convert.ToString(row["UserId"]);
                                mod_useraccounts.AccountId = Convert.ToString(row["AccountId"]);
                                if (!string.IsNullOrEmpty(row["LogoImage"].ToString()))
                                    mod_useraccounts.LogoImage = Convert.ToString(row["LogoImage"]);
                                else
                                    mod_useraccounts.LogoImage = "No_Logo.png";
                                list_useraccounts.Add(mod_useraccounts);

                            }
                        }
                    }
                }
            }
            ViewBag.AccountList = list_useraccounts;
            List<Menuscreen> _menus = new List<Menuscreen>();
            string roleid = string.Empty;
            string userid = User.Identity.GetUserId();
            var role = UserManager.GetRoles(userid);
            if (role.Count > 0)
            {
                roleid = role[0].ToString();
            }
            SqlConnection connection = new SqlConnection(TransCanadaConnection);
            SqlCommand command = new SqlCommand("get_menus_by_role", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@roledid", roleid);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    _menus.Add(new Menuscreen()
                    {

                        MainMenuId =dataReader["mainmenuid"].ToString(),
                        MainMenuName = dataReader["mainmenu"].ToString(),
                        SubMenuId = dataReader["id"].ToString(),
                        SubMenuName = dataReader["submenu"].ToString(),
                        ControllerName = dataReader["controller"].ToString(),
                        ActionName = dataReader["Action"].ToString()
                    });

                }
                dataReader.Close();
                connection.Close();
            }
            else
            {
                dataReader.Close();
                connection.Close();
            }
            Session["Sessionmaster"] = _menus;
            return View(list_useraccounts);
        }


        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                
                if (User.IsInRole("Admin"))
                {

                }
            }
            else
            {

            }

            return View();
            //return RedirectToAction("Login", "Account");
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index1(int Acc_id)
        {
            List<UserAccounts_Model> list_useraccounts = new List<UserAccounts_Model>();
            UserAccounts_Model mod_useraccounts;
            if (User.Identity.IsAuthenticated)
            {
                //GetLogo(User.Identity.Name.ToString());        
                //ViewBag.AccountId = "101";
                
                using (SqlConnection con = new SqlConnection(TransCanadaConnection))
                {
                    string query = "get_user_Accounts";
                    using (SqlCommand Cmd = new SqlCommand(query, con))
                    {
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@userid", User.Identity.GetUserId());
                        using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                        {
                            //Cmd.Parameters.AddWithValue("@UserName", User.Identity.Name.ToString().Trim());
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            foreach (DataRow row in dt.Rows)
                            {
                                //ViewBag.AccountId = Convert.ToString(dt.Rows[0]["AccountId"]);

                                mod_useraccounts = new UserAccounts_Model();
                                mod_useraccounts.Account_id = Convert.ToInt32(row["Acc_PK"]);
                                mod_useraccounts.AccountId_PK = Convert.ToInt32(row["AccountId_PK"]);
                                mod_useraccounts.UserId = Convert.ToString(row["UserId"]);
                                mod_useraccounts.AccountId = Convert.ToString(row["AccountId"]);
                                mod_useraccounts.LogoImage = Convert.ToString(row["LogoImage"]);
                                list_useraccounts.Add(mod_useraccounts);

                            }


                        }
                    }
                }


                //list_useraccount = list_useraccount.Where(x => x.AccountId_PK == 2);
                var list_useraccount = list_useraccounts.Where(x => x.AccountId_PK == Acc_id); 

                    foreach (UserAccounts_Model UAM in list_useraccount)
                    {
                    System.Web.HttpContext.Current.Session["Account_idPK"] = UAM.Account_id.ToString().Trim();
                    System.Web.HttpContext.Current.Session["Account_id"] = UAM.AccountId.ToString().Trim();
                    if (!string.IsNullOrEmpty(UAM.LogoImage))
                    {
                        System.Web.HttpContext.Current.Session["Logo_path"] = UAM.LogoImage;
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session["Logo_path"] = "No_Logo.png";
                    }
                    }

                
                if (User.IsInRole("Admin"))
                {    

                }
            }
            else
            {

            }

            //return View();
            return RedirectToAction("Index", "Dashboard");
        }

        public void GetLogo(string Para_UserName)
        {
            
            using (SqlConnection con = new SqlConnection(TransCanadaConnection))
            {

                //string query = "select AccountsId, LogoName from AspNetUsers Where UserName = @UserName";
                string query = "select AccountId, LogoImage from AspNetUserAccounts Where UserId = @UserName";
                using (SqlCommand Cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(Cmd))
                    { 
                        Cmd.Parameters.AddWithValue("@UserName", Para_UserName.Trim());
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ViewBag.AccountId = Convert.ToString(dt.Rows[0]["AccountId"]);

                        }
                    }
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}