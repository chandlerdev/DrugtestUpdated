using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    public class SampleController : Controller
    {
        // GET: Sample
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetOrganizations(int length, int start)
        {
            List<Organization> organizations = new List<Organization>();

            SqlConnection conn = new SqlConnection(@"server=75.148.178.12;database=Transcanada;user id=sa;password=Desss@2017;");
           
                conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbSQL1 = new StringBuilder();
            sbSQL.AppendFormat("select top({0}) * from (select org.*, row_number() over(order by OrgNumber DESC) as [row_number] from Organization org) org", length);
                sbSQL.AppendFormat(" where row_number >{0}", start);

            string searchVal = HttpContext.Request.Form["search[value]"];

            if (!string.IsNullOrEmpty(searchVal))
            {
                sbSQL.AppendFormat(" and Name like '%{0}%' or OrgNumber like '%{0}%'", searchVal);
            }
            sbSQL1.AppendFormat("select * from (select org.*, row_number() over(order by OrgNumber DESC) as [row_number] from Organization org) org");
            

            string searchVal1 = HttpContext.Request.Form["search[value]"];

            if (!string.IsNullOrEmpty(searchVal1))
            {
                sbSQL1.AppendFormat(" and Name like '%{0}%' or OrgNumber like '%{0}%'", searchVal1);
            }
            cmd1.Connection = conn;
            cmd1.CommandText = sbSQL1.ToString();
            Int32 count = (Int32)cmd1.ExecuteScalar();
            cmd.CommandText = sbSQL.ToString();
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                organizations.Add(new Organization()
                {
                    Name = reader["Name"].ToString(),
                    Fax = reader["Fax"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    OrgNumber = reader["OrgNumber"].ToString()
                });
            }
            reader.Close();
            conn.Close();
        

        var response = new { data = organizations, recordsFiltered = count, recordsTotal = count };
            return Json(response, JsonRequestBehavior.AllowGet);
    }
        public ActionResult folderview()
        {
            DirectoryInfo salesFTPDirectory = null;
            FileInfo[] files = null;

            try
            {
                string salesFTPPath = @"C:\DotNet\Live projects\TransCanada\TransCanada\Images";
                salesFTPDirectory = new DirectoryInfo(salesFTPPath);
                files = salesFTPDirectory.GetFiles("*.pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            files = files.OrderBy(f => f.Name).ToArray();

            return View(files);
        }

        public ActionResult Check()
        {
            return View();
        }
}
}