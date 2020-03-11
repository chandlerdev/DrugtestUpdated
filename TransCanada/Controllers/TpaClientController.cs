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
    public class TpaClientController : Controller
    {
        string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: TpaClient
        public ActionResult Index()
        {
            return View();
        }
        [BreadCrumb(Label = "Create TPA Client")]
        public ActionResult CreateTPaClient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTPaClient(TPAClient Tpa)
        {
            if (!ModelState.IsValid)
                return View(Tpa);
            SqlConnection Con = new SqlConnection(TransConnString);
            SqlCommand cmd = new SqlCommand("Tbl_TpaClient_Insert", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TPA_Client", Tpa.TPA_Client);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            return RedirectToAction("TpaClientList");
        }
        [BreadCrumb(Clear = true, Label = "TPA Clients")]
        public ActionResult TpaClientList()
        {
            SqlCommand selectCommand = new SqlCommand("Tbl_TpaClient_List", new SqlConnection(TransConnString));
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<TPAClient> Tpalist = new List<TPAClient>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TPAClient tpa = new TPAClient();
                tpa.TPAClient_Id = Convert.ToInt32(dataTable.Rows[i]["TPAClient_Id"].ToString());

                if (!string.IsNullOrEmpty(dataTable.Rows[i]["TPA_Client"].ToString()))
                    tpa.TPA_Client = dataTable.Rows[i]["TPA_Client"].ToString();
                else
                    tpa.TPA_Client = string.Empty;
                Tpalist.Add(tpa);
            }
            return View(Tpalist);

        }
        [BreadCrumb(Label = "Update TPA Client")]
        [HttpGet]
        public ActionResult TpaClientEdit(string id)
        {
            SqlCommand selectCommand = new SqlCommand("Tbl_TpaClient_Edit", new SqlConnection(this.TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@TPAClient_Id", id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            TPAClient TPA = new TPAClient();

            if (dataTable.Rows.Count > 0)
            {
                TPA.TPAClient_Id = Convert.ToInt32(dataTable.Rows[0]["TPAClient_Id"].ToString());
                TPA.TPA_Client = string.IsNullOrEmpty(dataTable.Rows[0]["TPA_Client"].ToString()) ? string.Empty : dataTable.Rows[0]["TPA_Client"].ToString();
            }
            return View(TPA);
        }
        [HttpPost]
        public ActionResult TpaClientEdit(TPAClient TPA)
        {
            if (!ModelState.IsValid)
                return View(TPA);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClient_Update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPAClient_Id", Convert.ToInt32(TPA.TPAClient_Id));
            if (!string.IsNullOrEmpty(TPA.TPA_Client))
                sqlCommand.Parameters.AddWithValue("@TPA_Client", TPA.TPA_Client);
            else
                sqlCommand.Parameters.AddWithValue("@TPA_Client", string.Empty);

            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("TpaClientList", "TpaClient");
        }


        [BreadCrumb(Label = "Locations")]
        public ActionResult TcLocationList(string id)
        {
            ViewBag.TPAClientid = id.Trim();
            SqlCommand selectCommand = new SqlCommand("Tbl_TpaClientLoc_list", new SqlConnection(TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TPAClient_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<TPALocation> Tclist = new List<TPALocation>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TPALocation tpa = new TPALocation();
                tpa.TPAClient_Id = Convert.ToInt32(dataTable.Rows[i]["TPAClient_Id"].ToString());
                tpa.Location_id = dataTable.Rows[i]["Location_id"].ToString();

                tpa.TPAClient_LocID = Convert.ToInt32(dataTable.Rows[i]["TPAClient_LocID"].ToString());

                //if (!string.IsNullOrEmpty(dataTable.Rows[i]["TPA_Client"].ToString()))
                //    tpa.TPA_Client = dataTable.Rows[i]["TPA_Client"].ToString();
                //else
                //    tpa.TPA_Client = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[i]["Address_1"].ToString()))
                    tpa.Address_1 = dataTable.Rows[i]["Address_1"].ToString();
                else
                    tpa.Address_1 = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[i]["Address_2"].ToString()))
                    tpa.Address_2 = dataTable.Rows[i]["Address_2"].ToString();
                else
                    tpa.Address_2 = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[i]["City"].ToString()))
                    tpa.City = dataTable.Rows[i]["City"].ToString();
                else
                    tpa.City = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[i]["State"].ToString()))
                    tpa.State = dataTable.Rows[i]["State"].ToString();
                else
                    tpa.State = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[i]["Zip"].ToString()))
                    tpa.Zip = dataTable.Rows[i]["Zip"].ToString();
                else
                    tpa.Zip = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[i]["Country"].ToString()))
                    tpa.Country = dataTable.Rows[i]["Country"].ToString();
                else
                    tpa.Country = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[i]["WebSite"].ToString()))
                    tpa.WebSite = dataTable.Rows[i]["WebSite"].ToString();
                else
                    tpa.WebSite = string.Empty;
                Tclist.Add(tpa);
            }
            return View(Tclist);
        }
        [BreadCrumb(Label = "New Location")]
        public ActionResult AddTcLocation(int id)

        {
            TPALocation tp = new TPALocation();
            tp.TPAClient_Id = id;

            return View(tp);
        }
        [HttpPost]
        public ActionResult AddTcLocation(TPALocation tpa)
        {
            if (!ModelState.IsValid)
                return View(tpa);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClientLoc_insert", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPAClient_Id", tpa.TPAClient_Id);
            //if (!string.IsNullOrEmpty(tpa.TPA_Client))
            //    sqlCommand.Parameters.AddWithValue("@TPA_Client", tpa.TPA_Client);
            //else
            //    sqlCommand.Parameters.AddWithValue("@TPA_Client", string.Empty);

            if (!string.IsNullOrEmpty(tpa.Location_id))
                sqlCommand.Parameters.AddWithValue("@Location_id", tpa.Location_id);
            else
                sqlCommand.Parameters.AddWithValue("@Location_id", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Address_1))
                sqlCommand.Parameters.AddWithValue("@Address_1", tpa.Address_1);
            else
                sqlCommand.Parameters.AddWithValue("@Address_1", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Address_2))
                sqlCommand.Parameters.AddWithValue("@Address_2", tpa.Address_2);
            else
                sqlCommand.Parameters.AddWithValue("@Address_2", string.Empty);
            if (!string.IsNullOrEmpty(tpa.City))
                sqlCommand.Parameters.AddWithValue("@City", tpa.City);
            else
                sqlCommand.Parameters.AddWithValue("@City", string.Empty);
            if (!string.IsNullOrEmpty(tpa.State))
                sqlCommand.Parameters.AddWithValue("@State", tpa.State);
            else
                sqlCommand.Parameters.AddWithValue("@State", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Zip))
                sqlCommand.Parameters.AddWithValue("@Zip", tpa.Zip);
            else
                sqlCommand.Parameters.AddWithValue("@Zip", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Country))
                sqlCommand.Parameters.AddWithValue("@Country", tpa.Country);
            else
                sqlCommand.Parameters.AddWithValue("@Country", string.Empty);
            if (!string.IsNullOrEmpty(tpa.WebSite))
                sqlCommand.Parameters.AddWithValue("@WebSite", tpa.WebSite);
            else
                sqlCommand.Parameters.AddWithValue("@WebSite", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Notes))
                sqlCommand.Parameters.AddWithValue("@Notes", tpa.Notes);
            else
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Phone_number))
                sqlCommand.Parameters.AddWithValue("@Phone_number", tpa.Phone_number);
            else
                sqlCommand.Parameters.AddWithValue("@Phone_number", string.Empty);
            if (!string.IsNullOrEmpty(tpa.email))
                sqlCommand.Parameters.AddWithValue("@email", tpa.email);
            else
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("TcLocationList", new { id = tpa.TPAClient_Id });
        }
        [BreadCrumb(Label = "Update Location")]
        [HttpGet]
        public ActionResult UpdateTcLocation(string id)
        {
            SqlCommand selectCommand = new SqlCommand("Tbl_TpaClientLoc_edit", new SqlConnection(TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@TPAClient_LocID", id);
            sqlDataAdapter.Fill(dataTable);
            TPALocation tpa = new TPALocation();
            if (dataTable.Rows.Count > 0)
            {
                tpa.TPAClient_LocID = Convert.ToInt32(dataTable.Rows[0]["TPAClient_LocID"].ToString());
                tpa.TPAClient_Id = Convert.ToInt32(dataTable.Rows[0]["TPAClient_Id"].ToString());
                tpa.Location_id = string.IsNullOrEmpty(dataTable.Rows[0]["Location_id"].ToString()) ? string.Empty : dataTable.Rows[0]["Location_id"].ToString();

                //tpa.TPA_Client = string.IsNullOrEmpty(dataTable.Rows[0]["TPA_Client"].ToString()) ? string.Empty : dataTable.Rows[0]["TPA_Client"].ToString();
                tpa.Address_1 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_1"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_1"].ToString();
                tpa.Address_2 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_2"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_2"].ToString();
                tpa.City = string.IsNullOrEmpty(dataTable.Rows[0]["City"].ToString()) ? string.Empty : dataTable.Rows[0]["City"].ToString();
                tpa.State = string.IsNullOrEmpty(dataTable.Rows[0]["State"].ToString()) ? string.Empty : dataTable.Rows[0]["State"].ToString();
                tpa.Zip = string.IsNullOrEmpty(dataTable.Rows[0]["Zip"].ToString()) ? string.Empty : dataTable.Rows[0]["Zip"].ToString();
                tpa.Country = string.IsNullOrEmpty(dataTable.Rows[0]["Country"].ToString()) ? string.Empty : dataTable.Rows[0]["Country"].ToString();
                tpa.WebSite = string.IsNullOrEmpty(dataTable.Rows[0]["WebSite"].ToString()) ? string.Empty : dataTable.Rows[0]["WebSite"].ToString();
                tpa.Notes = string.IsNullOrEmpty(dataTable.Rows[0]["Notes"].ToString()) ? string.Empty : dataTable.Rows[0]["Notes"].ToString();
                tpa.Phone_number = string.IsNullOrEmpty(dataTable.Rows[0]["Phone_number"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone_number"].ToString();
                tpa.email = string.IsNullOrEmpty(dataTable.Rows[0]["email"].ToString()) ? string.Empty : dataTable.Rows[0]["email"].ToString();

            }
            return View(tpa);
        }

        [HttpPost]
        public ActionResult UpdateTcLocation(TPALocation tpa)
        {
            if (!ModelState.IsValid)
                return View(tpa);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClientLoc_Update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPAClient_LocID", tpa.TPAClient_LocID);


            sqlCommand.Parameters.AddWithValue("@TPAClient_Id", Convert.ToInt32(tpa.TPAClient_Id));

            if (!string.IsNullOrEmpty(tpa.Location_id))
                sqlCommand.Parameters.AddWithValue("@Location_id", tpa.Location_id);
            else
                sqlCommand.Parameters.AddWithValue("@Location_id", string.Empty);
            //if (!string.IsNullOrEmpty(tpa.TPA_Client))
            //    sqlCommand.Parameters.AddWithValue("@TPA_Client", tpa.TPA_Client);
            //else
            //    sqlCommand.Parameters.AddWithValue("@TPA_Client", string.Empty);

            if (!string.IsNullOrEmpty(tpa.Address_1))
                sqlCommand.Parameters.AddWithValue("@Address_1", tpa.Address_1);
            else
                sqlCommand.Parameters.AddWithValue("@Address_1", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Address_2))
                sqlCommand.Parameters.AddWithValue("@Address_2", tpa.Address_2);
            else
                sqlCommand.Parameters.AddWithValue("@Address_2", string.Empty);
            if (!string.IsNullOrEmpty(tpa.City))
                sqlCommand.Parameters.AddWithValue("@City", tpa.City);
            else
                sqlCommand.Parameters.AddWithValue("@City", string.Empty);
            if (!string.IsNullOrEmpty(tpa.State))
                sqlCommand.Parameters.AddWithValue("@State", tpa.State);
            else
                sqlCommand.Parameters.AddWithValue("@State", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Zip))
                sqlCommand.Parameters.AddWithValue("@Zip", tpa.Zip);
            else
                sqlCommand.Parameters.AddWithValue("@Zip", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Country))
                sqlCommand.Parameters.AddWithValue("@Country", tpa.Country);
            else
                sqlCommand.Parameters.AddWithValue("@Country", string.Empty);
            if (!string.IsNullOrEmpty(tpa.WebSite))
                sqlCommand.Parameters.AddWithValue("@WebSite", tpa.WebSite);
            else
                sqlCommand.Parameters.AddWithValue("@WebSite", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Notes))
                sqlCommand.Parameters.AddWithValue("@Notes", tpa.Notes);
            else
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            if (!string.IsNullOrEmpty(tpa.Phone_number))
                sqlCommand.Parameters.AddWithValue("@Phone_number", tpa.Phone_number);
            else
                sqlCommand.Parameters.AddWithValue("@Phone_number", string.Empty);
            if (!string.IsNullOrEmpty(tpa.email))
                sqlCommand.Parameters.AddWithValue("@email", tpa.email);
            else
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("TcLocationList", new { id = tpa.TPAClient_Id });
        }
        [BreadCrumb(Label = "Contacts")]
        public ActionResult ListTPAContact(string id)
        {
            ViewBag.locid1 = id;
            SqlCommand selectCommand12 = new SqlCommand("getlocid_tpa", new SqlConnection(TransConnString));
            selectCommand12.CommandType = CommandType.StoredProcedure;
            selectCommand12.Parameters.AddWithValue("@id", id);
            selectCommand12.Connection.Open();
            SqlDataReader reader = null;
            try
            {
                reader = selectCommand12.ExecuteReader();
                while (reader.Read())
                {
                    ViewBag.locid = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                ViewBag.locid = 0;
            }
            selectCommand12.Connection.Close();
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClient_Contact_list", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPAClient_LocID", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            List<TPAContact> Tpacontact = new List<TPAContact>();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
                TPAContact contact = new TPAContact();
                contact.Id = Convert.ToInt32(dataTable.Rows[index]["Id"].ToString());

                //contact.TPAClient_Id = Convert.ToInt32(dataTable.Rows[index]["TPAClient_Id"].ToString());

                contact.TPAClient_LocID = Convert.ToInt32(dataTable.Rows[index]["TPAClient_LocID"].ToString());
                if (!string.IsNullOrEmpty(dataTable.Rows[index]["firstname"].ToString()))
                    contact.firstname = dataTable.Rows[index]["firstname"].ToString();
                else
                    contact.firstname = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[index]["Lastname"].ToString()))
                    contact.Lastname = dataTable.Rows[index]["Lastname"].ToString();
                else
                    contact.Lastname = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[index]["cell"].ToString()))
                    contact.cell = dataTable.Rows[index]["cell"].ToString();
                else
                    contact.cell = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[index]["email"].ToString()))
                    contact.email = dataTable.Rows[index]["email"].ToString();
                else
                    contact.email = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[index]["officephone"].ToString()))
                    contact.officephone = dataTable.Rows[index]["officephone"].ToString();
                else
                    contact.officephone = string.Empty;
                Tpacontact.Add(contact);
            }

            return View(Tpacontact);
        }

        [BreadCrumb(Label = "New Contact")]
        public ActionResult InsertTPAContact(int id)
        {
            TPAContact tp = new TPAContact();
            tp.TPAClient_LocID = id;
            return View(tp);
        }
        [HttpPost]
        public ActionResult InsertTPAContact(TPAContact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClient_Contact_insert", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPAClient_LocID", contact.TPAClient_LocID);
            if (!string.IsNullOrEmpty(contact.firstname))
                sqlCommand.Parameters.AddWithValue("@firstname", contact.firstname);
            else
                sqlCommand.Parameters.AddWithValue("@firstname", string.Empty);
            if (!string.IsNullOrEmpty(contact.Lastname))
                sqlCommand.Parameters.AddWithValue("@Lastname", contact.Lastname);
            else
                sqlCommand.Parameters.AddWithValue("@Lastname", string.Empty);
            if (!string.IsNullOrEmpty(contact.cell))
                sqlCommand.Parameters.AddWithValue("@cell", contact.cell);
            else
                sqlCommand.Parameters.AddWithValue("@cell", string.Empty);
            if (contact.officephone != null)
                sqlCommand.Parameters.AddWithValue("@officephone", contact.officephone);
            else
                sqlCommand.Parameters.AddWithValue("@officephone", string.Empty);
            if (!string.IsNullOrEmpty(contact.email))
                sqlCommand.Parameters.AddWithValue("@email", contact.email);
            else
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            if (contact.Title != null)
                sqlCommand.Parameters.AddWithValue("@Title", (object)contact.Title);
            else
                sqlCommand.Parameters.AddWithValue("@Title", string.Empty);
            if (contact.Role != null)
                sqlCommand.Parameters.AddWithValue("@Role", (object)contact.Role);
            else
                sqlCommand.Parameters.AddWithValue("@Role", string.Empty);
            if (contact.Notes != null)
                sqlCommand.Parameters.AddWithValue("@Notes", (object)contact.Notes);
            else
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            if (contact.Email1 != null)
                sqlCommand.Parameters.AddWithValue("@Email1", (object)contact.Email1);
            else
                sqlCommand.Parameters.AddWithValue("@Email1", string.Empty);
            if (contact.Phone1 != null)
                sqlCommand.Parameters.AddWithValue("@Phone1", (object)contact.Phone1);
            else
                sqlCommand.Parameters.AddWithValue("@Phone1", string.Empty);
            if (contact.Third_Phone != null)
                sqlCommand.Parameters.AddWithValue("@Third_Phone", (object)contact.Third_Phone);
            else
                sqlCommand.Parameters.AddWithValue("@Third_Phone", string.Empty);

            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("ListTPAContact", new { id = contact.TPAClient_LocID });
        }
        [BreadCrumb(Label = "Update Conatct")]
        [HttpGet]
        public ActionResult UpdateTPAContact(string id)
        {
            //SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand selectCommand = new SqlCommand("Tbl_TpaClient_Contact_Edit", new SqlConnection(this.TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@Id", id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            TPAContact contact = new TPAContact();

            if (dataTable.Rows.Count > 0)
            {
                contact.TPAClient_LocID = Convert.ToInt32(dataTable.Rows[0]["TPAClient_LocID"].ToString());

                contact.Id = Convert.ToInt32(dataTable.Rows[0]["Id"].ToString());
                contact.firstname = string.IsNullOrEmpty(dataTable.Rows[0]["firstname"].ToString()) ? string.Empty : dataTable.Rows[0]["firstname"].ToString();
                contact.Lastname = string.IsNullOrEmpty(dataTable.Rows[0]["Lastname"].ToString()) ? string.Empty : dataTable.Rows[0]["Lastname"].ToString();
                contact.cell = string.IsNullOrEmpty(dataTable.Rows[0]["cell"].ToString()) ? string.Empty : dataTable.Rows[0]["cell"].ToString();
                contact.email = string.IsNullOrEmpty(dataTable.Rows[0]["email"].ToString()) ? string.Empty : dataTable.Rows[0]["email"].ToString();
                contact.officephone = string.IsNullOrEmpty(dataTable.Rows[0]["officephone"].ToString()) ? string.Empty : dataTable.Rows[0]["officephone"].ToString();
                contact.Title = string.IsNullOrEmpty(dataTable.Rows[0]["Title"].ToString()) ? string.Empty : dataTable.Rows[0]["Title"].ToString();
                contact.Role = string.IsNullOrEmpty(dataTable.Rows[0]["Role"].ToString()) ? string.Empty : dataTable.Rows[0]["Role"].ToString();
                contact.Notes = string.IsNullOrEmpty(dataTable.Rows[0]["Notes"].ToString()) ? string.Empty : dataTable.Rows[0]["Notes"].ToString();
                contact.Email1 = string.IsNullOrEmpty(dataTable.Rows[0]["Email1"].ToString()) ? string.Empty : dataTable.Rows[0]["Email1"].ToString();
                contact.Phone1 = string.IsNullOrEmpty(dataTable.Rows[0]["Phone1"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone1"].ToString();
                contact.Third_Phone = string.IsNullOrEmpty(dataTable.Rows[0]["Third_Phone"].ToString()) ? string.Empty : dataTable.Rows[0]["Third_Phone"].ToString();

            }

            return View(contact);
        }
        [HttpPost]
        public ActionResult UpdateTPAContact(TPAContact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClient_Contact_Update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.Parameters.AddWithValue("@Id", contact.Id);

            sqlCommand.Parameters.AddWithValue("@Id", contact.Id);
            sqlCommand.Parameters.AddWithValue("@TPAClient_LocID", contact.TPAClient_LocID);


            if (!string.IsNullOrEmpty(contact.firstname))
                sqlCommand.Parameters.AddWithValue("@firstname", contact.firstname);
            else
                sqlCommand.Parameters.AddWithValue("@firstname", string.Empty);
            if (!string.IsNullOrEmpty(contact.Lastname))
                sqlCommand.Parameters.AddWithValue("@Lastname", contact.Lastname);
            else
                sqlCommand.Parameters.AddWithValue("@Lastname", string.Empty);
            if (!string.IsNullOrEmpty(contact.cell))
                sqlCommand.Parameters.AddWithValue("@cell", contact.cell);
            else
                sqlCommand.Parameters.AddWithValue("@cell", string.Empty);
            if (contact.officephone != null)
                sqlCommand.Parameters.AddWithValue("@officephone", contact.officephone);
            else
                sqlCommand.Parameters.AddWithValue("@officephone", string.Empty);
            if (!string.IsNullOrEmpty(contact.email))
                sqlCommand.Parameters.AddWithValue("@email", contact.email);
            else
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            if (contact.Title != null)
                sqlCommand.Parameters.AddWithValue("@Title", (object)contact.Title);
            else
                sqlCommand.Parameters.AddWithValue("@Title", string.Empty);
            if (contact.Role != null)
                sqlCommand.Parameters.AddWithValue("@Role", (object)contact.Role);
            else
                sqlCommand.Parameters.AddWithValue("@Role", string.Empty);
            if (contact.Notes != null)
                sqlCommand.Parameters.AddWithValue("@Notes", (object)contact.Notes);
            else
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            if (contact.Email1 != null)
                sqlCommand.Parameters.AddWithValue("@Email1", (object)contact.Email1);
            else
                sqlCommand.Parameters.AddWithValue("@Email1", string.Empty);
            if (contact.Phone1 != null)
                sqlCommand.Parameters.AddWithValue("@Phone1", (object)contact.Phone1);
            else
                sqlCommand.Parameters.AddWithValue("@Phone1", string.Empty);
            if (contact.Third_Phone != null)
                sqlCommand.Parameters.AddWithValue("@Third_Phone", (object)contact.Third_Phone);
            else
                sqlCommand.Parameters.AddWithValue("@Third_Phone", string.Empty);

            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("ListTPAContact", new { id = contact.TPAClient_LocID });
        }

        public ActionResult DeleteTcContact(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClient_Contact_delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public ActionResult DeleteTcLocation(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClientLoc_delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPAClient_LocID", id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult TpaClientdelete(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Tbl_TpaClient_delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPAClient_Id", id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}
