using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcBreadCrumbs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    [BreadCrumb]
    public class RoleController : Controller
    {
        [BreadCrumb(Clear = true, Label = "Roles")]
        public ActionResult Roles()
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roleStore = roleManager.Roles;
            List<string> roles = roleManager.Roles.Select(x => x.Name).ToList();
            List<Role> Roleslist = new List<Role>();
            if (roles.Count > 0)
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    Role role = new Role();
                    role.No = i + 1;
                    role.RoleName = roles[i];
                    Roleslist.Add(role);
                }
            }
            return View(Roleslist);
        }
        [BreadCrumb(Label = "New Role")]
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));


                if (!roleManager.RoleExists(role.RoleName))
                {
                    var _role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    _role.Name = role.RoleName;
                    roleManager.Create(_role);
                    ViewBag.Role_Status = "Role Added Successfully";
                }
                else
                {
                    ViewBag.Role_Status = "Role Name " + role.RoleName + " Already Exist";
                }
            }
            return View();
        }
        [BreadCrumb(Label = "Screens For Role")]
        public ActionResult Screens(string id)
        {
            Role role = new Role();
            List<CheckBox> items = new List<CheckBox>();
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "get_submenu_name";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Role", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new CheckBox
                            {
                                Value = sdr["submenu"].ToString(),
                                IsChecked = Convert.ToBoolean(sdr["user_account_status"])

                            });
                        }
                    }
                    con.Close();
                }
            }
            role.Screesns = items;
            role.RoleName = id;
            return View(role);
        }
        [HttpPost]
        public ActionResult Screens(Role role)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                               .ConnectionStrings["TransCanadaConnection"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "proc_update_Accounts_Screen";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    for (int i = 0; i < role.Screesns.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@roleid", role.RoleName);
                        cmd.Parameters.AddWithValue("@submenu", role.Screesns[i].Value);
                        cmd.Parameters.AddWithValue("@isact", role.Screesns[i].IsChecked);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return RedirectToAction("Roles");
        }
    }
}