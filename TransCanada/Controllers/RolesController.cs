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
    public class RolesController : Controller
    {
        string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        public ActionResult Roles_List()
        {
            List<AspNetRolesModel> Roles_List = new List<AspNetRolesModel>();

            using (SqlConnection con = new SqlConnection(TransConnString))
            {
                String query = "Select * from AspNetRoles";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    AspNetRolesModel Rls = new AspNetRolesModel();
                    Rls.Id = dt.Rows[j]["Id"].ToString();
                    Rls.Name = dt.Rows[j]["Name"].ToString();
                    Rls.CreatedBy = dt.Rows[j]["CreatedBy"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[j]["CreatedDate"].ToString()))
                    {
                        Rls.CreatedDate = Convert.ToDateTime(dt.Rows[j]["CreatedDate"].ToString());
                    }
                    Rls.UpdatedBy = dt.Rows[j]["UpdatedBy"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[j]["CreatedDate"].ToString()))
                    {
                        Rls.UpdatedDate = Convert.ToDateTime(dt.Rows[j]["UpdatedDate"].ToString());
                    }


                    Roles_List.Add(Rls);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return View(Roles_List);
        }
        // GET: Roles
        public ActionResult InsertRoles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertRoles(AspNetRolesModel Rls)
        {
            DateTime CreatedDate = System.DateTime.Now;
            DateTime UpdatedDate = System.DateTime.Now;
            string CreatedBy = "SSS";
            string UpdatedBy = "SSS";

            using (SqlConnection con = new SqlConnection(TransConnString))

            {
                con.Open();
                String query = "Insert into AspNetRoles (Id, Name,CreatedBy, CreatedDate, UpdatedBy, UpdatedDate) values(@Id, @Name, @CreatedBy, @CreatedDate, @UpdatedBy, @UpdatedDate)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", Rls.Id);
                cmd.Parameters.AddWithValue("@Name", Rls.Name);
                cmd.Parameters.AddWithValue("@CreatedBy", Rls.CreatedBy).Value = CreatedBy;
                cmd.Parameters.AddWithValue("@CreatedDate", Convert.ToDateTime(Rls.CreatedDate)).Value = DateTime.Now;
                cmd.Parameters.AddWithValue("@UpdatedBy", Rls.UpdatedBy).Value = UpdatedBy;
                cmd.Parameters.AddWithValue("@UpdatedDate", Convert.ToDateTime(Rls.UpdatedDate)).Value = DateTime.Now;

                
                cmd.ExecuteNonQuery();
                con.Close();

            }
            return RedirectToAction("Roles_List", "Roles");
           
        }

        //public ActionResult SelectCategory()

        //{
        //    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0LLQTUT\SQLEXPRESS;Initial Catalog=Transcanada;Integrated Security=True");
        //    String query = "Select Name, Id from AspNetRoles";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    return View();
        //}
        [HttpGet]
        public ActionResult Update_Roles(string id)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            string query = "Select Id, Name, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from AspNetRoles where Id=@Id";
            SqlCommand Cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(Cmd);
            Cmd.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            AspNetRolesModel Rls = new AspNetRolesModel();
            if (dt.Rows.Count > 0)
            {
                Rls.Id = dt.Rows[0]["Id"].ToString();
                Rls.Name = dt.Rows[0]["Name"].ToString();

                Rls.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
                if(!string.IsNullOrEmpty(dt.Rows[0]["CreatedDate"].ToString()))
                { 
                Rls.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                }
                Rls.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                if(!string.IsNullOrEmpty(dt.Rows[0]["UpdatedDate"].ToString()))
                { 
                Rls.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());

                }
                //if(!string.IsNullOrEmpty(dt.Rows[0]["IsDeleted"].ToString()))
                //{ 
                //Rls.IsDeleted = Convert.ToBoolean(dt.Rows[0]["IsDeleted"].ToString());

                //}
            }

            con.Open();
            Cmd.ExecuteNonQuery();
            con.Close();
            return View(Rls);
        }
        [HttpPost]
        public ActionResult Update_Roles(AspNetRolesModel Rls)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            String query = "Update AspNetRoles set Id =@Id, Name=@Name, CreatedBy=@CreatedBy, CreatedDate=@CreatedDate, UpdatedBy=@UpdatedBy, UpdatedDate=@UpdatedDate where Id =@Id";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Id", Rls.Id);
            cmd.Parameters.AddWithValue("@Name", Rls.Name);
            cmd.Parameters.AddWithValue("@CreatedBy", Rls.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", Convert.ToDateTime(Rls.CreatedDate));
            cmd.Parameters.AddWithValue("@UpdatedBy", Rls.UpdatedBy);
            cmd.Parameters.AddWithValue("@UpdatedDate", Convert.ToDateTime(Rls.UpdatedDate));
            //cmd.Parameters.AddWithValue("@IsDeleted", Rls.IsDeleted);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction ("Roles_List","Roles");
        }

        public ActionResult Roles_Details(string id)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            string query = "Select Id, Name, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate from AspNetRoles where Id=@Id";
            SqlCommand Cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(Cmd);
            Cmd.Parameters.AddWithValue("@Id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            AspNetRolesModel Rls = new AspNetRolesModel();
            if (dt.Rows.Count > 0)
            {
                Rls.Id = dt.Rows[0]["Id"].ToString();
                Rls.Name = dt.Rows[0]["Name"].ToString();
                Rls.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
                Rls.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                Rls.UpdatedBy = dt.Rows[0]["UpdatedBy"].ToString();
                Rls.UpdatedDate = Convert.ToDateTime(dt.Rows[0]["UpdatedDate"].ToString());

            }

            con.Open();
            Cmd.ExecuteNonQuery();
            con.Close();

            return View(Rls);

        }
        public ActionResult Delete_Roles(string id)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            string query = "Delete from AspNetRoles where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Roles_List", "Roles");
        }

        //public ActionResult Select_Roles()
        //{
        //    AspNetRolesModel selectroles = new AspNetRolesModel();

        //    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0LLQTUT\SQLEXPRESS;Initial Catalog=Transcanada;Integrated Security=True");

        //    List<AspNetRolesModel> select = new List<AspNetRolesModel>();

        //    string query = "Select Id, Name from  AspNetRoles";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    selectroles.Category = new List<AspNetRolesModel>();


        //    for (int i = 0; i < dt.Rows.Count; i++)
        //     {
        //       AspNetRolesModel Rls = new AspNetRolesModel();
        //       Rls.Id = dt.Rows[i]["Id"].ToString();
        //       Rls.Name = dt.Rows[i]["Name"].ToString();

        //       select.Add(Rls);
        //    }
        //       selectroles.Category = select;


        //    SqlConnection con1 = new SqlConnection(@"Data Source=DESKTOP-0LLQTUT\SQLEXPRESS;Initial Catalog=Transcanada;Integrated Security=True");

        //    List<UserModel> select1 = new List<UserModel>();
        //    string query1 = "Select Id, UserName from  AspNetUsers";
        //    SqlCommand cmd1 = new SqlCommand(query1, con1);
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
        //    DataTable dt1 = new DataTable();
        //    sda.Fill(dt1);

        //    selectroles.Users = new List<UserModel>();


        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        UserModel user = new UserModel();
        //        user.Id = dt1.Rows[i]["Id"].ToString();
        //        user.UserName = dt1.Rows[i]["UserName"].ToString();

        //        select1.Add(user);
        //    }
        //    selectroles.Users = select1;

        //    return View(selectroles);

        //}
           
    }
}
    
