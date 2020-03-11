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
    [Authorize]
    public class AssignController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
        // GET: Assign
        public ActionResult EmployeestoLab()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            Employeetolab employeetolab = new Employeetolab();
            List<SelectListItem> ls = new List<SelectListItem>();

            string query = "Select Employee_Name from  tbl_Loc_Employee";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Employee_Name"].ToString(),
                    Value = dt.Rows[i]["Employee_Name"].ToString()
                });
            }
            employeetolab.EmployeeNameList = ls;
            List<SelectListItem> ls1 = new List<SelectListItem>();

            string query1 = "Select Location_Name from  tbl_Clinic_Details where isdeleted=0 group by Location_Name";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                ls1.Add(new SelectListItem
                {

                    Text = dt1.Rows[i]["Location_Name"].ToString(),
                    Value = dt1.Rows[i]["Location_Name"].ToString()
                });
            }
            employeetolab.LabNameList = ls1;
            List<SelectListItem> ls2 = new List<SelectListItem>();
            employeetolab.LocationList = ls2;
            List<SelectListItem> ls3 = new List<SelectListItem>();
            employeetolab.AddressList = ls3;
            return View(employeetolab);
        }
        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            Employeetolab employeetolab = new Employeetolab();
            List<SelectListItem> ls = new List<SelectListItem>();

            string query = "Select Employee_Name from  tbl_Loc_Employee";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Employee_Name"].ToString(),
                    Value = dt.Rows[i]["Employee_Name"].ToString()
                });
            }
            employeetolab.EmployeeNameList = ls;
            List<SelectListItem> ls1 = new List<SelectListItem>();

            string query1 = "Select Location_Name from  tbl_Clinic_Details where isdeleted=0 group by Location_Name";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                ls1.Add(new SelectListItem
                {

                    Text = dt1.Rows[i]["Location_Name"].ToString(),
                    Value = dt1.Rows[i]["Location_Name"].ToString()
                });
            }
            employeetolab.LabNameList = ls1;
            SqlCommand cmd_get_values = new SqlCommand("proc_get_lab_employee_by_id", con);
            cmd_get_values.CommandType = CommandType.StoredProcedure;
            cmd_get_values.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dataReader = cmd_get_values.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    
                    employeetolab.Address = dataReader["address"].ToString();
                    employeetolab.Location = dataReader["city"].ToString();
                    employeetolab.State = dataReader["state"].ToString();
                    employeetolab.EmployeeName = dataReader["Employeename"].ToString();
                    employeetolab.Labname = dataReader["Labname"].ToString();
                }
                dataReader.Close();
                con.Close();
            }
            else
            {
                dataReader.Close();
                con.Close();
                employeetolab.LabNameList = ls1;
                List<SelectListItem> ls2 = new List<SelectListItem>();
                employeetolab.LocationList = ls2;
                List<SelectListItem> ls3 = new List<SelectListItem>();
                employeetolab.AddressList = ls3;
            }
            employeetolab.id = id;
            employeetolab.StateList = GetStates(employeetolab.Labname);
            employeetolab.LocationList = GetLocations(employeetolab.Labname, employeetolab.State);
            employeetolab.AddressList = GetAddressList(employeetolab.Labname, employeetolab.State, employeetolab.Location);
            return View(employeetolab);
        }
        [HttpPost]
        public ActionResult Edit(Employeetolab employeetolab)
        {
            if(ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("proc_update_emp_lab", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeename", employeetolab.EmployeeName);
                cmd.Parameters.AddWithValue("@Labname", employeetolab.Labname);
                cmd.Parameters.AddWithValue("@state", employeetolab.State);
                cmd.Parameters.AddWithValue("@city", employeetolab.Location);
                cmd.Parameters.AddWithValue("@address", employeetolab.Address);
                cmd.Parameters.AddWithValue("@id", employeetolab.id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("EmployeetoLab");
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                List<SelectListItem> ls = new List<SelectListItem>();
                string query = "Select Employee_Name from  tbl_Loc_Employee";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["Employee_Name"].ToString(),
                        Value = dt.Rows[i]["Employee_Name"].ToString()
                    });
                }
                employeetolab.EmployeeNameList = ls;
                List<SelectListItem> ls1 = new List<SelectListItem>();

                string query1 = "Select Location_Name from  tbl_Clinic_Details where isdeleted=0 group by Location_Name";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    ls1.Add(new SelectListItem
                    {

                        Text = dt1.Rows[i]["Location_Name"].ToString(),
                        Value = dt1.Rows[i]["Location_Name"].ToString()
                    });
                }
                employeetolab.LabNameList = ls1;
                employeetolab.StateList = GetStates(employeetolab.Labname);
                employeetolab.LocationList = GetLocations(employeetolab.Labname, employeetolab.State);
                employeetolab.AddressList = GetAddressList(employeetolab.Labname, employeetolab.State, employeetolab.Location);
                return View(employeetolab);
            }
        }
       public List<SelectListItem> GetAddressList(string Statecode, string state, string loc)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Statecode) && !string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(loc))
            {
                SqlCommand cmd = new SqlCommand("select Address_1 from tbl_Clinic_Details where Location_name=@Location_name and state=@state and city=@city order by Address_1", con);
                cmd.Parameters.AddWithValue("@Location_name", Statecode);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@city", loc);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["Address_1"].ToString(),
                        Value = dt.Rows[i]["Address_1"].ToString()
                    });
                }
            }

            return ls;
        }
        private List<SelectListItem> GetLocations(string labname, string state)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(labname) && !string.IsNullOrEmpty(state))
            {
                SqlCommand cmd = new SqlCommand("select city from tbl_Clinic_Details where Location_name=@Location_name and state=@state group by city", con);
                cmd.Parameters.AddWithValue("@Location_name", labname);
                cmd.Parameters.AddWithValue("@state", state);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["city"].ToString(),
                        Value = dt.Rows[i]["city"].ToString()
                    });
                }
            }
            return ls;
        }

        public ActionResult EmployeetoLab()
        {
            List<Employeetolab> employeetolabs = new List<Employeetolab>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("proc_get_lab_employee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            if(dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    Employeetolab employeetolab = new Employeetolab();
                    employeetolab.Address = dataReader["address"].ToString();
                    employeetolab.Location = dataReader["city"].ToString();
                    employeetolab.State = dataReader["state"].ToString();
                    employeetolab.EmployeeName = dataReader["Employeename"].ToString();
                    employeetolab.Labname = dataReader["Labname"].ToString();
                    employeetolab.id =Convert.ToInt32(dataReader["id"].ToString());
                    employeetolabs.Add(employeetolab);
                }
                dataReader.Close();
                con.Close();
            }
            else
            {
                dataReader.Close();
                con.Close();
            }
            return View(employeetolabs);
        }
        [HttpPost]
        public ActionResult EmployeestoLab(Employeetolab employeetolab)
        {
            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand cmd = new SqlCommand("proc_add_emp_lab", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeename", employeetolab.EmployeeName);
                cmd.Parameters.AddWithValue("@Labname", employeetolab.Labname);
                cmd.Parameters.AddWithValue("@state", employeetolab.State);
                cmd.Parameters.AddWithValue("@city", employeetolab.Location);
                cmd.Parameters.AddWithValue("@address", employeetolab.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Redirect("EmployeetoLab");
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                List<SelectListItem> ls = new List<SelectListItem>();

                string query = "Select Employee_Name from  tbl_Loc_Employee";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["Employee_Name"].ToString(),
                        Value = dt.Rows[i]["Employee_Name"].ToString()
                    });
                }
                employeetolab.EmployeeNameList = ls;
                List<SelectListItem> ls1 = new List<SelectListItem>();

                string query1 = "Select Location_Name from  tbl_Clinic_Details where isdeleted=0 group by Location_Name";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    ls1.Add(new SelectListItem
                    {

                        Text = dt1.Rows[i]["Location_Name"].ToString(),
                        Value = dt1.Rows[i]["Location_Name"].ToString()
                    });
                }
                employeetolab.LabNameList = ls1;
                List<SelectListItem> ls2 = new List<SelectListItem>();
                employeetolab.LocationList = ls2;
                List<SelectListItem> ls3 = new List<SelectListItem>();
                employeetolab.AddressList = ls3;
                return View(employeetolab);
            }
        }
        public JsonResult GetCities(string Statecode,string state)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("select city from tbl_Clinic_Details where Location_name=@Location_name and state=@state group by city", con);
            cmd.Parameters.AddWithValue("@Location_name", Statecode);
            cmd.Parameters.AddWithValue("@state", state);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["city"].ToString(),
                    Value = dt.Rows[i]["city"].ToString()
                });
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetState(string Statecode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("select state from tbl_Clinic_Details where Location_name=@Location_name group by state", con);
            cmd.Parameters.AddWithValue("@Location_name", Statecode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["state"].ToString(),
                    Value = dt.Rows[i]["state"].ToString()
                });
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAddress(string Statecode, string state,string loc)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Statecode) && !string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(loc))
            {
                SqlCommand cmd = new SqlCommand("select Address_1 from tbl_Clinic_Details where Location_name=@Location_name and state=@state and city=@city order by Address_1", con);
                cmd.Parameters.AddWithValue("@Location_name", Statecode);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@city", loc);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["Address_1"].ToString(),
                        Value = dt.Rows[i]["Address_1"].ToString()
                    });
                }
            }
            return Json(ls, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> GetStates(string Statecode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Statecode))
            {
                SqlCommand cmd = new SqlCommand("select state from tbl_Clinic_Details where Location_name=@Location_name group by state", con);
                cmd.Parameters.AddWithValue("@Location_name", Statecode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["state"].ToString(),
                        Value = dt.Rows[i]["state"].ToString()
                    });
                }
            }
            return ls;
        }
        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("proc_delete_emp_lab", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("EmployeetoLab");
        }
    }
}