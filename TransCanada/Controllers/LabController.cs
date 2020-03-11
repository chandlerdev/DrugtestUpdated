using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using TransCanada.Models;
using TransCanadaDemo.Controllers;
using MvcBreadCrumbs;

namespace TransCanada.Controllers
{
    [Authorize]
    [BreadCrumb]
    public class LabController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
        [BreadCrumb(Clear =true, Label = "LabList")]
        public ActionResult LabList()
        {
            List<Lab_loc> labLocList = new List<Lab_loc>();
            try
            {
                SqlCommand selectCommand = new SqlCommand("select Location_Name from tbl_Clinic_Details where isdeleted=0  group by Location_Name ", this.conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    for (int index = 0; index < dataTable.Rows.Count; ++index)
                        labLocList.Add(new Lab_loc()
                        {
                            Id = index + 1,
                            Lab_Name = string.IsNullOrEmpty(dataTable.Rows[index]["Location_Name"].ToString()) ? string.Empty : dataTable.Rows[index]["Location_Name"].ToString()
                        });
                }
            }
            catch (Exception ex)
            {
            }
            return (ActionResult)this.View((object)labLocList);
        }
        [BreadCrumb(Label = "Lab Location")]
        public ActionResult Locations(string id)
        {
            List<lab_location> labLocationList = new List<lab_location>();
            if (id != null)
            {
                TempData["Lab_Name"] = id;
                // ISSUE: reference to a compiler-generated field
                ViewBag.LabName = id.Trim();
                SqlCommand selectCommand = new SqlCommand("Get_Location_for_lab", this.conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@Location_Name", (object)id);
                DataTable dataTable = new DataTable();
                new SqlDataAdapter(selectCommand).Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    for (int index = 0; index < dataTable.Rows.Count; ++index)
                        labLocationList.Add(new lab_location()
                        {
                            Id1 = string.IsNullOrEmpty(dataTable.Rows[index]["Id"].ToString()) ? string.Empty : dataTable.Rows[index]["Id"].ToString(),
                            Location_Id = string.IsNullOrEmpty(dataTable.Rows[index]["Location_Id"].ToString()) ? string.Empty : dataTable.Rows[index]["Location_Id"].ToString(),
                            Address_1 = string.IsNullOrEmpty(dataTable.Rows[index]["Address_1"].ToString()) ? string.Empty : dataTable.Rows[index]["Address_1"].ToString(),
                            Address_2 = string.IsNullOrEmpty(dataTable.Rows[index]["Address_2"].ToString()) ? string.Empty : dataTable.Rows[index]["Address_2"].ToString(),
                            City = string.IsNullOrEmpty(dataTable.Rows[index]["City"].ToString()) ? string.Empty : dataTable.Rows[index]["City"].ToString(),
                            State = string.IsNullOrEmpty(dataTable.Rows[index]["State"].ToString()) ? string.Empty : dataTable.Rows[index]["State"].ToString(),
                            Zip = string.IsNullOrEmpty(dataTable.Rows[index]["Zip"].ToString()) ? string.Empty : dataTable.Rows[index]["Zip"].ToString(),
                            Country = string.IsNullOrEmpty(dataTable.Rows[index]["Country"].ToString()) ? string.Empty : dataTable.Rows[index]["Country"].ToString()
                        });
                }
            }
            return (ActionResult)this.View((object)labLocationList);
        }
        [BreadCrumb(Label = "Add Lab Location")]
        public ActionResult AddLab(string id)
        {
            if (id == null)
            {
                lab_location lab_Location = new lab_location();
                ViewBag.Status = string.Empty;
                lab_Location.hide_id = "2";
                lab_Location.Id1 = string.Empty;
                ClientController clientController = new ClientController();
                lab_Location.Cities = clientController.GetAllCities(string.Empty);
                return View(lab_Location);
            }
            else
            {

                lab_location lab_Location = new lab_location();
                ViewBag.Status = id;
                lab_Location.Lab_Name = id;
                lab_Location.hide_id = "3";
                lab_Location.Id1 = string.Empty;
                ClientController clientController = new ClientController();
                lab_Location.Cities = clientController.GetAllCities(string.Empty);
                return View(lab_Location);
            }
        }

        [HttpPost]
        public ActionResult AddLab(lab_location lab_Location)
        {
            if (ModelState.IsValid)
            {
                if (lab_Location.Address_1 == null)
                {
                    lab_Location.Address_1 = string.Empty;
                }
                if (lab_Location.City == null)
                {
                    lab_Location.City = string.Empty;
                }
                if (lab_Location.State == null)
                {
                    lab_Location.State = string.Empty;
                }
                if (lab_Location.Zip == null)
                {
                    lab_Location.Zip = string.Empty;
                }
                if (lab_Location.Country == null)
                {
                    lab_Location.Country = string.Empty;
                }
                SqlCommand sqlCommand = new SqlCommand("Proc_Create_Lab", this.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Location_Name", (object)lab_Location.Lab_Name);
                sqlCommand.Parameters.AddWithValue("@Address_1", (object)lab_Location.Address_1);
                sqlCommand.Parameters.AddWithValue("@Location_Id", (object)lab_Location.Id1);
                if (lab_Location.Address_2 == null)
                {
                    sqlCommand.Parameters.AddWithValue("@Address_2", string.Empty);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)lab_Location.Address_2);
                }
                sqlCommand.Parameters.AddWithValue("@City", (object)lab_Location.City);
                sqlCommand.Parameters.AddWithValue("@State", (object)lab_Location.State);
                sqlCommand.Parameters.AddWithValue("@Zip", (object)lab_Location.Zip);
                sqlCommand.Parameters.AddWithValue("@Country", (object)lab_Location.Country);
         
                if (lab_Location.Notes == null)
                {
                    sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)lab_Location.Notes);
                }
                //sqlCommand.Parameters.AddWithValue("@companyid", Session["Account_id"]);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                if (lab_Location.hide_id == "2")
                    return (ActionResult)this.RedirectToAction("LabList");
                return (ActionResult)this.RedirectToAction("Locations", (object)new
                {
                    id = lab_Location.Lab_Name
                });
            }
            else
            {
                if (lab_Location.hide_id == "2")
                {
                    ViewBag.Status = string.Empty;

                }
                else
                {
                    ViewBag.Status = lab_Location.Lab_Name;

                }
                ClientController clientController = new ClientController();
                if (lab_Location.State == null)
                    lab_Location.Cities = clientController.GetAllCities(string.Empty);
                else
                    lab_Location.Cities = clientController.GetAllCities(lab_Location.State);
                return (ActionResult)this.View(lab_Location);
            }

        }

        public ActionResult Delete(string id)
        {
            if (id != null)
            {
                SqlCommand sqlCommand = new SqlCommand("update tbl_Clinic_Details set isdeleted=1 where Location_Name=@Location_Name", this.conn);
                sqlCommand.Parameters.AddWithValue("@Location_Name", (object)id);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            return (ActionResult)this.RedirectToAction("LabList");
        }

        public ActionResult deletelabloc(string id)
        {
            SqlCommand sqlCommand1 = new SqlCommand("select Location_name from tbl_clinic_details where id=@id", this.conn);
            sqlCommand1.Parameters.AddWithValue("@id", (object)id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand1);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            SqlCommand sqlCommand = new SqlCommand("proc_delete_lab_loc", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", (object)id);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            string id1 = dataTable.Rows[0][0].ToString();
            return RedirectToAction("Locations",new {id=id1 });
        }
        [BreadCrumb(Label = "Update Lab Location")]
        public ActionResult UpdateLabLocation(string id)
        {
            lab_location labLocation = new lab_location();
            if (!string.IsNullOrEmpty(id))
            {
                SqlCommand selectCommand = new SqlCommand("Get_Location_for_lab_by_id", this.conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@Location_Id", (object)id);
                DataTable dataTable = new DataTable();
                new SqlDataAdapter(selectCommand).Fill(dataTable);
                labLocation.Id1 = id.ToString();
                labLocation.hide_id = id;
                if (dataTable.Rows.Count > 0)
                {
                    int index = 0;
                    labLocation.Lab_Name = string.IsNullOrEmpty(dataTable.Rows[index]["Location_Name"].ToString()) ? string.Empty : dataTable.Rows[index]["Location_Name"].ToString();
                    labLocation.Address_1 = string.IsNullOrEmpty(dataTable.Rows[index]["Address_1"].ToString()) ? string.Empty : dataTable.Rows[index]["Address_1"].ToString();
                    labLocation.Address_2 = string.IsNullOrEmpty(dataTable.Rows[index]["Address_2"].ToString()) ? string.Empty : dataTable.Rows[index]["Address_2"].ToString();
                    labLocation.City = string.IsNullOrEmpty(dataTable.Rows[index]["City"].ToString()) ? string.Empty : dataTable.Rows[index]["City"].ToString();
                    if (!string.IsNullOrEmpty(dataTable.Rows[index]["State"].ToString()))
                    {
                        labLocation.State = dataTable.Rows[index]["State"].ToString();
                        ClientController clientController = new ClientController();
                        labLocation.Cities = clientController.GetAllCities(dataTable.Rows[0]["State"].ToString());
                    }
                    else
                    {
                        labLocation.State = string.Empty;
                        ClientController clientController = new ClientController();
                        labLocation.Cities = clientController.GetAllCities(string.Empty);
                    }
                    labLocation.Zip = string.IsNullOrEmpty(dataTable.Rows[index]["Zip"].ToString()) ? string.Empty : dataTable.Rows[index]["Zip"].ToString();
                    labLocation.Country = string.IsNullOrEmpty(dataTable.Rows[index]["Country"].ToString()) ? string.Empty : dataTable.Rows[index]["Country"].ToString();
                    labLocation.Notes = string.IsNullOrEmpty(dataTable.Rows[index]["Notes"].ToString()) ? string.Empty : dataTable.Rows[index]["Notes"].ToString();

                }
            }
            return (ActionResult)this.View((object)labLocation);
        }

        [HttpPost]
        public ActionResult UpdateLabLocation(lab_location lab_Location)
        {
            if (ModelState.IsValid)
            {
                if (lab_Location.Address_1 == null)
                {
                    lab_Location.Address_1 = string.Empty;
                }
                if (lab_Location.City == null)
                {
                    lab_Location.City = string.Empty;
                }
                if (lab_Location.State == null)
                {
                    lab_Location.State = string.Empty;
                }
                if (lab_Location.Zip == null)
                {
                    lab_Location.Zip = string.Empty;
                }
                if (lab_Location.Country == null)
                {
                    lab_Location.Country = string.Empty;
                }
                SqlCommand sqlCommand = new SqlCommand("Proc_Update_Lab", this.conn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Location_Id", (object)lab_Location.Id1);
                sqlCommand.Parameters.AddWithValue("@Address_1", (object)lab_Location.Address_1);
                if (!string.IsNullOrEmpty(lab_Location.Address_2))
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)lab_Location.Address_2);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_2", string.Empty);
                sqlCommand.Parameters.AddWithValue("@City", (object)lab_Location.City);
                sqlCommand.Parameters.AddWithValue("@State", (object)lab_Location.State);
                sqlCommand.Parameters.AddWithValue("@Zip", (object)lab_Location.Zip);
                if (!string.IsNullOrEmpty(lab_Location.Country))
                    sqlCommand.Parameters.AddWithValue("@Country", (object)lab_Location.Country);
                else
                    sqlCommand.Parameters.AddWithValue("@Country", string.Empty);
                if (!string.IsNullOrEmpty(lab_Location.Notes))
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)lab_Location.Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                return (ActionResult)this.RedirectToAction("Locations", (object)new
                {
                    id = lab_Location.Lab_Name+"/"
                });
            }
            ClientController clientController = new ClientController();
            if(string.IsNullOrEmpty(lab_Location.State))
            lab_Location.Cities = clientController.GetAllCities(string.Empty);
            else
                lab_Location.Cities = clientController.GetAllCities(lab_Location.State);
            return (ActionResult)this.View(lab_Location);
        }
        [BreadCrumb(Label = "Add Lab")]
        public ActionResult NewLab()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewLab(lab_loc_name lab_Loc_Name)
        {
            if (!ModelState.IsValid)
                return View(lab_Loc_Name);
            string values = string.Empty;
            foreach(string value in lab_Loc_Name.Lab_Type)
            {
                if(string.Empty==values)
                {
                    values = value;
                }
                else
                {
                    values = values + "," + value;
                }
            }
            SqlCommand sqlCommand = new SqlCommand("proc_new_lab", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Locationname", (object)lab_Loc_Name.Lab_Name);
            sqlCommand.Parameters.AddWithValue("@location_id", lab_Loc_Name.Lab_Name + "Def_loc");
            sqlCommand.Parameters.AddWithValue("@Type", (object)lab_Loc_Name.Type);
            sqlCommand.Parameters.AddWithValue("@Lab_Type", values);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            foreach (string item in lab_Loc_Name.Lab_Type)
            {
                SqlCommand sqlCommand1 = new SqlCommand("AssignProductServicetoLab", this.conn);
                sqlCommand1.CommandType = CommandType.StoredProcedure;
                sqlCommand1.Parameters.AddWithValue("@Labname", (object)lab_Loc_Name.Lab_Name);
                sqlCommand1.Parameters.AddWithValue("@id", item);
                conn.Open();
                sqlCommand1.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Lablist");
        }
        [BreadCrumb(Label = "Update Lab")]
        public ActionResult UpdateLab(string id)
        {
            lab_loc_name lab = new lab_loc_name();
            if (!string.IsNullOrEmpty(id))
            {
                SqlCommand selectCommand = new SqlCommand("proc_edit_lab", this.conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@Location_Name", (object)id);
                DataTable dataTable = new DataTable();
                new SqlDataAdapter(selectCommand).Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    int index = 0;
                    lab.Lab_Name = string.IsNullOrEmpty(dataTable.Rows[index]["Location_Name"].ToString()) ? string.Empty : dataTable.Rows[index]["Location_Name"].ToString();
                    //lab.Lab_Name = string.IsNullOrEmpty(dataTable.Rows[index]["location_id"].ToString()) ? string.Empty : dataTable.Rows[index]["location_id"].ToString();
                    lab.Lab_Type = string.IsNullOrEmpty(dataTable.Rows[index]["Lab_Type"].ToString()) ? string.Empty.Split(',') : dataTable.Rows[index]["Lab_Type"].ToString().Split(',');
                }
            }
            return (ActionResult)this.View((object)lab);
        }

        [HttpPost]
        public ActionResult UpdateLab(lab_loc_name lab)
        {
            if (!ModelState.IsValid)
                return View(lab);

            SqlCommand sqlCommand = new SqlCommand("proc_update_labs", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Location_name", (object)lab.Lab_Name);
            sqlCommand.Parameters.AddWithValue("@location_id", lab.Lab_Name + "Def_loc");
            sqlCommand.Parameters.AddWithValue("@Lab_Type", lab.Lab_Type);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Lablist");
        }
        [BreadCrumb(Label = "Update Lab Contact")]
        public ActionResult ContactDetails(int id)
        {
            Lab_contact labContact = new Lab_contact();
            if (id != 0)
            {
                SqlCommand selectCommand = new SqlCommand("proc_edit_Lab_contact", this.conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@lab_contact_id", (object)id);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                labContact.contact_id = id;
                if (dataTable.Rows.Count > 0)
                {
                    labContact.Title = string.IsNullOrEmpty(dataTable.Rows[0]["Title"].ToString()) ? string.Empty : dataTable.Rows[0]["Title"].ToString();
                    labContact.Phone1 = string.IsNullOrEmpty(dataTable.Rows[0]["Phone1"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone1"].ToString();
                    labContact.Email1 = string.IsNullOrEmpty(dataTable.Rows[0]["Email1"].ToString()) ? string.Empty : dataTable.Rows[0]["Email1"].ToString();
                    labContact.Role = string.IsNullOrEmpty(dataTable.Rows[0]["Role"].ToString()) ? string.Empty : dataTable.Rows[0]["Role"].ToString();
                    labContact.Notes = string.IsNullOrEmpty(dataTable.Rows[0]["Notes"].ToString()) ? string.Empty : dataTable.Rows[0]["Notes"].ToString();

                    labContact.email = string.IsNullOrEmpty(dataTable.Rows[0]["Email"].ToString()) ? string.Empty : dataTable.Rows[0]["Email"].ToString();
                    labContact.firstname = string.IsNullOrEmpty(dataTable.Rows[0]["firstname"].ToString()) ? string.Empty : dataTable.Rows[0]["firstname"].ToString();
                    labContact.Lastname = string.IsNullOrEmpty(dataTable.Rows[0]["lastname"].ToString()) ? string.Empty : dataTable.Rows[0]["lastname"].ToString();
                    labContact.officephone = string.IsNullOrEmpty(dataTable.Rows[0]["officephone"].ToString()) ? string.Empty : dataTable.Rows[0]["officephone"].ToString();
                    labContact.cell = string.IsNullOrEmpty(dataTable.Rows[0]["cell"].ToString()) ? string.Empty : dataTable.Rows[0]["cell"].ToString();
                    labContact.Third_Phone = string.IsNullOrEmpty(dataTable.Rows[0]["Third_Phone"].ToString()) ? string.Empty : dataTable.Rows[0]["Third_Phone"].ToString();
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["lab_Location_Id"].ToString()))
                        labContact.location_id = dataTable.Rows[0]["lab_Location_Id"].ToString();
                }
            }
            return (ActionResult)this.View((object)labContact);
        }

        [HttpPost]
        public ActionResult ContactDetails(Lab_contact _Contact)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View();
            SqlCommand sqlCommand = new SqlCommand("proc_Update_Lab_contact", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@lab_contact_id", (object)_Contact.contact_id);

            if (!string.IsNullOrEmpty(_Contact.email))
            {
                sqlCommand.Parameters.AddWithValue("@email", (object)_Contact.email);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.firstname))
            {
                sqlCommand.Parameters.AddWithValue("@firstname", (object)_Contact.firstname);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@firstname", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Lastname))
            {
                sqlCommand.Parameters.AddWithValue("@lastname", (object)_Contact.Lastname);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@lastname", string.Empty);
            }


            if (_Contact.officephone == null)
                sqlCommand.Parameters.AddWithValue("@officephone", (object)string.Empty);
            else
                sqlCommand.Parameters.AddWithValue("@officephone", (object)_Contact.officephone);
            if (_Contact.cell != null)
                sqlCommand.Parameters.AddWithValue("@cell", (object)_Contact.cell);
            else
                sqlCommand.Parameters.AddWithValue("@cell", string.Empty);
            if (!string.IsNullOrEmpty(_Contact.Email1))
            {
                sqlCommand.Parameters.AddWithValue("@Email1", _Contact.Email1);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Email1", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Phone1))
            {
                sqlCommand.Parameters.AddWithValue("@Phone1", _Contact.Phone1);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Phone1", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Role))
            {
                sqlCommand.Parameters.AddWithValue("@Role", _Contact.Role);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Role", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Title))
            {
                sqlCommand.Parameters.AddWithValue("@Title", _Contact.Title);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Title", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Notes))
            {
                sqlCommand.Parameters.AddWithValue("@Notes", _Contact.Notes);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Third_Phone))
            {
                sqlCommand.Parameters.AddWithValue("@Third_Phone", _Contact.Third_Phone);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Third_Phone", string.Empty);
            }
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.RedirectToAction("ContactsList", (object)new
            {
                id = _Contact.location_id
            });
        }
        [BreadCrumb(Label = "Lab Products")]
        public ActionResult LabServices(string id)
        {
            TempData["LabNameforservices"] = id.Trim();
            List<Services> servicesList = new List<Services>();
            ViewBag.LabName = id.Trim();
            id = id.Trim();
            if (string.IsNullOrEmpty(id))
                return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
            SqlCommand sqlCommand = new SqlCommand("select id,service_grp_name from lab_service_grp where LabName=@LabName", this.conn);
            sqlCommand.Parameters.AddWithValue("@LabName", (object)id);
            conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                    servicesList.Add(new Services()
                    {
                        Service_id = Convert.ToInt32(sqlDataReader[nameof(id)]),
                        Service_Name = Convert.ToString(sqlDataReader["service_grp_name"])
                    });
            }
            return (ActionResult)this.View((object)servicesList);
        }
        public  List<SelectListItem> LabTypelist(string lab)
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("select Id,Service_Type from Product_Service where Service_Type not in (select service_grp_name from lab_service_grp where LabName=@Labname)", con);
            selectCommand.Parameters.AddWithValue("@Labname", lab);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<SelectListItem> ServiceList = new List<SelectListItem>();
            for (int index = 0; index < dataTable.Rows.Count; index++)
                ServiceList.Add(new SelectListItem
                {
                    Value = dataTable.Rows[index]["Id"].ToString().Trim(),
                    Text = string.IsNullOrEmpty(dataTable.Rows[index]["Service_Type"].ToString().Trim()) ? string.Empty : dataTable.Rows[index]["Service_Type"].ToString().Trim()
                });
            return ServiceList;
        }
        [BreadCrumb(Label = "Add Product Group")]
        public ActionResult CreateServiceGroup(string id)
        {
            
            return (ActionResult)this.View((object)new lab_loc_name()
            {
                Lab_Type=string.Empty.Split(','),
                Lab_Name = id.ToString(),
                Type = "2",
                RemainProducts = LabTypelist(id.Trim())
            });
        }

        [HttpPost]
        public ActionResult CreateServiceGroup(lab_loc_name lab_Loc_Name)
        {
            if (!ModelState.IsValid)
            {
                lab_Loc_Name.RemainProducts = LabTypelist(lab_Loc_Name.Lab_Name);
                return (ActionResult)this.View((object)lab_Loc_Name);
            }
            foreach (string item in lab_Loc_Name.Lab_Type)
            {
                SqlCommand sqlCommand1 = new SqlCommand("AssignProductServicetoLab", this.conn);
                sqlCommand1.CommandType = CommandType.StoredProcedure;
                sqlCommand1.Parameters.AddWithValue("@Labname", (object)lab_Loc_Name.Lab_Name);
                sqlCommand1.Parameters.AddWithValue("@id", item);
                conn.Open();
                sqlCommand1.ExecuteNonQuery();
                conn.Close();
            }
            return (ActionResult)this.RedirectToAction("LabServices", (object)new
            {
                id = lab_Loc_Name.Lab_Name
            });
        }
        [BreadCrumb(Label = "Update Lab Product Group")]
        public ActionResult UpdateServiceGroup(int id)
        {
            TempData["Lab_product_service"] = getservicenamebyid(id);
            List<SubServices> subServicesList = new List<SubServices>();
            Services services = new Services();
            services.Service_id = id;
            SqlCommand selectCommand = new SqlCommand("select service_grp_name,Labname from lab_service_grp where id=@id", this.conn);
            selectCommand.Parameters.AddWithValue("@id", (object)id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                services.Service_Name = string.IsNullOrEmpty(dataTable.Rows[0]["service_grp_name"].ToString()) ? string.Empty : dataTable.Rows[0]["service_grp_name"].ToString();
                services.Labid = string.IsNullOrEmpty(dataTable.Rows[0]["Labname"].ToString()) ? string.Empty : dataTable.Rows[0]["Labname"].ToString();
            }
            SqlCommand sqlCommand = new SqlCommand("select * from tbl_lab_sub_service where lab_service_grp_id=@id", this.conn);
            sqlCommand.Parameters.AddWithValue("@id", (object)id);
            conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                    subServicesList.Add(new SubServices()
                    {
                        lab_services_description = sqlDataReader["lab_services_description"].ToString(),
                        lab_service_id = Convert.ToInt32(sqlDataReader[nameof(id)].ToString()),
                        lab_services_ext_description = sqlDataReader["lab_services_ext_description"].ToString(),
                        service_charges = Convert.ToDecimal(sqlDataReader["service_charges"].ToString()),
                        Billing_Price = Convert.ToDecimal(sqlDataReader["Billing_Price"].ToString())
                    });
                sqlDataReader.Close();
                conn.Close();
            }
            else
            {
                sqlDataReader.Close();
                conn.Close();
            }
            ViewData["Data"] = subServicesList;
            return (ActionResult)this.View((object)services);
        }

        [HttpPost]
        public ActionResult UpdateServiceGroup(Services services)
        {
            if (!ModelState.IsValid)
            {
                List<SubServices> subServicesList = new List<SubServices>();
                
                SqlCommand sqlCommand = new SqlCommand("select * from tbl_lab_sub_service where lab_service_grp_id=@id", this.conn);
                sqlCommand.Parameters.AddWithValue("@id", (object)services.Service_id);
                conn.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                        subServicesList.Add(new SubServices()
                        {
                            lab_services_description = sqlDataReader["lab_services_description"].ToString(),
                            lab_service_id = Convert.ToInt32(sqlDataReader["id"].ToString()),
                            lab_services_ext_description = sqlDataReader["lab_services_ext_description"].ToString(),
                            service_charges = Convert.ToDecimal(sqlDataReader["service_charges"].ToString()),
                            client_billing_charges = Convert.ToDecimal(sqlDataReader["client_billing_charges"].ToString())
                        });
                    sqlDataReader.Close();
                    conn.Close();
                }
                else
                {
                    sqlDataReader.Close();
                    conn.Close();
                }
                ViewData["Data"] = subServicesList;
                return (ActionResult)this.View((object)services);
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("update lab_service_grp set service_grp_name=@service_grp_name where id=@id", this.conn);
                sqlCommand.Parameters.AddWithValue("@id", (object)services.Service_id);
                sqlCommand.Parameters.AddWithValue("@service_grp_name", (object)services.Service_Name);
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                return (ActionResult)this.RedirectToAction("LabServices", (object)new
                {
                    id = services.Labid
                });
            }
        }

        public ActionResult DeleteServiceGroup(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("delete from lab_service_grp where id=@service_grp_id", this.conn);
            sqlCommand.Parameters.AddWithValue("@service_grp_id", (object)id);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [BreadCrumb(Label = "Create Sub-Panel")]
        public ActionResult CreateSubService(int id)
        {
            List<SelectListItem> listItems = Labsubservicelist(id.ToString());
            return (ActionResult)this.View((object)new SubServices()
            {
                PS_Id = id.ToString(),
                RemainProducts=listItems,
                PS_Type=string.Empty.Split(',')
                
            });
        }

        [HttpPost]
        public ActionResult CreateSubService(SubServices subServices)
        {
            if(subServices.PS_Type!=null)
            {
                foreach (string id in subServices.PS_Type)
                {
                    SqlCommand sqlCommand = new SqlCommand("proc_assign_lab_sub_service", this.conn);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@sp_id", subServices.PS_Id);
                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }
           
            
            
            return (ActionResult)this.RedirectToAction("UpdateServiceGroup", (object)new
            {
                id = subServices.PS_Id
            });
        }
        [BreadCrumb(Label = "Update Sub-Panel")]
        public ActionResult UpdateSubService(int id)
        {
            SubServices subServices = new SubServices();
            subServices.lab_service_id = id;
            SqlCommand sqlCommand = new SqlCommand("tbl_lab_sub_service_get", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", (object)id);
            conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(sqlDataReader["lab_services_description"].ToString()))
                    {
                        subServices.lab_services_description = sqlDataReader["lab_services_description"].ToString();
                    }
                    else
                    {
                        subServices.lab_services_description = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["lab_services_ext_description"].ToString()))
                    {
                        subServices.lab_services_ext_description = sqlDataReader["lab_services_ext_description"].ToString();
                    }
                    else
                    {
                        subServices.lab_services_ext_description = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["client_billing_charges"].ToString()))
                    {
                        subServices.client_billing_charges = Convert.ToDecimal(sqlDataReader["client_billing_charges"].ToString());
                    }
                    else
                    {
                        subServices.client_billing_charges = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["service_charges"].ToString()))
                    {
                        subServices.service_charges = Convert.ToDecimal(sqlDataReader["service_charges"].ToString());
                    }
                    else
                    {
                        subServices.service_charges = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["lab_service_grp_id"].ToString()))
                    {
                        subServices.lab_group_id = Convert.ToInt32(sqlDataReader["lab_service_grp_id"].ToString());
                    }
                    else
                    {
                        subServices.lab_group_id = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Specimen_Type"].ToString()))
                    {
                        subServices.Specimen_Type = sqlDataReader["Specimen_Type"].ToString();
                    }
                    else
                    {
                        subServices.Specimen_Type = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Drugs"].ToString()))
                    {
                        subServices.Drugs = sqlDataReader["Drugs"].ToString();
                    }
                    else
                    {
                        subServices.Drugs = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Medical_Review_Office_Cost"].ToString()))
                    {
                        subServices.Medical_Review_Office_Cost = Convert.ToDecimal(sqlDataReader["Medical_Review_Office_Cost"].ToString());
                    }
                    else
                    {
                        subServices.Medical_Review_Office_Cost = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Vendor_management"].ToString()))
                    {
                        subServices.Vendor_management = Convert.ToDecimal(sqlDataReader["Vendor_management"].ToString());
                    }
                    else
                    {
                        subServices.Vendor_management = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Document_Upload"].ToString()))
                    {
                        subServices.Document_Upload = Convert.ToDecimal(sqlDataReader["Document_Upload"].ToString());
                    }
                    else
                    {
                        subServices.Document_Upload = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Billing_Price"].ToString()))
                    {
                        subServices.Billing_Price = Convert.ToDecimal(sqlDataReader["Billing_Price"].ToString());
                    }
                    else
                    {
                        subServices.Billing_Price = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Collection_Cost"].ToString()))
                    {
                        subServices.Collection_Cost = Convert.ToDecimal(sqlDataReader["Collection_Cost"].ToString());
                    }
                    else
                    {
                        subServices.Collection_Cost = 0;
                    }
                }
                sqlDataReader.Close();
                conn.Close();
            }
            else
            {
                sqlDataReader.Close();
                conn.Close();
            }
            return (ActionResult)this.View((object)subServices);
        }

        [HttpPost]
        public ActionResult UpdateSubService(SubServices subServices)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View(subServices);
            SqlCommand sqlCommand = new SqlCommand("tbl_lab_sub_service_Update", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", (object)subServices.lab_service_id);
            sqlCommand.Parameters.AddWithValue("@lab_services_description", (object)subServices.lab_services_description);
            sqlCommand.Parameters.AddWithValue("@lab_services_ext_description", (object)subServices.lab_services_ext_description);
            sqlCommand.Parameters.AddWithValue("@service_charges", (object)subServices.service_charges);
            sqlCommand.Parameters.AddWithValue("@client_billing_charges", (object)subServices.client_billing_charges);
            sqlCommand.Parameters.AddWithValue("@Specimen_Type", (object)subServices.Specimen_Type);
            sqlCommand.Parameters.AddWithValue("@Drugs", (object)subServices.Drugs);
            sqlCommand.Parameters.AddWithValue("@Medical_Review_Office_Cost", (object)subServices.Medical_Review_Office_Cost);
            sqlCommand.Parameters.AddWithValue("@Vendor_management", (object)subServices.Vendor_management);
            sqlCommand.Parameters.AddWithValue("@Document_Upload", (object)subServices.Document_Upload);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Billing_Price).ToString()))
                sqlCommand.Parameters.AddWithValue("@Billing_Price", (object)subServices.Billing_Price);
            else
                sqlCommand.Parameters.AddWithValue("@Billing_Price", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Collection_Cost).ToString()))
                sqlCommand.Parameters.AddWithValue("@Collection_Cost", (object)subServices.Collection_Cost);
            else
                sqlCommand.Parameters.AddWithValue("@Collection_Cost", string.Empty);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            SqlCommand selectCommand = new SqlCommand("select lab_service_grp_id from tbl_lab_sub_service where id=@id", this.conn);
            selectCommand.Parameters.AddWithValue("@id", (object)subServices.lab_service_id);
            DataTable dataTable = new DataTable();
            new SqlDataAdapter(selectCommand).Fill(dataTable);
            if (dataTable.Rows.Count > 0)
                subServices.lab_group_id = Convert.ToInt32(dataTable.Rows[0]["lab_service_grp_id"]);
            return (ActionResult)this.RedirectToAction("UpdateServiceGroup", (object)new
            {
                id = subServices.lab_group_id
            });
        }

        public ActionResult DeleteSubService(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("tbl_lab_sub_service_delete", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id", (object)id);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [BreadCrumb(Label = "Lab Contacts List")]
        public ActionResult ContactsList(string id)
        {
            ViewBag.locationid = id;
            TempData["Lab_location"] = getnamebyid(Convert.ToInt32(id));
            List<Lab_contact> labContactList = new List<Lab_contact>();
            SqlCommand sqlCommand = new SqlCommand("select * from Tbl_Lab_location_Contact where lab_location_id=@id", this.conn);
            sqlCommand.Parameters.AddWithValue("@id", (object)id);
            conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                    labContactList.Add(new Lab_contact()
                    {
                        firstname = sqlDataReader["firstname"].ToString(),
                        Lastname = sqlDataReader["Lastname"].ToString(),
                        officephone = sqlDataReader["officephone"].ToString(),
                        cell = sqlDataReader["cell"].ToString(),
                        email = sqlDataReader["email"].ToString(),
                        contact_id = Convert.ToInt32(sqlDataReader["lab_contact_id"].ToString())
                    });
                sqlDataReader.Close();
                conn.Close();
            }
            else
            {
                sqlDataReader.Close();
                conn.Close();
            }
            SqlCommand sqlCommand1 = new SqlCommand("select Location_Name from tbl_Clinic_Details where id=@id", this.conn);
            sqlCommand1.Parameters.AddWithValue("@id", id.ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand1);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            ViewBag.LabName = dataTable.Rows[0]["Location_Name"].ToString();
            // ISSUE: reference to a compiler-generated field

            return (ActionResult)this.View((object)labContactList);
        }
        [BreadCrumb(Label = "Add Contact")]
        public ActionResult NewContact(string id)
        {
            return (ActionResult)this.View((object)new Lab_contact()
            {
                location_id = id
            });
        }

        [HttpPost]
        public ActionResult NewContact(Lab_contact _Contact)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)_Contact);
            SqlCommand sqlCommand = new SqlCommand("proc_insert_Lab_contact", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(_Contact.location_id))
            {
                sqlCommand.Parameters.AddWithValue("@lab_location_Id", (object)_Contact.location_id);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@lab_location_Id", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Email1))
            {
                sqlCommand.Parameters.AddWithValue("@Email1", _Contact.Email1);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Email1", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Phone1))
            {
                sqlCommand.Parameters.AddWithValue("@Phone1", _Contact.Phone1);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Phone1", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Role))
            {
                sqlCommand.Parameters.AddWithValue("@Role", _Contact.Role);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Role", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Title))
            {
                sqlCommand.Parameters.AddWithValue("@Title", _Contact.Title);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Title", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Notes))
            {
                sqlCommand.Parameters.AddWithValue("@Notes", _Contact.Notes);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            }

            if (!string.IsNullOrEmpty(_Contact.firstname))
            {
                sqlCommand.Parameters.AddWithValue("@firstname", (object)_Contact.firstname);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@firstname", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.Lastname))
            {
                sqlCommand.Parameters.AddWithValue("@lastname", (object)_Contact.Lastname);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@Lastname", string.Empty);
            }
            if (!string.IsNullOrEmpty(_Contact.email))
            {
                sqlCommand.Parameters.AddWithValue("@email", (object)_Contact.email);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            }

            if (_Contact.officephone == null)
                sqlCommand.Parameters.AddWithValue("@officephone", (object)string.Empty);
            else
                sqlCommand.Parameters.AddWithValue("@officephone", (object)_Contact.officephone);
            if (_Contact.cell != null)
                sqlCommand.Parameters.AddWithValue("@cell", (object)_Contact.cell);
            else
                sqlCommand.Parameters.AddWithValue("@cell", string.Empty);
            if (_Contact.Third_Phone != null)
                sqlCommand.Parameters.AddWithValue("@Third_Phone", (object)_Contact.Third_Phone);
            else
                sqlCommand.Parameters.AddWithValue("@Third_Phone", string.Empty);

            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.RedirectToAction("ContactsList", (object)new
            {
                id = _Contact.location_id+"/"
            });
        }

        public ActionResult DeleteContact(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("proc_delete_Lab_contact", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@lab_contact_id", (object)id);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public List<SelectListItem> Labsubservicelist(string id)
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("proc_assign_sub_service", con);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<SelectListItem> ServiceList = new List<SelectListItem>();
            if (dataTable.Rows.Count > 0)
            {
                for (int index = 0; index < dataTable.Rows.Count; index++)
                    ServiceList.Add(new SelectListItem
                    {
                        Value = dataTable.Rows[index]["Id"].ToString().Trim(),
                        Text = string.IsNullOrEmpty(dataTable.Rows[index]["lab_services_description"].ToString().Trim()) ? string.Empty : dataTable.Rows[index]["lab_services_description"].ToString().Trim()
                    });
            }
            return ServiceList;
        }

        public string getnamebyid(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.GetLocation_idById (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;

        }
        public string getservicenamebyid(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.Getlab_service_grp_nameById (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;

        }
    }
}
