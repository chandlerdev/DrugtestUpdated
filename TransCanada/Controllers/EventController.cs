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
    [Authorize]
    [BreadCrumb]
    public class EventController : Controller
    {
        string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
        // GET: Event
        [BreadCrumb(Clear = true, Label = "Events")]
        public ActionResult EventList()
        {
            if (Session["Account_Id"] == null)
                return RedirectToAction("Account_List", "Home");
            else
                Session["Account_Id"] = Session["Account_Id"];
            List<Event_Model> Event_List = new List<Event_Model>();

            using (SqlConnection con = new SqlConnection(TransConnString))
            {
                string query = "Select * from Tbl_Event where Client_id=@Client_id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Client_id", Session["Account_Id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Event_Model Ev_Mo = new Event_Model();
                    Ev_Mo.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());
                    Ev_Mo.Event_name = dt.Rows[j]["Event_name"].ToString();
                    Ev_Mo.Event_Start_Date = Convert.ToDateTime(dt.Rows[j]["Event_Start_Date"].ToString());
                    Ev_Mo.Event_End_Date = Convert.ToDateTime(dt.Rows[j]["Event_End_Date"].ToString());
                    Ev_Mo.Client_Contact = dt.Rows[j]["Client_Contact"].ToString();
                    //Ev_Mo.Created_By = Convert.ToInt32(dt.Rows[j]["CreatedBy"].ToString());
                    //if (!string.IsNullOrEmpty(dt.Rows[j]["CreatedDate"].ToString()))
                    //{
                    //    Ev_Mo.Created_On = Convert.ToDateTime(dt.Rows[j]["CreatedDate"].ToString());
                    //}
                    //Ev_Mo.Update_By = Convert.ToInt32(dt.Rows[j]["UpdatedBy"].ToString());
                    //if (!string.IsNullOrEmpty(dt.Rows[j]["CreatedDate"].ToString()))
                    //{
                    //    Ev_Mo.Update_On = Convert.ToDateTime(dt.Rows[j]["UpdatedDate"].ToString());
                    //}


                    Event_List.Add(Ev_Mo);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return View(Event_List);
        }

        public ActionResult Multiselect()
        {
            return View();
        }
        [BreadCrumb(Label = "New Event")]
        public ActionResult NewEvent()
        {
            if (Session["Account_Id"] == null)
                return RedirectToAction("Account_List", "Home");
            Event_Model event_Model = new Event_Model();
            event_Model.Client_id = System.Web.HttpContext.Current.Session["Account_Id"].ToString();
            string query = "Select * from Tbl_temp_event where user_name=@user_name and Client_Name=@Client_Name";
            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@user_name", User.Identity.Name);
            cmd.Parameters.AddWithValue("@Client_Name", event_Model.Client_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //event_Model.Tpa_Client = string.Empty;
            //event_Model.Tpa_Client_location = string.Empty;
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["Event_Name"].ToString()))
                    event_Model.Event_name = dt.Rows[0]["Event_Name"].ToString();
                else
                    event_Model.Event_name = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Event_Start_Date"].ToString()))
                    event_Model.Event_Start_Date = Convert.ToDateTime(dt.Rows[0]["Event_Start_Date"].ToString());
                else
                    event_Model.Event_Start_Date = DateTime.Now;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Event_End_Date"].ToString()))
                    event_Model.Event_End_Date = Convert.ToDateTime(dt.Rows[0]["Event_End_Date"].ToString());
                else
                    event_Model.Event_End_Date = DateTime.Now;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Client_Location"].ToString()))
                    event_Model.Client_Location = dt.Rows[0]["Client_Location"].ToString();
                else
                    event_Model.Client_Location = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Client_Contact"].ToString()))
                    event_Model.Client_Contact = dt.Rows[0]["Client_Contact"].ToString();
                else
                    event_Model.Client_Contact = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Event_Start_Time"].ToString()))
                    event_Model.Event_Start_Time = Convert.ToDateTime(dt.Rows[0]["Event_Start_Time"].ToString());
                else
                    event_Model.Event_Start_Time = DateTime.Now;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Event_End_Time"].ToString()))
                    event_Model.Event_End_Time = Convert.ToDateTime(dt.Rows[0]["Event_End_Time"].ToString());
                else
                    event_Model.Event_End_Time = DateTime.Now;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Notes"].ToString()))
                    event_Model.Notes = dt.Rows[0]["Notes"].ToString();
                else
                    event_Model.Notes = string.Empty;
                //if (!string.IsNullOrEmpty(dt.Rows[0]["Service_Type"].ToString()))
                //    event_Model.Service_Prov_Type = dt.Rows[0]["Service_Type"].ToString();
                //else
                //    event_Model.Service_Prov_Type = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Service_Location"].ToString()))
                    event_Model.Service_Location = dt.Rows[0]["Service_Location"].ToString();
                else
                    event_Model.Service_Location = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Lab_Sp_Name"].ToString()))
                    event_Model.Service_Prov_Id = dt.Rows[0]["Lab_Sp_Name"].ToString();
                else
                    event_Model.Service_Prov_Id = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Lab_Sp_Location"].ToString()))
                    event_Model.Service_Prov_Location = dt.Rows[0]["Lab_Sp_Location"].ToString();
                else
                    event_Model.Service_Prov_Location = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Lab_Sp_Contact"].ToString()))
                    event_Model.Service_Prov_Contact = dt.Rows[0]["Lab_Sp_Contact"].ToString();
                else
                    event_Model.Service_Prov_Contact = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Event_Type"].ToString()))
                    event_Model.Event_type = dt.Rows[0]["Event_Type"].ToString();
                else
                    event_Model.Event_type = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Tpa"].ToString()))
                    event_Model.Tpa_Client = dt.Rows[0]["Tpa"].ToString();
                else
                    event_Model.Tpa_Client = string.Empty;

                if (!string.IsNullOrEmpty(dt.Rows[0]["Tpa_Location"].ToString()))
                    event_Model.Tpa_Client_location = dt.Rows[0]["Tpa_Location"].ToString();
                else
                    event_Model.Tpa_Client_location = string.Empty;

                if (!string.IsNullOrEmpty(dt.Rows[0]["Tpa_Contact"].ToString()))
                    event_Model.Tpa_Client_Contact = dt.Rows[0]["Tpa_Contact"].ToString();
                else
                    event_Model.Tpa_Client_Contact = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["ServiceProvider_Name"].ToString()))
                    event_Model.Lab_Name = dt.Rows[0]["ServiceProvider_Name"].ToString();
                else
                    event_Model.Lab_Name = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["ServiceProvider_Location"].ToString()))
                    event_Model.Lab_Locations = dt.Rows[0]["ServiceProvider_Location"].ToString();
                else
                    event_Model.Lab_Locations = string.Empty;
                if (!string.IsNullOrEmpty(dt.Rows[0]["ServiceProvider_Contact"].ToString()))
                    event_Model.Lab_Contacts = dt.Rows[0]["ServiceProvider_Contact"].ToString();
                else
                    event_Model.Lab_Contacts = string.Empty;
               
                SqlCommand command = new SqlCommand("GetLabdetails", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Client_Name", Session["Account_idPK"].ToString());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Labs"].ToString()))
                {
                    event_Model.Servicelist = GetServices("Lab", dataTable.Rows[0]["Labs"].ToString());
                    
                }
                else
                {
                    event_Model.Servicelist = GetServices("Lab", string.Empty);
                }
                event_Model.Servicegroup = dt.Rows[0]["Servicegroup"].ToString();
                ViewBag.CC = dt.Rows[0]["Servicegroup"].ToString();



                //foreach (var item in event_Model.Servicelist)
                //{
                //    for (int i = 0; i < event_Model.Servicegroups.Length; i++)
                //    {
                //        string val = event_Model.Servicegroups[i].ToString().Trim();
                //        if (item.Value.Trim() == val)
                //        {
                //            item.Selected = true;
                //        }
                //    }
                //}
                event_Model.subServiceslist = GetSubServices(event_Model.Service_Location, dt.Rows[0]["Servicegroup"].ToString());
                event_Model.subServices = dt.Rows[0]["subService"].ToString().Split(',');
                event_Model.List_price = GetSubServicePrice(dt.Rows[0]["subService"].ToString());
                ViewBag.cc1 = dt.Rows[0]["subService"].ToString();
                //foreach (var item in event_Model.subServiceslist)
                //{
                //    for (int i = 0; i < event_Model.subServices.Length; i++)
                //    {
                //        string val = event_Model.subServices[i].ToString().Trim();
                //        if (item.Value.Trim() == val)
                //        {
                //            item.Selected = true;
                //        }
                //    }
                //}
            }
            else
            {
                event_Model.Tpa_Client = string.Empty;
                event_Model.Tpa_Client_location = string.Empty;
                event_Model.Service_Location = string.Empty;
                //event_Model.Location_Type = string.Empty;
                event_Model.Client_Location = string.Empty;
                event_Model.Service_Prov_Id = string.Empty;
                event_Model.Lab_Name = string.Empty;
                event_Model.Lab_Locations = string.Empty;
                event_Model.Event_Start_Date = DateTime.Now;
                event_Model.Event_End_Date = DateTime.Now;
                SqlCommand command = new SqlCommand("GetLabdetails", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Client_Name", Session["Account_idPK"].ToString());
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count>0)
                {
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["Labs"].ToString()))
                    {
                        event_Model.Servicelist = GetServices("Lab", dataTable.Rows[0]["Labs"].ToString());

                    }
                    else
                    {
                        event_Model.Servicelist = GetServices("Lab", string.Empty);
                    }
                }
                else
                {
                    event_Model.Servicelist = GetServices("Lab", string.Empty);
                }
                event_Model.subServiceslist = GetSubServices(string.Empty, string.Empty);
                event_Model.List_price = GetSubServicePrice(string.Empty);
            }
            return View(event_Model);

        }

        [HttpPost]
        public ActionResult NewEvent(Event_Model event_Model)
        {
            if (event_Model.Servicegroups == null)
            {

                event_Model.Servicegroups = string.Empty.Split(',');
            }
            if (event_Model.subServices == null)
            {
                event_Model.subServices = string.Empty.Split(',');
            }
            SqlConnection con = new SqlConnection(TransConnString);
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/Event"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Event"));
            HttpPostedFileBase fileBase = null;
            string path = Server.MapPath("~/Event/");
            if (Request.Files.Count > 0)
            {
                fileBase = Request.Files[0];
            }
            if (fileBase != null)
            {

                SqlCommand command123 = new SqlCommand("proc_event_id", con);
                command123.CommandType = CommandType.StoredProcedure;
                con.Open();
                int event_id = (Int32)command123.ExecuteScalar();
                con.Close();
                string filetype = Path.GetExtension(fileBase.FileName);
                event_Model.Document_Upload = "Event" + event_id + filetype;
                fileBase.SaveAs(path + event_Model.Document_Upload);
            }
            else
            {
                event_Model.Document_Upload = string.Empty;
            }
            SqlCommand command2 = new SqlCommand("proc_new_event", con);
            command2.CommandType = CommandType.StoredProcedure;
            command2.Parameters.AddWithValue("@Billing_price", event_Model.Billing_price);
            if (!string.IsNullOrEmpty(event_Model.Event_name))
                command2.Parameters.AddWithValue("@Event_name", event_Model.Event_name);
            else
                command2.Parameters.AddWithValue("@Event_name", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_id))
                command2.Parameters.AddWithValue("@Client_id", event_Model.Client_id);
            else
                command2.Parameters.AddWithValue("@Client_id", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_Location))
                command2.Parameters.AddWithValue("@Client_Location", event_Model.Client_Location);
            else
                command2.Parameters.AddWithValue("@Client_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_Contact))
                command2.Parameters.AddWithValue("@Client_Contact", event_Model.Client_Contact);
            else
                command2.Parameters.AddWithValue("@Client_Contact", string.Empty);
            if (event_Model.Event_Start_Date != null)
                command2.Parameters.AddWithValue("@Event_Start_Date", event_Model.Event_Start_Date);
            else
                command2.Parameters.AddWithValue("@Event_Start_Date", DateTime.Now);
            if (event_Model.Event_End_Date != null)
                command2.Parameters.AddWithValue("@Event_End_Date", event_Model.Event_End_Date);
            else
                command2.Parameters.AddWithValue("@Event_End_Date", DateTime.Now);
            if (event_Model.Event_Start_Time != null)
                command2.Parameters.AddWithValue("@Event_Start_Time", event_Model.Event_Start_Time);
            else
                command2.Parameters.AddWithValue("@Event_Start_Time", DateTime.Now);
            if (event_Model.Event_End_Time != null)
                command2.Parameters.AddWithValue("@Event_End_Time", event_Model.Event_End_Time);
            else
                command2.Parameters.AddWithValue("@Event_End_Time", DateTime.Now);

            if (!string.IsNullOrEmpty(event_Model.Document_Upload))
                command2.Parameters.AddWithValue("@Document_Upload", event_Model.Document_Upload);
            else
                command2.Parameters.AddWithValue("@Document_Upload", string.Empty);

            if (event_Model.Notes != null)
                command2.Parameters.AddWithValue("@Notes", event_Model.Notes);
            else
                command2.Parameters.AddWithValue("@Notes", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Location))
                command2.Parameters.AddWithValue("@Service_Location", event_Model.Service_Location);
            else
                command2.Parameters.AddWithValue("@Service_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Location))
                command2.Parameters.AddWithValue("@Location_Type", event_Model.Service_Location);
            else
                command2.Parameters.AddWithValue("@Location_Type", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Id))
                command2.Parameters.AddWithValue("@Service_Prov_Id", event_Model.Service_Prov_Id);
            else
                command2.Parameters.AddWithValue("@Service_Prov_Id", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Location))
                command2.Parameters.AddWithValue("@Service_Prov_Location", event_Model.Service_Prov_Location);
            else
                command2.Parameters.AddWithValue("@Service_Prov_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Contact))
                command2.Parameters.AddWithValue("@Service_Prov_Contact", event_Model.Service_Prov_Contact);
            else
                command2.Parameters.AddWithValue("@Service_Prov_Contact", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Event_Status))
                command2.Parameters.AddWithValue("@Event_Status", event_Model.Event_Status);
            else
                command2.Parameters.AddWithValue("@Event_Status", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Event_type))
                command2.Parameters.AddWithValue("@Event_Type", event_Model.Event_type);
            else
                command2.Parameters.AddWithValue("@Event_Type", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Tpa_Client))
                command2.Parameters.AddWithValue("@Tpa", event_Model.Tpa_Client);
            else
                command2.Parameters.AddWithValue("@Tpa", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Tpa_Client_location))
                command2.Parameters.AddWithValue("@Tpa_Location", event_Model.Tpa_Client_location);
            else
                command2.Parameters.AddWithValue("@Tpa_Location", string.Empty);

            if (!string.IsNullOrEmpty(event_Model.Tpa_Client_Contact))
                command2.Parameters.AddWithValue("@Tpa_Contact", event_Model.Tpa_Client_Contact);
            else
                command2.Parameters.AddWithValue("@Tpa_Contact", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Name))
                command2.Parameters.AddWithValue("@ServiceProvider_Name", event_Model.Lab_Name);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Name", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Locations))
                command2.Parameters.AddWithValue("@ServiceProvider_Location", event_Model.Lab_Locations);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Contacts))
                command2.Parameters.AddWithValue("@ServiceProvider_Contact", event_Model.Lab_Contacts);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Contact", string.Empty);
            if(event_Model.Servicegroups==null)
                command2.Parameters.AddWithValue("@servicegroups", string.Empty);
            else
                command2.Parameters.AddWithValue("@servicegroups", event_Model.Servicegroup);
            if (event_Model.Servicegroups == null)
                command2.Parameters.AddWithValue("@subservices", string.Empty);
            else
                command2.Parameters.AddWithValue("@subservices", ConvertStringArrayToString(event_Model.subServices));
            command2.Parameters.AddWithValue("@Created_By", User.Identity.Name);
            con.Open();
            int event_id1 = (int)command2.ExecuteScalar();
            con.Close();
            
            
            //List<SelectListItem> selectedItems = event_Model.Servicelist.Where(p => event_Model.Servicegroups.Contains(p.Value)).ToList();
            //string servicegroups = string.Empty;
            //foreach (var selectedItem in selectedItems)
            //{
            //    SqlCommand command3 = new SqlCommand("insert into tbl_event_Servicegroup (Servicegrp_id,Servicegrp_text,event_id,isact) values (@service_id,@Servicegrp_text,@event_id,1)", con);
            //    command3.Parameters.AddWithValue("@service_id", selectedItem.Value);
            //    command3.Parameters.AddWithValue("@Servicegrp_text", selectedItem.Text);
            //    command3.Parameters.AddWithValue("@event_id", event_id1);
            //    con.Open();
            //    command3.ExecuteNonQuery();
            //    con.Close();
            //    if (servicegroups == string.Empty)
            //        servicegroups = servicegroups + selectedItem.Value;
            //    else
            //        servicegroups = servicegroups + "," + selectedItem.Value;
            //}
            //selectedItems = event_Model.Servicelist.Where(p => !event_Model.Servicegroups.Contains(p.Value)).ToList();
            //foreach (var selectedItem in selectedItems)
            //{
            //    SqlCommand command3 = new SqlCommand("insert into tbl_event_Servicegroup (Servicegrp_id,Servicegrp_text,event_id,isact) values (@service_id,@Servicegrp_text,@event_id,0)", con);
            //    command3.Parameters.AddWithValue("@service_id", selectedItem.Value);
            //    command3.Parameters.AddWithValue("@Servicegrp_text", selectedItem.Text);
            //    command3.Parameters.AddWithValue("@event_id", event_id1);
            //    con.Open();
            //    command3.ExecuteNonQuery();
            //    con.Close();
            //}
            //event_Model.subServiceslist = GetSubServices("Lab", servicegroups);
            //List<SelectListItem> selectedItems1 = event_Model.subServiceslist.Where(p => event_Model.subServices.Contains(p.Value)).ToList();
            //foreach (var selectedItem1 in selectedItems1)
            //{
            //    SqlCommand command3 = new SqlCommand("insert into tbl_event_SubService (SubService_id,Servicegrp_text,event_id,isact) values (@SubService_id,@Servicegrp_text,@event_id,1)", con);
            //    command3.Parameters.AddWithValue("@SubService_id", selectedItem1.Value);
            //    command3.Parameters.AddWithValue("@Servicegrp_text", selectedItem1.Text);
            //    command3.Parameters.AddWithValue("@event_id", event_id1);
            //    con.Open();
            //    command3.ExecuteNonQuery();
            //    con.Close();
            //}
            //selectedItems1 = event_Model.subServiceslist.Where(p => !event_Model.subServices.Contains(p.Value)).ToList();
            //foreach (var selectedItem1 in selectedItems1)
            //{
            //    SqlCommand command3 = new SqlCommand("insert into tbl_event_SubService (SubService_id,Servicegrp_text,event_id,isact) values (@SubService_id,@Servicegrp_text,@event_id,0)", con);
            //    command3.Parameters.AddWithValue("@SubService_id", selectedItem1.Value);
            //    command3.Parameters.AddWithValue("@Servicegrp_text", selectedItem1.Text);
            //    command3.Parameters.AddWithValue("@event_id", event_id1);
            //    con.Open();
            //    command3.ExecuteNonQuery();
            //    con.Close();
            //}
            SqlCommand command1 = new SqlCommand("delete from Tbl_temp_event where User_Name=@User_Name and Client_Name=@Client_Name", con);
            command1.Parameters.AddWithValue("@User_Name", User.Identity.Name);
            command1.Parameters.AddWithValue("@Client_Name", event_Model.Client_id);
            con.Open();
            command1.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("EventList");
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
        [BreadCrumb(Label = "Update Event")]
        public ActionResult UpdateEvent(int id)
        {
            Event_Model Ev_Mo = new Event_Model();
            string query = "Select * from Tbl_Event where id=@id";
            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    Ev_Mo.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Client_id"].ToString()))
                        Ev_Mo.Client_id = dt.Rows[j]["Client_id"].ToString();
                    else
                        Ev_Mo.Client_id = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Event_name"].ToString()))
                        Ev_Mo.Event_name = dt.Rows[j]["Event_name"].ToString();
                    else
                        Ev_Mo.Event_name = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Event_Start_Date"].ToString()))
                        Ev_Mo.Event_Start_Date = Convert.ToDateTime(dt.Rows[j]["Event_Start_Date"].ToString());
                    else
                        Ev_Mo.Event_Start_Date = DateTime.Now;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Event_End_Date"].ToString()))
                        Ev_Mo.Event_End_Date = Convert.ToDateTime(dt.Rows[j]["Event_End_Date"].ToString());
                    else
                        Ev_Mo.Event_End_Date = DateTime.Now;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Client_Location"].ToString()))
                        Ev_Mo.Client_Location = dt.Rows[j]["Client_Location"].ToString();
                    else
                        Ev_Mo.Client_Location = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Client_Contact"].ToString()))
                        Ev_Mo.Client_Contact = dt.Rows[j]["Client_Contact"].ToString();
                    else
                        Ev_Mo.Client_Contact = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Event_Start_Time"].ToString()))
                        Ev_Mo.Event_Start_Time = Convert.ToDateTime(dt.Rows[j]["Event_Start_Time"].ToString());
                    else
                        Ev_Mo.Event_Start_Time = DateTime.Now;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Event_End_Time"].ToString()))
                        Ev_Mo.Event_End_Time = Convert.ToDateTime(dt.Rows[j]["Event_End_Time"].ToString());
                    else
                        Ev_Mo.Event_End_Time = DateTime.Now;

                    if (!string.IsNullOrEmpty(dt.Rows[j]["Notes"].ToString()))
                        Ev_Mo.Notes = dt.Rows[j]["Notes"].ToString();
                    else
                        Ev_Mo.Notes = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Document_Upload"].ToString()))
                        Ev_Mo.Document_Upload = dt.Rows[j]["Document_Upload"].ToString();
                    else
                        Ev_Mo.Document_Upload = string.Empty;
                    //if (!string.IsNullOrEmpty(dt.Rows[j]["Location_Type"].ToString()))
                    //    Ev_Mo.Service_Prov_Type = dt.Rows[j]["Location_Type"].ToString();
                    //else
                    //    Ev_Mo.Service_Prov_Type = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Service_Location"].ToString()))
                        Ev_Mo.Service_Location = dt.Rows[j]["Service_Location"].ToString();
                    else
                        Ev_Mo.Service_Location = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Service_Prov_Id"].ToString()))
                        Ev_Mo.Service_Prov_Id = dt.Rows[j]["Service_Prov_Id"].ToString();
                    else
                        Ev_Mo.Service_Prov_Id = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Service_Prov_Location"].ToString()))
                        Ev_Mo.Service_Prov_Location = dt.Rows[j]["Service_Prov_Location"].ToString();
                    else
                        Ev_Mo.Service_Prov_Location = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Service_Prov_Contact"].ToString()))
                        Ev_Mo.Service_Prov_Contact = dt.Rows[j]["Service_Prov_Contact"].ToString();
                    else
                        Ev_Mo.Service_Prov_Contact = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Event_Status"].ToString()))
                        Ev_Mo.Event_Status = dt.Rows[j]["Event_Status"].ToString();
                    else
                        Ev_Mo.Event_Status = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Event_Type"].ToString()))
                        Ev_Mo.Event_type = dt.Rows[0]["Event_Type"].ToString();
                    else
                        Ev_Mo.Event_type = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Tpa"].ToString()))
                        Ev_Mo.Tpa_Client = dt.Rows[0]["Tpa"].ToString();
                    else
                        Ev_Mo.Tpa_Client = string.Empty;

                    if (!string.IsNullOrEmpty(dt.Rows[0]["Tpa_Location"].ToString()))
                        Ev_Mo.Tpa_Client_location = dt.Rows[0]["Tpa_Location"].ToString();
                    else
                        Ev_Mo.Tpa_Client_location = string.Empty;

                    if (!string.IsNullOrEmpty(dt.Rows[0]["Tpa_Contact"].ToString()))
                        Ev_Mo.Tpa_Client_Contact = dt.Rows[0]["Tpa_Contact"].ToString();
                    else
                        Ev_Mo.Tpa_Client_Contact = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ServiceProvider_Name"].ToString()))
                        Ev_Mo.Lab_Name = dt.Rows[0]["ServiceProvider_Name"].ToString();
                    else
                        Ev_Mo.Lab_Name = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ServiceProvider_Location"].ToString()))
                        Ev_Mo.Lab_Locations = dt.Rows[0]["ServiceProvider_Location"].ToString();
                    else
                        Ev_Mo.Lab_Locations = string.Empty;
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ServiceProvider_Contact"].ToString()))
                        Ev_Mo.Lab_Contacts = dt.Rows[0]["ServiceProvider_Contact"].ToString();
                    else
                        Ev_Mo.Lab_Contacts = string.Empty;
                    SqlCommand command = new SqlCommand("GetLabdetails", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Client_Name", Session["Account_idPK"].ToString());
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Labs"].ToString()))
                        {
                            Ev_Mo.Servicelist = GetServices("Lab", dataTable.Rows[0]["Labs"].ToString());

                        }
                        else
                        {
                            Ev_Mo.Servicelist = GetServices("Lab", string.Empty);
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Servicegroups"].ToString()))
                        {
                            Ev_Mo.Servicegroup = dt.Rows[0]["Servicegroups"].ToString();
                        }
                        else
                        {
                            Ev_Mo.Servicegroup = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Servicegroups"].ToString()))
                        {
                            Ev_Mo.subServiceslist = GetSubServices("Lab", dt.Rows[0]["Servicegroups"].ToString());
                        }
                        else
                        {
                            Ev_Mo.subServiceslist = GetSubServices("Lab", string.Empty);
                        }
                        if (!string.IsNullOrEmpty(dt.Rows[0]["subservices"].ToString()))
                        {
                            Ev_Mo.subServices = dt.Rows[0]["subservices"].ToString().Split(',');
                        }
                        else
                        {
                            Ev_Mo.subServices = string.Empty.Split(',');
                        }
                        Ev_Mo.List_price = GetSubServicePrice(dt.Rows[0]["subservices"].ToString());
                    }
                    //Ev_Mo.Servicelist = GeteventServices(id);
                    //Ev_Mo.subServiceslist = GeteventSubServices(id);
                }
            }

            return View(Ev_Mo);
        }
        [HttpPost]
        public ActionResult UpdateEvent(Event_Model event_Model)
        {
            
            if(event_Model.subServices == null)
            {
                event_Model.subServices = string.Empty.Split(',');
            }
            
            // your code goes here
            SqlConnection con = new SqlConnection(TransConnString);
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/Event"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Event"));
            HttpPostedFileBase fileBase = null;
            string path = Server.MapPath("~/Event/");
            if (Request.Files.Count > 0)
            {
                fileBase = Request.Files[0];
            }
            if (fileBase != null)
            {

                SqlCommand command123 = new SqlCommand("proc_event_id", con);
                command123.CommandType = CommandType.StoredProcedure;
                con.Open();
                int event_id = (Int32)command123.ExecuteScalar();
                con.Close();
                string filetype = Path.GetExtension(fileBase.FileName);
                event_Model.Document_Upload = "Event" + event_id + filetype;
                fileBase.SaveAs(path + event_Model.Document_Upload);
            }
            else
            {
                event_Model.Document_Upload = event_Model.Document_Upload;
            }
            SqlCommand command2 = new SqlCommand("proc_Update_event", con);
            command2.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(event_Model.Event_name))
                command2.Parameters.AddWithValue("@Event_name", event_Model.Event_name);
            else
                command2.Parameters.AddWithValue("@Event_name", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_id))
                command2.Parameters.AddWithValue("@Client_id", event_Model.Client_id);
            else
                command2.Parameters.AddWithValue("@Client_id", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_Location))
                command2.Parameters.AddWithValue("@Client_Location", event_Model.Client_Location);
            else
                command2.Parameters.AddWithValue("@Client_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_Contact))
                command2.Parameters.AddWithValue("@Client_Contact", event_Model.Client_Contact);
            else
                command2.Parameters.AddWithValue("@Client_Contact", string.Empty);
            if (event_Model.Event_Start_Date != null)
                command2.Parameters.AddWithValue("@Event_Start_Date", event_Model.Event_Start_Date);
            else
                command2.Parameters.AddWithValue("@Event_Start_Date", DateTime.Now);
            if (event_Model.Event_End_Date != null)
                command2.Parameters.AddWithValue("@Event_End_Date", event_Model.Event_End_Date);
            else
                command2.Parameters.AddWithValue("@Event_End_Date", DateTime.Now);
            if (event_Model.Event_Start_Time != null)
                command2.Parameters.AddWithValue("@Event_Start_Time", event_Model.Event_Start_Time);
            else
                command2.Parameters.AddWithValue("@Event_Start_Time", DateTime.Now);
            if (event_Model.Event_End_Time != null)
                command2.Parameters.AddWithValue("@Event_End_Time", event_Model.Event_End_Time);
            else
                command2.Parameters.AddWithValue("@Event_End_Time", DateTime.Now);

            if (!string.IsNullOrEmpty(event_Model.Document_Upload))
                command2.Parameters.AddWithValue("@Document_Upload", event_Model.Document_Upload);
            else
                command2.Parameters.AddWithValue("@Document_Upload", string.Empty);

            if (event_Model.Notes != null)
                command2.Parameters.AddWithValue("@Notes", event_Model.Notes);
            else
                command2.Parameters.AddWithValue("@Notes", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Location))
                command2.Parameters.AddWithValue("@Service_Location", event_Model.Service_Location);
            else
                command2.Parameters.AddWithValue("@Service_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Location))
                command2.Parameters.AddWithValue("@Location_Type", event_Model.Service_Location);
            else
                command2.Parameters.AddWithValue("@Location_Type", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Id))
                command2.Parameters.AddWithValue("@Service_Prov_Id", event_Model.Service_Prov_Id);
            else
                command2.Parameters.AddWithValue("@Service_Prov_Id", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Location))
                command2.Parameters.AddWithValue("@Service_Prov_Location", event_Model.Service_Prov_Location);
            else
                command2.Parameters.AddWithValue("@Service_Prov_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Contact))
                command2.Parameters.AddWithValue("@Service_Prov_Contact", event_Model.Service_Prov_Contact);
            else
                command2.Parameters.AddWithValue("@Service_Prov_Contact", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Event_Status))
                command2.Parameters.AddWithValue("@Event_Status", event_Model.Event_Status);
            else
                command2.Parameters.AddWithValue("@Event_Status", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Event_type))
                command2.Parameters.AddWithValue("@Event_Type", event_Model.Event_type);
            else
                command2.Parameters.AddWithValue("@Event_Type", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Tpa_Client))
                command2.Parameters.AddWithValue("@Tpa", event_Model.Tpa_Client);
            else
                command2.Parameters.AddWithValue("@Tpa", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Tpa_Client_location))
                command2.Parameters.AddWithValue("@Tpa_Location", event_Model.Tpa_Client_location);
            else
                command2.Parameters.AddWithValue("@Tpa_Location", string.Empty);

            if (!string.IsNullOrEmpty(event_Model.Tpa_Client_Contact))
                command2.Parameters.AddWithValue("@Tpa_Contact", event_Model.Tpa_Client_Contact);
            else
                command2.Parameters.AddWithValue("@Tpa_Contact", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Name))
                command2.Parameters.AddWithValue("@ServiceProvider_Name", event_Model.Lab_Name);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Name", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Locations))
                command2.Parameters.AddWithValue("@ServiceProvider_Location", event_Model.Lab_Locations);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Contacts))
                command2.Parameters.AddWithValue("@ServiceProvider_Contact", event_Model.Lab_Contacts);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Contact", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Servicegroup))
                command2.Parameters.AddWithValue("@servicegroups", event_Model.Servicegroup);
            else
                command2.Parameters.AddWithValue("@servicegroups", string.Empty);
            if (event_Model.subServices!=null)
                command2.Parameters.AddWithValue("@subservices",ConvertStringArrayToString(event_Model.subServices));
            else
                command2.Parameters.AddWithValue("@subservices", ConvertStringArrayToString(event_Model.subServices));
            command2.Parameters.AddWithValue("@update_By", User.Identity.Name);
            command2.Parameters.AddWithValue("@Id", event_Model.Id);
            con.Open();
            command2.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("EventList");

        }
        [HttpPost]
        public void AddEvent(Event_Model model)
        {

            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand command = new SqlCommand("Proc_insert_clientdetails", con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@User_Name", User.Identity.Name);
            if (!string.IsNullOrEmpty(model.Client_Location))
                command.Parameters.AddWithValue("@Client_Location", model.Client_Location);
            else
                command.Parameters.AddWithValue("@Client_Location", string.Empty);
            if (!string.IsNullOrEmpty(model.Client_Contact))
                command.Parameters.AddWithValue("@Client_Contact", model.Client_Contact);
            else
                command.Parameters.AddWithValue("@Client_Contact", string.Empty);
            if (!string.IsNullOrEmpty(model.Client_id))
                command.Parameters.AddWithValue("@Client_Name", model.Client_id);
            else
                command.Parameters.AddWithValue("@Client_Name", string.Empty);
            if (!string.IsNullOrEmpty(model.Event_type))
                command.Parameters.AddWithValue("@eventtype", model.Event_type);
            else
                command.Parameters.AddWithValue("@eventtype", string.Empty);
            if (!string.IsNullOrEmpty(model.Tpa_Client))
                command.Parameters.AddWithValue("@Tpa", model.Tpa_Client);
            else
                command.Parameters.AddWithValue("@Tpa", string.Empty);
            if (!string.IsNullOrEmpty(model.Tpa_Client_location))
                command.Parameters.AddWithValue("@Tpa_Location", model.Tpa_Client_location);
            else
                command.Parameters.AddWithValue("@Tpa_Location", string.Empty);
            if (!string.IsNullOrEmpty(model.Tpa_Client_Contact))
                command.Parameters.AddWithValue("@Tpa_Contact", model.Tpa_Client_Contact);
            else
                command.Parameters.AddWithValue("@Tpa_Contact", string.Empty);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
        }

        [HttpPost]
        public void AddEvent1(Event_Model event_Model)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand command1 = new SqlCommand("Proc_insert_Eventdate", con);
            command1.CommandType = CommandType.StoredProcedure;

            command1.Parameters.AddWithValue("@User_Name", User.Identity.Name);
            if (!string.IsNullOrEmpty(event_Model.Event_name))
                command1.Parameters.AddWithValue("@Event_name", event_Model.Event_name);
            else
                command1.Parameters.AddWithValue("@Event_name", string.Empty);
            if (event_Model.Event_Start_Date != null)
                command1.Parameters.AddWithValue("@Event_Start_Date", event_Model.Event_Start_Date);
            else
                command1.Parameters.AddWithValue("@Event_Start_Date", DateTime.Now);
            if (event_Model.Event_End_Date != null)
                command1.Parameters.AddWithValue("@Event_End_Date", event_Model.Event_End_Date);
            else
                command1.Parameters.AddWithValue("@Event_End_Date", DateTime.Now);
            if (event_Model.Event_Start_Time != null)
                command1.Parameters.AddWithValue("@Event_Start_Time", event_Model.Event_Start_Time);
            else
                command1.Parameters.AddWithValue("@Event_Start_Time", DateTime.Now);
            if (event_Model.Event_End_Time != null)
                command1.Parameters.AddWithValue("@Event_End_Time", event_Model.Event_End_Time);
            else
                command1.Parameters.AddWithValue("@Event_End_Time", DateTime.Now);
            if (!string.IsNullOrEmpty(event_Model.Notes))
                command1.Parameters.AddWithValue("@Notes", event_Model.Notes);
            else
                command1.Parameters.AddWithValue("@Notes", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_id))
                command1.Parameters.AddWithValue("@Client_Name", event_Model.Client_id);
            else
                command1.Parameters.AddWithValue("@Client_Name", string.Empty);

            con.Open();
            command1.ExecuteNonQuery();
            con.Close();
        }
        [HttpPost]
        public void AddEvent2(Event_Model event_Model)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand command2 = new SqlCommand("Proc_insert_Labdetails", con);
            command2.CommandType = CommandType.StoredProcedure;

            command2.Parameters.AddWithValue("@User_Name", User.Identity.Name);
            if (!string.IsNullOrEmpty(event_Model.Service_Location))
                command2.Parameters.AddWithValue("@Service_Type", event_Model.Service_Location);
            else
                command2.Parameters.AddWithValue("@Service_Type", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Id))
                command2.Parameters.AddWithValue("@Lab_Sp_Name", event_Model.Service_Prov_Id);
            else
                command2.Parameters.AddWithValue("@Lab_Sp_Name", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Contact))
                command2.Parameters.AddWithValue("@Lab_Sp_Contact", event_Model.Service_Prov_Contact);
            else
                command2.Parameters.AddWithValue("@Lab_Sp_Contact", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Location))
                command2.Parameters.AddWithValue("@Lab_Sp_Location", event_Model.Service_Prov_Location);
            else
                command2.Parameters.AddWithValue("@Lab_Sp_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Location))
                command2.Parameters.AddWithValue("@Service_Location", event_Model.Service_Location);
            else
                command2.Parameters.AddWithValue("@Service_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_id))
                command2.Parameters.AddWithValue("@Client_Name", event_Model.Client_id);
            else
                command2.Parameters.AddWithValue("@Client_Name", string.Empty);

            con.Open();
            command2.ExecuteNonQuery();
            con.Close();
        }

        [HttpPost]
        public void AddEvent3(Event_Model event_Model)
        {
            string[] values = event_Model.Servicegroups;
            string servicegrp = string.Empty;
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0)
                {
                    servicegrp = values[i].ToString();
                }
                else
                {
                    servicegrp = servicegrp + values[i].ToString();
                }
            }
            values = event_Model.subServices;
            string subservices = string.Empty;
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0)
                {
                    subservices = values[i].ToString();
                }
                else
                {
                    subservices = subservices + values[i].ToString();
                }
            }
            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand command3 = new SqlCommand("Proc_insert_Servicedetails", con);
            command3.CommandType = CommandType.StoredProcedure;

            command3.Parameters.AddWithValue("@User_Name", User.Identity.Name);
            command3.Parameters.AddWithValue("@Servicegroup", servicegrp);
            command3.Parameters.AddWithValue("@subService", subservices);
            command3.Parameters.AddWithValue("@Client_Name", event_Model.Client_id);
            con.Open();
            command3.ExecuteNonQuery();
            con.Close();
        }
        [HttpPost]
        public void AddEvent4(Event_Model event_Model)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand command2 = new SqlCommand("proc_lab_into_event", con);
            command2.CommandType = CommandType.StoredProcedure;

            command2.Parameters.AddWithValue("@User_Name", User.Identity.Name);
            if (!string.IsNullOrEmpty(event_Model.Service_Prov_Location))
                command2.Parameters.AddWithValue("@Service_Type", event_Model.Service_Prov_Location);
            else
                command2.Parameters.AddWithValue("@Service_Type", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Name))
                command2.Parameters.AddWithValue("@ServiceProvider_Name", event_Model.Lab_Name);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Name", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Contacts))
                command2.Parameters.AddWithValue("@ServiceProvider_Contact", event_Model.Lab_Contacts);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Contact", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Lab_Locations))
                command2.Parameters.AddWithValue("@ServiceProvider_Location", event_Model.Lab_Locations);
            else
                command2.Parameters.AddWithValue("@ServiceProvider_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Service_Location))
                command2.Parameters.AddWithValue("@Service_Location", event_Model.Service_Location);
            else
                command2.Parameters.AddWithValue("@Service_Location", string.Empty);
            if (!string.IsNullOrEmpty(event_Model.Client_id))
                command2.Parameters.AddWithValue("@Client_Name", event_Model.Client_id);
            else
                command2.Parameters.AddWithValue("@Client_Name", string.Empty);

            con.Open();
            command2.ExecuteNonQuery();
            con.Close();
        }
        public ActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(TransConnString);
            SqlCommand command = new SqlCommand("delete from Tbl_Event where id=@id", con);
            command.Parameters.AddWithValue("@id", id);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            return Redirect(Request.UrlReferrer.AbsolutePath);

        }
        public static List<SelectListItem> ListServicelocation()
        {

            List<SelectListItem> sl = new List<SelectListItem>() {


                new SelectListItem
                {
                    Text = "Client",
                    Value = "SP"
                },
                new SelectListItem
                {
                    Text = "Lab",
                    Value = "Lab"
                },
              };
            return sl;
        }
        public static List<SelectListItem> ListEventType()
        {

            List<SelectListItem> Et = new List<SelectListItem>() {



                new SelectListItem
                {
                    Text = " Lab",
                    Value = "Lab"
                },
                new SelectListItem
                {
                    Text = "Service",
                    Value = "SP"
                },
              };
            return Et;
        }

        public static List<SelectListItem> ListEventStatus()
        {

            List<SelectListItem> Es = new List<SelectListItem>() {


                new SelectListItem
                {
                    Text = "Initiated",
                    Value = "1",
                    Selected=true
                },
                new SelectListItem
                {
                    Text = "In-Progress",
                    Value = "2",
                    Selected=false
                },
                 new SelectListItem
                {
                    Text = "Completed",
                    Value = "3",
                    Selected=false
                }, new SelectListItem
                {
                    Text = "Billing-Pending",
                    Value = "4",
                    Selected=false

                }, new SelectListItem
                {
                    Text = "Billing-Completed",
                    Value = "5",
                    Selected=false
                },
              };
            return Es;
        }
        public static List<SelectListItem> ListLocationName()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> Ln = new List<SelectListItem>();

            string query = "Select Location_Id,concat(Address_1,',',city) as Locations_LocationName from  demo_location_table where company_id=@Accounts_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Accounts_Id", System.Web.HttpContext.Current.Session["Account_Id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Ln.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Locations_LocationName"].ToString(),
                    Value = dt.Rows[i]["Location_Id"].ToString()
                });
            }

            return Ln;
        }
        public static List<SelectListItem> ListContactName(string id)

        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> Cn = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(id))
            {
                string query = "Select client_contact_id,firstname from  tbl_client_location_contact where Location_Id=@Location_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Location_Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Cn.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["firstname"].ToString(),
                        Value = dt.Rows[i]["client_contact_id"].ToString()
                    });
                }
            }

            return Cn;
        }
        public JsonResult ListContactsName(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> Cn = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(id))
            {
                string query = "Select client_contact_id,firstname from  tbl_client_location_contact where Location_Id=@Location_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Location_Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Cn.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["firstname"].ToString(),
                        Value = dt.Rows[i]["client_contact_id"].ToString()
                    });
                }
            }

            return Json(Cn, JsonRequestBehavior.AllowGet);
        }
        //public static List<SelectListItem> GetClientContact(string Location)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

        //    List<SelectListItem> Cn = new List<SelectListItem>();

        //    string query = "Select client_contact_id,firstname from  tbl_client_location_contact where Location_Id=@Location_Id";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    cmd.Parameters.AddWithValue("@Location_Id", Location);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {

        //        Cn.Add(new SelectListItem
        //        {

        //            Text = dt.Rows[i]["firstname"].ToString(),
        //            Value = dt.Rows[i]["client_contact_id"].ToString()
        //        });
        //    }

        //    return Cn;
        //}

        public static List<SelectListItem> GetName(string Type)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            List<SelectListItem> Gt = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Type))
            {
                if (Type == "Lab")
                {


                    string query = "select Lab_demo.Labs from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    string[] vs;
                    if (dt.Rows.Count > 0)
                    {
                        vs = dt.Rows[0]["Labs"].ToString().Split(',');
                    }
                    else
                    {
                        vs = new string[0];
                    }
                    for (int i = 0; i < vs.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(vs[i].ToString()))
                        {
                            Gt.Add(new SelectListItem
                            {
                                Text = vs[i].Trim(),
                                Value = vs[i].Trim()
                            });
                        }
                    }

                }

                else if (Type == "SP")

                {




                    string query1 = "Select ServiceProvider_id,Serviceprovider_Name from  tbl_service_provider";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    SqlCommand command = new SqlCommand("select Lab_demo.Serviceprovider from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                    DataTable data = new DataTable();
                    dataAdapter.Fill(data);
                    if (data.Rows.Count > 0)
                    {
                        string[] vs = data.Rows[0]["Serviceprovider"].ToString().Split(',');
                        foreach (string val in vs)
                        {
                            if (!string.IsNullOrEmpty(val))
                            {
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    if (dt1.Rows[i]["ServiceProvider_id"].ToString().Trim() == val.Trim())
                                        Gt.Add(new SelectListItem
                                        {

                                            Text = dt1.Rows[i]["Serviceprovider_Name"].ToString(),
                                            Value = dt1.Rows[i]["Serviceprovider_Name"].ToString()
                                        });
                                }
                            }
                        }
                    }


                }
            }
            return Gt;

        }
        public JsonResult GetNames(string Type)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            List<SelectListItem> Gt = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Type))
            {
                if (Type == "Lab")
                {


                    string query = "select Lab_demo.Labs from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", Session["Account_Id"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(dt);
                    string[] vs;
                    if (dt.Rows.Count > 0)
                    {
                        vs = dt.Rows[0]["Labs"].ToString().Split(',');
                    }
                    else
                    {
                        vs = new string[0];
                    }
                    for (int i = 0; i < vs.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(vs[i].ToString()))
                        {
                            Gt.Add(new SelectListItem
                            {
                                Text = vs[i].Trim(),
                                Value = vs[i].Trim()
                            });
                        }
                    }

                }

                else if (Type == "SP")

                {
                    string query1 = "Select ServiceProvider_id,Serviceprovider_Name from  tbl_service_provider";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    SqlCommand command = new SqlCommand("select Lab_demo.Serviceprovider from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                    DataTable data = new DataTable();
                    dataAdapter.Fill(data);
                    if (data.Rows.Count > 0)
                    {
                        string[] vs = data.Rows[0]["Serviceprovider"].ToString().Split(',');
                        foreach (string val in vs)
                        {
                            if (!string.IsNullOrEmpty(val))
                            {
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    if (dt1.Rows[i]["ServiceProvider_id"].ToString().Trim() == val.Trim())
                                        Gt.Add(new SelectListItem
                                        {

                                            Text = dt1.Rows[i]["Serviceprovider_Name"].ToString(),
                                            Value = dt1.Rows[i]["Serviceprovider_Name"].ToString()
                                        });
                                }
                            }
                        }
                    }

                }
            }
            return Json(Gt, JsonRequestBehavior.AllowGet);

        }
        public static List<SelectListItem> GetLocation(string Type, string Name)
        {
            List<SelectListItem> Gl = new List<SelectListItem>();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type))
            {
                if (Type == "Lab")
                {


                    string query = "Select Id,CONCAT(Address_1,',',city) as Address from  tbl_Clinic_Details where Location_Name=@Location_Name and isdeleted=0 and islocationdeleted=0";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Location_Name", Name);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Gl.Add(new SelectListItem
                        {

                            Text = dt.Rows[i]["Address"].ToString(),
                            Value = dt.Rows[i]["Id"].ToString()
                        });
                    }

                }

                else if (Type == "SP")

                {




                    string query1 = "Select Serviceprovider_Id,CONCAT(Address_1,',',city) as Address  from Tbl_service_location where Serviceprovider_Name=@name";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Parameters.AddWithValue("@name", Name);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        Gl.Add(new SelectListItem
                        {

                            Text = dt1.Rows[i]["Address"].ToString(),
                            Value = dt1.Rows[i]["Serviceprovider_id"].ToString()
                        });
                    }
                }

            }
            return Gl;

        }
        public JsonResult GetLocations(string Type, string Name)
        {
            List<SelectListItem> Gl = new List<SelectListItem>();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type))
            {
                if (Type == "Lab")
                {


                    string query = "Select Id,CONCAT(Address_1,',',city) as Address from  tbl_Clinic_Details where Location_Name=@Location_Name and isdeleted=0 and islocationdeleted=0";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Location_Name", Name);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Gl.Add(new SelectListItem
                        {

                            Text = dt.Rows[i]["Address"].ToString(),
                            Value = dt.Rows[i]["Id"].ToString()
                        });
                    }

                }

                else if (Type == "SP")

                {




                    string query1 = "Select Serviceprovider_Id,CONCAT(Address_1,',',city) as Address  from Tbl_service_location where Serviceprovider_Name=@name";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Parameters.AddWithValue("@name", Name);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        Gl.Add(new SelectListItem
                        {

                            Text = dt1.Rows[i]["Address"].ToString(),
                            Value = dt1.Rows[i]["Serviceprovider_id"].ToString()
                        });
                    }
                }

            }
            return Json(Gl, JsonRequestBehavior.AllowGet);

        }
        public static List<SelectListItem> GetContact(string Type, string Location)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            List<SelectListItem> Gc = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Location))
            {
                if (Type == "Lab")
                {
                    string query = "select lab_contact_id,Concat(firstname,' ',lastname) as name from Tbl_Lab_location_Contact where lab_Location_Id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Location);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Gc.Add(new SelectListItem
                        {

                            Text = dt.Rows[i]["name"].ToString(),
                            Value = dt.Rows[i]["lab_contact_id"].ToString()
                        });
                    }

                }

                else if (Type == "SP")

                {

                    string query1 = "select sp_contact_id,Concat(firstname,' ',lastname) as name from Tbl_Serviceprovider_Contact where sp_Location_Id=@id";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Parameters.AddWithValue("@Id", Location);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        Gc.Add(new SelectListItem
                        {

                            Text = dt1.Rows[i]["name"].ToString(),
                            Value = dt1.Rows[i]["Sp_contact_id"].ToString()
                        });
                    }


                }
            }
            return Gc;

        }
        public JsonResult GetContacts(string Type, string Location)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            List<SelectListItem> Gc = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Location))
            {
                if (Type == "Lab")
                {


                    string query = "select lab_contact_id,Concat(firstname,' ',lastname) as name from Tbl_Lab_location_Contact where lab_Location_Id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Location);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Gc.Add(new SelectListItem
                        {

                            Text = dt.Rows[i]["name"].ToString(),
                            Value = dt.Rows[i]["lab_contact_id"].ToString()
                        });
                    }

                }

                else if (Type == "SP")

                {

                    string query1 = "select sp_contact_id,Concat(firstname,' ',lastname) as name from Tbl_Serviceprovider_Contact where sp_Location_Id=@id";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Parameters.AddWithValue("@Id", Location);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        Gc.Add(new SelectListItem
                        {

                            Text = dt1.Rows[i]["name"].ToString(),
                            Value = dt1.Rows[i]["Sp_contact_id"].ToString()
                        });
                    }


                }
            }
            return Json(Gc, JsonRequestBehavior.AllowGet);

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
            Type = "Lab";
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
                            DataTable serDataTable = new DataTable();
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                            sqlDataAdapter.Fill(serDataTable);
                            SqlCommand command = new SqlCommand("select Lab_demo.SubServices from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                            DataTable data = new DataTable();
                            dataAdapter.Fill(data);
                            if (data.Rows.Count > 0)
                            {
                                string[] vs = data.Rows[0]["SubServices"].ToString().Split(',');
                                DataTable data1 = new DataTable();
                                data1.Columns.Add("tempId", typeof(int));
                                foreach (string val in vs)
                                {
                                    if (!string.IsNullOrEmpty(val))
                                    {
                                        data1.Rows.Add(val);
                                    }
                                }
                                try
                                {
                                    var results = from table1 in serDataTable.AsEnumerable()
                                                  join table2 in data1.AsEnumerable() on (int)table1["id"] equals (int)table2["tempId"]
                                                  select new
                                                  {
                                                      id = (int)table1["id"],
                                                      service_grp_name = (string)table1["lab_services_description"],
                                                      Labname = (string)table1["Labname"]
                                                  };
                                    foreach (var result in results)
                                    {
                                        items.Add(new SelectListItem
                                        {
                                            Text = result.Labname + "-" + result.service_grp_name,
                                            Value = result.id.ToString(),
                                            Selected = false
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {

                                }

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
                            DataTable serDataTable = new DataTable();
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                            sqlDataAdapter.Fill(serDataTable);
                            SqlCommand command = new SqlCommand("select Lab_demo.Sp_Sub_Service from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                            DataTable data = new DataTable();
                            dataAdapter.Fill(data);
                            if (data.Rows.Count > 0)
                            {
                                string[] vs = data.Rows[0]["Sp_Sub_Service"].ToString().Split(',');
                                DataTable data1 = new DataTable();
                                data1.Columns.Add("tempId", typeof(int));
                                foreach (string val in vs)
                                {
                                    if (!string.IsNullOrEmpty(val))
                                    {
                                        data1.Rows.Add(val);
                                    }
                                }
                                try { 
                                var results = from table1 in serDataTable.AsEnumerable()
                                              join table2 in data1.AsEnumerable() on (int)table1["Sp_service_id"] equals (int)table2["tempId"]
                                              select new
                                              {
                                                  id = (int)table1["Sp_service_id"],
                                                  service_grp_name = (string)table1["Sp_services_description"],
                                                  Labname = (string)table1["Labname"]
                                              };
                                foreach (var result in results)
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = result.Labname+"-"+ result.service_grp_name,
                                        Value = result.id.ToString(),
                                        Selected = false
                                    });
                                }
                                }
                                catch (Exception ex) { }
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

        public JsonResult jsonGetServices(string Type, string Name)
        {
            Type = "Lab";
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Type))
            {
                string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
                if (Type == "Lab")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        DataTable serDataTable = new DataTable();
                        string query = "SELECT id,service_grp_name FROM lab_service_grp where LabName=@LabName";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Parameters.AddWithValue("@LabName", Name);
                            cmd.Connection = con;
                            con.Open();
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                            sqlDataAdapter.Fill(serDataTable);
                            SqlCommand command = new SqlCommand("select Lab_demo.ServiceGroups from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                            DataTable data = new DataTable();
                            dataAdapter.Fill(data);
                            string[] vs = data.Rows[0]["ServiceGroups"].ToString().Split(',');
                            DataTable data1 = new DataTable();
                            data1.Columns.Add("tempId", typeof(int));
                            
                            foreach (string val in vs)
                            {
                                if (!string.IsNullOrEmpty(val))
                                {
                                    data1.Rows.Add(val);
                                }
                            }
                            try
                            {
                                var results = from table1 in serDataTable.AsEnumerable()
                                          join table2 in data1.AsEnumerable() on (int)table1["id"] equals (int)table2["tempId"]
                                          select new
                                          {
                                              id = (int)table1["id"],
                                              service_grp_name = (string)table1["service_grp_name"]
                                          };
                            foreach (var result in results)
                            {
                                items.Add(new SelectListItem
                                {
                                    Text = result.service_grp_name,
                                    Value = result.id.ToString(),
                                    Selected = false
                                });
                            }
                            }
                            catch (Exception ex) { }
                            con.Close();
                        }
                    }
                }
                else if (Type == "SP")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string query = "select Sp_id,service_grp_name from sp_service_grp spgrp inner join Tbl_Service_Provider tsp on spgrp.SpName=tsp.ServiceProvider_id  where tsp.Serviceprovider_Name=@Serviceprovider_Name";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Parameters.AddWithValue("@Serviceprovider_Name", Name);
                            cmd.Connection = con;
                            DataTable serDataTable = new DataTable();
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                            sqlDataAdapter.Fill(serDataTable);
                            SqlCommand command = new SqlCommand("select Lab_demo.SP_Service from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                            DataTable data = new DataTable();
                            dataAdapter.Fill(data);
                            if (data.Rows.Count > 0)
                            {
                                string[] vs = data.Rows[0]["SP_Service"].ToString().Split(',');
                                DataTable data1 = new DataTable();
                                data1.Columns.Add("tempId", typeof(int));
                                foreach (string val in vs)
                                {
                                    if (!string.IsNullOrEmpty(val))
                                    {
                                        data1.Rows.Add(val);
                                    }
                                }
                                try { 
                                var results = from table1 in serDataTable.AsEnumerable()
                                              join table2 in data1.AsEnumerable() on (int)table1["Sp_id"] equals (int)table2["tempId"]
                                              select new
                                              {
                                                  id = (int)table1["Sp_id"],
                                                  service_grp_name = (string)table1["service_grp_name"]
                                              };
                                foreach (var result in results)
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = result.service_grp_name,
                                        Value = result.id.ToString(),
                                        Selected = false
                                    });
                                }
                                }
                                catch (Exception ex) { }
                            }

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

        public JsonResult JsonGetSubServices(string Type, string id)
        {
            Type = "Lab";
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Type))
            {
                string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
                if (Type == "Lab")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        string[] values = id.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "Getlabsub_services";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@lab_service_grp_id", values[i].Trim());
                            DataTable serDataTable = new DataTable();
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                            sqlDataAdapter.Fill(serDataTable);
                            SqlCommand command = new SqlCommand("select Lab_demo.SubServices from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                            DataTable data = new DataTable();
                            dataAdapter.Fill(data);
                            if (data.Rows.Count > 0)
                            {
                                string[] vs = data.Rows[0]["SubServices"].ToString().Split(',');
                                DataTable data1 = new DataTable();
                                data1.Columns.Add("tempId", typeof(int));
                                foreach (string val in vs)
                                {
                                    if (!string.IsNullOrEmpty(val))
                                    {
                                        data1.Rows.Add(val);
                                    }
                                }
                                var results = from table1 in serDataTable.AsEnumerable()
                                              join table2 in data1.AsEnumerable() on (int)table1["id"] equals (int)table2["tempId"]
                                              select new
                                              {
                                                  id = (int)table1["id"],
                                                  service_grp_name = (string)table1["lab_services_description"],
                                                  Labname = (string)table1["Labname"]
                                              };
                                foreach (var result in results)
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text =result.Labname+"-"+ result.service_grp_name,
                                        Value = result.id.ToString(),
                                        Selected = false
                                    });
                                }
                            }

                        }
                    }
                }
                else if (Type == "SP")
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string[] values = id.Split(',');
                        for (int i = 0; i < values.Length; i++)
                        {
                            string query = "select Sp_service_id,Sp_services_description from Sp_sub_services where Sp_group_id=@Sp_group_id";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@Sp_group_id", values[i].Trim());
                            DataTable serDataTable = new DataTable();
                            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                            sqlDataAdapter.Fill(serDataTable);
                            SqlCommand command = new SqlCommand("select Lab_demo.Sp_Sub_Service from Lab_demo inner join aspnetaccounts on aspnetaccounts.accountid_pk=Lab_demo.Client_name where aspnetaccounts.accountid=@id", con);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            command.Parameters.AddWithValue("@id", System.Web.HttpContext.Current.Session["Account_Id"].ToString());
                            DataTable data = new DataTable();
                            dataAdapter.Fill(data);
                            if (data.Rows.Count > 0)
                            {
                                string[] vs = data.Rows[0]["Sp_Sub_Service"].ToString().Split(',');
                                DataTable data1 = new DataTable();
                                data1.Columns.Add("tempId", typeof(int));
                                foreach (string val in vs)
                                {
                                    if (!string.IsNullOrEmpty(val))
                                    {
                                        data1.Rows.Add(val);
                                    }
                                }
                                var results = from table1 in serDataTable.AsEnumerable()
                                              join table2 in data1.AsEnumerable() on (int)table1["Sp_service_id"] equals (int)table2["tempId"]
                                              select new
                                              {
                                                  id = (int)table1["Sp_service_id"],
                                                  service_grp_name = (string)table1["Sp_services_description"]
                                              };
                                foreach (var result in results)
                                {
                                    items.Add(new SelectListItem
                                    {
                                        Text = result.service_grp_name,
                                        Value = result.id.ToString(),
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
        public List<Billing> GetSubServicePrice(string id)
        {
            List<Billing> billings = new List<Billing>();
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(constr);
            if (!string.IsNullOrEmpty(id))
            {
                string[] panel = id.Split(',');
                for (int i = 0; i < panel.Count(); i++)
                {
                    DataTable dataTable = new DataTable();
                    SqlCommand command = new SqlCommand("Proc_AssignPrice", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@lab_services_description", panel[i].Trim());
                    command.Parameters.AddWithValue("@Client_id", Session["Account_idPK"].ToString());
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        Billing billing = new Billing();
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["lab_services_description"].ToString()))
                            billing.lab_services_description = dataTable.Rows[0]["lab_services_description"].ToString();
                        else
                            billing.lab_services_description = string.Empty;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["client_billing_charges"].ToString()))
                            billing.client_billing_charges = Convert.ToDecimal(dataTable.Rows[0]["client_billing_charges"]);
                        else
                            billing.client_billing_charges = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["service_charges"].ToString()))
                            billing.service_charges = Convert.ToDecimal(dataTable.Rows[0]["service_charges"]);
                        else
                            billing.service_charges = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Billing_Price"].ToString()))
                            billing.Billing_Price = Convert.ToDecimal(dataTable.Rows[0]["Billing_Price"]);
                        else
                            billing.Billing_Price = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Medical_Review_Office_Cost"].ToString()))
                            billing.Medical_Review_Office_Cost = Convert.ToDecimal(dataTable.Rows[0]["Medical_Review_Office_Cost"]);
                        else
                            billing.Medical_Review_Office_Cost = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Vendor_management"].ToString()))
                            billing.Vendor_management = Convert.ToDecimal(dataTable.Rows[0]["Vendor_management"]);
                        else
                            billing.Vendor_management = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Document_Upload"].ToString()))
                            billing.Document_Upload = Convert.ToDecimal(dataTable.Rows[0]["Document_Upload"]);
                        else
                            billing.Document_Upload = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Collection_Cost"].ToString()))
                            billing.Collection_Cost = Convert.ToDecimal(dataTable.Rows[0]["Collection_Cost"]);
                        else
                            billing.Collection_Cost = 0;
                        billings.Add(billing);
                    }
                }
                return billings;
            }
            else
            {
                return billings;
            }
        }
        public JsonResult JsonGetSubServicePrice(string id)
        {
            List<Billing> billings = new List<Billing>();
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(constr);
            if (!string.IsNullOrEmpty(id))
            {
                string[] panel = id.Split(',');
                for (int i = 0; i < panel.Count(); i++)
                {
                    DataTable dataTable = new DataTable();
                    SqlCommand command = new SqlCommand("Proc_AssignPrice", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@lab_services_description",panel[i].Trim());
                    command.Parameters.AddWithValue("@Client_id", Session["Account_idPK"].ToString());
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                    dataAdapter.Fill(dataTable);
                    if(dataTable.Rows.Count>0)
                    {
                        Billing billing = new Billing();
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["lab_services_description"].ToString()))
                            billing.lab_services_description = dataTable.Rows[0]["lab_services_description"].ToString();
                        else
                            billing.lab_services_description = string.Empty;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["client_billing_charges"].ToString()))
                            billing.client_billing_charges = Convert.ToDecimal(dataTable.Rows[0]["client_billing_charges"]);
                        else
                            billing.client_billing_charges = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["service_charges"].ToString()))
                            billing.service_charges = Convert.ToDecimal(dataTable.Rows[0]["service_charges"]);
                        else
                            billing.service_charges = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Billing_Price"].ToString()))
                            billing.Billing_Price = Convert.ToDecimal(dataTable.Rows[0]["Billing_Price"]);
                        else
                            billing.Billing_Price = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Medical_Review_Office_Cost"].ToString()))
                            billing.Medical_Review_Office_Cost = Convert.ToDecimal(dataTable.Rows[0]["Medical_Review_Office_Cost"]);
                        else
                            billing.Medical_Review_Office_Cost = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Vendor_management"].ToString()))
                            billing.Vendor_management= Convert.ToDecimal(dataTable.Rows[0]["Vendor_management"]);
                        else
                            billing.Vendor_management = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Document_Upload"].ToString()))
                            billing.Document_Upload = Convert.ToDecimal(dataTable.Rows[0]["Document_Upload"]);
                        else
                            billing.Document_Upload = 0;
                        if (!string.IsNullOrEmpty(dataTable.Rows[0]["Collection_Cost"].ToString()))
                            billing.Collection_Cost = Convert.ToDecimal(dataTable.Rows[0]["Collection_Cost"]);
                        else
                            billing.Collection_Cost = 0;
                        billings.Add(billing);
                    }
                }
                return Json(billings, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(billings, JsonRequestBehavior.AllowGet);
            }
        }
        public static List<SelectListItem> GeteventServices(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            SqlCommand command = new SqlCommand("get_event_services", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@event_id", id);
            con.Open();
            using (SqlDataReader sdr = command.ExecuteReader())
            {
                while (sdr.Read())
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Text = sdr["Servicegrp_text"].ToString(),
                        Value = sdr["Servicegrp_id"].ToString(),
                        Selected = Convert.ToBoolean(sdr["isact"])

                    });
                }
            }
            con.Close();

            return selectListItems;
        }

        public static List<SelectListItem> GeteventSubServices(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            SqlCommand command = new SqlCommand("get_event_subservice", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@event_id", id);
            con.Open();
            using (SqlDataReader sdr = command.ExecuteReader())
            {
                while (sdr.Read())
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Text = sdr["Servicegrp_text"].ToString(),
                        Value = sdr["SubService_id"].ToString(),
                        Selected = Convert.ToBoolean(sdr["isact"])

                    }); ;
                }
            }
            con.Close();
            return selectListItems;


        }
    }
}