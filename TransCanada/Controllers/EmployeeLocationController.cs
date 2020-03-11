using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;
using TransCanadaDemo.Controllers;
using TransCanadaDemo.Models;


namespace TransCanada.Controllers
{
    public class EmployeeLocationController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: EmployeeLocation
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Employeelist()

        {

            try
            {
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("Proc_get_all_Emp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<EmployeeLocation> LocList = new List<EmployeeLocation>();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    EmployeeLocation EmpLoc = new EmployeeLocation();
                    EmpLoc.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Employee_Name"].ToString()))
                    {
                        EmpLoc.Employee_Name = dt.Rows[j]["Employee_Name"].ToString();
                    }
                    else
                    {
                        EmpLoc.Employee_Name = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Employee_Test"].ToString()))
                    {
                        EmpLoc.Employee_Test = dt.Rows[j]["Employee_Test"].ToString();
                    }
                    else
                    {
                        EmpLoc.Employee_Test = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Address_1"].ToString()))
                    {
                        EmpLoc.Address_1 = dt.Rows[j]["Address_1"].ToString();
                    }
                    else
                    {
                        EmpLoc.Address_1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Address_2"].ToString()))
                    {
                        EmpLoc.Address_2 = dt.Rows[j]["Address_2"].ToString();
                    }
                    else
                    {
                        EmpLoc.Address_2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["city"].ToString()))
                    {
                        EmpLoc.city = dt.Rows[j]["city"].ToString();
                    }
                    else
                    {
                        EmpLoc.city = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["State"].ToString()))
                    {
                        EmpLoc.State = dt.Rows[j]["State"].ToString();
                    }
                    else
                    {
                        EmpLoc.State = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Country"].ToString()))
                    {
                        EmpLoc.Country = dt.Rows[j]["Country"].ToString();
                    }
                    else
                    {
                        EmpLoc.Country = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Email"].ToString()))
                    {
                        EmpLoc.Email = dt.Rows[j]["Email"].ToString();
                    }
                    else
                    {
                        EmpLoc.Email = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Phone"].ToString()))
                    {
                        EmpLoc.Phone = dt.Rows[j]["Phone"].ToString();
                    }
                    else
                    {
                        EmpLoc.Phone = string.Empty;
                    }


                    if (!string.IsNullOrEmpty(dt.Rows[j]["Mobile"].ToString()))
                    {
                        EmpLoc.Mobile = dt.Rows[j]["Mobile"].ToString();
                    }
                    else
                    {
                        EmpLoc.Mobile = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Priority"].ToString()))
                    {
                        EmpLoc.Priority = dt.Rows[j]["Priority"].ToString();
                    }
                    else
                    {
                        EmpLoc.Priority = string.Empty;
                    }
                    LocList.Add(EmpLoc);
                }
                return View(LocList);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "EmployeeLocation");
            }


        }

        public ActionResult InsertEmployee()
        {
            EmployeeLocation client = new EmployeeLocation();
            ClientController clientController = new ClientController();
            client.Cities = clientController.GetAllCities(string.Empty);
            return View(client);
        }

        [HttpPost]
        public ActionResult InsertEmployee(EmployeeLocation EmpLoc)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    SqlConnection con = new SqlConnection(TransCanadaConnection);
                    SqlCommand cmd = new SqlCommand("proc_insert_Emploc", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(EmpLoc.Employee_Name))
                    {
                        cmd.Parameters.AddWithValue("@Employee_Name", EmpLoc.Employee_Name);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Employee_Name", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Employee_Test))
                    {
                        cmd.Parameters.AddWithValue("@Employee_Test", EmpLoc.Employee_Test);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Employee_Test", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Address_1))
                    {
                        cmd.Parameters.AddWithValue("@Address_1", EmpLoc.Address_1);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Address_1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Address_2))
                    {
                        cmd.Parameters.AddWithValue("@Address_2", EmpLoc.Address_2);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Address_2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.city))
                    {
                        cmd.Parameters.AddWithValue("@city", EmpLoc.city);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@city", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.State))
                    {
                        cmd.Parameters.AddWithValue("@State", EmpLoc.State);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@State", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Country))
                    {
                        cmd.Parameters.AddWithValue("@Country", EmpLoc.Country);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Country", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Email))
                    {
                        cmd.Parameters.AddWithValue("@Email", EmpLoc.Email);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Email", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Phone))
                    {
                        cmd.Parameters.AddWithValue("@Phone", EmpLoc.Phone);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Phone", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Mobile))
                    {
                        cmd.Parameters.AddWithValue("@Mobile", EmpLoc.Mobile);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Mobile", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Priority))
                    {
                        cmd.Parameters.AddWithValue("@Priority", EmpLoc.Priority);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Priority", string.Empty);
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("Employeelist", "EmployeeLocation");

                }

                catch (Exception)
                {
                    return RedirectToAction("Error", "EmployeeLocation");
                }
            }
            else
            {
                ClientController clientController = new ClientController();
                EmpLoc.Cities = clientController.GetAllCities(string.Empty);
                return View();
            }

        }



        [HttpGet]

        public ActionResult UpdateEmployee(string id)
        {
            try { 
            
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("Proc_get_Emploc_byid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                EmployeeLocation EmpLoc = new EmployeeLocation();

                if (dt.Rows.Count > 0)
                {
                    //EmpLoc.Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());

                    if (!string.IsNullOrEmpty(dt.Rows[0]["Employee_Name"].ToString()))
                    {
                        EmpLoc.Employee_Name = dt.Rows[0]["Employee_Name"].ToString();
                    }
                    else
                    {
                        EmpLoc.Employee_Name = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Employee_Test"].ToString()))
                    {
                        EmpLoc.Employee_Test = dt.Rows[0]["Employee_Test"].ToString();
                    }
                    else
                    {
                        EmpLoc.Employee_Test = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Address_1"].ToString()))
                    {
                        EmpLoc.Address_1 = dt.Rows[0]["Address_1"].ToString();
                    }
                    else
                    {
                        EmpLoc.Address_1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Address_2"].ToString()))
                    {
                        EmpLoc.Address_2 = dt.Rows[0]["Address_2"].ToString();
                    }
                    else
                    {
                        EmpLoc.Address_2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["city"].ToString()))
                    {
                        EmpLoc.city = dt.Rows[0]["city"].ToString();
                    }
                    else
                    {
                        EmpLoc.city = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["State"].ToString()))
                    {
                        EmpLoc.State = dt.Rows[0]["State"].ToString();
                        ClientController clientController = new ClientController();
                        EmpLoc.Cities = clientController.GetAllCities(dt.Rows[0]["State"].ToString());
                    }
                    else
                    {
                        EmpLoc.State = string.Empty;
                        ClientController clientController = new ClientController();
                        EmpLoc.Cities = clientController.GetAllCities(string.Empty);
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Country"].ToString()))
                    {
                        EmpLoc.Country = dt.Rows[0]["Country"].ToString();
                    }
                    else
                    {
                        EmpLoc.Country = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Email"].ToString()))
                    {
                        EmpLoc.Email = dt.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        EmpLoc.Email = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Phone"].ToString()))
                    {
                        EmpLoc.Phone = dt.Rows[0]["Phone"].ToString();
                    }
                    else
                    {
                        EmpLoc.Phone = string.Empty;
                    }


                    if (!string.IsNullOrEmpty(dt.Rows[0]["Mobile"].ToString()))
                    {
                        EmpLoc.Mobile = dt.Rows[0]["Mobile"].ToString();
                    }
                    else
                    {
                        EmpLoc.Mobile = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Priority"].ToString()))
                    {
                        EmpLoc.Priority = dt.Rows[0]["Priority"].ToString();
                    }
                    else
                    {
                        EmpLoc.Priority = string.Empty;
                    }
                }

                return View(EmpLoc);
            }
            catch (Exception)
            {
                return RedirectToAction("Errorpage", "EmployeeLocation");
            }


        }

        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeLocation EmpLoc)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    SqlConnection con = new SqlConnection(TransCanadaConnection);
                    SqlCommand cmd = new SqlCommand("Proc_Update_Emploc", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(EmpLoc.Id));
                    if (!string.IsNullOrEmpty(EmpLoc.Employee_Name))
                    {
                        cmd.Parameters.AddWithValue("@Employee_Name", EmpLoc.Employee_Name);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Employee_Name", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Employee_Test))
                    {
                        cmd.Parameters.AddWithValue("@Employee_Test", EmpLoc.Employee_Test);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Employee_Test", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Address_1))
                    {
                        cmd.Parameters.AddWithValue("@Address_1", EmpLoc.Address_1);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Address_1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Address_2))
                    {
                        cmd.Parameters.AddWithValue("@Address_2", EmpLoc.Address_2);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Address_2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.city))
                    {
                        cmd.Parameters.AddWithValue("@city", EmpLoc.city);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@city", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.State))
                    {
                        cmd.Parameters.AddWithValue("@State", EmpLoc.State);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@State", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Country))
                    {
                        cmd.Parameters.AddWithValue("@Country", EmpLoc.Country);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Country", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Email))
                    {
                        cmd.Parameters.AddWithValue("@Email", EmpLoc.Email);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Email", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Phone))
                    {
                        cmd.Parameters.AddWithValue("@Phone", EmpLoc.Phone);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Phone", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Mobile))
                    {
                        cmd.Parameters.AddWithValue("@Mobile", EmpLoc.Mobile);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Mobile", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(EmpLoc.Priority))
                    {
                        cmd.Parameters.AddWithValue("@Priority", EmpLoc.Priority);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Priority", string.Empty);
                    }
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("Employeelist", "EmployeeLocation");

                }

                catch (Exception)
                {
                    return RedirectToAction("Errorpage", "EmployeeLocation");
                }
            }
            else
            {
                ClientController clientController = new ClientController();
                EmpLoc.Cities = clientController.GetAllCities(string.Empty);
                return View(EmpLoc);
            }

        }

        public ActionResult DeleteEmployee(string id)
        {
            try
            {
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("Proc_Delete_Emploc", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Employeelist", "EmployeeLocation");
            }
            catch (Exception)
            {
                return RedirectToAction("Errorpage", "EmployeeLocation");

            }
        }

        public ActionResult Errorpage()
        {
            return View();
        }
    }
}
         
