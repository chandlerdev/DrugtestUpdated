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
using MvcBreadCrumbs;

namespace TransCanada.Controllers
{
    [Authorize]
    [BreadCrumb]
    public class UserController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

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
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [BreadCrumb(Clear = true, Label = "User list")]
        public ActionResult Userlist()
        {
            SqlConnection conn = new SqlConnection(TransCanadaConnection);
            string query = "select AspNetUsers.Id, AspNetRoles.name,AspNetUsers.Email from AspNetUserRoles inner join AspNetRoles on AspNetUserRoles.Roleid=AspNetRoles.Id  inner join AspNetUsers on AspNetUserRoles.UserId=AspNetUsers.Id";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<User_Model> Userlist = new List<User_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User_Model Ulist = new User_Model();
                Ulist.Id = dt.Rows[i]["Id"].ToString();
                Ulist.Email = dt.Rows[i]["Email"].ToString();
                Ulist.RoleId = dt.Rows[i]["name"].ToString();
                Userlist.Add(Ulist);

            }

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return View(Userlist);
        }
        [BreadCrumb(Label = "Upadate User")]
        public ActionResult UpadateUser(string userid)
        {
            userViewmodel user_Model = new userViewmodel();
            var user = userid;
            var user1 = UserManager.FindById(userid);
            user_Model.username = user1.UserName.ToString();
            List<AspNetUserRoles> select = new List<AspNetUserRoles>();
            SqlConnection conn = new SqlConnection(TransCanadaConnection);
            string query11 = "Select Id, Name from  AspNetRoles";
            SqlCommand cmd11 = new SqlCommand(query11, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd11);
            DataTable dt = new DataTable();
            da.Fill(dt);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AspNetUserRoles Rls = new AspNetUserRoles();
                Rls.Id = dt.Rows[i]["Name"].ToString();
                Rls.Name = dt.Rows[i]["Name"].ToString();

                select.Add(Rls);
            }
            user_Model.Category1 = select;
            var role = UserManager.GetRoles(userid);
            if (role.Count > 0)
            {
                user_Model.roleid = role[0].ToString();
            }
            List<CheckBox> items = new List<CheckBox>();
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select_user_ascc_status";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", userid.Trim());
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new CheckBox
                            {
                                Value = sdr["Accountid"].ToString(),
                                IsChecked = Convert.ToBoolean(sdr["user_account_status"])

                            });
                        }
                    }
                    con.Close();
                }
            }
            user_Model.Accounts_Id = items;

            user_Model.Id = userid;
            return View(user_Model);
        }
        [HttpPost]
        public ActionResult UpadateUser(userViewmodel user)
        {
            var oldUser = UserManager.FindById(user.Id);
            var role = UserManager.GetRoles(user.Id);
            string oldroleid = string.Empty;
            if (role.Count > 0)
            {
                oldroleid = role[0].ToString();
                UserManager.RemoveFromRole(user.Id, oldroleid);
                UserManager.AddToRole(user.Id, user.roleid);
            }
            else
            {

                UserManager.AddToRole(user.Id, user.roleid);
            }
            if (user.Accounts_Id != null)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager
                                   .ConnectionStrings["TransCanadaConnection"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "proc_update_Accounts";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;
                        conn.Open();

                        for (int i = 0; i < user.Accounts_Id.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@userid", user.Id);
                            cmd.Parameters.AddWithValue("@AccountId", user.Accounts_Id[i].Value);
                            cmd.Parameters.AddWithValue("@user_account_status", user.Accounts_Id[i].IsChecked);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }

            return Redirect("Userlist");
        }


    }
 }
   

