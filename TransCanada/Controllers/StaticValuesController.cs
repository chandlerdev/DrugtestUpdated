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
    public class StaticValuesController : Controller
    {
        // GET: StaticValues
        public ActionResult Index()
        {
            return View();
        }
        public static List<SelectListItem> Lablist()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> Labs = new List<SelectListItem>();
            {
                string query = "select Location_name from tbl_clinic_details where isdeleted=0  group by Location_name ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Labs.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["Location_name"].ToString(),
                        Value = dt.Rows[i]["Location_name"].ToString()
                    });
                }
            }

            return Labs;
        }
        public static List<SelectListItem> Listlabtype()
        {

            List<SelectListItem> lab = new List<SelectListItem>()
            {


                new SelectListItem
                {
                    Text = "Lab",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Manufacturer",
                    Value = "2"
                },

            };
            return lab;
        }
        public static List<SelectListItem> ListCategoryType()
        {

            List<SelectListItem> Ct = new List<SelectListItem>()
            {
                ///


                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Target",
                    Value = "1"
                },
                  new SelectListItem
                {
                    Text = "Prospect",
                    Value = "2"
                },
                    new SelectListItem
                {
                    Text = "Client",
                    Value = "3"
                },
                      new SelectListItem
                {
                    Text = "Lost Client",
                    Value = "4"
                },
            };
            return Ct;

        }
        public static List<SelectListItem> ListTPACategory()
        {

            List<SelectListItem> Tc = new List<SelectListItem>()
            {
                
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Typical",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "TPA",
                    Value = "2"
                },
            };
            return Tc;
        }
        public static List<SelectListItem> ListEventType()
        {

            List<SelectListItem> Tc = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "DTC",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "TPA",
                    Value = "2"
                },
            };
            return Tc;
        }
        public static List<SelectListItem> ListAddressType()
        {

            List<SelectListItem> At = new List<SelectListItem>()
            {
                ///


                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Mailing",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "On-Site",
                    Value = "2"
                },
                  new SelectListItem
                {
                    Text = "Billing",
                    Value = "3"
                },
            };
            return At;
        }
        
        public static List<SelectListItem> ListTitle()
        {

            List<SelectListItem> Tl = new List<SelectListItem>()
            {
                ///


                // Asign Value from database
                
                new SelectListItem
                {
                    Text = "CEO",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "COO",
                    Value = "2"
                },
                  new SelectListItem
                {
                    Text = "Ops Manager",
                    Value = "3"
                },
                    new SelectListItem
                {
                    Text = "HR",
                    Value = "4"
                },
                    new SelectListItem
                {
                    Text = "Safety Manager",
                    Value = "5"
                },

            };
            return Tl;
        }

        public static List<SelectListItem> ListFunction()
        {

            List<SelectListItem> Fn = new List<SelectListItem>()
            {
                ///


                // Asign Value from database
                //new SelectListItem
                //{
                //    Text = "--Select--",
                //    Value = "0"
                //},
                new SelectListItem
                {
                    Text = "Owner/CEO",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Day to Day",
                    Value = "2"
                },
                  new SelectListItem
                {
                    Text = "DER",
                    Value = "3"
                },
                    new SelectListItem
                {
                    Text = "Billing",
                    Value = "4"
                },
                    new SelectListItem
                {
                    Text = "Pool Updates",
                    Value = "5"
                },
                     new SelectListItem
                {
                    Text = "Results",
                    Value = "6"
                },
                      new SelectListItem
                {
                    Text = "Pool Selections",
                    Value = "7"
                },

            };
            return Fn;
        }

        public static List<SelectListItem> ListReporting()
        {

            List<SelectListItem> Rg = new List<SelectListItem>()
            {
                ///


                // Asign Value from database
                
                new SelectListItem
                {
                    Text = "i-3screen (*)",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "e-mail",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Fax",
                    Value = "3"
                },
            };
            return Rg;

        }
        public static List<SelectListItem> ListPaymentDetails()
        {

            List<SelectListItem> Bd = new List<SelectListItem>()
            {
                ///


                // Asign Value from database
                 new SelectListItem
                {
                    Text = "--SELECT--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "ACH",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Cash",
                    Value = "2"
                },
                new SelectListItem
                {
                    Text = "Check",
                    Value = "3"
                },
                new SelectListItem
                {
                    Text = "Credit Card",
                    Value = "2"
                },

            };
            return Bd;

        }
        public static List<SelectListItem> ListBillingDetails()
        {

            List<SelectListItem> Bd = new List<SelectListItem>()
            {
                ///


                // Asign Value from database
                
                new SelectListItem
                {
                    Text = "Email",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Mail",
                    Value = "2"
                },
                
            };
            return Bd;

        }
        public static List<SelectListItem> LabTypelist()
        {
            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("select Id,Service_Type from Product_Service ", con);

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

        public static List<SelectListItem> ListLocation(string id)
        {
            // AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            //int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();

            string query = "Select Location,Location_id from  demo_location_table where Company_id = @companyid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@companyid", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Location"].ToString(),
                    Value = dt.Rows[i]["Location_id"].ToString()
                });
            }

            return ls;
        }
        public static List<SelectListItem> getTpa()
        {
            // AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            //int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();

            string query = "Select * from  Tbl_TpaClient order by TPA_Client";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["TPA_Client"].ToString(),
                    Value = dt.Rows[i]["TPAClient_Id"].ToString()
                });
            }

            return ls;
        }
        public static List<SelectListItem> getTpaLocation(string id)
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            if (string.IsNullOrEmpty(id))
                return ls;
            // AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            //int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            
            string query = "select TpaClient_locid as id,address_1+','+city as address  from Tbl_TpaClientLoc where Tpaclient_id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["address"].ToString(),
                    Value = dt.Rows[i]["id"].ToString()
                });
            }

            return ls;
        }

        public JsonResult jsongetTpaLocation(string id)
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            if (string.IsNullOrEmpty(id))
                return Json(ls,JsonRequestBehavior.AllowGet);
            // AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            //int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);


            string query = "select TpaClient_locid as id,address_1+','+city as address  from Tbl_TpaClientLoc where Tpaclient_id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["address"].ToString(),
                    Value = dt.Rows[i]["id"].ToString()
                });
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }
        public static List<SelectListItem> getTpaLocationContact(string id)
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            if (string.IsNullOrEmpty(id))
                return ls;
            // AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            //int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);


            string query = "select id,firstname+' '+lastname as name from Tbl_TpaClient_contact where TPAClient_LocID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["name"].ToString(),
                    Value = dt.Rows[i]["id"].ToString()
                });
            }

            return ls;
        }
        public JsonResult josngetTpaLocationContact(string id)
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            if (string.IsNullOrEmpty(id))
                return Json(ls,JsonRequestBehavior.AllowGet);
            // AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            //int id = Convert.ToInt32(System.Web.HttpContext.Current.Session["id"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);


            string query = "select id,firstname+' '+lastname as name from Tbl_TpaClient_contact where TPAClient_LocID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ls.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["name"].ToString(),
                    Value = dt.Rows[i]["id"].ToString()
                });
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }

        public static List<SelectListItem> ListTPAclient()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> lab = new List<SelectListItem>();

            string query = "Select TPALab_Id, TPALab_Name from  TPA_Labs";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                lab.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["TPALab_Name"].ToString(),
                    Value = dt.Rows[i]["TPALab_Id"].ToString()
                });
            }

            return lab;
        }
        public JsonResult jsonListTPAclient()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> lab = new List<SelectListItem>();

            string query = "Select TPALab_Id, TPALab_Name from  TPA_Labs";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                lab.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["TPALab_Name"].ToString(),
                    Value = dt.Rows[i]["TPALab_Id"].ToString()
                });
            }

            return Json(lab,JsonRequestBehavior.AllowGet);
        }
        public static List<SelectListItem> ListTPAlabtLocs(string Loc)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> loc = new List<SelectListItem>();
            if (string.IsNullOrEmpty(Loc))
                return loc;
                string query = "Select TPALab_LocId,Address_1 from  TPA_Labs_Loc where TPALab_Id = @TPALab_Id ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TPALab_Id", Loc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                loc.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Address_1"].ToString(),
                    Value = dt.Rows[i]["TPALab_LocId"].ToString()
                });
            }
            return loc;
        }
        public static List<SelectListItem> ListTPAlabContacts(string Con)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (string.IsNullOrEmpty(Con))
                return ls;
            if (!string.IsNullOrEmpty(Con))
            {
                string query = "Select FirstName,TPALab_Contact_Id from  TPA_Labs_Loc_Contact where TPALab_LocId=@TPALab_LocId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TPALab_LocId", Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["FirstName"].ToString(),
                        Value = dt.Rows[i]["TPALab_Contact_Id"].ToString()
                    });
                }
            }
            return ls;
        }
        public JsonResult ListTPAclientLoc(string Loc)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> loc = new List<SelectListItem>();
            if (string.IsNullOrEmpty(Loc))
                return Json(loc, JsonRequestBehavior.AllowGet);
            string query = "Select TPALab_LocId,Address_1 from  TPA_Labs_Loc where TPALab_Id = @TPALab_Id ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TPALab_Id", Loc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                loc.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Address_1"].ToString(),
                    Value = dt.Rows[i]["TPALab_LocId"].ToString()
                });
            }

            return Json(loc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListTPAclientContact(string Con)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (string.IsNullOrEmpty(Con))
                return Json(ls, JsonRequestBehavior.AllowGet);
            if (!string.IsNullOrEmpty(Con))
            {
                string query = "Select FirstName,TPALab_Contact_Id from  TPA_Labs_Loc_Contact where TPALab_LocId=@TPALab_LocId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TPALab_LocId", Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["FirstName"].ToString(),
                        Value = dt.Rows[i]["TPALab_Contact_Id"].ToString()
                    });
                }
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }
        
        public static List<SelectListItem> ListTPAlabService(string ser)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> loc = new List<SelectListItem>();

            string query = "select TPALab_Service_Id, Service_Grp_Name from  TPALab_Service_Grp where TPALab_Id = @TPALab_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TPALab_Id", ser);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                loc.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Service_Grp_Name"].ToString(),
                    Value = dt.Rows[i]["TPALab_Service_Id"].ToString(),
                    Selected = false
                });
            }

            return loc;
        }
        public static List<SelectListItem> ListTPAlabSubService(string SubSer)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(SubSer))
            {
                string query = "select TPALab_SubService_Id ,TPAlab_Service_Description from  TPA_Lab_SubService_Grp where TPALab_Service_Id=@TPALab_Service_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TPALab_Service_Id", SubSer);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["TPAlab_Service_Description"].ToString(),
                        Value = dt.Rows[i]["TPALab_SubService_Id"].ToString(),
                        Selected = false
                    });
                }
            }

            return ls;
        }
        public JsonResult JsonListTPAlabService(string ser)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> loc = new List<SelectListItem>();

            string query = "select TPALab_Service_Id, Service_Grp_Name from  TPALab_Service_Grp where TPALab_Id = @TPALab_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TPALab_Id", ser);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                loc.Add(new SelectListItem
                {

                    Text = dt.Rows[i]["Service_Grp_Name"].ToString(),
                    Value = dt.Rows[i]["TPALab_Service_Id"].ToString(),
                    Selected = false
                });
            }

            return Json(loc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonListTPAlabSubService(string SubSer)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<SelectListItem> ls = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(SubSer))
            {
                string query = "select TPALab_SubService_Id ,TPAlab_Service_Description from  TPA_Lab_SubService_Grp where TPALab_Service_Id=@TPALab_Service_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TPALab_Service_Id", SubSer);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ls.Add(new SelectListItem
                    {

                        Text = dt.Rows[i]["TPAlab_Service_Description"].ToString(),
                        Value = dt.Rows[i]["TPALab_SubService_Id"].ToString(),
                        Selected=false
                    });
                }
            }

            return Json(ls, JsonRequestBehavior.AllowGet);
        }
        public static List<SelectListItem> ListSpecimenType()
        {

            string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand selectCommand = new SqlCommand("tbl_specimanTypeList", con);
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<SelectListItem> specimanTypeList = new List<SelectListItem>();
            for (int index = 0; index < dataTable.Rows.Count; index++)
                specimanTypeList.Add(new SelectListItem
                {
                    Value = dataTable.Rows[index]["Id"].ToString().Trim(),
                    Text = string.IsNullOrEmpty(dataTable.Rows[index]["specimanType"].ToString().Trim()) ? string.Empty : dataTable.Rows[index]["specimanType"].ToString().Trim()
                });
            return specimanTypeList;
        }
    }
}