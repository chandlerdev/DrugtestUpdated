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
    public class TpaLabController : Controller
    {
        string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
        // GET: TpaLab
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateTPaLab()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTPaLab(TpaLab_model Tpalab)
        {
            if (!ModelState.IsValid)
                return View(Tpalab);
            SqlConnection Con = new SqlConnection(TransConnString);
            SqlCommand cmd = new SqlCommand("Insert into Tpa_Labs (TPALab_Name) values(@TPALab_Name)", Con);
            //cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TPALab_Name", Tpalab.TPALab_Name);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            return RedirectToAction("TpaLabList");
        }
        public ActionResult TpaLabList()
        {
            SqlCommand selectCommand = new SqlCommand("Select * from Tpa_Labs", new SqlConnection(TransConnString));
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<TpaLab_model> Tpalablist = new List<TpaLab_model>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TpaLab_model tpalab = new TpaLab_model();
                tpalab.TPALab_Id = Convert.ToInt32(dataTable.Rows[i]["TPALab_Id"].ToString());

                if (!string.IsNullOrEmpty(dataTable.Rows[i]["TPALab_Name"].ToString()))
                    tpalab.TPALab_Name = dataTable.Rows[i]["TPALab_Name"].ToString();
                else
                    tpalab.TPALab_Name = string.Empty;
                Tpalablist.Add(tpalab);
            }
            return View(Tpalablist);

        }
        [HttpGet]
        public ActionResult TpaLabEdit(string id)
        {
            SqlCommand selectCommand = new SqlCommand("TPA_Labs_Edit", new SqlConnection(this.TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@TPALab_Id", id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            TpaLab_model TPA = new TpaLab_model();

            if (dataTable.Rows.Count > 0)
            {
                TPA.TPALab_Id = Convert.ToInt32(dataTable.Rows[0]["TPALab_Id"].ToString());
                TPA.TPALab_Name = string.IsNullOrEmpty(dataTable.Rows[0]["TPALab_Name"].ToString()) ? string.Empty : dataTable.Rows[0]["TPALab_Name"].ToString();
            }
            return View(TPA);
        }
        [HttpPost]
        public ActionResult TpaLabEdit(TpaLab_model TPA)
        {
            if (!ModelState.IsValid)
                return View(TPA);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPALab_Id", Convert.ToInt32(TPA.TPALab_Id));
            if (!string.IsNullOrEmpty(TPA.TPALab_Name))
                sqlCommand.Parameters.AddWithValue("@TPALab_Name", TPA.TPALab_Name);
            else
                sqlCommand.Parameters.AddWithValue("@TPALab_Name", string.Empty);

            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("TpaLabList");
        }
        public ActionResult TLLocationList(string id)
        {
            ViewBag.Locationid  = id.Trim();
            SqlCommand selectCommand = new SqlCommand("TPA_Labs_Loc_list", new SqlConnection(TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TPALab_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<TpaLabLocation> Tllist = new List<TpaLabLocation>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TpaLabLocation tpa = new TpaLabLocation();
                tpa.TPALab_Id = Convert.ToInt32(dataTable.Rows[i]["TPALab_Id"].ToString());

                tpa.TPALAB_LocID = Convert.ToInt32(dataTable.Rows[i]["TPALAB_LocID"].ToString());


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
                
                Tllist.Add(tpa);
            }
            return View(Tllist);
        }

        public ActionResult AddTlLocation(int id)

        {
            TpaLabLocation tp = new TpaLabLocation();
            tp.TPALab_Id = id;

            return View(tp);
        }
        [HttpPost]
        public ActionResult AddTlLocation(TpaLabLocation tpa)
        {
            if (!ModelState.IsValid)
                return View(tpa);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Loc_insert", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPALab_Id", tpa.TPALab_Id);   
            
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
         
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("TLLocationList", new { id = tpa.TPALab_Id });
        }
        [HttpGet]
        public ActionResult UpdateTlLocation(string id)
        {
            SqlCommand selectCommand = new SqlCommand("TPA_Labs_Loc_Edit", new SqlConnection(TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@TPALAB_LocID", id);
            sqlDataAdapter.Fill(dataTable);
            TpaLabLocation tpa = new TpaLabLocation();
            if (dataTable.Rows.Count > 0)
            {
                tpa.TPALAB_LocID = Convert.ToInt32(dataTable.Rows[0]["TPALAB_LocID"].ToString());
                tpa.TPALab_Id = Convert.ToInt32(dataTable.Rows[0]["TPALab_Id"].ToString());
                tpa.Address_1 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_1"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_1"].ToString();
                tpa.Address_2 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_2"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_2"].ToString();
                tpa.City = string.IsNullOrEmpty(dataTable.Rows[0]["City"].ToString()) ? string.Empty : dataTable.Rows[0]["City"].ToString();
                tpa.State = string.IsNullOrEmpty(dataTable.Rows[0]["State"].ToString()) ? string.Empty : dataTable.Rows[0]["State"].ToString();
                tpa.Zip = string.IsNullOrEmpty(dataTable.Rows[0]["Zip"].ToString()) ? string.Empty : dataTable.Rows[0]["Zip"].ToString();
                tpa.Country = string.IsNullOrEmpty(dataTable.Rows[0]["Country"].ToString()) ? string.Empty : dataTable.Rows[0]["Country"].ToString();
                
            }
            return View(tpa);
        }

        [HttpPost]
        public ActionResult UpdateTlLocation(TpaLabLocation tpa)
        {
            if (!ModelState.IsValid)
                return View(tpa);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Loc_update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPALAB_LocID", tpa.TPALAB_LocID);

            sqlCommand.Parameters.AddWithValue("@TPALab_Id", Convert.ToInt32(tpa.TPALab_Id));

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
           
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("TLLocationList", new { id = tpa.TPALab_Id });
        }

        public ActionResult ListTpaLocationContact(string id)
        {
            ViewBag.Locationid = id.Trim();
            SqlCommand selectCommand12 = new SqlCommand("select TPALab_Id from TPA_Labs_Loc where tpalab_locid=@id", new SqlConnection(TransConnString));
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
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Loc_Contact_list", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPALab_LocId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            List<Tpalablocationcontact> Tpacontact = new List<Tpalablocationcontact>();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
                Tpalablocationcontact contact = new Tpalablocationcontact();
                contact.TPALab_Contact_Id = Convert.ToInt32(dataTable.Rows[index]["TPALab_Contact_Id"].ToString());
                contact.TPALab_LocId = Convert.ToInt32(dataTable.Rows[index]["TPALab_LocId"].ToString());

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

        public ActionResult InsertTPALocContact(int id)
        {
            Tpalablocationcontact tp = new Tpalablocationcontact();
            tp.TPALab_LocId = id;
            return View(tp);
        }
        [HttpPost]
        public ActionResult InsertTPALocContact(Tpalablocationcontact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Loc_Contact_insert", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALab_LocId", contact.TPALab_LocId);
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

            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("ListTpaLocationContact", new { id = contact.TPALab_LocId });
        }

        [HttpGet]
        public ActionResult UpdateTPALocContact(string id)
        {
            SqlCommand selectCommand = new SqlCommand("TPA_Labs_Loc_Contact_edit", new SqlConnection(this.TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("TPALab_Contact_Id", id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            Tpalablocationcontact contact = new Tpalablocationcontact();

            if (dataTable.Rows.Count > 0)
            {
                contact.TPALab_LocId = Convert.ToInt32(dataTable.Rows[0]["TPALab_LocId"].ToString());

                contact.TPALab_Contact_Id = Convert.ToInt32(dataTable.Rows[0]["TPALab_Contact_Id"].ToString());
                contact.firstname = string.IsNullOrEmpty(dataTable.Rows[0]["firstname"].ToString()) ? string.Empty : dataTable.Rows[0]["firstname"].ToString();
                contact.Lastname = string.IsNullOrEmpty(dataTable.Rows[0]["Lastname"].ToString()) ? string.Empty : dataTable.Rows[0]["Lastname"].ToString();
                contact.cell = string.IsNullOrEmpty(dataTable.Rows[0]["cell"].ToString()) ? string.Empty : dataTable.Rows[0]["cell"].ToString();
                contact.email = string.IsNullOrEmpty(dataTable.Rows[0]["email"].ToString()) ? string.Empty : dataTable.Rows[0]["email"].ToString();
                contact.officephone = string.IsNullOrEmpty(dataTable.Rows[0]["officephone"].ToString()) ? string.Empty : dataTable.Rows[0]["officephone"].ToString();
            }

            return View(contact);
        }
        [HttpPost]
        public ActionResult UpdateTPALocContact(Tpalablocationcontact contact)
        {
            if (!ModelState.IsValid)
                return View(contact);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Loc_Contact_update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPALab_Contact_Id", contact.TPALab_Contact_Id);
            sqlCommand.Parameters.AddWithValue("@TPALab_LocId", contact.TPALab_LocId);


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

            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("ListTpaLocationContact", new { id = contact.TPALab_LocId });
        }

        public ActionResult TpaLabService(string id)
        {
            ViewBag.LabId = id.Trim();
            SqlCommand sqlCommand = new SqlCommand("TPALab_Service_Grp_List", new SqlConnection(TransConnString));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALab_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<TpaLabservice> servicesList = new List<TpaLabservice>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TpaLabservice service = new TpaLabservice();
                service.TPALab_Service_Id = Convert.ToInt32(dataTable.Rows[i]["TPALab_Service_Id"].ToString());

                service.TPALab_Id = Convert.ToInt32(dataTable.Rows[i]["TPALab_Id"].ToString());

                if (!string.IsNullOrEmpty(dataTable.Rows[i]["Service_Grp_Name"].ToString()))
                    service.Service_Grp_Name = dataTable.Rows[i]["Service_Grp_Name"].ToString();
                else
                    service.Service_Grp_Name = string.Empty;
                servicesList.Add(service);
            }
            return View(servicesList);

        }
        public ActionResult CreateTpalabService(int id)
        {
            TpaLabservice ts = new TpaLabservice();
            ts.TPALab_Id = id;
            return View(ts);
        }
        [HttpPost]
        public ActionResult CreateTpalabService(TpaLabservice service)
        {
            if (!ModelState.IsValid)
                return View(service);
            SqlConnection Con = new SqlConnection(TransConnString);
            SqlCommand cmd = new SqlCommand("TPALab_Service_Grp_Insert", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Service_Grp_Name", service.Service_Grp_Name);
            cmd.Parameters.AddWithValue("@TPALab_Id", service.TPALab_Id);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            return RedirectToAction("TpaLabService",new { id=service.TPALab_Id});

        }

        [HttpGet]
        public ActionResult UpdateTpaLabService(int id)
        {
            
            List<TpaLabsubservice> SubservicesList = new List<TpaLabsubservice>();
            TpaLabservice service = new TpaLabservice();
            SqlCommand selectCommand = new SqlCommand("TPALab_Service_Grp_Edit", this.conn);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TPALab_Service_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                service.TPALab_Service_Id = Convert.ToInt32(dataTable.Rows[0]["TPALab_Service_Id"].ToString());
                service.TPALab_Id = Convert.ToInt32(dataTable.Rows[0]["TPALab_Id"].ToString());
                service.Service_Grp_Name = string.IsNullOrEmpty(dataTable.Rows[0]["Service_Grp_Name"].ToString()) ? string.Empty : dataTable.Rows[0]["Service_Grp_Name"].ToString();
            }
            
            SqlCommand sqlCommand1 = new SqlCommand("TPA_Lab_SubService_Grp_List", this.conn);
            sqlCommand1.CommandType = CommandType.StoredProcedure;
            sqlCommand1.Parameters.AddWithValue("@TPALab_Service_Id", id);
            this.conn.Open();
            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                    SubservicesList.Add(new TpaLabsubservice()
                    {
                        TPALab_SubService_Id = Convert.ToInt32(sqlDataReader["TPALab_SubService_Id"].ToString()),
                        TPAlab_Service_Description = sqlDataReader["TPAlab_Service_Description"].ToString(),
                        TPALab_Service_Id = Convert.ToInt32(sqlDataReader["TPALab_Service_Id"].ToString()),
                        TPAlab_Service_Ext_Description = sqlDataReader["TPAlab_Service_Ext_Description"].ToString(),
                        Service_Charges = Convert.ToDecimal(sqlDataReader["Service_Charges"].ToString()),
                        Client_Billing_Charges = Convert.ToDecimal(sqlDataReader["Client_Billing_Charges"].ToString())
                    });
                sqlDataReader.Close();
                this.conn.Close();
            }
            else
            {
                sqlDataReader.Close();
                this.conn.Close();
            }
            ViewData["Data"] = SubservicesList;
            return (ActionResult)this.View((object)service);

        }

        [HttpPost]
        public ActionResult UpdateTpaLabService(TpaLabservice service)
        {
            if (!ModelState.IsValid)
                return View(service);
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPALab_Service_Grp_Update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@TPALab_Id", Convert.ToInt32(service.TPALab_Id));

            sqlCommand.Parameters.AddWithValue("@TPALab_Service_Id", Convert.ToInt32(service.TPALab_Service_Id));

            if (!string.IsNullOrEmpty(service.Service_Grp_Name))
                sqlCommand.Parameters.AddWithValue("@Service_Grp_Name", service.Service_Grp_Name);
            else
                sqlCommand.Parameters.AddWithValue("@Service_Grp_Name", string.Empty);



            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("TpaLabService", new { id = service.TPALab_Id });
        }


        public ActionResult AddSubService(int id)
        {
            TpaLabsubservice tpa = new TpaLabsubservice();
            tpa.TPALab_Service_Id= id;
            return View(tpa);
        }
        [HttpPost]
        public ActionResult AddSubService(TpaLabsubservice service)
        {
            if (!ModelState.IsValid)
                return View(service);
            SqlConnection Con = new SqlConnection(TransConnString);
            SqlCommand cmd = new SqlCommand("TPA_Lab_SubService_Grp_Insert", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(service.TPAlab_Service_Description))
                cmd.Parameters.AddWithValue("@TPAlab_Service_Description", service.TPAlab_Service_Description);
            else
                cmd.Parameters.AddWithValue("@TPAlab_Service_Description", string.Empty);

            cmd.Parameters.AddWithValue("@TPALab_Service_Id", service.TPALab_Service_Id);
            if (!string.IsNullOrEmpty(service.TPAlab_Service_Ext_Description))
                cmd.Parameters.AddWithValue("@TPAlab_Service_Ext_Description", service.TPAlab_Service_Ext_Description);
            else
                cmd.Parameters.AddWithValue("@TPAlab_Service_Ext_Description", string.Empty);

            cmd.Parameters.AddWithValue("@Service_Charges", service.Service_Charges);
            cmd.Parameters.AddWithValue("@Client_Billing_Charges", service.Client_Billing_Charges);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            return RedirectToAction("UpdateTpaLabService", new { id=service.TPALab_Service_Id});


        }

        [HttpGet]
        public ActionResult UpdateSubService(string id)
        {

            SqlCommand selectCommand = new SqlCommand("TPA_Lab_SubService_Grp_Edit", new SqlConnection(TransConnString));
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@TPALab_SubService_Id", id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            TpaLabsubservice service = new TpaLabsubservice();
            if (dataTable.Rows.Count > 0)
            {
                service.TPALab_SubService_Id = Convert.ToInt32(dataTable.Rows[0]["TPALab_SubService_Id"].ToString());
                service.TPALab_Service_Id = Convert.ToInt32(dataTable.Rows[0]["TPALab_Service_Id"].ToString());
                service.TPAlab_Service_Description = string.IsNullOrEmpty(dataTable.Rows[0]["TPAlab_Service_Description"].ToString()) ? string.Empty : dataTable.Rows[0]["TPAlab_Service_Description"].ToString();
                service.TPAlab_Service_Ext_Description = string.IsNullOrEmpty(dataTable.Rows[0]["TPAlab_Service_Ext_Description"].ToString()) ? string.Empty : dataTable.Rows[0]["TPAlab_Service_Ext_Description"].ToString();
                service.Service_Charges = Convert.ToInt32(dataTable.Rows[0]["Service_Charges"].ToString());
                service.Client_Billing_Charges = Convert.ToInt32(dataTable.Rows[0]["Client_Billing_Charges"].ToString());

            }


            return View(service);
        }
        [HttpPost]
        public ActionResult UpdateSubService(TpaLabsubservice service)
        {
            if (!ModelState.IsValid)
                return View(service);
            SqlConnection Con = new SqlConnection(TransConnString);
            SqlCommand cmd = new SqlCommand("TPA_Lab_SubService_Grp_Update", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TPALab_SubService_Id", service.TPALab_SubService_Id);
            if (!string.IsNullOrEmpty(service.TPAlab_Service_Description))
                cmd.Parameters.AddWithValue("@TPAlab_Service_Description", service.TPAlab_Service_Description);
            else
                cmd.Parameters.AddWithValue("@TPAlab_Service_Description", string.Empty);

            if (!string.IsNullOrEmpty(service.TPAlab_Service_Ext_Description))
                cmd.Parameters.AddWithValue("@TPAlab_Service_Ext_Description", service.TPAlab_Service_Ext_Description);
            else
                cmd.Parameters.AddWithValue("@TPAlab_Service_Ext_Description", string.Empty);

            cmd.Parameters.AddWithValue("@Service_Charges", service.Service_Charges);
            cmd.Parameters.AddWithValue("@Client_Billing_Charges", service.Client_Billing_Charges);

            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
            return RedirectToAction("UpdateTpaLabService", new { id = service.TPALab_Service_Id });
        }

        public ActionResult DeleteSubService(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("TPA_Lab_SubService_Grp_delete",this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALab_SubService_Id", (object)id);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult DeleteTLContact(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Loc_Contact_delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALab_Contact_Id", id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public ActionResult DeleteTLLocation(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_Loc_delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALab_LocId", id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult TpaLabdelete(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Labs_delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALAB_Id", id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public ActionResult DeleteTlSubService(int id)
        {
            SqlConnection conn = new SqlConnection(TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPA_Lab_SubService_Grp_delete");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALab_SubService_Id", (object)id);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult DeleteTlService(int id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("TPALab_Service_Grp_Delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@TPALab_Service_Id", id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
            //SqlConnection conn = new SqlConnection(TransConnString);
            //SqlCommand sqlCommand = new SqlCommand("TPALab_Service_Grp_Delete",this.conn);
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.Parameters.AddWithValue("@TPALab_Service_Id", (object)id);
            //conn.Open();
            //sqlCommand.ExecuteNonQuery();
            //conn.Close();
            //return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public ActionResult TpaListSubService(string id)
        {

            SqlCommand sqlCommand = new SqlCommand("TPA_Lab_SubService_Grp_List", new SqlConnection(TransConnString));
            sqlCommand.Parameters.AddWithValue("@TPALab_Service_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<TpaLabsubservice> servicesList = new List<TpaLabsubservice>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                TpaLabsubservice Subservice = new TpaLabsubservice();

                Subservice.TPALab_SubService_Id = Convert.ToInt32(dataTable.Rows[i]["TPALab_SubService_Id"].ToString());
                Subservice.TPALab_Service_Id = Convert.ToInt32(dataTable.Rows[i]["TPALab_Service_Id"].ToString());
                Subservice.TPALab_Id = Convert.ToInt32(dataTable.Rows[i]["TPALab_Id"].ToString());
                Subservice.TPAlab_Service_Description = string.IsNullOrEmpty(dataTable.Rows[i]["TPAlab_Service_Description"].ToString()) ? string.Empty : dataTable.Rows[i]["TPAlab_Service_Description"].ToString();
                Subservice.TPAlab_Service_Ext_Description = string.IsNullOrEmpty(dataTable.Rows[i]["TPAlab_Service_Ext_Description"].ToString()) ? string.Empty : dataTable.Rows[i]["TPAlab_Service_Ext_Description"].ToString();
                Subservice.Service_Charges = Convert.ToInt32(dataTable.Rows[i]["Service_Charges"].ToString());
                Subservice.Client_Billing_Charges = Convert.ToInt32(dataTable.Rows[i]["Client_Billing_Charges"].ToString());



                servicesList.Add(Subservice);
            }
            return View(servicesList);

        }

    }
}