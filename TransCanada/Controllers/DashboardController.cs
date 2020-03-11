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
    public class DashboardController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: Dashboard
        [BreadCrumb(Clear = true, Label = "Dashboard")]
        public ActionResult Index()
        {
            if (Session["Account_idPK"] == null)
                return RedirectToAction("Account_List", "Home");
            SqlConnection conn = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("proc_get_all_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@companyid", Session["Account_id"]);
            string bala = Session["Account_Id"].ToString();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ClientView_Model clientList = new ClientView_Model();
            List<Location> locations = new List<Location>();
            List<Lab_contact> Lab_contacts = new List<Lab_contact>();
            
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                Location location = new Location();
                if (!string.IsNullOrEmpty(dt.Rows[j]["address_Type"].ToString()))
                {

                    location.ltype = dt.Rows[j]["address_Type"].ToString();

                }
                else
                {
                    location.ltype = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Location"].ToString()))
                {

                    location.Location_Name = dt.Rows[j]["Location"].ToString();

                }
                else
                {
                    location.Location_Name = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Location_id"].ToString()))
                {

                    location.Location_id =Convert.ToInt32(dt.Rows[j]["Location_id"].ToString());

                }
                else
                {
                    location.Location_id = 0;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Address_1"].ToString()))
                {
                    location.Address_1 = dt.Rows[j]["Address_1"].ToString();
                }
                else
                {
                    location.Address_1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Address_2"].ToString()))
                {
                    location.Address_2 = dt.Rows[j]["Address_2"].ToString();
                }
                else
                {
                    location.Address_2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["City"].ToString()))
                {
                    location.City = dt.Rows[j]["City"].ToString();
                }
                else
                {
                    location.City = string.Empty;
                }

                if (!string.IsNullOrEmpty(dt.Rows[j]["State"].ToString()))
                {
                    location.State = dt.Rows[j]["State"].ToString();
                }
                else
                {
                    location.State = string.Empty;
                }

                locations.Add(location);

            }

            SqlConnection conn1 = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd1 = new SqlCommand("clientcontact_list", conn1);
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@Location_Name", bala);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                for (int j = 0; j < dt1.Rows.Count; j++)
                {

                    Lab_contact contact = new Lab_contact();
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["client_contact_id"].ToString()))
                    {

                        contact.contact_id =Convert.ToInt32(dt1.Rows[j]["client_contact_id"].ToString());

                    }
                    else
                    {
                        contact.contact_id = 0;
                    }
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["location_id"].ToString()))
                    {

                        contact.location_id = dt1.Rows[j]["location_id"].ToString();

                    }
                    else
                    {
                        contact.location_id =string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["Location"].ToString()))
                    {

                        contact.Location_Name = dt1.Rows[j]["Location"].ToString();

                    }
                    else
                    {
                        contact.Location_Name = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["firstname"].ToString()))
                    {

                        contact.firstname = (dt1.Rows[j]["firstname"].ToString());

                    }
                    else
                    {
                        contact.firstname = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(dt1.Rows[j]["email"].ToString()))
                    {

                        contact.email = (dt1.Rows[j]["email"].ToString());

                    }
                    else
                    {
                        contact.email = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["cell"].ToString()))
                    {

                        contact.cell = (dt1.Rows[j]["cell"].ToString());

                    }
                    else
                    {
                        contact.cell = string.Empty;
                    }

                    Lab_contacts.Add(contact);
                }
            }
            ClientLabController clientLabController = new ClientLabController();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<SelectListItem> listItems = clientLabController.ProviderList();
            SqlCommand command = new SqlCommand("GetLabdetails", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Client_Name", Session["Account_idPK"].ToString());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Serviceprovider"].ToString()))
                {
                    string Title = dataTable.Rows[0]["Serviceprovider"].ToString();
                    clientList.Sp_selected = Title.Split(',');
                    for (int i = 0; i < clientList.Sp_selected.Length; i++)
                    {

                        foreach (var item in listItems)
                        {
                            if (clientList.Sp_selected[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Labs"].ToString()))
                {
                    ClientLabController clientLabController1 = new ClientLabController();
                    selectListItems = GetServices("Lab", dataTable.Rows[0]["Labs"].ToString());
                    string Title = string.Empty;
                    if (!string.IsNullOrEmpty(dataTable.Rows[0]["ServiceGroups"].ToString()))
                    {
                        Title = dataTable.Rows[0]["ServiceGroups"].ToString();
                    }
                    string[] services = Title.Split(',');
                    for (int i = 0; i < services.Length; i++)
                    {

                        foreach (var item in selectListItems)
                        {
                            if (services[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
            }
            clientList.list_location = locations;
            clientList.List_Lab_contact = Lab_contacts;
            clientList.SP = listItems.Where(c => c.Selected == true).ToList();
            clientList.LabServices= selectListItems.Where(c => c.Selected == true).ToList();
            List<Event_Model> Events = new List<Event_Model>();
            SqlCommand sqlcmd = new SqlCommand("Select * from Tbl_Event where Client_id=@Client_id", conn);
            sqlcmd.Parameters.AddWithValue("@Client_id", Session["Account_Id"]);
            SqlDataAdapter Adapter = new SqlDataAdapter(sqlcmd);
            DataTable table = new DataTable();
            Adapter.Fill(table);

            for (int j = 0; j < table.Rows.Count; j++)
            {
                Event_Model Ev_Mo = new Event_Model();

                Ev_Mo.Id = Convert.ToInt32(table.Rows[j]["Id"].ToString());
                Ev_Mo.Event_name = table.Rows[j]["Event_name"].ToString();
                Ev_Mo.Event_Start_Date = Convert.ToDateTime(table.Rows[j]["Event_Start_Date"].ToString());
                Ev_Mo.Event_End_Date = Convert.ToDateTime(table.Rows[j]["Event_End_Date"].ToString());

                Events.Add(Ev_Mo);
            }
            clientList.List_Events = Events;

            return View(clientList);
        }

        private List<SelectListItem> GetServices(string Type, string name)
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
    }
   
    //public ActionResult Contact()
    //{
    //    SqlConnection conn = new SqlConnection(TransCanadaConnection);
    //    SqlCommand cmd = new SqlCommand("proc_List_client_contact", conn);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("location_id", Session["Account_id"]);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    List<Lab_contact> ContactList = new List<Lab_contact>();
    //    if (dt.Rows.Count > 0)
    //    {
    //        for (int j = 0; j < dt.Rows.Count; j++)
    //        {

    //            Lab_contact contact = new Lab_contact();
    //            if (!string.IsNullOrEmpty(dt.Rows[j]["client_contact_id"].ToString()))
    //            {

    //                contact.contact_id = Convert.ToInt32(dt.Rows[j]["client_contact_id"].ToString());

    //            }
    //            else
    //            {
    //                contact.contact_id = 0;
    //            }
    //            if (!string.IsNullOrEmpty(dt.Rows[j]["Location_id"].ToString()))
    //            {

    //                contact.location_id = dt.Rows[j]["Location_id"].ToString();

    //            }
    //            else
    //            {
    //                contact.location_id = "0";
    //            }
    //            if (!string.IsNullOrEmpty(dt.Rows[j]["firstname"].ToString()))
    //            {

    //                contact.firstname = (dt.Rows[j]["firstname"].ToString());

    //            }
    //            else
    //            {
    //                contact.firstname = string.Empty;
    //            }
    //            if (!string.IsNullOrEmpty(dt.Rows[j]["Lastname"].ToString()))
    //            {

    //                contact.Lastname = (dt.Rows[j]["Lastname"].ToString());

    //            }
    //            else
    //            {
    //                contact.Lastname = string.Empty;
    //            }
    //            if (!string.IsNullOrEmpty(dt.Rows[j]["email"].ToString()))
    //            {

    //                contact.email = (dt.Rows[j]["email"].ToString());

    //            }
    //            else
    //            {
    //                contact.email = string.Empty;
    //            }
    //            if (!string.IsNullOrEmpty(dt.Rows[j]["cell"].ToString()))
    //            {

    //                contact.cell = (dt.Rows[j]["cell"].ToString());

    //            }
    //            else
    //            {
    //                contact.cell = string.Empty;
    //            }
    //            if (!string.IsNullOrEmpty(dt.Rows[j]["officephone"].ToString()))
    //            {

    //                contact.officephone = (dt.Rows[j]["officephone"].ToString());

    //            }
    //            else
    //            {
    //                contact.officephone = string.Empty;
    //            }
    //            ContactList.Add(contact);
    //        }
    //    }
    //    return View(ContactList);
    //}


}