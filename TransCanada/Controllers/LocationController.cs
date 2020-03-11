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
using MvcBreadCrumbs;
using TransCanada.Models;
using TransCanadaDemo.Models;

namespace TransCanadaDemo.Controllers
{
    [BreadCrumb]
    public class LocationController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: Location
        public ActionResult Index()
        {
            Location_Model location = new Location_Model();
            return View(location);
        }
        [BreadCrumb(Label = "Locations")]
        public ActionResult LocationList()
        {
            try
            {

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("get_all_location", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accounts_id", System.Web.HttpContext.Current.Session["Account_id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<Location_Model> locationlist = new List<Location_Model>();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Location_Model LOC = new Location_Model();
                    LOC.Location_Id = Convert.ToInt32(dt.Rows[j]["Location_Id"].ToString());

                    if (!string.IsNullOrEmpty(dt.Rows[j]["Locations_LocationName"].ToString()))
                    {
                        LOC.Locations_LocationName = dt.Rows[j]["Locations_LocationName"].ToString();
                    }
                    else
                    {
                        LOC.Locations_LocationName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Locations_StreetAddress"].ToString()))
                    {
                        LOC.Locations_StreetAddress = dt.Rows[j]["Locations_StreetAddress"].ToString();
                    }
                    else
                    {
                        LOC.Locations_StreetAddress = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[j]["Locations_City"].ToString()))
                    {
                        LOC.Locations_City = dt.Rows[j]["Locations_City"].ToString();
                    }
                    else
                    {
                        LOC.Locations_City = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Locations_State"].ToString()))
                    {
                        LOC.Locations_State = dt.Rows[j]["Locations_State"].ToString();
                    }
                    else
                    {
                        LOC.Locations_State = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Locations_Zip"].ToString()))
                    {
                        LOC.Locations_Zip = dt.Rows[j]["Locations_Zip"].ToString();
                    }
                    else
                    {
                        LOC.Locations_Zip = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Locations_Main_Number"].ToString()))
                    {
                        LOC.Locations_Main_Number = dt.Rows[j]["Locations_Main_Number"].ToString();
                    }
                    else
                    {
                        LOC.Locations_Main_Number = string.Empty;
                    }
                    locationlist.Add(LOC);



                }
                //con.Open();
                //cmd.ExecuteNonQuery();
                //con.Close();
                return View(locationlist);
            }

            catch
            {
                return RedirectToAction("Errorpage", "Location");
            }

        }


        public ActionResult Create()
        {
            Location_Model location = new Location_Model();
            SqlConnection con = new SqlConnection(TransCanadaConnection);

            List<Location_Model> select = new List<Location_Model>();

            string query = "Select Lab_Id, LabsLabNameLabLocation from  Labs";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            location.ListLab = new List<Location_Model>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Location_Model lab = new Location_Model();
                lab.Lab_Id = Convert.ToInt32(dt.Rows[i]["Lab_Id"].ToString());
                lab.LabsLabNameLabLocation = dt.Rows[i]["LabsLabNameLabLocation"].ToString();

                select.Add(lab);
            }
            location.ListLab = select;

            return View(location);
        }
        [HttpPost]
        public ActionResult Create(Location_Model Location)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    SqlConnection con = new SqlConnection(TransCanadaConnection);
                    SqlCommand cmd = new SqlCommand("proc_insert_location", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@Createdon", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@Updatedon", DateTime.Now);
                    if (System.Web.HttpContext.Current.Session["Account_id"] != null)
                    {

                        cmd.Parameters.AddWithValue("@Accounts_Id", System.Web.HttpContext.Current.Session["Account_id"]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Accounts_Id", string.Empty);
                    }

                    cmd.Parameters.AddWithValue("@Createdby", System.Web.HttpContext.Current.User.Identity.GetUserName());




                    cmd.Parameters.AddWithValue("@Updatedby", System.Web.HttpContext.Current.User.Identity.GetUserName());





                    if (!string.IsNullOrEmpty(Location.Locations_LocationName))
                    {
                        cmd.Parameters.AddWithValue("@Locations_LocationName", Location.Locations_LocationName);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_LocationName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_StreetAddress))
                    {
                        cmd.Parameters.AddWithValue("@Locations_StreetAddress", Location.Locations_StreetAddress);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_StreetAddress", string.Empty);

                    }
                    if (!string.IsNullOrEmpty(Location.Locations_City))
                    {
                        cmd.Parameters.AddWithValue("@Locations_City", Location.Locations_City);

                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_City", string.Empty);

                    }
                    if (!string.IsNullOrEmpty(Location.Locations_State))
                    {
                        cmd.Parameters.AddWithValue("@Locations_State", Location.Locations_State);

                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_State", string.Empty);

                    }
                    if (!string.IsNullOrEmpty(Location.Locations_Zip))
                    {
                        cmd.Parameters.AddWithValue("@Locations_Zip", Location.Locations_Zip);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_Zip", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_Main_Number))
                    {
                        cmd.Parameters.AddWithValue("@Locations_Main_Number", Location.Locations_Main_Number);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_Main_Number", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ConatctWithANY))
                    {
                        cmd.Parameters.AddWithValue("@ConatctWithANY", Location.ConatctWithANY);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ConatctWithANY", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ShyBladdersLugWithNo))
                    {
                        cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", Location.ShyBladdersLugWithNo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ClientSample))
                    {
                        cmd.Parameters.AddWithValue("@ClientSample", Location.ClientSample);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ClientSample", string.Empty);
                    }

                    if (!string.IsNullOrEmpty(Location.RefusalToTest))
                    {
                        cmd.Parameters.AddWithValue("@RefusalToTest", Location.RefusalToTest);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RefusalToTest", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BatConfirmedPostive))
                    {
                        cmd.Parameters.AddWithValue("@BatConfirmedPostive", Location.BatConfirmedPostive);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BatConfirmedPostive", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.CancelledOrIncompleteTests))
                    {
                        cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", Location.CancelledOrIncompleteTests);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.AuthorizationFormsSentVia))
                    {
                        cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", Location.AuthorizationFormsSentVia);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_FullName))
                    {
                        cmd.Parameters.AddWithValue("@Contact_FullName", Location.Contact_FullName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_FullName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_JobTitle))
                    {
                        cmd.Parameters.AddWithValue("@Contact_JobTitle", Location.Contact_JobTitle);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_JobTitle", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_OfficeNumber))
                    {
                        cmd.Parameters.AddWithValue("@Contact_OfficeNumber", Location.Contact_OfficeNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_OfficeNumber", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_CellPhone))
                    {
                        cmd.Parameters.AddWithValue("@Contact_CellPhone", Location.Contact_CellPhone);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_CellPhone", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_Email))
                    {
                        cmd.Parameters.AddWithValue("@Contact_Email", Location.Contact_Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_Email", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_i3ScreenAccess))
                    {
                        cmd.Parameters.AddWithValue("@Locations_i3ScreenAccess", Location.Locations_i3ScreenAccess);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_i3ScreenAccess", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_BackgroundScreeningAccess))
                    {
                        cmd.Parameters.AddWithValue("@Locations_BackgroundScreeningAccess", Location.Locations_BackgroundScreeningAccess);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_BackgroundScreeningAccess", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_OccupationalMedicineAccess))
                    {
                        cmd.Parameters.AddWithValue("@Locations_OccupationalMedicineAccess", Location.Locations_OccupationalMedicineAccess);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_OccupationalMedicineAccess", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactFullName))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFullName", Location.BillingContactFullName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFullName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactOfficeNumber))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", Location.BillingContactOfficeNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactFax))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFax", Location.BillingContactFax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFax", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactEmail))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmail", Location.BillingContactEmail);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmail", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactStreetAddress))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactStreetAddress", Location.BillingContactStreetAddress);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactStreetAddress", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactCity))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactCity", Location.BillingContactCity);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactCity", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactState))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactState", Location.BillingContactState);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactState", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactZip))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactZip", Location.BillingContactZip);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactZip", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactEmailInvoices))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", Location.BillingContactEmailInvoices);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactNotes))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactNotes", Location.BillingContactNotes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactNotes", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactBillingOptions))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactBillingOptions", Location.BillingContactBillingOptions);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactBillingOptions", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttacheCopyCCF))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", Location.LabAccAttacheCopyCCF);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccLab1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab1", Location.LabAccLab1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAccountNumber1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber1", Location.LabAccAccountNumber1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccPannel))
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel", Location.LabAccPannel);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccTpa1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa1", Location.LabAccTpa1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccMro1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro1", Location.LabAccMro1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccSampleType1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType1", Location.LabAccSampleType1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttachment1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment1", Location.LabAccAttachment1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccCcf1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf1", Location.LabAccCcf1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccLab2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab2", Location.LabAccLab2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAccountNumber2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber2", Location.LabAccAccountNumber2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccPannel2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel2", Location.LabAccPannel2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccTpa2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa2", Location.LabAccTpa2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccMro2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro2", Location.LabAccMro2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccSampleType2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType2", Location.LabAccSampleType2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttachment2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment2", Location.LabAccAttachment2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccCcf2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf2", Location.LabAccCcf2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccLab3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab3", Location.LabAccLab3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAccountNumber3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber3", Location.LabAccAccountNumber3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccPannel3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel3", Location.LabAccPannel3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccTpa3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa3", Location.LabAccTpa3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccMro3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro3", Location.LabAccMro3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccSampleType3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType3", Location.LabAccSampleType3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttachment3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment3", Location.LabAccAttachment3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccCcf3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf3", Location.LabAccCcf3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ServicesProvided))
                    {
                        cmd.Parameters.AddWithValue("@ServicesProvided", Location.ServicesProvided);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ServicesProvided", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPPreEmployment))
                    {
                        cmd.Parameters.AddWithValue("@SPPreEmployment", Location.SPPreEmployment);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPPreEmployment", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPBackgroundPackagePreEmploy))
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", Location.SPBackgroundPackagePreEmploy);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPRandom))
                    {
                        cmd.Parameters.AddWithValue("@SPRandom", Location.SPRandom);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPRandom", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPPostAccident))
                    {
                        cmd.Parameters.AddWithValue("@SPPostAccident", Location.SPPostAccident);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPPostAccident", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPReasonableSuspicion))
                    {
                        cmd.Parameters.AddWithValue("@SPReasonableSuspicion", Location.SPReasonableSuspicion);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPReasonableSuspicion", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPFollowUp))
                    {
                        cmd.Parameters.AddWithValue("@SPFollowUp", Location.SPFollowUp);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPFollowUp", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPReturntoDuty))
                    {
                        cmd.Parameters.AddWithValue("@SPReturntoDuty", Location.SPReturntoDuty);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPReturntoDuty", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPAnnual))
                    {
                        cmd.Parameters.AddWithValue("@SPAnnual", Location.SPAnnual);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPAnnual", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPBackgroundPackageAnnual))
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", Location.SPBackgroundPackageAnnual);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPNegativeDilute))
                    {
                        cmd.Parameters.AddWithValue("@SPNegativeDilute", Location.SPNegativeDilute);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPNegativeDilute", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTPoolName))
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolName", Location.RPTPoolName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTPoolType))
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolType", Location.RPTPoolType);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolType", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTOwner))
                    {
                        cmd.Parameters.AddWithValue("@RPTOwner", Location.RPTOwner);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTOwner", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTPoolManager))
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolManager", Location.RPTPoolManager);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolManager", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTOwnerType))
                    {
                        cmd.Parameters.AddWithValue("@RPTOwnerType", Location.RPTOwnerType);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTOwnerType", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTdotnondot))
                    {
                        cmd.Parameters.AddWithValue("@RPTdotnondot", Location.RPTdotnondot);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTdotnondot", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTdotagency))
                    {
                        cmd.Parameters.AddWithValue("@RPTdotagency", Location.RPTdotagency);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTdotagency", string.Empty);
                    }
                    if (Location.RTPAlcoholGetsDrug == true)
                    {
                        cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", false);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPSelectionLevelforDrug))
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", Location.RTPSelectionLevelforDrug);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPSelectionLevelforAlcohol))
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforAlcohol", Location.RTPSelectionLevelforAlcohol);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforAlcohol", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent2))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent2", Location.RTPPercent2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPAlternatesforDrug))
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", Location.RTPAlternatesforDrug);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent3))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent3", Location.RTPPercent3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPAlternatesforAlcohol))
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", Location.RTPAlternatesforAlcohol);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent4))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent4", Location.RTPPercent4);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent4", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPNotes))
                    {
                        cmd.Parameters.AddWithValue("@RTPNotes", Location.RTPNotes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPNotes", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_Notes))
                    {
                        cmd.Parameters.AddWithValue("@Locations_Notes", Location.Locations_Notes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_Notes", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_Fax))
                    {
                        cmd.Parameters.AddWithValue("@Contact_Fax", Location.Contact_Fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_Fax", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent1))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent1", Location.RTPPercent1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent1", string.Empty);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return RedirectToAction("LocationList", "Location");
                }
                catch
                {
                    return RedirectToAction("Errorpage", "Location");
                }
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult UpdateLocation(string id)
        {
            try
            {
                Location_Model LOC = new Location_Model();
                SqlConnection conn = new SqlConnection(TransCanadaConnection);

                List<Location_Model> select = new List<Location_Model>();

                string query = "Select Lab_Id, LabsLabNameLabLocation from  Labs";
                SqlCommand cmd1 = new SqlCommand(query, conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();

                da1.Fill(dt1);

                LOC.ListLab = new List<Location_Model>();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Location_Model lab = new Location_Model();
                    lab.Lab_Id = Convert.ToInt32(dt1.Rows[i]["Lab_Id"].ToString());
                    lab.LabsLabNameLabLocation = (dt1.Rows[i]["LabsLabNameLabLocation"].ToString());

                    select.Add(lab);
                }
                LOC.ListLab = select;

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("get_location_by_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Location_Id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    LOC.Location_Id = Convert.ToInt32(dt.Rows[0]["Location_Id"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_LocationName"].ToString()))
                    {
                        LOC.Locations_LocationName = dt.Rows[0]["Locations_LocationName"].ToString();
                    }
                    else
                    {
                        LOC.Locations_LocationName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Updatedby"].ToString()))
                    {
                        LOC.Updatedby = dt.Rows[0]["Updatedby"].ToString();
                    }
                    else
                    {
                        LOC.Updatedby = string.Empty;
                    }


                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_StreetAddress"].ToString()))
                    {
                        LOC.Locations_StreetAddress = dt.Rows[0]["Locations_StreetAddress"].ToString();
                    }
                    else
                    {
                        LOC.Locations_StreetAddress = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_City"].ToString()))
                    {
                        LOC.Locations_City = dt.Rows[0]["Locations_City"].ToString();
                    }
                    else
                    {
                        LOC.Locations_City = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_State"].ToString()))
                    {
                        LOC.Locations_State = dt.Rows[0]["Locations_State"].ToString();
                    }
                    else
                    {
                        LOC.Locations_State = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_Zip"].ToString()))
                    {
                        LOC.Locations_Zip = dt.Rows[0]["Locations_Zip"].ToString();
                    }
                    else
                    {
                        LOC.Locations_Zip = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_Main_Number"].ToString()))
                    {
                        LOC.Locations_Main_Number = dt.Rows[0]["Locations_Main_Number"].ToString();
                    }
                    else
                    {
                        LOC.Locations_Main_Number = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_Notes"].ToString()))
                    {
                        LOC.Locations_Notes = dt.Rows[0]["Locations_Notes"].ToString();
                    }
                    else
                    {
                        LOC.Locations_Notes = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ConatctWithANY"].ToString()))
                    {
                        LOC.ConatctWithANY = dt.Rows[0]["ConatctWithANY"].ToString();
                    }
                    else
                    {
                        LOC.ConatctWithANY = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ShyBladdersLugWithNo"].ToString()))
                    {
                        LOC.ShyBladdersLugWithNo = dt.Rows[0]["ShyBladdersLugWithNo"].ToString();
                    }
                    else
                    {
                        LOC.ShyBladdersLugWithNo = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ClientSample"].ToString()))
                    {
                        LOC.ClientSample = dt.Rows[0]["ClientSample"].ToString();
                    }
                    else
                    {
                        LOC.ClientSample = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RefusalToTest"].ToString()))
                    {
                        LOC.RefusalToTest = dt.Rows[0]["RefusalToTest"].ToString();
                    }
                    else
                    {
                        LOC.RefusalToTest = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BatConfirmedPostive"].ToString()))
                    {
                        LOC.BatConfirmedPostive = dt.Rows[0]["BatConfirmedPostive"].ToString();
                    }
                    else
                    {
                        LOC.BatConfirmedPostive = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CancelledOrIncompleteTests"].ToString()))
                    {
                        LOC.CancelledOrIncompleteTests = dt.Rows[0]["CancelledOrIncompleteTests"].ToString();
                    }
                    else
                    {
                        LOC.CancelledOrIncompleteTests = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["AuthorizationFormsSentVia"].ToString()))
                    {
                        LOC.AuthorizationFormsSentVia = dt.Rows[0]["AuthorizationFormsSentVia"].ToString();
                    }
                    else
                    {
                        LOC.AuthorizationFormsSentVia = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_FullName"].ToString()))
                    {
                        LOC.Contact_FullName = dt.Rows[0]["Contact_FullName"].ToString();
                    }
                    else
                    {
                        LOC.Contact_FullName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_JobTitle"].ToString()))
                    {
                        LOC.Contact_JobTitle = dt.Rows[0]["Contact_JobTitle"].ToString();
                    }
                    else
                    {
                        LOC.Contact_JobTitle = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_OfficeNumber"].ToString()))
                    {
                        LOC.Contact_OfficeNumber = dt.Rows[0]["Contact_OfficeNumber"].ToString();
                    }
                    else
                    {
                        LOC.Contact_OfficeNumber = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_CellPhone"].ToString()))
                    {
                        LOC.Contact_CellPhone = dt.Rows[0]["Contact_CellPhone"].ToString();
                    }
                    else
                    {
                        LOC.Contact_CellPhone = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_Fax"].ToString()))
                    {
                        LOC.Contact_Fax = dt.Rows[0]["Contact_Fax"].ToString();
                    }
                    else
                    {
                        LOC.Contact_Fax = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_Email"].ToString()))
                    {
                        LOC.Contact_Email = dt.Rows[0]["Contact_Email"].ToString();
                    }
                    else
                    {
                        LOC.Contact_Email = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_i3ScreenAccess"].ToString()))
                    {
                        LOC.Locations_i3ScreenAccess = dt.Rows[0]["Locations_i3ScreenAccess"].ToString();
                    }
                    else
                    {
                        LOC.Locations_i3ScreenAccess = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_BackgroundScreeningAccess"].ToString()))
                    {
                        LOC.Locations_BackgroundScreeningAccess = dt.Rows[0]["Locations_BackgroundScreeningAccess"].ToString();
                    }
                    else
                    {
                        LOC.Locations_BackgroundScreeningAccess = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Locations_OccupationalMedicineAccess"].ToString()))
                    {
                        LOC.Locations_OccupationalMedicineAccess = dt.Rows[0]["Locations_OccupationalMedicineAccess"].ToString();
                    }
                    else
                    {
                        LOC.Locations_OccupationalMedicineAccess = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactFullName"].ToString()))
                    {
                        LOC.BillingContactFullName = dt.Rows[0]["BillingContactFullName"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactFullName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactOfficeNumber"].ToString()))
                    {
                        LOC.BillingContactOfficeNumber = dt.Rows[0]["BillingContactOfficeNumber"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactOfficeNumber = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactFax"].ToString()))
                    {
                        LOC.BillingContactFax = dt.Rows[0]["BillingContactFax"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactFax = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactEmail"].ToString()))
                    {
                        LOC.BillingContactEmail = dt.Rows[0]["BillingContactEmail"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactEmail = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactStreetAddress"].ToString()))
                    {
                        LOC.BillingContactStreetAddress = dt.Rows[0]["BillingContactStreetAddress"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactStreetAddress = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactCity"].ToString()))
                    {
                        LOC.BillingContactCity = dt.Rows[0]["BillingContactCity"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactCity = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactState"].ToString()))
                    {
                        LOC.BillingContactState = dt.Rows[0]["BillingContactState"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactState = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactZip"].ToString()))
                    {
                        LOC.BillingContactZip = dt.Rows[0]["BillingContactZip"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactZip = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactEmailInvoices"].ToString()))
                    {
                        LOC.BillingContactEmailInvoices = dt.Rows[0]["BillingContactEmailInvoices"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactEmailInvoices = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactNotes"].ToString()))
                    {
                        LOC.BillingContactNotes = dt.Rows[0]["BillingContactNotes"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactNotes = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["BillingContactBillingOptions"].ToString()))
                    {
                        LOC.BillingContactBillingOptions = dt.Rows[0]["BillingContactBillingOptions"].ToString();
                    }
                    else
                    {
                        LOC.BillingContactBillingOptions = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttacheCopyCCF"].ToString()))
                    {
                        LOC.LabAccAttacheCopyCCF = dt.Rows[0]["LabAccAttacheCopyCCF"].ToString();
                    }
                    else
                    {
                        LOC.LabAccAttacheCopyCCF = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccLab1"].ToString()))
                    {
                        LOC.LabAccLab1 = dt.Rows[0]["LabAccLab1"].ToString();
                    }
                    else
                    {
                        LOC.LabAccLab1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAccountNumber1"].ToString()))
                    {
                        LOC.LabAccAccountNumber1 = dt.Rows[0]["LabAccAccountNumber1"].ToString();
                    }
                    else
                    {
                        LOC.LabAccAccountNumber1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccPannel"].ToString()))
                    {
                        LOC.LabAccPannel = dt.Rows[0]["LabAccPannel"].ToString();
                    }
                    else
                    {
                        LOC.LabAccPannel = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccTpa1"].ToString()))
                    {
                        LOC.LabAccTpa1 = dt.Rows[0]["LabAccTpa1"].ToString();
                    }
                    else
                    {
                        LOC.LabAccTpa1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccMro1"].ToString()))
                    {
                        LOC.LabAccMro1 = dt.Rows[0]["LabAccMro1"].ToString();
                    }
                    else
                    {
                        LOC.LabAccMro1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccSampleType1"].ToString()))
                    {
                        LOC.LabAccSampleType1 = dt.Rows[0]["LabAccSampleType1"].ToString();
                    }
                    else
                    {
                        LOC.LabAccSampleType1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttachment1"].ToString()))
                    {
                        LOC.LabAccAttachment1 = dt.Rows[0]["LabAccAttachment1"].ToString();
                    }
                    else
                    {
                        LOC.LabAccAttachment1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccCcf1"].ToString()))
                    {
                        LOC.LabAccCcf1 = dt.Rows[0]["LabAccCcf1"].ToString();
                    }
                    else
                    {
                        LOC.LabAccCcf1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccLab2"].ToString()))
                    {
                        LOC.LabAccLab2 = dt.Rows[0]["LabAccLab2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccLab2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAccountNumber2"].ToString()))
                    {
                        LOC.LabAccAccountNumber2 = dt.Rows[0]["LabAccAccountNumber2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccAccountNumber2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccPannel2"].ToString()))
                    {
                        LOC.LabAccPannel2 = dt.Rows[0]["LabAccPannel2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccPannel2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccTpa2"].ToString()))
                    {
                        LOC.LabAccTpa2 = dt.Rows[0]["LabAccTpa2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccTpa2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccMro2"].ToString()))
                    {
                        LOC.LabAccMro2 = dt.Rows[0]["LabAccMro2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccMro2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccSampleType2"].ToString()))
                    {
                        LOC.LabAccSampleType2 = dt.Rows[0]["LabAccSampleType2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccSampleType2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttachment2"].ToString()))
                    {
                        LOC.LabAccAttachment2 = dt.Rows[0]["LabAccAttachment2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccAttachment2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccCcf2"].ToString()))
                    {
                        LOC.LabAccCcf2 = dt.Rows[0]["LabAccCcf2"].ToString();
                    }
                    else
                    {
                        LOC.LabAccCcf2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccLab3"].ToString()))
                    {
                        LOC.LabAccLab3 = dt.Rows[0]["LabAccLab3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccLab3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAccountNumber3"].ToString()))
                    {
                        LOC.LabAccAccountNumber3 = dt.Rows[0]["LabAccAccountNumber3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccAccountNumber3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccPannel3"].ToString()))
                    {
                        LOC.LabAccPannel3 = dt.Rows[0]["LabAccPannel3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccPannel3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccTpa3"].ToString()))
                    {
                        LOC.LabAccTpa3 = dt.Rows[0]["LabAccTpa3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccTpa3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccMro3"].ToString()))
                    {
                        LOC.LabAccMro3 = dt.Rows[0]["LabAccMro3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccMro3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccSampleType3"].ToString()))
                    {
                        LOC.LabAccSampleType3 = dt.Rows[0]["LabAccSampleType3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccSampleType3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccAttachment3"].ToString()))
                    {
                        LOC.LabAccAttachment3 = dt.Rows[0]["LabAccAttachment3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccAttachment3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabAccCcf3"].ToString()))
                    {
                        LOC.LabAccCcf3 = dt.Rows[0]["LabAccCcf3"].ToString();
                    }
                    else
                    {
                        LOC.LabAccCcf3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ServicesProvided"].ToString()))
                    {
                        LOC.ServicesProvided = dt.Rows[0]["ServicesProvided"].ToString();
                    }
                    else
                    {
                        LOC.ServicesProvided = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPPreEmployment"].ToString()))
                    {
                        LOC.SPPreEmployment = dt.Rows[0]["SPPreEmployment"].ToString();
                    }
                    else
                    {
                        LOC.SPPreEmployment = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPBackgroundPackagePreEmploy"].ToString()))
                    {
                        LOC.SPBackgroundPackagePreEmploy = dt.Rows[0]["SPBackgroundPackagePreEmploy"].ToString();
                    }
                    else
                    {
                        LOC.SPBackgroundPackagePreEmploy = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPRandom"].ToString()))
                    {
                        LOC.SPRandom = dt.Rows[0]["SPRandom"].ToString();
                    }
                    else
                    {
                        LOC.SPRandom = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPPostAccident"].ToString()))
                    {
                        LOC.SPPostAccident = dt.Rows[0]["SPPostAccident"].ToString();
                    }
                    else
                    {
                        LOC.SPPostAccident = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPReasonableSuspicion"].ToString()))
                    {
                        LOC.SPReasonableSuspicion = dt.Rows[0]["SPReasonableSuspicion"].ToString();
                    }
                    else
                    {
                        LOC.SPReasonableSuspicion = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPFollowUp"].ToString()))
                    {
                        LOC.SPFollowUp = dt.Rows[0]["SPFollowUp"].ToString();
                    }
                    else
                    {
                        LOC.SPFollowUp = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPReturntoDuty"].ToString()))
                    {
                        LOC.SPReturntoDuty = dt.Rows[0]["SPReturntoDuty"].ToString();
                    }
                    else
                    {
                        LOC.SPReturntoDuty = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPAnnual"].ToString()))
                    {
                        LOC.SPAnnual = dt.Rows[0]["SPAnnual"].ToString();
                    }
                    else
                    {
                        LOC.SPAnnual = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPBackgroundPackageAnnual"].ToString()))
                    {
                        LOC.SPBackgroundPackageAnnual = dt.Rows[0]["SPBackgroundPackageAnnual"].ToString();
                    }
                    else
                    {
                        LOC.SPBackgroundPackageAnnual = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SPNegativeDilute"].ToString()))
                    {
                        LOC.SPNegativeDilute = dt.Rows[0]["SPNegativeDilute"].ToString();
                    }
                    else
                    {
                        LOC.SPNegativeDilute = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RPTPoolName"].ToString()))
                    {
                        LOC.RPTPoolName = dt.Rows[0]["RPTPoolName"].ToString();
                    }
                    else
                    {
                        LOC.RPTPoolName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RPTPoolType"].ToString()))
                    {
                        LOC.RPTPoolType = dt.Rows[0]["RPTPoolType"].ToString();
                    }
                    else
                    {
                        LOC.RPTPoolType = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RPTOwner"].ToString()))
                    {
                        LOC.RPTOwner = dt.Rows[0]["RPTOwner"].ToString();
                    }
                    else
                    {
                        LOC.RPTOwner = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RPTPoolManager"].ToString()))
                    {
                        LOC.RPTPoolManager = dt.Rows[0]["RPTPoolManager"].ToString();
                    }
                    else
                    {
                        LOC.RPTPoolManager = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RPTOwnerType"].ToString()))
                    {
                        LOC.RPTOwnerType = dt.Rows[0]["RPTOwnerType"].ToString();
                    }
                    else
                    {
                        LOC.RPTOwnerType = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RPTdotnondot"].ToString()))
                    {
                        LOC.RPTdotnondot = dt.Rows[0]["RPTdotnondot"].ToString();
                    }
                    else
                    {
                        LOC.RPTdotnondot = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RPTdotagency"].ToString()))
                    {
                        LOC.RPTdotagency = dt.Rows[0]["RPTdotagency"].ToString();
                    }
                    else
                    {
                        LOC.RPTdotagency = string.Empty;
                    }
                    if (LOC.RTPAlcoholGetsDrug == true)
                    {
                        LOC.RTPAlcoholGetsDrug = true;
                    }
                    else
                    {
                        LOC.RTPAlcoholGetsDrug = false;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPSelectionLevelforDrug"].ToString()))
                    {
                        LOC.RTPSelectionLevelforDrug = dt.Rows[0]["RTPSelectionLevelforDrug"].ToString();
                    }
                    else
                    {
                        LOC.RTPSelectionLevelforDrug = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPPercent1"].ToString()))
                    {
                        LOC.RTPPercent1 = dt.Rows[0]["RTPPercent1"].ToString();
                    }
                    else
                    {
                        LOC.RTPPercent1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPSelectionLevelforAlcohol"].ToString()))
                    {
                        LOC.RTPSelectionLevelforAlcohol = dt.Rows[0]["RTPSelectionLevelforAlcohol"].ToString();
                    }
                    else
                    {
                        LOC.RTPSelectionLevelforAlcohol = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPPercent2"].ToString()))
                    {
                        LOC.RTPPercent2 = dt.Rows[0]["RTPPercent2"].ToString();
                    }
                    else
                    {
                        LOC.RTPPercent2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPAlternatesforDrug"].ToString()))
                    {
                        LOC.RTPAlternatesforDrug = dt.Rows[0]["RTPAlternatesforDrug"].ToString();
                    }
                    else
                    {
                        LOC.RTPAlternatesforDrug = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPPercent3"].ToString()))
                    {
                        LOC.RTPPercent3 = dt.Rows[0]["RTPPercent3"].ToString();
                    }
                    else
                    {
                        LOC.RTPPercent3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPAlternatesforAlcohol"].ToString()))
                    {
                        LOC.RTPAlternatesforAlcohol = dt.Rows[0]["RTPAlternatesforAlcohol"].ToString();
                    }
                    else
                    {
                        LOC.RTPAlternatesforAlcohol = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPPercent4"].ToString()))
                    {
                        LOC.RTPPercent4 = dt.Rows[0]["RTPPercent4"].ToString();
                    }
                    else
                    {
                        LOC.RTPPercent4 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["RTPNotes"].ToString()))
                    {
                        LOC.RTPNotes = dt.Rows[0]["RTPNotes"].ToString();
                    }
                    else
                    {
                        LOC.RTPNotes = string.Empty;
                    }
                }



                return View(LOC);
            }
            catch
            {
                return RedirectToAction("Errorpage", "Location");
            }
        }

        [HttpPost]
        public ActionResult UpdateLocation(Location_Model Location)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    SqlConnection con = new SqlConnection(TransCanadaConnection);
                    SqlCommand cmd = new SqlCommand("proc_update_location", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (Location.Location_Id != 0)
                    {
                        cmd.Parameters.AddWithValue("@Location_Id", Convert.ToInt32(Location.Location_Id));
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Location_Id", string.Empty);
                    }
                    //cmd.Parameters.AddWithValue("@Updatedon",DateTime.Now);


                    cmd.Parameters.AddWithValue("@Updatedby", System.Web.HttpContext.Current.User.Identity.GetUserName());

                    //cmd.Parameters.AddWithValue("@Updatedby", string.Empty);

                    if (!string.IsNullOrEmpty(Location.Locations_LocationName))
                    {
                        cmd.Parameters.AddWithValue("@Locations_LocationName", Location.Locations_LocationName);
                    }


                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_LocationName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_StreetAddress))
                    {
                        cmd.Parameters.AddWithValue("@Locations_StreetAddress", Location.Locations_StreetAddress);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_StreetAddress", string.Empty);

                    }
                    if (!string.IsNullOrEmpty(Location.Locations_City))
                    {
                        cmd.Parameters.AddWithValue("@Locations_City", Location.Locations_City);

                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_City", string.Empty);

                    }
                    if (!string.IsNullOrEmpty(Location.Locations_State))
                    {
                        cmd.Parameters.AddWithValue("@Locations_State", Location.Locations_State);

                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_State", string.Empty);

                    }
                    if (!string.IsNullOrEmpty(Location.Locations_Zip))
                    {
                        cmd.Parameters.AddWithValue("@Locations_Zip", Location.Locations_Zip);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_Zip", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_Main_Number))
                    {
                        cmd.Parameters.AddWithValue("@Locations_Main_Number", Location.Locations_Main_Number);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_Main_Number", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ConatctWithANY))
                    {
                        cmd.Parameters.AddWithValue("@ConatctWithANY", Location.ConatctWithANY);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ConatctWithANY", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ShyBladdersLugWithNo))
                    {
                        cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", Location.ShyBladdersLugWithNo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ShyBladdersLugWithNo", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ClientSample))
                    {
                        cmd.Parameters.AddWithValue("@ClientSample", Location.ClientSample);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ClientSample", string.Empty);
                    }

                    if (!string.IsNullOrEmpty(Location.RefusalToTest))
                    {
                        cmd.Parameters.AddWithValue("@RefusalToTest", Location.RefusalToTest);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RefusalToTest", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BatConfirmedPostive))
                    {
                        cmd.Parameters.AddWithValue("@BatConfirmedPostive", Location.BatConfirmedPostive);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BatConfirmedPostive", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.CancelledOrIncompleteTests))
                    {
                        cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", Location.CancelledOrIncompleteTests);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CancelledOrIncompleteTests", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.AuthorizationFormsSentVia))
                    {
                        cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", Location.AuthorizationFormsSentVia);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AuthorizationFormsSentVia", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_FullName))
                    {
                        cmd.Parameters.AddWithValue("@Contact_FullName", Location.Contact_FullName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_FullName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_JobTitle))
                    {
                        cmd.Parameters.AddWithValue("@Contact_JobTitle", Location.Contact_JobTitle);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_JobTitle", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_Fax))
                    {
                        cmd.Parameters.AddWithValue("@Contact_Fax", Location.Contact_Fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_Fax", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_OfficeNumber))
                    {
                        cmd.Parameters.AddWithValue("@Contact_OfficeNumber", Location.Contact_OfficeNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_OfficeNumber", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_CellPhone))
                    {
                        cmd.Parameters.AddWithValue("@Contact_CellPhone", Location.Contact_CellPhone);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_CellPhone", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Contact_Email))
                    {
                        cmd.Parameters.AddWithValue("@Contact_Email", Location.Contact_Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Contact_Email", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_i3ScreenAccess))
                    {
                        cmd.Parameters.AddWithValue("@Locations_i3ScreenAccess", Location.Locations_i3ScreenAccess);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_i3ScreenAccess", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_BackgroundScreeningAccess))
                    {
                        cmd.Parameters.AddWithValue("@Locations_BackgroundScreeningAccess", Location.Locations_BackgroundScreeningAccess);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_BackgroundScreeningAccess", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_OccupationalMedicineAccess))
                    {
                        cmd.Parameters.AddWithValue("@Locations_OccupationalMedicineAccess", Location.Locations_OccupationalMedicineAccess);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_OccupationalMedicineAccess", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactFullName))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFullName", Location.BillingContactFullName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFullName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactOfficeNumber))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", Location.BillingContactOfficeNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactOfficeNumber", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactFax))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFax", Location.BillingContactFax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactFax", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactEmail))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmail", Location.BillingContactEmail);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmail", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactStreetAddress))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactStreetAddress", Location.BillingContactStreetAddress);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactStreetAddress", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactCity))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactCity", Location.BillingContactCity);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactCity", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactState))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactState", Location.BillingContactState);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactState", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactZip))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactZip", Location.BillingContactZip);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactZip", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactEmailInvoices))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", Location.BillingContactEmailInvoices);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactEmailInvoices", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactNotes))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactNotes", Location.BillingContactNotes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactNotes", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.BillingContactBillingOptions))
                    {
                        cmd.Parameters.AddWithValue("@BillingContactBillingOptions", Location.BillingContactBillingOptions);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@BillingContactBillingOptions", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttacheCopyCCF))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", Location.LabAccAttacheCopyCCF);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttacheCopyCCF", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccLab1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab1", Location.LabAccLab1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAccountNumber1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber1", Location.LabAccAccountNumber1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccPannel))
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel", Location.LabAccPannel);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccTpa1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa1", Location.LabAccTpa1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccMro1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro1", Location.LabAccMro1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccSampleType1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType1", Location.LabAccSampleType1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttachment1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment1", Location.LabAccAttachment1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccCcf1))
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf1", Location.LabAccCcf1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf1", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccLab2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab2", Location.LabAccLab2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAccountNumber2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber2", Location.LabAccAccountNumber2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccPannel2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel2", Location.LabAccPannel2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccTpa2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa2", Location.LabAccTpa2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccMro2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro2", Location.LabAccMro2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccSampleType2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType2", Location.LabAccSampleType2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttachment2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment2", Location.LabAccAttachment2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccCcf2))
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf2", Location.LabAccCcf2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccLab3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab3", Location.LabAccLab3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccLab3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAccountNumber3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber3", Location.LabAccAccountNumber3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAccountNumber3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccPannel3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel3", Location.LabAccPannel3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccPannel3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccTpa3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa3", Location.LabAccTpa3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccTpa3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccMro3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro3", Location.LabAccMro3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccMro3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.Locations_Notes))
                    {
                        cmd.Parameters.AddWithValue("@Locations_Notes", Location.Locations_Notes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Locations_Notes", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccSampleType3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType3", Location.LabAccSampleType3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccSampleType3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccAttachment3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment3", Location.LabAccAttachment3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccAttachment3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.LabAccCcf3))
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf3", Location.LabAccCcf3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LabAccCcf3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.ServicesProvided))
                    {
                        cmd.Parameters.AddWithValue("@ServicesProvided", Location.ServicesProvided);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ServicesProvided", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPPreEmployment))
                    {
                        cmd.Parameters.AddWithValue("@SPPreEmployment", Location.SPPreEmployment);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPPreEmployment", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPBackgroundPackagePreEmploy))
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", Location.SPBackgroundPackagePreEmploy);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackagePreEmploy", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPRandom))
                    {
                        cmd.Parameters.AddWithValue("@SPRandom", Location.SPRandom);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPRandom", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPPostAccident))
                    {
                        cmd.Parameters.AddWithValue("@SPPostAccident", Location.SPPostAccident);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPPostAccident", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPReasonableSuspicion))
                    {
                        cmd.Parameters.AddWithValue("@SPReasonableSuspicion", Location.SPReasonableSuspicion);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPReasonableSuspicion", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPFollowUp))
                    {
                        cmd.Parameters.AddWithValue("@SPFollowUp", Location.SPFollowUp);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPFollowUp", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPReturntoDuty))
                    {
                        cmd.Parameters.AddWithValue("@SPReturntoDuty", Location.SPReturntoDuty);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPReturntoDuty", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPAnnual))
                    {
                        cmd.Parameters.AddWithValue("@SPAnnual", Location.SPAnnual);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPAnnual", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPBackgroundPackageAnnual))
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", Location.SPBackgroundPackageAnnual);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPBackgroundPackageAnnual", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.SPNegativeDilute))
                    {
                        cmd.Parameters.AddWithValue("@SPNegativeDilute", Location.SPNegativeDilute);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SPNegativeDilute", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTPoolName))
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolName", Location.RPTPoolName);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolName", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTPoolType))
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolType", Location.RPTPoolType);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolType", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTOwner))
                    {
                        cmd.Parameters.AddWithValue("@RPTOwner", Location.RPTOwner);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTOwner", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTPoolManager))
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolManager", Location.RPTPoolManager);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTPoolManager", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTOwnerType))
                    {
                        cmd.Parameters.AddWithValue("@RPTOwnerType", Location.RPTOwnerType);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTOwnerType", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTdotnondot))
                    {
                        cmd.Parameters.AddWithValue("@RPTdotnondot", Location.RPTdotnondot);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTdotnondot", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RPTdotagency))
                    {
                        cmd.Parameters.AddWithValue("@RPTdotagency", Location.RPTdotagency);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RPTdotagency", string.Empty);
                    }
                    if (Location.RTPAlcoholGetsDrug == true)
                    {
                        cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPAlcoholGetsDrug", false);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPSelectionLevelforDrug))
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", Location.RTPSelectionLevelforDrug);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforDrug", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPSelectionLevelforAlcohol))
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforAlcohol", Location.RTPSelectionLevelforAlcohol);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPSelectionLevelforAlcohol", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent2))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent2", Location.RTPPercent2);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent2", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPAlternatesforDrug))
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", Location.RTPAlternatesforDrug);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforDrug", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent3))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent3", Location.RTPPercent3);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent3", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPAlternatesforAlcohol))
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", Location.RTPAlternatesforAlcohol);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPAlternatesforAlcohol", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent4))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent4", Location.RTPPercent4);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent4", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPNotes))
                    {
                        cmd.Parameters.AddWithValue("@RTPNotes", Location.RTPNotes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPNotes", string.Empty);
                    }
                    if (!string.IsNullOrEmpty(Location.RTPPercent1))
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent1", Location.RTPPercent1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RTPPercent1", string.Empty);
                    }




                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return RedirectToAction("LocationList", "Location");
                }
                catch
                {
                    return RedirectToAction("Errorpage", "Location");
                }
            }
            else
            {
                return View();
            }

        }


        public ActionResult Deletelocatoin(string id)
        {
            try
            {
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("delete_locaton", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Location_Id", id);



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                var controller = (Request.UrlReferrer.Segments.Skip(1).Take(1).SingleOrDefault() ?? "dashboard").Trim('/');
                if (controller.ToString().Trim() != "Location")
                    return RedirectToAction("Index", "DashBoard");
                else
                   return RedirectToAction("LocationList", "Location");
            }

            catch
            {
                return RedirectToAction("Errorpage", "Location");
            }
        }
        public ActionResult Errorpage()
        {

            return View();
        }

        public static List<SelectListItem> ListI3ScreenAccess()
        {

            List<SelectListItem> Sa = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "NO",
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
            return Sa;
        }
        public static List<SelectListItem> ListBackgroundScreeningAccess()
        {

            List<SelectListItem> Bgm = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "NO",
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
            return Bgm;
        }

        public static List<SelectListItem> ListOccupationalMedicineAccess()
        {

            List<SelectListItem> Oms = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "NO",
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
            return Oms;
        }
        public static List<SelectListItem> ListSamplType()
        {

            List<SelectListItem> St = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Urine, Hair, Oral Fluid,",
                    Value = "1"
                },

              };
            return St;
        }
        public static List<SelectListItem> ListCCF()
        {

            List<SelectListItem> Ccf = new List<SelectListItem>() {

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
            return Ccf;
        }
        public static List<SelectListItem> ListSamplType2()
        {

            List<SelectListItem> St2 = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Urine, Hair, Oral Fluid",
                    Value = "1"
                },

              };
            return St2;
        }
        public static List<SelectListItem> ListCCF2()
        {

            List<SelectListItem> Ccf = new List<SelectListItem>() {

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
            return Ccf;
        }
        public static List<SelectListItem> ListSamplType3()
        {

            List<SelectListItem> St = new List<SelectListItem>() {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Urine, Hair, Oral Fluid",
                    Value = "1"
                },

              };
            return St;
        }
        public static List<SelectListItem> ListCCF3()
        {

            List<SelectListItem> Ccf = new List<SelectListItem>() {

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
            return Ccf;
        }
        public static List<SelectListItem> ListPoolType()
        {

            List<SelectListItem> Pt = new List<SelectListItem>() {

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
            return Pt;
        }

        public static List<SelectListItem> ListOwnerType()
        {

            List<SelectListItem> Ot = new List<SelectListItem>() {

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
            return Ot;
        }
        public static List<SelectListItem> ListDotNOnDot()
        {

            List<SelectListItem> Dn = new List<SelectListItem>() {

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
            return Dn;
        }
        public static List<SelectListItem> ListDotAgency()
        {

            List<SelectListItem> Da = new List<SelectListItem>() {

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
            return Da;
        }
        public ActionResult Location()
        {
            return RedirectToAction("Locations");
        }

        [BreadCrumb(Clear =true, Label = "Location List")]
        public ActionResult Locations(string id)
        {  
            if (string.IsNullOrEmpty(id))
            {
                if (Session["Account_Id"] == null)
                    return RedirectToAction("Account_List", "Home");
            }
            if (string.IsNullOrEmpty(id))
            {
                id = System.Web.HttpContext.Current.Session["Account_id"].ToString().Trim();
                TempData["client_name"] = id;
                TempData["client_id"] = Getidbyclientname(id);
                ViewBag.BacktoClient = string.Empty;
            }
            else
            {
                TempData["client_name"] = id;
                TempData["client_id"] = Getidbyclientname(id);
                ViewBag.BacktoClient = id;
            }
            SqlConnection conn = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("proc_get_all_location", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@companyid", id.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Location> clientList = new List<Location>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                Location TC = new Location();
                if (!string.IsNullOrEmpty(dt.Rows[j]["Location_id"].ToString()))
                {

                    TC.Location_id = Convert.ToInt32(dt.Rows[j]["Location_id"].ToString());

                }
                else
                {
                    TC.Location_id = 0;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Location"].ToString()))
                {

                    TC.Location_id1 = dt.Rows[j]["Location"].ToString();

                }
                else
                {
                    TC.Location_id1 = dt.Rows[j]["Location_id1"].ToString();
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["Location_Name"].ToString()))
                {

                    TC.Location_Name = dt.Rows[j]["Location_Name"].ToString();

                }
                else
                {
                    TC.Location_Name = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[j]["address_Type"].ToString()))
                {

                    TC.ltype = dt.Rows[j]["address_Type"].ToString();

                }
                else
                {
                    TC.ltype = string.Empty;
                }
                //if (!string.IsNullOrEmpty(dt.Rows[j]["Location_Contact_Person"].ToString()))
                //{

                //    TC.Location_Contact_Person = dt.Rows[j]["Location_Contact_Person"].ToString();

                //}
                //else
                //{
                //    TC.Location_Contact_Person = string.Empty;
                //}

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
                    string Client_State = selectListItems.Where(p => p.Value == dt.Rows[j]["State"].ToString().ToUpper()).First().Text;
                    TC.State = Client_State;
                }
                else
                {
                    TC.State = string.Empty;
                }


                clientList.Add(TC);



            }

            return View(clientList);

        }
        [BreadCrumb(Label = "New Location")]
        public ActionResult NewLocation(string id)
        {
            Location location = new Location();
            ClientController clientController = new ClientController();
            location.Cities = clientController.GetAllCities(string.Empty);
            if (string.IsNullOrEmpty(id))
            {
                location.Location_Name = System.Web.HttpContext.Current.Session["Account_id"].ToString();
            }
            else
            {
                location.Location_Name = id.Trim();
            }
            return View(location);

        }
        [HttpPost]
        public ActionResult NewLocation(Location client)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(client.City))
                {
                    client.City= string.Empty;
                }
                if (string.IsNullOrEmpty(client.State))
                {
                    client.State = string.Empty;
                }
                if (string.IsNullOrEmpty(client.Zip))
                {
                    client.Zip = string.Empty;
                }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand command = new SqlCommand("proc_new_demo_location", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Location_Name", client.Location_Name);

                if (!string.IsNullOrEmpty(client.Address_1))
                {
                    command.Parameters.AddWithValue("@Address_1", client.Address_1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address_1", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Address_2))
                {
                    command.Parameters.AddWithValue("@Address_2", client.@Address_2);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address_2", string.Empty);
                }

                command.Parameters.AddWithValue("@City", client.City);
                command.Parameters.AddWithValue("@State", client.State);
                command.Parameters.AddWithValue("@Country", client.Country);

                if (!string.IsNullOrEmpty(client.WebSite))
                {
                    command.Parameters.AddWithValue("@Website", client.WebSite);
                }
                else
                {
                    command.Parameters.AddWithValue("@Website", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Location_1))
                {
                    command.Parameters.AddWithValue("@Location", client.Location_1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Location", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Notes))
                {
                    command.Parameters.AddWithValue("@Notes", client.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Phone_number))
                {
                    command.Parameters.AddWithValue("@Phone_number", client.Phone_number);
                }
                else
                {
                    command.Parameters.AddWithValue("@Phone_number", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.email))
                {
                    command.Parameters.AddWithValue("@email", client.email);
                }
                else
                {
                    command.Parameters.AddWithValue("@email", string.Empty);
                }
                command.Parameters.AddWithValue("@zip", client.Zip);
                command.Parameters.AddWithValue("@company_id", client.Location_Name);
                command.Parameters.AddWithValue("@address_Type", "BRANCH");
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                ClientController clientController = new ClientController();
                if (client.State == null)
                    client.Cities = clientController.GetAllCities(string.Empty);
                else
                    client.Cities = clientController.GetAllCities(client.State);
                return View(client);
            }

            return RedirectToAction("Locations",new { id = client.Location_Name });

        }
        [BreadCrumb(Label = "Update Location")]
        public ActionResult UpdateLocations(int id)
        {
            TempData["client_name"] = getclientnamebyid(id);
            SqlConnection con = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("proc_get_demo_location_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Location_id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Location TC = new Location();

            if (dt.Rows.Count > 0)
            {

                TC.Location_id = id;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Location_Name"].ToString()))
                {

                    TC.Location_Name = dt.Rows[0]["Location_Name"].ToString();

                }
                else
                {
                    TC.Location_Name = string.Empty;
                }
                //if (!string.IsNullOrEmpty(dt.Rows[0]["Location_Contact_Person"].ToString()))
                //{

                //    TC.Location_Contact_Person = dt.Rows[0]["Location_Contact_Person"].ToString();

                //}
                //else
                //{
                //    TC.Location_Contact_Person = string.Empty;
                //}

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
                    ClientController clientController = new ClientController();
                    TC.Cities = clientController.GetAllCities(dt.Rows[0]["State"].ToString());
                }
                else
                {
                    TC.State = string.Empty;
                    ClientController clientController = new ClientController();
                    TC.Cities = clientController.GetAllCities(string.Empty);
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["Zip"].ToString()))
                {
                    TC.Zip = dt.Rows[0]["Zip"].ToString();
                }
                else
                {
                    TC.Zip = string.Empty;
                }

                //if (!string.IsNullOrEmpty(dt.Rows[0]["Email"].ToString()))
                //{
                //    TC.Email = dt.Rows[0]["Email"].ToString();
                //}
                //else
                //{
                //    TC.Email = string.Empty;
                //}
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
                if (!string.IsNullOrEmpty(dt.Rows[0]["Location"].ToString()))
                {
                    TC.Location_1 = dt.Rows[0]["Location"].ToString();
                }
                else
                {
                    TC.Location_1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Notes"].ToString()))
                {
                    TC.Notes = dt.Rows[0]["Notes"].ToString();
                }
                else
                {
                    TC.Notes = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Phone_number"].ToString()))
                {
                    TC.Phone_number = dt.Rows[0]["Phone_number"].ToString();
                }
                else
                {
                    TC.Phone_number = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["email"].ToString()))
                {
                    TC.email = dt.Rows[0]["email"].ToString();
                }
                else
                {
                    TC.email = string.Empty;
                }
            }
            return View(TC);

        }
        [HttpPost]
        public ActionResult UpdateLocations(Location client)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(client.City))
                {
                    client.City = string.Empty;
                }
                if (string.IsNullOrEmpty(client.State))
                {
                    client.State = string.Empty;
                }
                if (string.IsNullOrEmpty(client.Zip))
                {
                    client.Zip = string.Empty;
                }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand command = new SqlCommand("proc_update_demo_location", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Location_id", client.Location_id);
                command.Parameters.AddWithValue("@Location_Name", client.Location_Name);

                if (!string.IsNullOrEmpty(client.Address_1))
                {
                    command.Parameters.AddWithValue("@Address_1", client.Address_1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address_1", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Address_2))
                {
                    command.Parameters.AddWithValue("@Address_2", client.@Address_2);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address_2", string.Empty);
                }
                command.Parameters.AddWithValue("@City", client.City);
                command.Parameters.AddWithValue("@State", client.State);
                command.Parameters.AddWithValue("@Country", client.Country);
                if (!string.IsNullOrEmpty(client.WebSite))
                {
                    command.Parameters.AddWithValue("@Website", client.WebSite);
                }
                else
                {
                    command.Parameters.AddWithValue("@Website", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Location_1))
                {
                    command.Parameters.AddWithValue("@Location", client.Location_1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Location", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Notes))
                {
                    command.Parameters.AddWithValue("@Notes", client.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.Phone_number))
                {
                    command.Parameters.AddWithValue("@Phone_number", client.Phone_number);
                }
                else
                {
                    command.Parameters.AddWithValue("@Phone_number", string.Empty);
                }
                if (!string.IsNullOrEmpty(client.email))
                {
                    command.Parameters.AddWithValue("@email", client.email);
                }
                else
                {
                    command.Parameters.AddWithValue("@email", string.Empty);
                }
                command.Parameters.AddWithValue("@zip", client.Zip);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                ClientController clientController = new ClientController();
                if (client.State == null)
                    client.Cities = clientController.GetAllCities(string.Empty);
                else
                    client.Cities = clientController.GetAllCities(client.State);
                return View(client);
            }

            return RedirectToAction("Locations",new { id=client.Location_Name});

        }
        public ActionResult deleteLocation(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("proc_delete_demo_location_by_id", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Location_id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            var controller = (Request.UrlReferrer.Segments.Skip(1).Take(1).SingleOrDefault() ?? "dashboard").Trim('/');
            if (controller.ToString().Trim() != "Location")
                return RedirectToAction("Index", "DashBoard");
            else
                return RedirectToAction("Locations", "Location");
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
        [BreadCrumb(Label = "Contact List")]
        public ActionResult LocContactList(int id)
        {
            TempData["CLient_loc"] = getnamebyid(id);
            TempData["client_name"] = getclientnamebyid(id);
            System.Web.HttpContext.Current.Session["Location_id"] = id;
            SqlConnection conn = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("proc_List_client_contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("location_id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Lab_contact> ContactList = new List<Lab_contact>();
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    Lab_contact contact = new Lab_contact();
                    if (!string.IsNullOrEmpty(dt.Rows[j]["client_contact_id"].ToString()))
                    {

                        contact.contact_id = Convert.ToInt32(dt.Rows[j]["client_contact_id"].ToString());

                    }
                    else
                    {
                        contact.contact_id = 0;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Location_id"].ToString()))
                    {

                        contact.location_id = dt.Rows[j]["Location_id"].ToString();

                    }
                    else
                    {
                        contact.location_id = "0";
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["firstname"].ToString()))
                    {

                        contact.firstname = (dt.Rows[j]["firstname"].ToString());

                    }
                    else
                    {
                        contact.firstname = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Lastname"].ToString()))
                    {

                        contact.Lastname = (dt.Rows[j]["Lastname"].ToString());

                    }
                    else
                    {
                        contact.Lastname = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["email"].ToString()))
                    {

                        contact.email = (dt.Rows[j]["email"].ToString());

                    }
                    else
                    {
                        contact.email = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["cell"].ToString()))
                    {

                        contact.cell = (dt.Rows[j]["cell"].ToString());

                    }
                    else
                    {
                        contact.cell = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["officephone"].ToString()))
                    {

                        contact.officephone = (dt.Rows[j]["officephone"].ToString());

                    }
                    else
                    {
                        contact.officephone = string.Empty;
                    }
                    ContactList.Add(contact);
                }
            }
            return View(ContactList);
        }
        [BreadCrumb(Label = "New Contact")]
        public ActionResult CreateLocContact()
        {
            Lab_contact Icontact = new Lab_contact();
            //ClientController clientController = new ClientController();
            //location.Cities = clientController.GetAllCities(string.Empty);
            Icontact.location_id = System.Web.HttpContext.Current.Session["Location_id"].ToString();
            return View(Icontact);

        }
        [HttpPost]
        public ActionResult CreateLocContact(Lab_contact Icontact)
        {
            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand command = new SqlCommand("proc_insert_client_contact", con);
                command.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(Icontact.Email1))
                {
                    command.Parameters.AddWithValue("@Email1", Icontact.Email1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Icontact.Phone1))
                {
                    command.Parameters.AddWithValue("@Phone1", Icontact.Phone1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Phone1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Icontact.Role))
                {
                    command.Parameters.AddWithValue("@Role", Icontact.Role);
                }
                else
                {
                    command.Parameters.AddWithValue("@Role", string.Empty);
                }
                if (!string.IsNullOrEmpty(Icontact.Title))
                {
                    command.Parameters.AddWithValue("@Title", Icontact.Title);
                }
                else
                {
                    command.Parameters.AddWithValue("@Title", string.Empty);
                }
                if (!string.IsNullOrEmpty(Icontact.Notes))
                {
                    command.Parameters.AddWithValue("@Notes", Icontact.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", string.Empty);
                }
                command.Parameters.AddWithValue("@location_id", Icontact.location_id);
                if (!string.IsNullOrEmpty(Icontact.firstname))
                {
                    command.Parameters.AddWithValue("@firstname", Icontact.firstname);
                }
                else
                {
                    command.Parameters.AddWithValue("@firstname", string.Empty);
                }
                if (!string.IsNullOrEmpty(Icontact.Lastname))
                {
                    command.Parameters.AddWithValue("@Lastname", Icontact.Lastname);
                }
                else
                {
                    command.Parameters.AddWithValue("@Lastname", string.Empty);
                }
                if (!string.IsNullOrEmpty(Icontact.email))
                {
                    command.Parameters.AddWithValue("@email", Icontact.email);
                }
                else
                {
                    command.Parameters.AddWithValue("@email", string.Empty);
                }


                
                if (Icontact.officephone != null)
                    command.Parameters.AddWithValue("@officephone", Icontact.officephone);
                else
                    command.Parameters.AddWithValue("@officephone", string.Empty);
                if (!string.IsNullOrEmpty(Icontact.cell))
                {
                    command.Parameters.AddWithValue("@cell", Icontact.cell);
                }
                else
                {
                    command.Parameters.AddWithValue("@cell", string.Empty);
                }
                if (!string.IsNullOrEmpty(Icontact.Third_Phone))
                {
                    command.Parameters.AddWithValue("@Third_Phone", Icontact.Third_Phone);
                }
                else
                {
                    command.Parameters.AddWithValue("@Third_Phone", string.Empty);
                }
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                return View(Icontact);
            }

            return RedirectToAction("LocContactList", new { id = Icontact.location_id });

        }

        [HttpGet]
        [BreadCrumb(Label = "Update Contact")]
        public ActionResult UpdateLocContact(int id)
        {
            TempData["CLient_loc"] = getnamebyid(id);
            TempData["client_name"] = GetClientnameByLocConId(id);
            SqlConnection con = new SqlConnection(TransCanadaConnection);
            SqlCommand cmd = new SqlCommand("proc_edit_client_contact", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@client_contact_id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Lab_contact Ucontact = new Lab_contact();

            if (dt.Rows.Count > 0)
            {

                // TC.Location_id = id;
                if (!string.IsNullOrEmpty(dt.Rows[0]["Role"].ToString()))
                {

                    Ucontact.Role = (dt.Rows[0]["Role"].ToString());

                }
                else
                {
                    Ucontact.Role = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Phone1"].ToString()))
                {

                    Ucontact.Phone1 = (dt.Rows[0]["Phone1"].ToString());

                }
                else
                {
                    Ucontact.Phone1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Email1"].ToString()))
                {

                    Ucontact.Email1 = (dt.Rows[0]["Email1"].ToString());

                }
                else
                {
                    Ucontact.Email1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Title"].ToString()))
                {

                    Ucontact.Title = (dt.Rows[0]["Title"].ToString());

                }
                else
                {
                    Ucontact.Title = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Notes"].ToString()))
                {

                    Ucontact.Notes = (dt.Rows[0]["Notes"].ToString());

                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["client_contact_id"].ToString()))
                {

                    Ucontact.contact_id = Convert.ToInt32(dt.Rows[0]["client_contact_id"].ToString());

                }
                else
                {
                    Ucontact.contact_id = 0;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Location_id"].ToString()))
                {

                    Ucontact.location_id = dt.Rows[0]["Location_id"].ToString();

                }
                else
                {
                    Ucontact.location_id = "0";
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["firstname"].ToString()))
                {

                    Ucontact.firstname = (dt.Rows[0]["firstname"].ToString());

                }
                else
                {
                    Ucontact.firstname = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Lastname"].ToString()))
                {

                    Ucontact.Lastname = (dt.Rows[0]["Lastname"].ToString());

                }
                else
                {
                    Ucontact.Lastname = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["email"].ToString()))
                {

                    Ucontact.email = (dt.Rows[0]["email"].ToString());

                }
                else
                {
                    Ucontact.email = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["officephone"].ToString()))
                {

                    Ucontact.officephone = (dt.Rows[0]["officephone"].ToString());

                }
                else
                {
                    Ucontact.officephone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["cell"].ToString()))
                {

                    Ucontact.cell = (dt.Rows[0]["cell"].ToString());

                }
                else
                {
                    Ucontact.cell = string.Empty;
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Third_Phone"].ToString()))
                {

                    Ucontact.Third_Phone = (dt.Rows[0]["Third_Phone"].ToString());

                }
                else
                {
                    Ucontact.Third_Phone = string.Empty;
                }
            }
            return View(Ucontact);
        }


        [HttpPost]
        public ActionResult UpdateLocContact(Lab_contact Ucontact)
        {
            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                SqlCommand command = new SqlCommand("proc_Update_client_contact", con);
                command.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(Ucontact.Email1))
                {
                    command.Parameters.AddWithValue("@Email1", Ucontact.Email1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.Phone1))
                {
                    command.Parameters.AddWithValue("@Phone1", Ucontact.Phone1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Phone1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.Role))
                {
                    command.Parameters.AddWithValue("@Role", Ucontact.Role);
                }
                else
                {
                    command.Parameters.AddWithValue("@Role", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.Title))
                {
                    command.Parameters.AddWithValue("@Title", Ucontact.Title);
                }
                else
                {
                    command.Parameters.AddWithValue("@Title", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.Notes))
                {
                    command.Parameters.AddWithValue("@Notes", Ucontact.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.firstname))
                {
                    command.Parameters.AddWithValue("@firstname", Ucontact.firstname);
                }
                else
                {
                    command.Parameters.AddWithValue("@firstname", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.Lastname))
                {
                    command.Parameters.AddWithValue("@Lastname", Ucontact.Lastname);
                }
                else
                {
                    command.Parameters.AddWithValue("@Lastname", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.email))
                {
                    command.Parameters.AddWithValue("@email", Ucontact.email);
                }
                else
                {
                    command.Parameters.AddWithValue("@email", string.Empty);
                }
                command.Parameters.AddWithValue("@client_contact_id", Ucontact.contact_id);
                command.Parameters.AddWithValue("@Location_id", Ucontact.location_id);


                if (Ucontact.officephone != null)
                    command.Parameters.AddWithValue("@officephone", Ucontact.officephone);
                else
                    command.Parameters.AddWithValue("@officephone", string.Empty);
                if (!string.IsNullOrEmpty(Ucontact.cell))
                {
                    command.Parameters.AddWithValue("@cell", Ucontact.cell);
                }
                else
                {
                    command.Parameters.AddWithValue("@cell", string.Empty);
                }
                if (!string.IsNullOrEmpty(Ucontact.Third_Phone))
                {
                    command.Parameters.AddWithValue("@Third_Phone", Ucontact.Third_Phone);
                }
                else
                {
                    command.Parameters.AddWithValue("@Third_Phone", string.Empty);
                }
                con.Open();
                command.ExecuteNonQuery();
                con.Close();

            }
            else
            {

                return View(Ucontact);
            }

            return RedirectToAction("LocContactList", new { id = Ucontact.location_id });

        }
        public ActionResult DeleteLocContact(int id, int del_id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("proc_delete_client_contact", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@client_contact_id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            var controller = (Request.UrlReferrer.Segments.Skip(1).Take(1).SingleOrDefault() ?? "Home").Trim('/');
            if (controller.ToString().Trim() != "Location")
                return RedirectToAction("Index", "DashBoard");
            else
                return RedirectToAction("LocContactList", new { id = del_id });

        }
        public string getnamebyid(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.GetLocationByLId (@ID)", con);
            cmd.Parameters.AddWithValue("@ID",id);
            con.Open();
            name =Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;

        }

        public string getclientnamebyid(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.GetClientnameByCId (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;
        }

        public string GetClientnameByLocConId(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.GetClientnameByLocConId (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;
        }

        public string GetLocationnameBycId(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.GetLocationnameBycId (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;
        }
        public string Getidbyclientname(string id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.Getidbyclientname (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;
        }
    }
}

