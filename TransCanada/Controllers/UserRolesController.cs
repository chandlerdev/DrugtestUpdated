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
    public class UserRolesController : Controller
    {
        string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
        // GET: UserRoles
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectUserRoles()
        {
            AspNetUserRoles user_roles = new AspNetUserRoles();

            using (SqlConnection con = new SqlConnection(TransConnString))
            {

                List<AspNetUserRoles> select = new List<AspNetUserRoles>();

                string query = "Select Id, Name from  AspNetRoles";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                user_roles.Category = new List<AspNetUserRoles>();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AspNetUserRoles Rls = new AspNetUserRoles();
                    Rls.Id = dt.Rows[i]["Id"].ToString();
                    Rls.Name = dt.Rows[i]["Name"].ToString();

                    select.Add(Rls);
                }
                user_roles.Category = select;


                //SqlConnection con1 = new SqlConnection(@"Data Source=DESKTOP-0LLQTUT\SQLEXPRESS;Initial Catalog=Transcanada;Integrated Security=True");

                List<User_Roles> select1 = new List<User_Roles>();
                string query1 = "Select Id, UserName from  AspNetUsers";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);

                user_roles.Users = new List<User_Roles>();


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    User_Roles user = new User_Roles();
                    user.Id = dt1.Rows[i]["Id"].ToString();
                    user.UserName = dt1.Rows[i]["UserName"].ToString();

                    select1.Add(user);
                }
                user_roles.Users = select1;

            }

            return View(user_roles);

        }
    }
}