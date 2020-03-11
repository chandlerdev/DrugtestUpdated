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
using System.IO;
using System.Web.UI.WebControls;

namespace TransCanadaDemo.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        String constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;


        // GET: Client
        public ActionResult Index()
        {
            TcClient client = new TcClient();

            return View(client);
        }


        public ActionResult List(string id)
        {
            List<TcClient> listtcclient = new List<TcClient>();
            TcClient threescreen;

            threescreen = new TcClient();
            threescreen.AccountsId = "101";
            threescreen.MainNumber = "456-852-123";
            threescreen.ClientStreetAddress = "3333 Raleigh St";
            threescreen.ClientCity = "Houston";
            threescreen.ClientZip = "77021";
            threescreen.LocationSpecificNotes = "Notes";
            threescreen.ConatctWithANY = "Dev Chandler";
            threescreen.ShyBladdersLugWithNo = "456-856";
            listtcclient.Add(threescreen);

            threescreen = new TcClient();
            threescreen.AccountsId = "101";
            threescreen.MainNumber = "852-852-123";
            threescreen.ClientStreetAddress = "450 Evening Bazar";
            threescreen.ClientCity = "Houston";
            threescreen.ClientZip = "77085";
            threescreen.LocationSpecificNotes = "Notes for Evening Bazar";
            threescreen.ConatctWithANY = "Yogesh";
            threescreen.ShyBladdersLugWithNo = "856-989";
            listtcclient.Add(threescreen);

            threescreen = new TcClient();
            threescreen.AccountsId = "102";
            threescreen.MainNumber = "852-852-123";
            threescreen.ClientStreetAddress = "450 Evening Bazar";
            threescreen.ClientCity = "Houston";
            threescreen.ClientZip = "77085";
            threescreen.LocationSpecificNotes = "Notes for Evening Bazar";
            threescreen.ConatctWithANY = "Yogesh";
            threescreen.ShyBladdersLugWithNo = "856-989";
            listtcclient.Add(threescreen);

            threescreen = new TcClient();
            threescreen.AccountsId = "103";
            threescreen.MainNumber = "456-852-123";
            threescreen.ClientStreetAddress = "3333 Raleigh St";
            threescreen.ClientCity = "Houston";
            threescreen.ClientZip = "77021";
            threescreen.LocationSpecificNotes = "Notes";
            threescreen.ConatctWithANY = "Dev Chandler";
            threescreen.ShyBladdersLugWithNo = "456-856";
            listtcclient.Add(threescreen);


            return View(listtcclient.Where(x => x.AccountsId == id));
        }

        public ActionResult Back_ClientList(string mainnumber)
        {

            return RedirectToAction("List", "Client", new { @id = mainnumber });
            //return View();
        }
        public ActionResult NewClient()
        {
            Client client = new Client();
            client.Cities = GetAllCities(string.Empty);
            return View(client);
        }
        [HttpPost]
        public ActionResult NewClient(Client client)
        {
            if(ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand command = new SqlCommand("proc_new_client", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Client_Name", client.Client_Name);
                command.Parameters.AddWithValue("@Contact_Person",client.Contact_Person);
                command.Parameters.AddWithValue("@Address_1", client.Address_1);
                command.Parameters.AddWithValue("@Address_2", client.@Address_2);
                command.Parameters.AddWithValue("@City", client.City);
                command.Parameters.AddWithValue("@State", client.State);
                command.Parameters.AddWithValue("@Country", client.Country);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Fax", client.Fax);
                command.Parameters.AddWithValue("@Website", client.WebSite);
                command.Parameters.AddWithValue("@zip", client.Zip);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                ClientController clientController = new ClientController();
                client.Cities = clientController.GetAllCities(string.Empty);
                return View();
            }
            return RedirectToAction("Clients");
        }
        public ActionResult Edit_Client(string mainnumber)
        {
            return RedirectToAction("Index", "Client");
        }

        public ActionResult Create_Client(string mainnumber)
        {
            return RedirectToAction("Index", "Client");
        }

        public ActionResult Delete_Client(string mainnumber)
        {
            TempData["msg"] = "<script>alert('Client Deleted');</script>";

            return RedirectToAction("List", "Client");
        }

        public ActionResult Detail_Client()
        {
            TcClient client = new TcClient();

            return View(client);
        }





        public static List<SelectListItem> ListClientType()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();

            string query = "Select StateName,StateCode from  Usstates";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["StateName"].ToString(),
                    Value = dt.Rows[i]["StateCode"].ToString()
                });
            }

            return ls;
        }

        public static List<SelectListItem> ListClientState()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();

            string query = "Select StateName,StateCode from  Usstates";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["StateName"].ToString(),
                    Value = dt.Rows[i]["StateCode"].ToString()
                });
            }

            return ls;
        }

        public static List<SelectListItem> Listi3ScreenAccess()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "No",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Full Acces - Voice, Email, Web, Fax",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Full Access - Voice, Web, Email/Fax only results w/action required",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "Web Access Only",
                    Value = "4"
                },

              };
            return ls;
        }

        public static List<SelectListItem> BackgroundScreeningAccess()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "No",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Yes - On-line Access Only",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Yes - Online & Email all results",
                    Value = "3"
                },

              };
            return ls;
        }
        public static List<SelectListItem> ListCategory()
        {

            List<SelectListItem> Lc = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "Dot",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Non Dot",
                    Value = "2"
                },

              };
            return Lc;
        }

        public static List<SelectListItem> ListActive()
        {

            List<SelectListItem> La = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "Active",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Non Active",
                    Value = "0"
                },

              };
            return La;
        }
        public static List<SelectListItem> OccupationalMedicineAccess()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
               new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "No",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Yes - On-line Access Only",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Yes - Online & Email all results",
                    Value = "3"
                },

              };
            return ls;
        }

        public static List<SelectListItem> ListCompanyProtocolsPolicies()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
               new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Corporate",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Client Specific: ________________________",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "DOT FMCSA",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "DOT PHMSA",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = "DOT USCG",
                    Value = "5"
                },
                new SelectListItem
                {
                    Text = "DOT FAA",
                    Value = "6"
                },
                new SelectListItem
                {
                    Text = "DOT FTA",
                    Value = "7"
                },
                new SelectListItem
                {
                    Text = "DOT FRA",
                    Value = "8"
                },


              };
            return ls;
        }

        public static List<SelectListItem> ListLabAccountsSampleType()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Urine",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Hair",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Oral Fluid",
                    Value = "3"
                },

              };
            return ls;
        }

        public static List<SelectListItem> ListBackgroundPackagePreEmploy()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "FMCSA",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "PHMSA",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "FRA",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "FTA",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = "FAA",
                    Value = "5"
                },
                new SelectListItem
                {
                    Text = "USCG",
                    Value = "6"
                },


              };
            return ls;
        }

        public static List<SelectListItem> ListBackgroundPackageAnnual()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "FMCSA",
                    Value = "1"
                },

              };
            return ls;
        }

        public static List<SelectListItem> ListRandomTestingPoolName()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Participant",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Location",
                    Value = "2"
                },


              };
            return ls;
        }

        public static List<SelectListItem> ListRandomTestingOwnerType()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Customer",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Consortium",
                    Value = "2"
                },


              };
            return ls;
        }

        public static List<SelectListItem> ListRandomTestingDotNonDot()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "DOT",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Non-DOT",
                    Value = "2"
                },


              };
            return ls;
        }

        public static List<SelectListItem> ListRandomTestingDOTAgency()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "FMCSA",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "PHMSA",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "FTA",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "FRA",
                    Value = "4"
                },
                new SelectListItem
                {
                    Text = "FAA",
                    Value = "5"
                },
                new SelectListItem
                {
                    Text = "USCG",
                    Value = "6"
                },

              };
            return ls;
        }


        public static List<SelectListItem> ListEmpty()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },

              };
            return ls;
        }

        public static List<SelectListItem> ListLabCCF()
        {

            List<SelectListItem> ls = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Use FormFox",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Paper CCF's @ WSS",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Partner Facilities",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "Client Locations",
                    Value = "4"
                },



              };
            return ls;
        }



        //public ActionResult AutoComplete(string term)
        //{
        //    if (!String.IsNullOrEmpty(term))
        //    {
        //        var list = ListClientType().Where(c => c.Text.ToUpper().Contains(term.ToUpper())).Select(c => new { Name = c.Text, ID = c.Value })
        //            .ToList();
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        var list = ListClientType().Select(c => new { Name = c.Text, ID = c.Value })
        //            .ToList();
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult Edit(int id)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("get_client_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Client TC = new Client();
            
            if (dt.Rows.Count>0)
            {

                TC.client_id = id;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Client_Name"].ToString()))
                {

                    TC.Client_Name = dt.Rows[0]["Client_Name"].ToString();

                }
                else
                {
                    TC.Client_Name = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_Person"].ToString()))
                {

                    TC.Contact_Person = dt.Rows[0]["Contact_Person"].ToString();

                }
                else
                {
                    TC.Contact_Person = string.Empty;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["Address_1"].ToString()))
                {
                    TC.Address_1 = dt.Rows[0]["Address_1"].ToString();
                }
                else
                {
                    TC.Address_1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Address_2"].ToString()))
                {
                    TC.Address_2 = dt.Rows[0]["Address_2"].ToString();
                }
                else
                {
                    TC.Address_2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["City"].ToString()))
                {
                    TC.City = dt.Rows[0]["City"].ToString();
                }
                else
                {
                    TC.City = string.Empty;
                }
                
                if (!string.IsNullOrEmpty(dt.Rows[0]["State"].ToString()))
                {
                    //List<SelectListItem> selectListItems = ListClientState();
                    //string Client_State = selectListItems.Where(p => p.Value == dt.Rows[0]["State"].ToString()).First().Text;
                    //TC.State = Client_State;
                    TC.State = dt.Rows[0]["State"].ToString();
                    TC.Cities = GetAllCities(TC.State);
                }
                else
                {
                    TC.State = string.Empty;
                    TC.Cities = GetAllCities(string.Empty);
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Phone"].ToString()))
                {
                    TC.Phone = dt.Rows[0]["Phone"].ToString();
                }
                else
                {
                    TC.Phone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Zip"].ToString()))
                {
                    TC.Zip = dt.Rows[0]["Zip"].ToString();
                }
                else
                {
                    TC.Zip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Fax"].ToString()))
                {
                    TC.Fax = dt.Rows[0]["Fax"].ToString();
                }
                else
                {
                    TC.Fax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Email"].ToString()))
                {
                    TC.Email = dt.Rows[0]["Email"].ToString();
                }
                else
                {
                    TC.Email = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Country"].ToString()))
                {
                    TC.Country = dt.Rows[0]["Country"].ToString();
                }
                else
                {
                    TC.Country = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["WebSite"].ToString()))
                {
                    TC.WebSite = dt.Rows[0]["WebSite"].ToString();
                }
                else
                {
                    TC.WebSite = string.Empty;
                }
            }
            return View(TC);
        }
        [HttpPost]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand command = new SqlCommand("proc_update_client", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", client.client_id);
                command.Parameters.AddWithValue("@Client_Name", client.Client_Name);
                command.Parameters.AddWithValue("@Contact_Person", client.Contact_Person);
                command.Parameters.AddWithValue("@Address_1", client.Address_1);
                command.Parameters.AddWithValue("@Address_2", client.@Address_2);
                command.Parameters.AddWithValue("@City", client.City);
                command.Parameters.AddWithValue("@State", client.State);
                command.Parameters.AddWithValue("@Country", client.Country);
                command.Parameters.AddWithValue("@Email", client.Email);
                command.Parameters.AddWithValue("@Phone", client.Phone);
                command.Parameters.AddWithValue("@Fax", client.Fax);
                command.Parameters.AddWithValue("@Website", client.WebSite);
                command.Parameters.AddWithValue("@zip", client.Zip);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                ClientController clientController = new ClientController();
                client.Cities = clientController.GetAllCities(string.Empty);
                return View();
            }
            return RedirectToAction("Clients");
        }

        public ActionResult Clients()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("Proc_get_Client", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Client> clientList = new List<Client>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                Client TC = new Client();
                if (!string.IsNullOrEmpty(dt.Rows[j]["Client_Id"].ToString()))
                {

                    TC.client_id =Convert.ToInt32(dt.Rows[j]["Client_Id"].ToString());

                }
                else
                {
                    TC.client_id = 0;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Client_Name"].ToString()))
                {

                    TC.Client_Name = dt.Rows[j]["Client_Name"].ToString();

                }
                else
                {
                    TC.Client_Name =string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Contact_Person"].ToString()))
                {

                    TC.Contact_Person = dt.Rows[j]["Contact_Person"].ToString();

                }
                else
                {
                    TC.Contact_Person = string.Empty;
                }
                
                if (!string.IsNullOrEmpty(dt.Rows[j]["Address_1"].ToString()))
                {
                    TC.Address_1 = dt.Rows[j]["Address_1"].ToString();
                }
                else
                {
                    TC.Address_1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Address_2"].ToString()))
                {
                    TC.Address_2 = dt.Rows[j]["Address_2"].ToString();
                }
                else
                {
                    TC.Address_2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["City"].ToString()))
                {
                    TC.City = dt.Rows[j]["City"].ToString();
                }
                else
                {
                    TC.City = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["State"].ToString()))
                {
                    List<SelectListItem> selectListItems = ListClientState();
                    string Client_State = selectListItems.Where(p => p.Value == dt.Rows[j]["State"].ToString()).First().Text;
                    TC.State = Client_State;
                }
                else
                {
                    TC.State = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Phone"].ToString()))
                {
                    TC.Phone = dt.Rows[j]["Phone"].ToString();
                }
                else
                {
                    TC.Phone = string.Empty;
                }

                clientList.Add(TC);



            }

            return View(clientList);
        }
        public ActionResult Clientlist()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("ListTCC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountsId", System.Web.HttpContext.Current.Session["Account_id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<TcClient> clientList = new List<TcClient>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                TcClient TC = new TcClient();
                if (!string.IsNullOrEmpty(dt.Rows[j]["Client_Id"].ToString()))
                {
                    
                    TC.clientid = dt.Rows[j]["Client_Id"].ToString();
                }
                else
                {
                    TC.clientid = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["ClientType_LI"].ToString()))
                {
                    List<SelectListItem> selectListItems = ListClientType();
                    string Client_type = selectListItems.Where(p => p.Value == dt.Rows[j]["ClientType_LI"].ToString()).First().Text;
                    TC.ClientType_LI = Client_type;
                }
                else
                {
                    TC.ClientType_LI = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["ClientStreetAddress"].ToString()))
                {
                    TC.ClientStreetAddress = dt.Rows[j]["ClientStreetAddress"].ToString();
                }
                else
                {
                    TC.ClientStreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["ClientCity"].ToString()))
                {
                    TC.ClientCity = dt.Rows[j]["ClientCity"].ToString();
                }
                else
                {
                    TC.ClientCity = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["ClientState"].ToString()))
                {
                    List<SelectListItem> selectListItems = ListClientState();
                    string Client_State = selectListItems.Where(p => p.Value == dt.Rows[j]["ClientState"].ToString()).First().Text;
                    TC.ClientState = Client_State;
                }
                else
                {
                    TC.ClientState = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["ClientZip"].ToString()))
                {
                    TC.ClientZip = dt.Rows[j]["ClientZip"].ToString();
                }
                else
                {
                    TC.ClientZip = string.Empty;
                }
               
                clientList.Add(TC);



            }
          
            return View(clientList);
        }

        public ActionResult InsertClient()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult InsertClient(TcClient Tc)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("InsertClient", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Createdby", User.Identity.GetUserName());
            cmd.Parameters.AddWithValue("@Updatedby", User.Identity.GetUserName());
            if (System.Web.HttpContext.Current.Session["Account_id"]!=null)
            {
                cmd.Parameters.AddWithValue("@AccountsId", System.Web.HttpContext.Current.Session["Account_id"]);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AccountsId", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientType_LI))
            {
                cmd.Parameters.AddWithValue("@ClientType_LI", Tc.ClientType_LI);
            }
            else
            {

                cmd.Parameters.AddWithValue("@ClientType_LI", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientStreetAddress))
            {
                cmd.Parameters.AddWithValue("@ClientStreetAddress", Tc.ClientStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientCity))
            {
                cmd.Parameters.AddWithValue("@ClientCity", Tc.ClientCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientState))
            {
                cmd.Parameters.AddWithValue("@ClientState", Tc.ClientState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientZip))
            {
                cmd.Parameters.AddWithValue("@ClientZip", Tc.ClientZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.MainNumber))
            {
                cmd.Parameters.AddWithValue("@MainNumber", Tc.MainNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MainNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationSpecificNotes))
            {
                cmd.Parameters.AddWithValue("@LocationSpecificNotes", Tc.LocationSpecificNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationSpecificNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ConatctWithANY))
            {
                cmd.Parameters.AddWithValue("@ConatctWithANY", Tc.ConatctWithANY);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ConatctWithANY", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ShyBladdersLugWithNo))
            {
                cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", Tc.ShyBladdersLugWithNo);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientSample))
            {
                cmd.Parameters.AddWithValue("@ClientSample", Tc.ClientSample);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientSample", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RefusalToTest))
            {
                cmd.Parameters.AddWithValue("@RefusalToTest", Tc.RefusalToTest);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RefusalToTest", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BatConfirmedPostive))
            {
                cmd.Parameters.AddWithValue("@BatConfirmedPostive", Tc.BatConfirmedPostive);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BatConfirmedPostive", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CancelledOrIncompleteTests))
            {
                cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", Tc.CancelledOrIncompleteTests);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.AuthorizationFormsSentVia))
            {
                cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", Tc.AuthorizationFormsSentVia);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerFullName))
            {
                cmd.Parameters.AddWithValue("@DerFullName", Tc.DerFullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerFullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerJobTitle))
            {
                cmd.Parameters.AddWithValue("@DerJobTitle", Tc.DerJobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerJobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerOfficeNumber))
            {
                cmd.Parameters.AddWithValue("@DerOfficeNumber", Tc.DerOfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerOfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerCellPhone))
            {
                cmd.Parameters.AddWithValue("@DerCellPhone", Tc.DerCellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerCellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerFax))
            {
                cmd.Parameters.AddWithValue("@DerFax", Tc.DerFax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerFax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerEmail))
            {
                cmd.Parameters.AddWithValue("@DerEmail", Tc.DerEmail);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerEmail", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerStreetAddress))
            {
                cmd.Parameters.AddWithValue("@DerStreetAddress", Tc.DerStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerCity))
            {
                cmd.Parameters.AddWithValue("@DerCity", Tc.DerCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerState))
            {
                cmd.Parameters.AddWithValue("@DerState", Tc.DerState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerZip))
            {
                cmd.Parameters.AddWithValue("@DerZip", Tc.DerZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Deri3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Deri3ScreenAccess", Tc.Deri3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Deri3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerBackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@DerBackgroundScreeningAccess", Tc.DerBackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerBackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerOccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@DerOccupationalMedicineAccess", Tc.DerOccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerOccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2FullName))
            {
                cmd.Parameters.AddWithValue("@Der2FullName", Tc.Der2FullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2FullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2JobTitle))
            {
                cmd.Parameters.AddWithValue("@Der2JobTitle", Tc.Der2JobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2JobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2OfficeNumber))
            {
                cmd.Parameters.AddWithValue("@Der2OfficeNumber", Tc.Der2OfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2OfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2CellPhone))
            {
                cmd.Parameters.AddWithValue("@Der2CellPhone", Tc.Der2CellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2CellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2Fax))
            {
                cmd.Parameters.AddWithValue("@Der2Fax", Tc.Der2Fax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2Fax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2Email))
            {
                cmd.Parameters.AddWithValue("@Der2Email", Tc.Der2Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2Email", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2StreetAddress))
            {
                cmd.Parameters.AddWithValue("@Der2StreetAddress", Tc.Der2StreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2StreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2City))
            {
                cmd.Parameters.AddWithValue("@Der2City", Tc.Der2City);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2City", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2State))
            {
                cmd.Parameters.AddWithValue("@Der2State", Tc.Der2State);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2State", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2Zip))
            {
                cmd.Parameters.AddWithValue("@Der2Zip", Tc.Der2Zip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2Zip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2i3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Der2i3ScreenAccess", Tc.Der2i3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2i3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2BackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@Der2BackgroundScreeningAccess", Tc.Der2BackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2BackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2OccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@Der2OccupationalMedicineAccess", Tc.Der2OccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2OccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1FullName))
            {
                cmd.Parameters.AddWithValue("@Contact1FullName", Tc.Contact1FullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1FullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1JobTitle))
            {
                cmd.Parameters.AddWithValue("@Contact1JobTitle", Tc.Contact1JobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1JobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1OfficeNumber))
            {
                cmd.Parameters.AddWithValue("@Contact1OfficeNumber", Tc.Contact1OfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1OfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1CellPhone))
            {
                cmd.Parameters.AddWithValue("@Contact1CellPhone", Tc.Contact1CellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1CellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1Fax))
            {
                cmd.Parameters.AddWithValue("@Contact1Fax", Tc.Contact1Fax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1Fax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1Email))
            {
                cmd.Parameters.AddWithValue("@Contact1Email", Tc.Contact1Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1Email", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1StreetAddress))
            {
                cmd.Parameters.AddWithValue("@Contact1StreetAddress", Tc.Contact1StreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1StreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1City))
            {
                cmd.Parameters.AddWithValue("@Contact1City", Tc.Contact1City);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1City", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1State))
            {
                cmd.Parameters.AddWithValue("@Contact1State", Tc.Contact1State);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1State", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1Zip))
            {
                cmd.Parameters.AddWithValue("@Contact1Zip", Tc.Contact1Zip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1Zip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1i3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Contact1i3ScreenAccess", Tc.Contact1i3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1i3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1BackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@Contact1BackgroundScreeningAccess", Tc.Contact1BackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1BackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1OccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@Contact1OccupationalMedicineAccess", Tc.Contact1OccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1OccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2FullName))
            {
                cmd.Parameters.AddWithValue("@Contact2FullName", Tc.Contact2FullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2FullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2JobTitle))
            {
                cmd.Parameters.AddWithValue("@Contact2JobTitle", Tc.Contact2JobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2JobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2OfficeNumber))
            {
                cmd.Parameters.AddWithValue("@Contact2OfficeNumber", Tc.Contact2OfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2OfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2CellPhone))
            {
                cmd.Parameters.AddWithValue("@Contact2CellPhone", Tc.Contact2CellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2CellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2Fax))
            {
                cmd.Parameters.AddWithValue("@Contact2Fax", Tc.Contact2Fax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2Fax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2Email))
            {
                cmd.Parameters.AddWithValue("@Contact2Email", Tc.Contact2Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2Email", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2StreetAddress))
            {
                cmd.Parameters.AddWithValue("@Contact2StreetAddress", Tc.Contact2StreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2StreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2City))
            {
                cmd.Parameters.AddWithValue("@Contact2City", Tc.Contact2City);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2City", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2State))
            {
                cmd.Parameters.AddWithValue("@Contact2State", Tc.Contact2State);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2State", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2Zip))
            {
                cmd.Parameters.AddWithValue("@Contact2Zip", Tc.Contact2Zip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2Zip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2i3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Contact2i3ScreenAccess", Tc.Contact2i3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2i3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2BackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@Contact2BackgroundScreeningAccess", Tc.Contact2BackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2BackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2OccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@Contact2OccupationalMedicineAccess", Tc.Contact2OccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2OccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactFullName))
            {
                cmd.Parameters.AddWithValue("@BillingContactFullName", Tc.BillingContactFullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactFullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactOfficeNumber))
            {
                cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", Tc.BillingContactOfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactFax))
            {
                cmd.Parameters.AddWithValue("@BillingContactFax", Tc.BillingContactFax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactFax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactEmail))
            {
                cmd.Parameters.AddWithValue("@BillingContactEmail", Tc.BillingContactEmail);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactEmail", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactStreetAddress))
            {
                cmd.Parameters.AddWithValue("@BillingContactStreetAddress", Tc.BillingContactStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactCity))
            {
                cmd.Parameters.AddWithValue("@BillingContactCity", Tc.BillingContactCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactState))
            {
                cmd.Parameters.AddWithValue("@BillingContactState", Tc.BillingContactState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactZip))
            {
                cmd.Parameters.AddWithValue("@BillingContactZip", Tc.BillingContactZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactEmailInvoices))
            {
                cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", Tc.BillingContactEmailInvoices);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactNotes))
            {
                cmd.Parameters.AddWithValue("@BillingContactNotes", Tc.BillingContactNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactBillingOptions))
            {
                cmd.Parameters.AddWithValue("@BillingContactBillingOptions", Tc.BillingContactBillingOptions);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactBillingOptions", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsLocationName))
            {
                cmd.Parameters.AddWithValue("@LocationsLocationName", Tc.LocationsLocationName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsLocationName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsStreetAddress))
            {
                cmd.Parameters.AddWithValue("@LocationsStreetAddress", Tc.LocationsStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsCity))
            {
                cmd.Parameters.AddWithValue("@LocationsCity", Tc.LocationsCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsState))
            {
                cmd.Parameters.AddWithValue("@LocationsState", Tc.LocationsState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsZip))
            {
                cmd.Parameters.AddWithValue("@LocationsZip", Tc.LocationsZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsMainNumber))
            {
                cmd.Parameters.AddWithValue("@LocationsMainNumber", Tc.LocationsMainNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsMainNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsNotes))
            {
                cmd.Parameters.AddWithValue("@LocationsNotes", Tc.LocationsNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactFullName))
            {
                cmd.Parameters.AddWithValue("@LocationsContactFullName", Tc.LocationsContactFullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactFullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactOfficeNumber))
            {
                cmd.Parameters.AddWithValue("@LocationsContactOfficeNumber", Tc.LocationsContactOfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactOfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactCellPhone))
            {
                cmd.Parameters.AddWithValue("@LocationsContactCellPhone", Tc.LocationsContactCellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactCellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactFax))
            {
                cmd.Parameters.AddWithValue("@LocationsContactFax", Tc.LocationsContactFax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactFax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactEmail))
            {
                cmd.Parameters.AddWithValue("@LocationsContactEmail", Tc.LocationsContactEmail);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactEmail", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Locationsi3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Locationsi3ScreenAccess", Tc.Locationsi3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Locationsi3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyProtocolsPolicies))
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsPolicies", Tc.CompanyProtocolsPolicies);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsPolicies", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyProtocolsNotes))
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsNotes", Tc.CompanyProtocolsNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyProtocolsProtocols))
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsProtocols", Tc.CompanyProtocolsProtocols);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsProtocols", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyPartnerProtocols))
            {
                cmd.Parameters.AddWithValue("@CompanyPartnerProtocols", Tc.CompanyPartnerProtocols);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyPartnerProtocols", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttacheCopyCCF))
            {
                cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", Tc.LabAccAttacheCopyCCF);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccLab1))
            {
                cmd.Parameters.AddWithValue("@LabAccLab1", Tc.LabAccLab1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccLab1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAccountNumber1))
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber1", Tc.LabAccAccountNumber1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccPannel))
            {
                cmd.Parameters.AddWithValue("@LabAccPannel", Tc.LabAccPannel);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccPannel", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa1))
            {
                cmd.Parameters.AddWithValue("@LabAccTpa1", Tc.LabAccTpa1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccTpa1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccMro1))
            {
                cmd.Parameters.AddWithValue("@LabAccMro1", Tc.LabAccMro1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccMro1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccSampleType1))
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType1", Tc.LabAccSampleType1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttachment1))
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment1", Tc.LabAccAttachment1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccLab2))
            {
                cmd.Parameters.AddWithValue("@LabAccLab2", Tc.LabAccLab2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccLab2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAccountNumber2))
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber2", Tc.LabAccAccountNumber2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccPannel2))
            {
                cmd.Parameters.AddWithValue("@LabAccPannel2", Tc.LabAccPannel2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccPannel2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa2))
            {
                cmd.Parameters.AddWithValue("@LabAccTpa2", Tc.LabAccTpa2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccTpa2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccMro2))
            {
                cmd.Parameters.AddWithValue("@LabAccMro2", Tc.LabAccMro2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccMro2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccSampleType2))
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType2", Tc.LabAccSampleType2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttachment2))
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment2", Tc.LabAccAttachment2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccLab3))
            {
                cmd.Parameters.AddWithValue("@LabAccLab3", Tc.LabAccLab3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccLab3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAccountNumber3))
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber3", Tc.LabAccAccountNumber3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccPannel3))
            {
                cmd.Parameters.AddWithValue("@LabAccPannel3", Tc.LabAccPannel3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccPannel3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa3))
            {
                cmd.Parameters.AddWithValue("@LabAccTpa3", Tc.LabAccTpa3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccTpa3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa3))
            {
                cmd.Parameters.AddWithValue("@LabAccMro3", Tc.LabAccMro3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccMro3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccSampleType3))
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType3", Tc.LabAccSampleType3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttachment3))
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment3", Tc.LabAccAttachment3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ServicesProvided))
            {
                cmd.Parameters.AddWithValue("@ServicesProvided", Tc.ServicesProvided);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ServicesProvided", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPPreEmployment))
            {
                cmd.Parameters.AddWithValue("@SPPreEmployment", Tc.SPPreEmployment);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPPreEmployment", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPBackgroundPackagePreEmploy))
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", Tc.SPBackgroundPackagePreEmploy);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPRandom))
            {
                cmd.Parameters.AddWithValue("@SPRandom", Tc.SPRandom);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPRandom", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPPostAccident))
            {
                cmd.Parameters.AddWithValue("@SPPostAccident", Tc.SPPostAccident);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPPostAccident", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPReasonableSuspicion))
            {
                cmd.Parameters.AddWithValue("@SPReasonableSuspicion", Tc.SPReasonableSuspicion);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPReasonableSuspicion", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPFollowUp))
            {
                cmd.Parameters.AddWithValue("@SPFollowUp", Tc.SPFollowUp);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPFollowUp", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPReturntoDuty))
            {
                cmd.Parameters.AddWithValue("@SPReturntoDuty", Tc.SPReturntoDuty);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPReturntoDuty", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPAnnual))
            {
                cmd.Parameters.AddWithValue("@SPAnnual", Tc.SPAnnual);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPAnnual", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPBackgroundPackageAnnual))
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", Tc.SPBackgroundPackageAnnual);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPNegativeDilute))
            {
                cmd.Parameters.AddWithValue("@SPNegativeDilute", Tc.SPNegativeDilute);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPNegativeDilute", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTPoolName))
            {
                cmd.Parameters.AddWithValue("@RPTPoolName", Tc.RPTPoolName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTPoolName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTPoolType))
            {
                cmd.Parameters.AddWithValue("@RPTPoolType", Tc.RPTPoolType);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTPoolType", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTOwner))
            {
                cmd.Parameters.AddWithValue("@RPTOwner", Tc.RPTOwner);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTOwner", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTPoolManager))
            {
                cmd.Parameters.AddWithValue("@RPTPoolManager", Tc.RPTPoolManager);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTPoolManager", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTOwnerType))
            {
                cmd.Parameters.AddWithValue("@RPTOwnerType", Tc.RPTOwnerType);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTOwnerType", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTdotnondot))
            {
                cmd.Parameters.AddWithValue("@RPTdotnondot", Tc.RPTdotnondot);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTdotnondot", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTdotagency))
            {
                cmd.Parameters.AddWithValue("@RPTdotagency", Tc.RPTdotagency);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTdotagency", string.Empty);
            }
            if (Tc.RTPAlcoholGetsDrug == true)
            {
                cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", false);
            }
            if (!string.IsNullOrEmpty(Tc.RTPSelectionLevelforDrug))
            {
                cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", Tc.RTPSelectionLevelforDrug);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPPercent1))
            {
                cmd.Parameters.AddWithValue("@RTPPercent1", Tc.RTPPercent1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPPercent1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPAlternatesforDrug))
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", Tc.RTPAlternatesforDrug);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPPercent2))
            {
                cmd.Parameters.AddWithValue("@RTPPercent2", Tc.RTPPercent2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPPercent2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPAlternatesforAlcohol))
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", Tc.RTPAlternatesforAlcohol);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPPercent3))
            {
                cmd.Parameters.AddWithValue("@RTPPercent3", Tc.RTPPercent3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPPercent3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPNotes))
            {
                cmd.Parameters.AddWithValue("@RTPNotes", Tc.RTPNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPNotes", string.Empty);
            }
           
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Clientlist", "Client");



        }



        [HttpGet]
        public ActionResult UpdateClient(string id)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("EditTC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Client_Id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            TcClient TC = new TcClient();
            TC.clientid = id;
            if (dt.Rows.Count > 0)
            {

                if (!string.IsNullOrEmpty(dt.Rows[0]["AccountsId"].ToString()))
                {
                    TC.AccountsId = dt.Rows[0]["AccountsId"].ToString();
                }
                else
                {
                    TC.AccountsId = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientType_LI"].ToString()))
                {
                    TC.ClientType_LI = dt.Rows[0]["ClientType_LI"].ToString();
                }
                else
                {
                    TC.ClientType_LI = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientStreetAddress"].ToString()))
                {
                    TC.ClientStreetAddress = dt.Rows[0]["ClientStreetAddress"].ToString();
                }
                else
                {
                    TC.ClientStreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientCity"].ToString()))
                {
                    TC.ClientCity = dt.Rows[0]["ClientCity"].ToString();
                }
                else
                {
                    TC.ClientCity = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientState"].ToString()))
                {
                    TC.ClientState = dt.Rows[0]["ClientState"].ToString();
                }
                else
                {
                    TC.ClientState = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientZip"].ToString()))
                {
                    TC.ClientZip = dt.Rows[0]["ClientZip"].ToString();
                }
                else
                {
                    TC.ClientZip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["MainNumber"].ToString()))
                {
                    TC.MainNumber = dt.Rows[0]["MainNumber"].ToString();
                }
                else
                {
                    TC.MainNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationSpecificNotes"].ToString()))
                {
                    TC.LocationSpecificNotes = dt.Rows[0]["LocationSpecificNotes"].ToString();
                }
                else
                {
                    TC.LocationSpecificNotes = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ConatctWithANY"].ToString()))
                {
                    TC.ConatctWithANY = dt.Rows[0]["ConatctWithANY"].ToString();
                }
                else
                {
                    TC.ConatctWithANY = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ShyBladdersLugWithNo"].ToString()))
                {
                    TC.ShyBladdersLugWithNo = dt.Rows[0]["ShyBladdersLugWithNo"].ToString();
                }
                else
                {
                    TC.ShyBladdersLugWithNo = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientSample"].ToString()))
                {
                    TC.ClientSample = dt.Rows[0]["ClientSample"].ToString();
                }
                else
                {
                    TC.ClientSample = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RefusalToTest"].ToString()))
                {
                    TC.RefusalToTest = dt.Rows[0]["RefusalToTest"].ToString();
                }
                else
                {
                    TC.RefusalToTest = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BatConfirmedPostive"].ToString()))
                {
                    TC.BatConfirmedPostive = dt.Rows[0]["BatConfirmedPostive"].ToString();
                }
                else
                {
                    TC.BatConfirmedPostive = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CancelledOrIncompleteTests"].ToString()))
                {
                    TC.CancelledOrIncompleteTests = dt.Rows[0]["CancelledOrIncompleteTests"].ToString();
                }
                else
                {
                    TC.CancelledOrIncompleteTests = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["AuthorizationFormsSentVia"].ToString()))
                {
                    TC.AuthorizationFormsSentVia = dt.Rows[0]["AuthorizationFormsSentVia"].ToString();
                }
                else
                {
                    TC.AuthorizationFormsSentVia = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerFullName"].ToString()))
                {
                    TC.DerFullName = dt.Rows[0]["DerFullName"].ToString();
                }
                else
                {
                    TC.DerFullName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerJobTitle"].ToString()))
                {
                    TC.DerJobTitle = dt.Rows[0]["DerJobTitle"].ToString();
                }
                else
                {
                    TC.DerJobTitle = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerOfficeNumber"].ToString()))
                {
                    TC.DerOfficeNumber = dt.Rows[0]["DerOfficeNumber"].ToString();
                }
                else
                {
                    TC.DerOfficeNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerCellPhone"].ToString()))
                {
                    TC.DerCellPhone = dt.Rows[0]["DerCellPhone"].ToString();
                }
                else
                {
                    TC.DerCellPhone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerFax"].ToString()))
                {
                    TC.DerFax = dt.Rows[0]["DerFax"].ToString();
                }
                else
                {
                    TC.DerFax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerEmail"].ToString()))
                {
                    TC.DerEmail = dt.Rows[0]["DerEmail"].ToString();
                }
                else
                {
                    TC.DerEmail = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerStreetAddress"].ToString()))
                {
                    TC.DerStreetAddress = dt.Rows[0]["DerStreetAddress"].ToString();
                }
                else
                {
                    TC.DerStreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerCity"].ToString()))
                {
                    TC.DerCity = dt.Rows[0]["DerCity"].ToString();
                }
                else
                {
                    TC.DerCity = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerState"].ToString()))
                {
                    TC.DerState = dt.Rows[0]["DerState"].ToString();
                }
                else
                {
                    TC.DerState = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerZip"].ToString()))
                {
                    TC.DerZip = dt.Rows[0]["DerZip"].ToString();
                }
                else
                {
                    TC.DerZip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Deri3ScreenAccess"].ToString()))
                {
                    TC.Deri3ScreenAccess = dt.Rows[0]["Deri3ScreenAccess"].ToString();
                }
                else
                {
                    TC.Deri3ScreenAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerBackgroundScreeningAccess"].ToString()))
                {
                    TC.DerBackgroundScreeningAccess = dt.Rows[0]["DerBackgroundScreeningAccess"].ToString();
                }
                else
                {
                    TC.DerBackgroundScreeningAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["DerOccupationalMedicineAccess"].ToString()))
                {
                    TC.DerOccupationalMedicineAccess = dt.Rows[0]["DerOccupationalMedicineAccess"].ToString();
                }
                else
                {
                    TC.DerOccupationalMedicineAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2FullName"].ToString()))
                {
                    TC.Der2FullName = dt.Rows[0]["Der2FullName"].ToString();
                }
                else
                {
                    TC.Der2FullName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2JobTitle"].ToString()))
                {
                    TC.Der2JobTitle = dt.Rows[0]["Der2JobTitle"].ToString();
                }
                else
                {
                    TC.Der2JobTitle = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2OfficeNumber"].ToString()))
                {
                    TC.Der2OfficeNumber = dt.Rows[0]["Der2OfficeNumber"].ToString();
                }
                else
                {
                    TC.Der2OfficeNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2Fax"].ToString()))
                {
                    TC.Der2Fax = dt.Rows[0]["Der2Fax"].ToString();
                }
                else
                {
                    TC.Der2Fax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2Email"].ToString()))
                {
                    TC.Der2Email = dt.Rows[0]["Der2Email"].ToString();
                }
                else
                {
                    TC.Der2Email = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2StreetAddress"].ToString()))
                {
                    TC.Der2StreetAddress = dt.Rows[0]["Der2StreetAddress"].ToString();
                }
                else
                {
                    TC.Der2StreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2City"].ToString()))
                {
                    TC.Der2City = dt.Rows[0]["Der2City"].ToString();
                }
                else
                {
                    TC.Der2City = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2State"].ToString()))
                {
                    TC.Der2State = dt.Rows[0]["Der2State"].ToString();
                }
                else
                {
                    TC.Der2State = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2Zip"].ToString()))
                {
                    TC.Der2Zip = dt.Rows[0]["Der2Zip"].ToString();
                }
                else
                {
                    TC.Der2Zip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2i3ScreenAccess"].ToString()))
                {
                    TC.Der2i3ScreenAccess = dt.Rows[0]["Der2i3ScreenAccess"].ToString();
                }
                else
                {
                    TC.Der2i3ScreenAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2BackgroundScreeningAccess"].ToString()))
                {
                    TC.Der2BackgroundScreeningAccess = dt.Rows[0]["Der2BackgroundScreeningAccess"].ToString();
                }
                else
                {
                    TC.Der2BackgroundScreeningAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2OccupationalMedicineAccess"].ToString()))
                {
                    TC.Der2OccupationalMedicineAccess = dt.Rows[0]["Der2OccupationalMedicineAccess"].ToString();
                }
                else
                {
                    TC.Der2OccupationalMedicineAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1FullName"].ToString()))
                {
                    TC.Contact1FullName = dt.Rows[0]["Contact1FullName"].ToString();
                }
                else
                {
                    TC.Contact1FullName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1JobTitle"].ToString()))
                {
                    TC.Contact1JobTitle = dt.Rows[0]["Contact1JobTitle"].ToString();
                }
                else
                {
                    TC.Contact1JobTitle = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1OfficeNumber"].ToString()))
                {
                    TC.Contact1OfficeNumber = dt.Rows[0]["Contact1OfficeNumber"].ToString();
                }
                else
                {
                    TC.Contact1OfficeNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1CellPhone"].ToString()))
                {
                    TC.Contact1CellPhone = dt.Rows[0]["Contact1CellPhone"].ToString();
                }
                else
                {
                    TC.Contact1CellPhone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1Fax"].ToString()))
                {
                    TC.Contact1Fax = dt.Rows[0]["Contact1Fax"].ToString();
                }
                else
                {
                    TC.Contact1Fax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1Email"].ToString()))
                {
                    TC.Contact1Email = dt.Rows[0]["Contact1Email"].ToString();
                }
                else
                {
                    TC.Contact1Email = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1StreetAddress"].ToString()))
                {
                    TC.Contact1StreetAddress = dt.Rows[0]["Contact1StreetAddress"].ToString();
                }
                else
                {
                    TC.Contact1StreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1City"].ToString()))
                {
                    TC.Contact1City = dt.Rows[0]["Contact1City"].ToString();
                }
                else
                {
                    TC.Contact1City = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1State"].ToString()))
                {
                    TC.Contact1State = dt.Rows[0]["Contact1State"].ToString();
                }
                else
                {
                    TC.Contact1State = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1Zip"].ToString()))
                {
                    TC.Contact1Zip = dt.Rows[0]["Contact1Zip"].ToString();
                }
                else
                {
                    TC.Contact1Zip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1i3ScreenAccess"].ToString()))
                    {
                        TC.Contact1i3ScreenAccess = dt.Rows[0]["Contact1i3ScreenAccess"].ToString();
                }
                else
                {
                    TC.Contact1i3ScreenAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1BackgroundScreeningAccess"].ToString()))
                {
                    TC.Contact1BackgroundScreeningAccess = dt.Rows[0]["Contact1BackgroundScreeningAccess"].ToString();
                }
                else
                {
                    TC.Contact1BackgroundScreeningAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact1OccupationalMedicineAccess"].ToString()))
                {
                    TC.Contact1OccupationalMedicineAccess = dt.Rows[0]["Contact1OccupationalMedicineAccess"].ToString();
                }
                else
                {
                    TC.Contact1OccupationalMedicineAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2FullName"].ToString()))
                {
                    TC.Contact2FullName = dt.Rows[0]["Contact2FullName"].ToString();
                }
                else
                {
                    TC.Contact2FullName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2JobTitle"].ToString()))
                {
                    TC.Contact2JobTitle = dt.Rows[0]["Contact2JobTitle"].ToString();
                }
                else
                {
                    TC.Contact2JobTitle = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2OfficeNumber"].ToString()))
                {
                    TC.Contact2OfficeNumber = dt.Rows[0]["Contact2OfficeNumber"].ToString();
                }
                else
                {
                    TC.Contact2OfficeNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2CellPhone"].ToString()))
                {
                    TC.Contact2CellPhone = dt.Rows[0]["Contact2CellPhone"].ToString();
                }
                else
                {
                    TC.Contact2CellPhone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2Fax"].ToString()))
                {
                    TC.Contact2Fax = dt.Rows[0]["Contact2Fax"].ToString();
                }
                else
                {
                    TC.Contact2Fax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2Email"].ToString()))
                {
                    TC.Contact2Email = dt.Rows[0]["Contact2Email"].ToString();
                }
                else
                {
                    TC.Contact2Email = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Der2CellPhone"].ToString()))
                {
                    TC.Der2CellPhone = dt.Rows[0]["Der2CellPhone"].ToString();
                }
                else
                {
                    TC.Der2CellPhone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2StreetAddress"].ToString()))
                {
                    TC.Contact2StreetAddress = dt.Rows[0]["Contact2StreetAddress"].ToString();
                }
                else
                {
                    TC.Contact2StreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2City"].ToString()))
                {
                    TC.Contact2City = dt.Rows[0]["Contact2City"].ToString();
                }
                else
                {
                    TC.Contact2City = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2State"].ToString()))
                {
                    TC.Contact2State = dt.Rows[0]["Contact2State"].ToString();
                }
                else
                {
                    TC.Contact2State = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2Zip"].ToString()))
                {
                    TC.Contact2Zip = dt.Rows[0]["Contact2Zip"].ToString();
                }
                else
                {
                    TC.Contact2Zip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2i3ScreenAccess"].ToString()))
                {
                    TC.Contact2i3ScreenAccess = dt.Rows[0]["Contact2i3ScreenAccess"].ToString();
                }
                else
                {
                    TC.Contact2i3ScreenAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2BackgroundScreeningAccess"].ToString()))
                {
                    TC.Contact2BackgroundScreeningAccess = dt.Rows[0]["Contact2BackgroundScreeningAccess"].ToString();
                }
                else
                {
                    TC.Contact2BackgroundScreeningAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Contact2OccupationalMedicineAccess"].ToString()))
                {
                    TC.Contact2OccupationalMedicineAccess = dt.Rows[0]["Contact2OccupationalMedicineAccess"].ToString();
                }
                else
                {
                    TC.Contact2OccupationalMedicineAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactFullName"].ToString()))
                {
                    TC.BillingContactFullName = dt.Rows[0]["BillingContactFullName"].ToString();
                }
                else
                {
                    TC.BillingContactFullName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactOfficeNumber"].ToString()))
                {
                    TC.BillingContactOfficeNumber = dt.Rows[0]["BillingContactOfficeNumber"].ToString();
                }
                else
                {
                    TC.BillingContactOfficeNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactFax"].ToString()))
                {
                    TC.BillingContactFax = dt.Rows[0]["BillingContactFax"].ToString();
                }
                else
                {
                    TC.BillingContactFax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactEmail"].ToString()))
                {
                    TC.BillingContactEmail = dt.Rows[0]["BillingContactEmail"].ToString();
                }
                else
                {
                    TC.BillingContactEmail = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactStreetAddress"].ToString()))
                {
                    TC.BillingContactStreetAddress = dt.Rows[0]["BillingContactStreetAddress"].ToString();
                }
                else
                {
                    TC.BillingContactStreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactCity"].ToString()))
                {
                    TC.BillingContactCity = dt.Rows[0]["BillingContactCity"].ToString();
                }
                else
                {
                    TC.BillingContactCity = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactState"].ToString()))
                {
                    TC.BillingContactState = dt.Rows[0]["BillingContactState"].ToString();
                }
                else
                {
                    TC.BillingContactState = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactZip"].ToString()))
                {
                    TC.BillingContactZip = dt.Rows[0]["BillingContactZip"].ToString();
                }
                else
                {
                    TC.BillingContactZip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactEmailInvoices"].ToString()))
                {
                    TC.BillingContactEmailInvoices = dt.Rows[0]["BillingContactEmailInvoices"].ToString();
                }
                else
                {
                    TC.BillingContactEmailInvoices = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactNotes"].ToString()))
                {
                    TC.BillingContactNotes = dt.Rows[0]["BillingContactNotes"].ToString();
                }
                else
                {
                    TC.BillingContactNotes = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactBillingOptions"].ToString()))
                {
                    TC.BillingContactBillingOptions = dt.Rows[0]["BillingContactBillingOptions"].ToString();
                }
                else
                {
                    TC.BillingContactBillingOptions = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsLocationName"].ToString()))
                {
                    TC.LocationsLocationName = dt.Rows[0]["LocationsLocationName"].ToString();
                }
                else
                {
                    TC.LocationsLocationName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsStreetAddress"].ToString()))
                {
                    TC.LocationsStreetAddress = dt.Rows[0]["LocationsStreetAddress"].ToString();
                }
                else
                {
                    TC.LocationsStreetAddress = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsCity"].ToString()))
                {
                    TC.LocationsCity = dt.Rows[0]["LocationsCity"].ToString();
                }
                else
                {
                    TC.LocationsCity = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsState"].ToString()))
                {
                    TC.LocationsState = dt.Rows[0]["LocationsState"].ToString();
                }
                else
                {
                    TC.LocationsState = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsZip"].ToString()))
                {
                    TC.LocationsZip = dt.Rows[0]["LocationsZip"].ToString();
                }
                else
                {
                    TC.LocationsZip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsMainNumber"].ToString()))
                {
                    TC.LocationsMainNumber = dt.Rows[0]["LocationsMainNumber"].ToString();
                }
                else
                {
                    TC.LocationsMainNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsNotes"].ToString()))
                {
                    TC.LocationsNotes = dt.Rows[0]["LocationsNotes"].ToString();
                }
                else
                {
                    TC.LocationsNotes = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsContactFullName"].ToString()))
                {
                    TC.LocationsContactFullName = dt.Rows[0]["LocationsContactFullName"].ToString();
                }
                else
                {
                    TC.LocationsContactFullName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsContactOfficeNumber"].ToString()))
                {
                    TC.LocationsContactOfficeNumber = dt.Rows[0]["LocationsContactOfficeNumber"].ToString();
                }
                else
                {
                    TC.LocationsContactOfficeNumber = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsContactCellPhone"].ToString()))
                {
                    TC.LocationsContactCellPhone = dt.Rows[0]["LocationsContactCellPhone"].ToString();
                }
                else
                {
                    TC.LocationsContactCellPhone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsContactFax"].ToString()))
                {
                    TC.LocationsContactFax = dt.Rows[0]["LocationsContactFax"].ToString();
                }
                else
                {
                    TC.LocationsContactFax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LocationsContactEmail"].ToString()))
                {
                    TC.LocationsContactEmail = dt.Rows[0]["LocationsContactEmail"].ToString();
                }
                else
                {
                    TC.LocationsContactEmail = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Locationsi3ScreenAccess"].ToString()))
                {
                    TC.Locationsi3ScreenAccess = dt.Rows[0]["Locationsi3ScreenAccess"].ToString();
                }
                else
                {
                    TC.Locationsi3ScreenAccess = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CompanyProtocolsPolicies"].ToString()))
                {
                    TC.CompanyProtocolsPolicies = dt.Rows[0]["CompanyProtocolsPolicies"].ToString();
                }
                else
                {
                    TC.CompanyProtocolsPolicies = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CompanyProtocolsNotes"].ToString()))
                {
                    TC.CompanyProtocolsNotes = dt.Rows[0]["CompanyProtocolsNotes"].ToString();
                }
                else
                {
                    TC.CompanyProtocolsNotes = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CompanyProtocolsProtocols"].ToString()))
                {
                    TC.CompanyProtocolsProtocols = dt.Rows[0]["CompanyProtocolsProtocols"].ToString();
                }
                else
                {
                    TC.CompanyProtocolsProtocols = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CompanyPartnerProtocols"].ToString()))
                {
                    TC.CompanyPartnerProtocols = dt.Rows[0]["CompanyPartnerProtocols"].ToString();
                }
                else
                {
                    TC.CompanyPartnerProtocols = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttacheCopyCCF"].ToString()))
                {
                    TC.LabAccAttacheCopyCCF = dt.Rows[0]["LabAccAttacheCopyCCF"].ToString();
                }
                else
                {
                    TC.LabAccAttacheCopyCCF = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccLab1"].ToString()))
                {
                    TC.LabAccLab1 = dt.Rows[0]["LabAccLab1"].ToString();
                }
                else
                {
                    TC.LabAccLab1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAccountNumber1"].ToString()))
                {
                    TC.LabAccAccountNumber1 = dt.Rows[0]["LabAccAccountNumber1"].ToString();
                }
                else
                {
                    TC.LabAccAccountNumber1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccPannel"].ToString()))
                {
                    TC.LabAccPannel = dt.Rows[0]["LabAccPannel"].ToString();
                }
                else
                {
                    TC.LabAccPannel = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccTpa1"].ToString()))
                {
                    TC.LabAccTpa1 = dt.Rows[0]["LabAccTpa1"].ToString();
                }
                else
                {
                    TC.LabAccTpa1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccMro1"].ToString()))
                {
                    TC.LabAccMro1 = dt.Rows[0]["LabAccMro1"].ToString();
                }
                else
                {
                    TC.LabAccMro1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccSampleType1"].ToString()))
                {
                    TC.LabAccSampleType1 = dt.Rows[0]["LabAccSampleType1"].ToString();
                }
                else
                {
                    TC.LabAccSampleType1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttachment1"].ToString()))
                {
                    TC.LabAccAttachment1 = dt.Rows[0]["LabAccAttachment1"].ToString();
                }
                else
                {
                    TC.LabAccAttachment1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccLab2"].ToString()))
                {
                    TC.LabAccLab2 = dt.Rows[0]["LabAccLab2"].ToString();
                }
                else
                {
                    TC.LabAccLab2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAccountNumber2"].ToString()))
                {
                    TC.LabAccAccountNumber2 = dt.Rows[0]["LabAccAccountNumber2"].ToString();
                }
                else
                {
                    TC.LabAccAccountNumber2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccPannel2"].ToString()))
                {
                    TC.LabAccPannel2 = dt.Rows[0]["LabAccPannel2"].ToString();
                }
                else
                {
                    TC.LabAccPannel2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccTpa2"].ToString()))
                {
                    TC.LabAccTpa2 = dt.Rows[0]["LabAccTpa2"].ToString();
                }
                else
                {
                    TC.LabAccTpa2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccMro2"].ToString()))
                {
                    TC.LabAccMro2 = dt.Rows[0]["LabAccMro2"].ToString();
                }
                else
                {
                    TC.LabAccMro2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccSampleType2"].ToString()))
                {
                    TC.LabAccSampleType2 = dt.Rows[0]["LabAccSampleType2"].ToString();
                }
                else
                {
                    TC.LabAccSampleType2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttachment2"].ToString()))
                {
                    TC.LabAccAttachment2 = dt.Rows[0]["LabAccAttachment2"].ToString();
                }
                else
                {
                    TC.LabAccAttachment2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccLab3"].ToString()))
                {
                    TC.LabAccLab3 = dt.Rows[0]["LabAccLab3"].ToString();
                }
                else
                {
                    TC.LabAccLab3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAccountNumber3"].ToString()))
                {
                    TC.LabAccAccountNumber3 = dt.Rows[0]["LabAccAccountNumber3"].ToString();
                }
                else
                {
                    TC.LabAccAccountNumber3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccPannel3"].ToString()))
                {
                    TC.LabAccPannel3 = dt.Rows[0]["LabAccPannel3"].ToString();
                }
                else
                {
                    TC.LabAccPannel3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccTpa3"].ToString()))
                {
                    TC.LabAccTpa3 = dt.Rows[0]["LabAccTpa3"].ToString();
                }
                else
                {
                    TC.LabAccTpa3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccSampleType3"].ToString()))
                {
                    TC.LabAccSampleType3 = dt.Rows[0]["LabAccSampleType3"].ToString();
                }
                else
                {
                    TC.LabAccSampleType3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttachment3"].ToString()))
                {
                    TC.LabAccAttachment3 = dt.Rows[0]["LabAccAttachment3"].ToString();
                }
                else
                {
                    TC.LabAccAttachment3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ServicesProvided"].ToString()))
                {
                    TC.ServicesProvided = dt.Rows[0]["ServicesProvided"].ToString();
                }
                else
                {
                    TC.ServicesProvided = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPPreEmployment"].ToString()))
                {
                    TC.SPPreEmployment = dt.Rows[0]["SPPreEmployment"].ToString();
                }
                else
                {
                    TC.SPPreEmployment = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPBackgroundPackagePreEmploy"].ToString()))
                {
                    TC.SPBackgroundPackagePreEmploy = dt.Rows[0]["SPBackgroundPackagePreEmploy"].ToString();
                }
                else
                {
                    TC.SPBackgroundPackagePreEmploy = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPRandom"].ToString()))
                {
                    TC.SPRandom = dt.Rows[0]["SPRandom"].ToString();
                }
                else
                {
                    TC.SPRandom = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPPostAccident"].ToString()))
                {
                    TC.SPPostAccident = dt.Rows[0]["SPPostAccident"].ToString();
                }
                else
                {
                    TC.SPPostAccident = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPReasonableSuspicion"].ToString()))
                {
                    TC.SPReasonableSuspicion = dt.Rows[0]["SPReasonableSuspicion"].ToString();
                }
                else
                {
                    TC.SPReasonableSuspicion = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPFollowUp"].ToString()))
                {
                    TC.SPFollowUp = dt.Rows[0]["SPFollowUp"].ToString();
                }
                else
                {
                    TC.SPFollowUp = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPReturntoDuty"].ToString()))
                {
                    TC.SPReturntoDuty = dt.Rows[0]["SPReturntoDuty"].ToString();
                }
                else
                {
                    TC.SPReturntoDuty = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPAnnual"].ToString()))
                {
                    TC.SPAnnual = dt.Rows[0]["SPAnnual"].ToString();
                }
                else
                {
                    TC.SPAnnual = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPBackgroundPackageAnnual"].ToString()))
                {
                    TC.SPBackgroundPackageAnnual = dt.Rows[0]["SPBackgroundPackageAnnual"].ToString();
                    }
                else
                {
                    TC.SPBackgroundPackageAnnual = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["SPNegativeDilute"].ToString()))
                {
                    TC.SPNegativeDilute = dt.Rows[0]["SPNegativeDilute"].ToString();
                }
                else
                {
                    TC.SPNegativeDilute = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RPTPoolName"].ToString()))
                {
                    TC.RPTPoolName = dt.Rows[0]["RPTPoolName"].ToString();
                }
                else
                {
                    TC.RPTPoolName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RPTPoolType"].ToString()))
                {
                    TC.RPTPoolType = dt.Rows[0]["RPTPoolType"].ToString();
                }
                else
                {
                    TC.RPTPoolType = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccMro3"].ToString()))
                {
                    TC.LabAccMro3 = dt.Rows[0]["LabAccMro3"].ToString();
                }
                else
                {
                    TC.LabAccMro3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RPTOwner"].ToString()))
                {
                    TC.RPTOwner = dt.Rows[0]["RPTOwner"].ToString();
                }
                else
                {
                    TC.RPTOwner = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RPTPoolManager"].ToString()))
                {
                    TC.RPTPoolManager = dt.Rows[0]["RPTPoolManager"].ToString();
                }
                else
                {
                    TC.RPTPoolManager = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RPTOwnerType"].ToString()))
                {
                    TC.RPTOwnerType = dt.Rows[0]["RPTOwnerType"].ToString();
                }
                else
                {
                    TC.RPTOwnerType = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RPTdotnondot"].ToString()))
                {
                    TC.RPTdotnondot = dt.Rows[0]["RPTdotnondot"].ToString();
                }
                else
                {
                    TC.RPTdotnondot = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RPTdotagency"].ToString()))
                {
                    TC.RPTdotagency = dt.Rows[0]["RPTdotagency"].ToString();
                }
                else
                {
                    TC.RPTdotagency = "0";
                }
                if (dt.Rows[0]["RTPAlcoholGetsDrug"].ToString() != null)
                {
                    TC.RTPAlcoholGetsDrug = Convert.ToBoolean(dt.Rows[0]["RTPAlcoholGetsDrug"].ToString());
                }
                else
                {
                    TC.RTPAlcoholGetsDrug = false;
                }
             
                if (!string.IsNullOrEmpty(dt.Rows[0]["RTPSelectionLevelforDrug"].ToString()))
                {
                    TC.RTPSelectionLevelforDrug = dt.Rows[0]["RTPSelectionLevelforDrug"].ToString();
                }
                else
                {
                    TC.RTPSelectionLevelforDrug = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RTPPercent1"].ToString()))
                {
                    TC.RTPPercent1 = dt.Rows[0]["RTPPercent1"].ToString();
                }
                else
                {
                    TC.RTPPercent1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RTPAlternatesforDrug"].ToString()))
                {
                    TC.RTPAlternatesforDrug = dt.Rows[0]["RTPAlternatesforDrug"].ToString();
                }
                else
                {
                    TC.RTPAlternatesforDrug = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RTPPercent2"].ToString()))
                {
                    TC.RTPPercent2 = dt.Rows[0]["RTPPercent2"].ToString();
                }
                else
                {
                    TC.RTPPercent2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RTPAlternatesforAlcohol"].ToString()))
                {
                    TC.RTPAlternatesforAlcohol = dt.Rows[0]["RTPAlternatesforAlcohol"].ToString();
                }
                else
                {
                    TC.RTPAlternatesforAlcohol = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RTPPercent3"].ToString()))
                {
                    TC.RTPPercent3 = dt.Rows[0]["RTPPercent3"].ToString();
                }
                else
                {
                    TC.RTPPercent3 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RTPNotes"].ToString()))
                {
                    TC.RTPNotes = dt.Rows[0]["RTPNotes"].ToString();
                }
                else
                {
                    TC.RTPNotes = string.Empty;
                }
              
            }
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return View(TC);
        }

        [HttpPost]
        public ActionResult UpdateClient(TcClient Tc)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("UpdateTC", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UpdatedOn",DateTime.Now);
            cmd.Parameters.AddWithValue("@Updatedby", User.Identity.GetUserName());
            cmd.Parameters.AddWithValue("@Client_id", Tc.clientid);
            if (System.Web.HttpContext.Current.Session["Account_id"] != null)
            {
                cmd.Parameters.AddWithValue("@AccountsId", System.Web.HttpContext.Current.Session["Account_id"]);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AccountsId", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientType_LI))
            {
                cmd.Parameters.AddWithValue("@ClientType_LI", Tc.ClientType_LI);
            }
            else
            {
                
                cmd.Parameters.AddWithValue("@ClientType_LI", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientStreetAddress))
            {
                cmd.Parameters.AddWithValue("@ClientStreetAddress", Tc.ClientStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientCity))
            {
                cmd.Parameters.AddWithValue("@ClientCity", Tc.ClientCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientState))
            {
                cmd.Parameters.AddWithValue("@ClientState", Tc.ClientState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientZip))
            {
                cmd.Parameters.AddWithValue("@ClientZip", Tc.ClientZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.MainNumber))
            {
                cmd.Parameters.AddWithValue("@MainNumber", Tc.MainNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MainNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationSpecificNotes))
            {
                cmd.Parameters.AddWithValue("@LocationSpecificNotes", Tc.LocationSpecificNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationSpecificNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ConatctWithANY))
            {
                cmd.Parameters.AddWithValue("@ConatctWithANY", Tc.ConatctWithANY);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ConatctWithANY", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ShyBladdersLugWithNo))
            {
                cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", Tc.ShyBladdersLugWithNo);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ClientSample))
            {
                cmd.Parameters.AddWithValue("@ClientSample", Tc.ClientSample);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ClientSample", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RefusalToTest))
            {
                cmd.Parameters.AddWithValue("@RefusalToTest", Tc.RefusalToTest);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RefusalToTest", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BatConfirmedPostive))
            {
                cmd.Parameters.AddWithValue("@BatConfirmedPostive", Tc.BatConfirmedPostive);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BatConfirmedPostive", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CancelledOrIncompleteTests))
            {
                cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", Tc.CancelledOrIncompleteTests);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.AuthorizationFormsSentVia))
            {
                cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", Tc.AuthorizationFormsSentVia);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerFullName))
            {
                cmd.Parameters.AddWithValue("@DerFullName", Tc.DerFullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerFullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerJobTitle))
            {
                cmd.Parameters.AddWithValue("@DerJobTitle", Tc.DerJobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerJobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerOfficeNumber))
            {
                cmd.Parameters.AddWithValue("@DerOfficeNumber", Tc.DerOfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerOfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerCellPhone))
            {
                cmd.Parameters.AddWithValue("@DerCellPhone", Tc.DerCellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerCellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerFax))
            {
                cmd.Parameters.AddWithValue("@DerFax", Tc.DerFax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerFax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerEmail))
            {
                cmd.Parameters.AddWithValue("@DerEmail", Tc.DerEmail);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerEmail", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerStreetAddress))
            {
                cmd.Parameters.AddWithValue("@DerStreetAddress", Tc.DerStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerCity))
            {
                cmd.Parameters.AddWithValue("@DerCity", Tc.DerCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerState))
            {
                cmd.Parameters.AddWithValue("@DerState", Tc.DerState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerZip))
            {
                cmd.Parameters.AddWithValue("@DerZip", Tc.DerZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Deri3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Deri3ScreenAccess", Tc.Deri3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Deri3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerBackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@DerBackgroundScreeningAccess", Tc.DerBackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerBackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.DerOccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@DerOccupationalMedicineAccess", Tc.DerOccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DerOccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2FullName))
            {
                cmd.Parameters.AddWithValue("@Der2FullName", Tc.Der2FullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2FullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2JobTitle))
            {
                cmd.Parameters.AddWithValue("@Der2JobTitle", Tc.Der2JobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2JobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2OfficeNumber))
            {
                cmd.Parameters.AddWithValue("@Der2OfficeNumber", Tc.Der2OfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2OfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2CellPhone))
            {
                cmd.Parameters.AddWithValue("@Der2CellPhone", Tc.Der2CellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2CellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2Fax))
            {
                cmd.Parameters.AddWithValue("@Der2Fax", Tc.Der2Fax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2Fax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2Email))
            {
                cmd.Parameters.AddWithValue("@Der2Email", Tc.Der2Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2Email", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2StreetAddress))
            {
                cmd.Parameters.AddWithValue("@Der2StreetAddress", Tc.Der2StreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2StreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2City))
            {
                cmd.Parameters.AddWithValue("@Der2City", Tc.Der2City);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2City", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2State))
            {
                cmd.Parameters.AddWithValue("@Der2State", Tc.Der2State);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2State", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2Zip))
            {
                cmd.Parameters.AddWithValue("@Der2Zip", Tc.Der2Zip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2Zip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2i3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Der2i3ScreenAccess", Tc.Der2i3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2i3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2BackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@Der2BackgroundScreeningAccess", Tc.Der2BackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2BackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Der2OccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@Der2OccupationalMedicineAccess", Tc.Der2OccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Der2OccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1FullName))
            {
                cmd.Parameters.AddWithValue("@Contact1FullName", Tc.Contact1FullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1FullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1JobTitle))
            {
                cmd.Parameters.AddWithValue("@Contact1JobTitle", Tc.Contact1JobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1JobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1OfficeNumber))
            {
                cmd.Parameters.AddWithValue("@Contact1OfficeNumber", Tc.Contact1OfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1OfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1CellPhone))
            {
                cmd.Parameters.AddWithValue("@Contact1CellPhone", Tc.Contact1CellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1CellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1Fax))
            {
                cmd.Parameters.AddWithValue("@Contact1Fax", Tc.Contact1Fax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1Fax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1Email))
            {
                cmd.Parameters.AddWithValue("@Contact1Email", Tc.Contact1Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1Email", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1StreetAddress))
            {
                cmd.Parameters.AddWithValue("@Contact1StreetAddress", Tc.Contact1StreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1StreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1City))
            {
                cmd.Parameters.AddWithValue("@Contact1City", Tc.Contact1City);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1City", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1State))
            {
                cmd.Parameters.AddWithValue("@Contact1State", Tc.Contact1State);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1State", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1Zip))
            {
                cmd.Parameters.AddWithValue("@Contact1Zip", Tc.Contact1Zip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1Zip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1i3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Contact1i3ScreenAccess", Tc.Contact1i3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1i3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1BackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@Contact1BackgroundScreeningAccess", Tc.Contact1BackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1BackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact1OccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@Contact1OccupationalMedicineAccess", Tc.Contact1OccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact1OccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2FullName))
            {
                cmd.Parameters.AddWithValue("@Contact2FullName", Tc.Contact2FullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2FullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2JobTitle))
            {
                cmd.Parameters.AddWithValue("@Contact2JobTitle", Tc.Contact2JobTitle);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2JobTitle", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2OfficeNumber))
            {
                cmd.Parameters.AddWithValue("@Contact2OfficeNumber", Tc.Contact2OfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2OfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2CellPhone))
            {
                cmd.Parameters.AddWithValue("@Contact2CellPhone", Tc.Contact2CellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2CellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2Fax))
            {
                cmd.Parameters.AddWithValue("@Contact2Fax", Tc.Contact2Fax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2Fax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2Email))
            {
                cmd.Parameters.AddWithValue("@Contact2Email", Tc.Contact2Email);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2Email", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2StreetAddress))
            {
                cmd.Parameters.AddWithValue("@Contact2StreetAddress", Tc.Contact2StreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2StreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2City))
            {
                cmd.Parameters.AddWithValue("@Contact2City", Tc.Contact2City);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2City", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2State))
            {
                cmd.Parameters.AddWithValue("@Contact2State", Tc.Contact2State);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2State", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2Zip))
            {
                cmd.Parameters.AddWithValue("@Contact2Zip", Tc.Contact2Zip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2Zip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2i3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Contact2i3ScreenAccess", Tc.Contact2i3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2i3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2BackgroundScreeningAccess))
            {
                cmd.Parameters.AddWithValue("@Contact2BackgroundScreeningAccess", Tc.Contact2BackgroundScreeningAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2BackgroundScreeningAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Contact2OccupationalMedicineAccess))
            {
                cmd.Parameters.AddWithValue("@Contact2OccupationalMedicineAccess", Tc.Contact2OccupationalMedicineAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact2OccupationalMedicineAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactFullName))
            {
                cmd.Parameters.AddWithValue("@BillingContactFullName", Tc.BillingContactFullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactFullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactOfficeNumber))
            {
                cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", Tc.BillingContactOfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactFax))
            {
                cmd.Parameters.AddWithValue("@BillingContactFax", Tc.BillingContactFax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactFax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactEmail))
            {
                cmd.Parameters.AddWithValue("@BillingContactEmail", Tc.BillingContactEmail);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactEmail", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactStreetAddress))
            {
                cmd.Parameters.AddWithValue("@BillingContactStreetAddress", Tc.BillingContactStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactCity))
            {
                cmd.Parameters.AddWithValue("@BillingContactCity", Tc.BillingContactCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactState))
            {
                cmd.Parameters.AddWithValue("@BillingContactState", Tc.BillingContactState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactZip))
            {
                cmd.Parameters.AddWithValue("@BillingContactZip", Tc.BillingContactZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactEmailInvoices))
            {
                cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", Tc.BillingContactEmailInvoices);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactNotes))
            {
                cmd.Parameters.AddWithValue("@BillingContactNotes", Tc.BillingContactNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.BillingContactBillingOptions))
            {
                cmd.Parameters.AddWithValue("@BillingContactBillingOptions", Tc.BillingContactBillingOptions);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BillingContactBillingOptions", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsLocationName))
            {
                cmd.Parameters.AddWithValue("@LocationsLocationName", Tc.LocationsLocationName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsLocationName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsStreetAddress))
            {
                cmd.Parameters.AddWithValue("@LocationsStreetAddress", Tc.LocationsStreetAddress);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsStreetAddress", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsCity))
            {
                cmd.Parameters.AddWithValue("@LocationsCity", Tc.LocationsCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsCity", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsState))
            {
                cmd.Parameters.AddWithValue("@LocationsState", Tc.LocationsState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsState", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsZip))
            {
                cmd.Parameters.AddWithValue("@LocationsZip", Tc.LocationsZip);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsZip", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsMainNumber))
            {
                cmd.Parameters.AddWithValue("@LocationsMainNumber", Tc.LocationsMainNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsMainNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsNotes))
            {
                cmd.Parameters.AddWithValue("@LocationsNotes", Tc.LocationsNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactFullName))
            {
                cmd.Parameters.AddWithValue("@LocationsContactFullName", Tc.LocationsContactFullName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactFullName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactOfficeNumber))
            {
                cmd.Parameters.AddWithValue("@LocationsContactOfficeNumber", Tc.LocationsContactOfficeNumber);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactOfficeNumber", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactCellPhone))
            {
                cmd.Parameters.AddWithValue("@LocationsContactCellPhone", Tc.LocationsContactCellPhone);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactCellPhone", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactFax))
            {
                cmd.Parameters.AddWithValue("@LocationsContactFax", Tc.LocationsContactFax);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactFax", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LocationsContactEmail))
            {
                cmd.Parameters.AddWithValue("@LocationsContactEmail", Tc.LocationsContactEmail);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LocationsContactEmail", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.Locationsi3ScreenAccess))
            {
                cmd.Parameters.AddWithValue("@Locationsi3ScreenAccess", Tc.Locationsi3ScreenAccess);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Locationsi3ScreenAccess", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyProtocolsPolicies))
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsPolicies", Tc.CompanyProtocolsPolicies);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsPolicies", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyProtocolsNotes))
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsNotes", Tc.CompanyProtocolsNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsNotes", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyProtocolsProtocols))
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsProtocols", Tc.CompanyProtocolsProtocols);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyProtocolsProtocols", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.CompanyPartnerProtocols))
            {
                cmd.Parameters.AddWithValue("@CompanyPartnerProtocols", Tc.CompanyPartnerProtocols);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyPartnerProtocols", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttacheCopyCCF))
            {
                cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", Tc.LabAccAttacheCopyCCF);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccLab1))
            {
                cmd.Parameters.AddWithValue("@LabAccLab1", Tc.LabAccLab1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccLab1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAccountNumber1))
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber1", Tc.LabAccAccountNumber1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccPannel))
            {
                cmd.Parameters.AddWithValue("@LabAccPannel", Tc.LabAccPannel);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccPannel", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa1))
            {
                cmd.Parameters.AddWithValue("@LabAccTpa1", Tc.LabAccTpa1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccTpa1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccMro1))
            {
                cmd.Parameters.AddWithValue("@LabAccMro1", Tc.LabAccMro1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccMro1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccSampleType1))
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType1", Tc.LabAccSampleType1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttachment1))
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment1", Tc.LabAccAttachment1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccLab2))
            {
                cmd.Parameters.AddWithValue("@LabAccLab2", Tc.LabAccLab2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccLab2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAccountNumber2))
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber2", Tc.LabAccAccountNumber2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccPannel2))
            {
                cmd.Parameters.AddWithValue("@LabAccPannel2", Tc.LabAccPannel2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccPannel2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa2))
            {
                cmd.Parameters.AddWithValue("@LabAccTpa2", Tc.LabAccTpa2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccTpa2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccMro2))
            {
                cmd.Parameters.AddWithValue("@LabAccMro2", Tc.LabAccMro2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccMro2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccSampleType2))
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType2", Tc.LabAccSampleType2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttachment2))
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment2", Tc.LabAccAttachment2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccLab3))
            {
                cmd.Parameters.AddWithValue("@LabAccLab3", Tc.LabAccLab3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccLab3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAccountNumber3))
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber3", Tc.LabAccAccountNumber3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAccountNumber3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccPannel3))
            {
                cmd.Parameters.AddWithValue("@LabAccPannel3", Tc.LabAccPannel3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccPannel3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa3))
            {
                cmd.Parameters.AddWithValue("@LabAccTpa3", Tc.LabAccTpa3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccTpa3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccTpa3))
            {
                cmd.Parameters.AddWithValue("@LabAccMro3", Tc.LabAccMro3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccMro3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccSampleType3))
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType3", Tc.LabAccSampleType3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccSampleType3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.LabAccAttachment3))
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment3", Tc.LabAccAttachment3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LabAccAttachment3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.ServicesProvided))
            {
                cmd.Parameters.AddWithValue("@ServicesProvided", Tc.ServicesProvided);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ServicesProvided", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPPreEmployment))
            {
                cmd.Parameters.AddWithValue("@SPPreEmployment", Tc.SPPreEmployment);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPPreEmployment", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPBackgroundPackagePreEmploy))
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", Tc.SPBackgroundPackagePreEmploy);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPRandom))
            {
                cmd.Parameters.AddWithValue("@SPRandom", Tc.SPRandom);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPRandom", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPPostAccident))
            {
                cmd.Parameters.AddWithValue("@SPPostAccident", Tc.SPPostAccident);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPPostAccident", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPReasonableSuspicion))
            {
                cmd.Parameters.AddWithValue("@SPReasonableSuspicion", Tc.SPReasonableSuspicion);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPReasonableSuspicion", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPFollowUp))
            {
                cmd.Parameters.AddWithValue("@SPFollowUp", Tc.SPFollowUp);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPFollowUp", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPReturntoDuty))
            {
                cmd.Parameters.AddWithValue("@SPReturntoDuty", Tc.SPReturntoDuty);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPReturntoDuty", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPAnnual))
            {
                cmd.Parameters.AddWithValue("@SPAnnual", Tc.SPAnnual);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPAnnual", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPBackgroundPackageAnnual))
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", Tc.SPBackgroundPackageAnnual);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.SPNegativeDilute))
            {
                cmd.Parameters.AddWithValue("@SPNegativeDilute", Tc.SPNegativeDilute);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SPNegativeDilute", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTPoolName))
            {
                cmd.Parameters.AddWithValue("@RPTPoolName", Tc.RPTPoolName);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTPoolName", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTPoolType))
            {
                cmd.Parameters.AddWithValue("@RPTPoolType", Tc.RPTPoolType);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTPoolType", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTOwner))
            {
                cmd.Parameters.AddWithValue("@RPTOwner", Tc.RPTOwner);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTOwner", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTPoolManager))
            {
                cmd.Parameters.AddWithValue("@RPTPoolManager", Tc.RPTPoolManager);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTPoolManager", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTOwnerType))
            {
                cmd.Parameters.AddWithValue("@RPTOwnerType", Tc.RPTOwnerType);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTOwnerType", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTdotnondot))
            {
                cmd.Parameters.AddWithValue("@RPTdotnondot", Tc.RPTdotnondot);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTdotnondot", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RPTdotagency))
            {
                cmd.Parameters.AddWithValue("@RPTdotagency", Tc.RPTdotagency);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RPTdotagency", string.Empty);
            }
            if (Tc.RTPAlcoholGetsDrug == true)
            {
                    cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", true);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", false);
            }
            if (!string.IsNullOrEmpty(Tc.RTPSelectionLevelforDrug))
            {
                cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", Tc.RTPSelectionLevelforDrug);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPPercent1))
            {
                cmd.Parameters.AddWithValue("@RTPPercent1", Tc.RTPPercent1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPPercent1", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPAlternatesforDrug))
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", Tc.RTPAlternatesforDrug);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPPercent2))
            {
                cmd.Parameters.AddWithValue("@RTPPercent2", Tc.RTPPercent2);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPPercent2", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPAlternatesforAlcohol))
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", Tc.RTPAlternatesforAlcohol);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPPercent3))
            {
                cmd.Parameters.AddWithValue("@RTPPercent3", Tc.RTPPercent3);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPPercent3", string.Empty);
            }
            if (!string.IsNullOrEmpty(Tc.RTPNotes))
            {
                cmd.Parameters.AddWithValue("@RTPNotes", Tc.RTPNotes);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RTPNotes", string.Empty);
            }
           

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Clientlist", "Client");
        }

        public ActionResult DeleteClient(string id)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("DeleteTCC", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Client_id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Clients", "Client");

        }

        public JsonResult GetCities(string Statecode)
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

            return Json(ls,JsonRequestBehavior.AllowGet);
        }
        public  List<SelectListItem> GetAllCities(string Statecode)
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            if (Statecode != string.Empty)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
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

                return ls;
            }
            else
            {
                return ls;
            }
        }

    }
}




