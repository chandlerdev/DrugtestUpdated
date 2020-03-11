using MvcBreadCrumbs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;

namespace TransCanada.Controllers
{
    [BreadCrumb]
    public class ClientLabController : Controller
    {

        string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: ClientLab
        [BreadCrumb(Label = "Client Lab and SP")]
        public ActionResult ClientLabSP(string id,string rtn)
        {
            Lab_dd service = new Lab_dd();
            service.src = rtn;
            service.Client_Name = id.Trim();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand command = new SqlCommand("GetLabdetails", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Client_Name", service.Client_Name);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            service.Labs = PopulateLabs();
            service.List_SP = ProviderList();
            if (dataTable.Rows.Count == 0)
            {
                service.Labservices = GetServices("Lab", string.Empty);
                service.Labsubservices = GetSubServices("Lab", string.Empty);
                service.List_Sp_Services = GetServices("SP", string.Empty);
                service.List_Sp_Sub_Services = GetSubServices("SP", string.Empty);
            }
            else
            {
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Labs"].ToString()))
                {
                    string Title =  dataTable.Rows[0]["Labs"].ToString();
                    service.Lab_Name = Title.Split(',');
                    for (int i = 0; i < service.Lab_Name.Length; i++)
                    {

                        foreach (var item in service.Labs)
                        {
                            if (service.Lab_Name[i] == item.Text.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Serviceprovider"].ToString()))
                {
                    string Title = dataTable.Rows[0]["Serviceprovider"].ToString();
                    service.SP_Names = Title.Split(',');
                    for (int i = 0; i < service.SP_Names.Length; i++)
                    {

                        foreach (var item in service.List_SP)
                        {
                            if (service.SP_Names[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Labs"].ToString()))
                {
                    service.Labservices = GetServices("Lab", dataTable.Rows[0]["Labs"].ToString());
                    string Title = string.Empty;
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["ServiceGroups"].ToString()))
                    {
                         Title = dataTable.Rows[0]["ServiceGroups"].ToString();
                    }
                    service.Servicegroups = Title.Split(',');
                    for (int i = 0; i < service.Servicegroups.Length; i++)
                    {

                        foreach (var item in service.Labservices)
                        {
                            if (service.Servicegroups[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    service.Labservices = GetServices("Lab", string.Empty);
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["ServiceGroups"].ToString()))
                {
                    service.Labsubservices = GetSubServices("Lab", dataTable.Rows[0]["ServiceGroups"].ToString());
                    string Title = string.Empty;
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["SubServices"].ToString()))
                    {
                        Title = dataTable.Rows[0]["SubServices"].ToString();
                    }
                    
                    service.SubServices = Title.Split(',');
                    for (int i = 0; i < service.SubServices.Length; i++)
                    {

                        foreach (var item in service.Labsubservices)
                        {
                            if (service.SubServices[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    service.Labsubservices = GetSubServices("Lab", string.Empty);
                }


                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Serviceprovider"].ToString()))
                {
                    service.List_Sp_Services = GetServices("SP", dataTable.Rows[0]["Serviceprovider"].ToString());
                    string Title = string.Empty;
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["SP_Service"].ToString()))
                    {
                        Title = dataTable.Rows[0]["SP_Service"].ToString();
                    }
                    service.SP_Panel_Groups = Title.Split(',');
                    for (int i = 0; i < service.SP_Panel_Groups.Length; i++)
                    {

                        foreach (var item in service.List_Sp_Services)
                        {
                            if (service.SP_Panel_Groups[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    service.List_Sp_Services = GetServices("SP", string.Empty);
                }

                if (!string.IsNullOrEmpty(dataTable.Rows[0]["SP_Service"].ToString()))
                {
                    service.List_Sp_Sub_Services = GetSubServices("SP", dataTable.Rows[0]["SP_Service"].ToString());
                    string Title = string.Empty;
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["Sp_Sub_Service"].ToString()))
                    {
                        Title = dataTable.Rows[0]["Sp_Sub_Service"].ToString();
                    }

                    service.SP_Panels = Title.Split(',');
                    for (int i = 0; i < service.SP_Panels.Length; i++)
                    {

                        foreach (var item in service.List_Sp_Sub_Services)
                        {
                            if (service.SP_Panels[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    service.List_Sp_Sub_Services = GetSubServices("SP", string.Empty);
                }
                
            }
            
            //List<SelectListItem> SelectListItem12  = service.Labs.(item => item.Selected == true);
            //service.Labs =System.Collections.Generic.List SelectListItem12;
            return View(service);
        }

        [HttpPost]
        public ActionResult ClientLabSP(Lab_dd service)
        {
            string Labs=string.Empty;
            string Labservices = string.Empty;
            string LabSubservices = string.Empty;
            string SP_name = string.Empty;
            string SP_Panel_Grp = string.Empty;
            string SP_Panel = string.Empty;
            if (service.Lab_Name != null)
            {
                Labs = ConvertStringArrayToString(service.Lab_Name);
            }
            if (service.Servicegroups != null)
            {
                Labservices = ConvertStringArrayToString(service.Servicegroups);
            }
            if (service.SubServices != null)
            {
                 LabSubservices = ConvertStringArrayToString(service.SubServices);
            }
            if (service.SP_Names != null)
            {
                SP_name = ConvertStringArrayToString(service.SP_Names);
            }
            if (service.SP_Panel_Groups != null)
            {
                SP_Panel_Grp = ConvertStringArrayToString(service.SP_Panel_Groups);
            }
            if (service.SP_Panels != null)
            {
                SP_Panel = ConvertStringArrayToString(service.SP_Panels);
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand command = new SqlCommand("ClientlabandSp", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Client_Name", service.Client_Name);
            command.Parameters.AddWithValue("@Labs", Labs);
            command.Parameters.AddWithValue("@ServiceGroups", Labservices);
            command.Parameters.AddWithValue("@SubServices", LabSubservices);
            command.Parameters.AddWithValue("@Serviceprovider", SP_name);
            command.Parameters.AddWithValue("@SP_Service", SP_Panel_Grp);
            command.Parameters.AddWithValue("@Sp_Sub_Service", SP_Panel);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            
            if (service.src == null)
                return RedirectToAction("Index", "DashBoard");
            else
                return RedirectToAction("AccountsList", "Asp_Accounts");
            
        }
       public string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(',');
            }
            return builder.ToString();
        }

        private static List<SelectListItem> PopulateLabs()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //string TransConnString = ConfigurationManager.ConnectionStrings["TransConnString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString))
            {
                string query = " SELECT  Distinct Location_Name  FROM tbl_Clinic_Details where isdeleted = 0  group by Location_Name" ;
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Location_Name"].ToString(),
                                //Value = sdr["Location_Id"].ToString()
                                

                            });
                        }
                    }
                    con.Close();


                }
            }

            return items;
        }

     

        //public JsonResult GetService(string[] Location_Name)

        //{
            
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
        //        List<SelectListItem> ls = new List<SelectListItem>();
        //    if (Location_Name != null)
        //    {
        //        string Labservice1 = string.Join(",", Location_Name);
        //        SqlCommand cmd = new SqlCommand("select service_grp_name from lab_service_grp where LabName in(@LabName) ", con);
        //        cmd.Parameters.AddWithValue("@LabName", Labservice1.ToString());
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
           
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {

        //            ls.Add(new SelectListItem
        //            {

        //                Text = dt.Rows[i]["service_grp_name"].ToString(),
        //                Value = dt.Rows[i]["service_grp_name"].ToString()
        //            });
        //        }
        //    }
        //    return Json(ls, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetSubService(string Statecode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("Select city from cities where state_code = @code", con);
            cmd.Parameters.AddWithValue("@code", Statecode);
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
        public static List<SelectListItem> GetServices(string Type, string name)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Type))
            {
                string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
                if (Type == "Lab")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "  SELECT id,service_grp_name FROM lab_service_grp where LabName=@LabName";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Parameters.AddWithValue("@LabName", values[i].Trim());
                                cmd.Connection = con;
                                con.Open();
                                using (SqlDataReader sdr = cmd.ExecuteReader())
                                {
                                    while (sdr.Read())
                                    {
                                        items.Add(new SelectListItem
                                        {
                                            Text = values[i] + "-" + sdr["service_grp_name"].ToString(),
                                            Value = sdr["id"].ToString(),
                                            Selected = false
                                        });
                                    }
                                }
                                con.Close();
                            }
                        }
                    }
                }
                else if (Type == "SP")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "select Sp_service_id,Sp_services_description from Sp_sub_services where Sp_group_id=@Sp_group_id";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@Sp_group_id", values[i].Trim());
                            con.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = sdr["Sp_services_description"].ToString(),
                                        Value = sdr["Sp_service_id"].ToString(),
                                        Selected = false
                                    });
                                }
                            }
                            con.Close();
                        }
                    }
                    return items;
                }
                return items;
            }
            else
            {
                return items;
            }
        }
        public static List<SelectListItem> GetSubServices(string Type, string name)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Type))
            {
                string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
                if (Type == "Lab")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "Getlabsub_services";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@lab_service_grp_id", values[i].Trim());

                            con.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = sdr["Labname"].ToString() + "-" + sdr["lab_services_description"].ToString(),
                                        Value = sdr["id"].ToString(),
                                        Selected = false
                                    });
                                }
                            }
                            con.Close();
                        }
                    }
                }
                else if (Type == "SP")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "select Sp_service_id,Sp_services_description from Sp_sub_services where Sp_group_id=@Sp_group_id";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@Sp_group_id", values[i].Trim());
                            con.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = sdr["Sp_services_description"].ToString(),
                                        Value = sdr["Sp_service_id"].ToString(),
                                        Selected = false
                                    });
                                }
                            }
                            con.Close();
                        }
                    }
                    return items;
                }
                return items;
            }
            else
            {
                return items;
            }
        }
        public JsonResult JsonGetServices(string Type, string name)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Type))
            {
                string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
                if (Type == "Lab")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "  SELECT id,service_grp_name FROM lab_service_grp where LabName=@LabName";
                            using (SqlCommand cmd = new SqlCommand(query))
                            {
                                cmd.Parameters.AddWithValue("@LabName", values[i].Trim());
                                cmd.Connection = con;
                                con.Open();
                                using (SqlDataReader sdr = cmd.ExecuteReader())
                                {
                                    while (sdr.Read())
                                    {
                                        items.Add(new SelectListItem
                                        {
                                            Text = values[i]+"-"+sdr["service_grp_name"].ToString(),
                                            Value = sdr["id"].ToString(),
                                            Selected = false
                                        });
                                    }
                                }
                                con.Close();
                            }
                        }
                    }
                }
                else if (Type == "SP")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            SqlCommand sqlCommand = new SqlCommand("Sp_group_services_List", con);
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.AddWithValue("@SpName", values[i].ToString().Trim());
                            con.Open();
                            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                            if (sqlDataReader.HasRows)
                            {
                                while (sqlDataReader.Read())
                                    items.Add(new SelectListItem
                                    {
                                        Value = Convert.ToInt32(sqlDataReader["Sp_id"]).ToString().Trim(),
                                        Text = Convert.ToString(sqlDataReader["service_grp_name"])
                                    });

                            }
                            con.Close();
                        }
                    }
                    return Json(items, JsonRequestBehavior.AllowGet);
                }
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(items,JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult JsonGetSubServices(string Type, string name)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Type))
            {
                string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
                if (Type == "Lab")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "Getlabsub_services";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@lab_service_grp_id", values[i].Trim());

                            con.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = sdr["Labname"].ToString()+"-"+ sdr["lab_services_description"].ToString(),
                                        Value = sdr["id"].ToString(),
                                        Selected = false
                                    });
                                }
                            }
                            con.Close();
                        }
                    }
                }
                else if (Type == "SP")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string[] values = name.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "select Sp_service_id,Sp_services_description from Sp_sub_services where Sp_group_id=@Sp_group_id";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@Sp_group_id", values[i].Trim());
                            con.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = sdr["Sp_services_description"].ToString(),
                                        Value = sdr["Sp_service_id"].ToString(),
                                        Selected = false
                                    });
                                }
                            }
                            con.Close();
                        }
                    }
                    return Json(items, JsonRequestBehavior.AllowGet);
                }
                return Json(items, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(items, JsonRequestBehavior.AllowGet);
            }
        }
        public List<SelectListItem> ProviderList()
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("select serviceprovider_id,Serviceprovider_Name from Tbl_Service_Provider ", con);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<SelectListItem> serviceProviderList = new List<SelectListItem>();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
                serviceProviderList.Add(new SelectListItem
                {
                    Value = dataTable.Rows[index]["serviceprovider_id"].ToString().Trim(),
                    Text = string.IsNullOrEmpty(dataTable.Rows[index]["Serviceprovider_Name"].ToString().Trim()) ? string.Empty : dataTable.Rows[index]["Serviceprovider_Name"].ToString().Trim()
                });
            return serviceProviderList;
        }
        
    }
}