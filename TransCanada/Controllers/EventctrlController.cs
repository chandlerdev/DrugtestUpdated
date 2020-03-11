using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using TransCanadaDemo.Models;

namespace TransCanada.Controllers
{
    public class EventctrlController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: Eventctrl
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Evelist()
        {

            SqlConnection con = new SqlConnection(TransCanadaConnection);
            String query = "Select * from tbl_Eventlist where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Events_mdl> eventsList = new List<Events_mdl>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                Events_mdl Eve = new Events_mdl();


                Eve.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());
                if (!string.IsNullOrEmpty(dt.Rows[j]["Eventid"].ToString()))
                {
                    Eve.Eventid = dt.Rows[j]["Eventid"].ToString();
                }
                else
                {
                    Eve.Eventid = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Eventbillingid"].ToString()))
                {
                    Eve.Eventbillingid = dt.Rows[j]["Eventbillingid"].ToString();
                }
                else
                {
                    Eve.Eventbillingid = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Main_service"].ToString()))
                {
                    Eve.Main_service = dt.Rows[j]["Main_service"].ToString();
                }
                else
                {
                    Eve.Main_service = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Sub_services"].ToString()))
                {
                    Eve.Sub_services = dt.Rows[j]["Sub_services"].ToString();
                }
                else
                {
                    Eve.Sub_services = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Cost"].ToString()))
                {
                    Eve.Cost = dt.Rows[j]["Cost"].ToString();
                }
                else
                {
                    Eve.Cost = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Billing_cost"].ToString()))
                {
                    Eve.Billing_cost = dt.Rows[j]["Billing_cost"].ToString();
                }
                else
                {
                    Eve.Billing_cost = string.Empty;
                }

                eventsList.Add(Eve);
            }


            return View(eventsList);
        }
        public ActionResult InsertEvents(string id)
        {
            Events_mdl Eve = new Events_mdl();
            Eve.Eventid = id.ToString();
            return View(Eve);
        }

        [HttpPost]
        public ActionResult InsertEvents(Events_mdl Eve)
        {
            try
            {
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("InsertEve", con);
                cmd.CommandType = CommandType.StoredProcedure;


                if (!string.IsNullOrEmpty(Eve.Eventid))
                {
                    cmd.Parameters.AddWithValue("@Eventid", Eve.Eventid);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Eventid", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Eventbillingid))
                {
                    cmd.Parameters.AddWithValue("@Eventbillingid", Eve.Eventbillingid);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Eventbillingid", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Main_service))
                {
                    cmd.Parameters.AddWithValue("@Main_service", Eve.Main_service);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Main_service", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Sub_services))
                {
                    cmd.Parameters.AddWithValue("@Sub_services", Eve.Sub_services);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sub_services", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Cost))
                {
                    cmd.Parameters.AddWithValue("@Cost", Eve.Cost);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cost", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Billing_cost))
                {
                    cmd.Parameters.AddWithValue("@Billing_cost", Eve.Billing_cost);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Billing_cost", string.Empty);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("UpdateEvent", "Event",new { id= Eve.Eventid });

            }
            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Eventctrl");
            }


        }
        public ActionResult ErrorPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateEvents(string id)
        {
            try
            {

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("EditEve", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Events_mdl Eve = new Events_mdl();  
                if (dt.Rows.Count > 0)
                {
                    Eve.Id =Convert.ToInt32( dt.Rows[0]["Id"].ToString());

                    if (!string.IsNullOrEmpty(dt.Rows[0]["Eventid"].ToString()))
                    {
                        Eve.Eventid = dt.Rows[0]["Eventid"].ToString();
                    }
                    else
                    {
                        Eve.Eventid = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Eventbillingid"].ToString()))
                    {
                        Eve.Eventbillingid = dt.Rows[0]["Eventbillingid"].ToString();
                    }
                    else
                    {
                        Eve.Eventbillingid = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Main_service"].ToString()))
                    {
                        Eve.Main_service = dt.Rows[0]["Main_service"].ToString();
                    }
                    else
                    {
                        Eve.Main_service = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Sub_services"].ToString()))
                    {
                        Eve.Sub_services = dt.Rows[0]["Sub_services"].ToString();
                    }
                    else
                    {
                        Eve.Sub_services = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Cost"].ToString()))
                    {
                        Eve.Cost = dt.Rows[0]["Cost"].ToString();
                    }
                    else
                    {
                        Eve.Cost = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Billing_cost"].ToString()))
                    {
                        Eve.Billing_cost = dt.Rows[0]["Billing_cost"].ToString();
                    }
                    else
                    {
                        Eve.Billing_cost = string.Empty;
                    }



                }
                
                return View(Eve);
            }

            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Eventctrl");
            }

        }

        [HttpPost]
        public ActionResult UpdateEvents(Events_mdl Eve)
        {
            try
            {

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("UpdateEve", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(Eve.Id));
                if (!string.IsNullOrEmpty(Eve.Eventid))
                {
                    cmd.Parameters.AddWithValue("@Eventid", Eve.Eventid);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Eventid", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Eventbillingid))
                {
                    cmd.Parameters.AddWithValue("@Eventbillingid", Eve.Eventbillingid);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Eventbillingid", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Main_service))
                {
                    cmd.Parameters.AddWithValue("@Main_service", Eve.Main_service);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Main_service", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Sub_services))
                {
                    cmd.Parameters.AddWithValue("@Sub_services", Eve.Sub_services);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sub_services", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Cost))
                {
                    cmd.Parameters.AddWithValue("@Cost", Eve.Cost);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Cost", string.Empty);
                }
                if (!string.IsNullOrEmpty(Eve.Billing_cost))
                {
                    cmd.Parameters.AddWithValue("@Billing_cost", Eve.Billing_cost);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Billing_cost", string.Empty);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("UpdateEvent", "Event",new { id= Eve .Eventid});

            }

            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Eventctrl");
            }

        }
        public ActionResult DeleteEvents(string id)
        {
            try
            {
                int id1=0;
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand command = new SqlCommand("Select Eventid from tbl_Eventlist where id=@id", con);
                command.Parameters.AddWithValue("@id", id);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                if(dataTable.Rows.Count>0)
                {
                    id1 =Convert.ToInt32(dataTable.Rows[0]["Eventid"].ToString());
                }
                SqlCommand cmd = new SqlCommand("DeleteEve", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("UpdateEvent", "Event", new { id = id1 });

            }
            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Eventctrl");

            }
        }

    }
}