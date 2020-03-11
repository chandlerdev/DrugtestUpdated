using Microsoft.AspNet.Identity;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;
using MvcBreadCrumbs;

namespace TransCanada.Controllers
{
    [BreadCrumb]
    public class ServiceProviderController : Controller
    {
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
        
        public ActionResult Index()
        {
            return (ActionResult)this.View();
        }
       [BreadCrumb(Clear =true, Label = "ProviderList")]
        public ActionResult ProviderList()
        {

            SqlCommand selectCommand = new SqlCommand("select Serviceprovider_id,Serviceprovider_Name from Tbl_Service_Provider ", new SqlConnection(this.TransCanadaConnection));

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            List<Service_provider> serviceProviderList = new List<Service_provider>();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
                serviceProviderList.Add(new Service_provider()
                {
                    Serviceprovider_id = Convert.ToInt32(dataTable.Rows[index]["Serviceprovider_id"].ToString()),
                    Serviceprovider_Name = string.IsNullOrEmpty(dataTable.Rows[index]["Serviceprovider_Name"].ToString()) ? string.Empty : dataTable.Rows[index]["Serviceprovider_Name"].ToString()
                });
            return (ActionResult)this.View((object)serviceProviderList);
        }
        [BreadCrumb(Label = "SP Locations")]
        public ActionResult SpLocations(string id)
        {
            TempData["sp_name"] = id.ToString();
            ViewBag.providername = id.Trim();
            List<Service_provider> serviceProviderList = new List<Service_provider>();

            SqlCommand selectCommand = new SqlCommand("Tbl_service_location_list", new SqlConnection(TransCanadaConnection));
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)id);
            DataTable dataTable = new DataTable();
            new SqlDataAdapter(selectCommand).Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                {
                    Service_provider serviceProvider = new Service_provider();
                    if (!string.IsNullOrEmpty(dataTable.Rows[index]["Serviceprovider_id"].ToString()))
                        serviceProvider.Serviceprovider_id = Convert.ToInt32(dataTable.Rows[index]["Serviceprovider_id"].ToString());
                    else
                        serviceProvider.Serviceprovider_Name = string.Empty;
                    serviceProvider.Serviceprovider_Name = string.IsNullOrEmpty(dataTable.Rows[index]["Serviceprovider_Name"].ToString()) ? string.Empty : dataTable.Rows[index]["Serviceprovider_Name"].ToString();
                    //serviceProvider.Contact_Person = string.IsNullOrEmpty(dataTable.Rows[index]["Contact_Person"].ToString()) ? string.Empty : dataTable.Rows[index]["Contact_Person"].ToString();
                    serviceProvider.Location = string.IsNullOrEmpty(dataTable.Rows[index]["Location"].ToString()) ? string.Empty : dataTable.Rows[index]["Location"].ToString();
                    serviceProvider.Address_1 = string.IsNullOrEmpty(dataTable.Rows[index]["Address_1"].ToString()) ? string.Empty : dataTable.Rows[index]["Address_1"].ToString();
                    serviceProvider.Address_2 = string.IsNullOrEmpty(dataTable.Rows[index]["Address_2"].ToString()) ? string.Empty : dataTable.Rows[index]["Address_2"].ToString();
                    serviceProvider.City = string.IsNullOrEmpty(dataTable.Rows[index]["City"].ToString()) ? string.Empty : dataTable.Rows[index]["City"].ToString();
                    serviceProvider.State = string.IsNullOrEmpty(dataTable.Rows[index]["State"].ToString()) ? string.Empty : dataTable.Rows[index]["State"].ToString();
                    serviceProvider.Zip = string.IsNullOrEmpty(dataTable.Rows[index]["Zip"].ToString()) ? string.Empty : dataTable.Rows[index]["Zip"].ToString();
                    serviceProvider.Country = string.IsNullOrEmpty(dataTable.Rows[index]["Country"].ToString()) ? string.Empty : dataTable.Rows[index]["Country"].ToString();
                    serviceProvider.WebSite = string.IsNullOrEmpty(dataTable.Rows[index]["WebSite"].ToString()) ? string.Empty : dataTable.Rows[index]["WebSite"].ToString();
                    serviceProviderList.Add(serviceProvider);
                }
            }
            return (ActionResult)this.View((object)serviceProviderList);
        }
        [BreadCrumb(Label = "New SP Location")]
        public ActionResult CreateSpLocation(string id)
        {
            return (ActionResult)this.View((object)new Sp_Location()
            {
                Serviceprovider_Name = id
            });
        }

        [HttpPost]
        public ActionResult CreateSpLocation(Sp_Location Service)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)Service);
            try
            {
                SqlConnection connection = new SqlConnection(this.TransCanadaConnection);
                SqlCommand sqlCommand = new SqlCommand("Tbl_service_location_insert", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(Service.Serviceprovider_Name))
                    sqlCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)Service.Serviceprovider_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)string.Empty);
                //if (!string.IsNullOrEmpty(Service.Contact_Person))
                //    sqlCommand.Parameters.AddWithValue("@Contact_Person", (object)Service.Contact_Person);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Contact_Person", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Address_1))
                    sqlCommand.Parameters.AddWithValue("@Address_1", (object)Service.Address_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_1", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Address_2))
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)Service.Address_2);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.City))
                    sqlCommand.Parameters.AddWithValue("@City", (object)Service.City);
                else
                    sqlCommand.Parameters.AddWithValue("@City", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.State))
                    sqlCommand.Parameters.AddWithValue("@State", (object)Service.State);
                else
                    sqlCommand.Parameters.AddWithValue("@State", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Zip))
                    sqlCommand.Parameters.AddWithValue("@Zip", (object)Service.Zip);
                else
                    sqlCommand.Parameters.AddWithValue("@Zip", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Country))
                    sqlCommand.Parameters.AddWithValue("@Country", (object)Service.Country);
                else
                    sqlCommand.Parameters.AddWithValue("@Country", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.WebSite))
                    sqlCommand.Parameters.AddWithValue("@WebSite", (object)Service.WebSite);
                else
                    sqlCommand.Parameters.AddWithValue("@WebSite", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Location_1))
                    sqlCommand.Parameters.AddWithValue("@Location", (object)Service.Location_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Location", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Notes))
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)Service.Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)string.Empty);
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return (ActionResult)this.RedirectToAction("SpLocations", "ServiceProvider", (object)new
                {
                    id = Service.Serviceprovider_Name
                });

            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        
        [HttpGet]
        [BreadCrumb(Label = "Update SP Location")]
        public ActionResult UpdateSploc(string id)
        {
            SqlCommand selectCommand = new SqlCommand("Tbl_service_location_edit", new SqlConnection(TransCanadaConnection));
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@Serviceprovider_id", (object)id);
            sqlDataAdapter.Fill(dataTable);
            Sp_Location serviceProvider = new Sp_Location();
            if (dataTable.Rows.Count > 0)
            {
                serviceProvider.Serviceprovider_id = Convert.ToInt32(dataTable.Rows[0]["Serviceprovider_id"].ToString());
                serviceProvider.Serviceprovider_Name = string.IsNullOrEmpty(dataTable.Rows[0]["Serviceprovider_Name"].ToString()) ? string.Empty : dataTable.Rows[0]["Serviceprovider_Name"].ToString();
                //serviceProvider.Contact_Person = string.IsNullOrEmpty(dataTable.Rows[0]["Contact_Person"].ToString()) ? string.Empty : dataTable.Rows[0]["Contact_Person"].ToString();
                serviceProvider.Address_1 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_1"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_1"].ToString();
                serviceProvider.Address_2 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_2"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_2"].ToString();
                serviceProvider.City = string.IsNullOrEmpty(dataTable.Rows[0]["City"].ToString()) ? string.Empty : dataTable.Rows[0]["City"].ToString();
                serviceProvider.State = string.IsNullOrEmpty(dataTable.Rows[0]["State"].ToString()) ? string.Empty : dataTable.Rows[0]["State"].ToString();
                serviceProvider.Zip = string.IsNullOrEmpty(dataTable.Rows[0]["Zip"].ToString()) ? string.Empty : dataTable.Rows[0]["Zip"].ToString();
                serviceProvider.Country = string.IsNullOrEmpty(dataTable.Rows[0]["Country"].ToString()) ? string.Empty : dataTable.Rows[0]["Country"].ToString();
                serviceProvider.WebSite = string.IsNullOrEmpty(dataTable.Rows[0]["WebSite"].ToString()) ? string.Empty : dataTable.Rows[0]["WebSite"].ToString();
                serviceProvider.Location_1 = string.IsNullOrEmpty(dataTable.Rows[0]["Location"].ToString()) ? string.Empty : dataTable.Rows[0]["Location"].ToString();
                serviceProvider.Notes = string.IsNullOrEmpty(dataTable.Rows[0]["Notes"].ToString()) ? string.Empty : dataTable.Rows[0]["Notes"].ToString();

            }
            return (ActionResult)this.View((object)serviceProvider);
        }

        [HttpPost]
        public ActionResult UpdateSploc(Sp_Location Service)
        {
            //if (!ModelState.IsValid)
            //    return (ActionResult)this.View((object)Service);
            SqlConnection connection = new SqlConnection(TransCanadaConnection);
            SqlCommand sqlCommand = new SqlCommand("Tbl_service_location_Update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Serviceprovider_id", (object)Service.Serviceprovider_id);
            sqlCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)Service.Serviceprovider_Name);
            //if (!string.IsNullOrEmpty(Service.Contact_Person))
            //  sqlCommand.Parameters.AddWithValue("@Contact_Person", (object) Service.Contact_Person);
            //else
            sqlCommand.Parameters.AddWithValue("@Contact_Person", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.Address_1))
                sqlCommand.Parameters.AddWithValue("@Address_1", (object)Service.Address_1);
            else
                sqlCommand.Parameters.AddWithValue("@Address_1", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.Address_2))
                sqlCommand.Parameters.AddWithValue("@Address_2", (object)Service.Address_2);
            else
                sqlCommand.Parameters.AddWithValue("@Address_2", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.City))
                sqlCommand.Parameters.AddWithValue("@City", (object)Service.City);
            else
                sqlCommand.Parameters.AddWithValue("@City", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.State))
                sqlCommand.Parameters.AddWithValue("@State", (object)Service.State);
            else
                sqlCommand.Parameters.AddWithValue("@State", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.Zip))
                sqlCommand.Parameters.AddWithValue("@Zip", (object)Service.Zip);
            else
                sqlCommand.Parameters.AddWithValue("@Zip", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.Country))
                sqlCommand.Parameters.AddWithValue("@Country", (object)Service.Country);
            else
                sqlCommand.Parameters.AddWithValue("@Country", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.WebSite))
                sqlCommand.Parameters.AddWithValue("@WebSite", (object)Service.WebSite);
            else
                sqlCommand.Parameters.AddWithValue("@WebSite", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.Location_1))
                sqlCommand.Parameters.AddWithValue("@Location", (object)Service.Location_1);
            else
                sqlCommand.Parameters.AddWithValue("@Location", (object)string.Empty);
            if (!string.IsNullOrEmpty(Service.Notes))
                sqlCommand.Parameters.AddWithValue("@Notes", (object)Service.Notes);
            else
                sqlCommand.Parameters.AddWithValue("@Notes", (object)string.Empty);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.RedirectToAction("SpLocations", "ServiceProvider", (object)new
            {
                id = Service.Serviceprovider_Name
            });
        }
        [BreadCrumb(Label = "New Service Provider")]
        public ActionResult CreateProvider()
        {
            List<SelectListItem> Sevice = new List<SelectListItem>()
            {
                  new SelectListItem {Text="Individual",Value="Individual",Selected=false },
                   new SelectListItem {Text="PSC",Value="PSC",Selected=false},
                   new SelectListItem {Text="PPN",Value="PPN",Selected=false },
                   new SelectListItem {Text="Third Party",Value="Third Party",Selected=false},

            };
            List<SelectListItem> Sevice1 = new List<SelectListItem>()
            {
                  new SelectListItem {Text="CEO",Value="CEO",Selected=false },
                   new SelectListItem {Text="Ops Manager",Value="Ops Manager",Selected=false},
                   new SelectListItem {Text="Collector",Value="Collector",Selected=false },
                    new SelectListItem {Text="Scheduling",Value="Scheduling",Selected=false },
                     new SelectListItem {Text="Invoicing",Value="Invoicing",Selected=false },

            };
            List<SelectListItem> Sevice2 = new List<SelectListItem>()
            {
                  new SelectListItem {Text="Mailing",Value="Mailing",Selected=false },
                   new SelectListItem {Text="On-Site",Value="On-Site",Selected=false},
                   new SelectListItem {Text="Billing",Value="Billing",Selected=false },
            };
            //List<SelectListItem> Sevice3 = new List<SelectListItem>()
            //{

            //};

            Service_provider objBind = new Service_provider();
            objBind.Category1 = Sevice;
            objBind.Title1 = Sevice1;
            objBind.Address_Type1 = Sevice2;
            objBind.List_Gender = Gender();
            //objBind.Servicing_which_clients1 = Sevice3;
            return View(objBind);
            //return (ActionResult) this.View();
        }

        [HttpPost]
        
        public ActionResult CreateProvider(Service_provider Service, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Service.List_Gender = Gender();
                    return View(Service);
                }
                string userName1 = IdentityExtensions.GetUserName(this.User.Identity);
                //string userName2 = IdentityExtensions.GetUserName(this.User.Identity);
                SqlConnection connection = new SqlConnection(TransCanadaConnection);
                SqlCommand sqlCommand = new SqlCommand("Tbl_ServiceProvider_Insert", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Createdby", userName1);
                sqlCommand.Parameters.AddWithValue("@Updatedby", userName1);
                if (!string.IsNullOrEmpty(Service.Serviceprovider_Name))
                    sqlCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)Service.Serviceprovider_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Contact_Person))
                    sqlCommand.Parameters.AddWithValue("@Contact_Person", (object)Service.Contact_Person);
                else
                    sqlCommand.Parameters.AddWithValue("@Contact_Person", (object)string.Empty);


                if (!string.IsNullOrEmpty(Service.Address_1))
                    sqlCommand.Parameters.AddWithValue("@Address_1", (object)Service.Address_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_1", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Address_2))
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)Service.Address_2);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.City))
                    sqlCommand.Parameters.AddWithValue("@City", (object)Service.City);
                else
                    sqlCommand.Parameters.AddWithValue("@City", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.State))
                    sqlCommand.Parameters.AddWithValue("@State", (object)Service.State);
                else
                    sqlCommand.Parameters.AddWithValue("@State", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Zip))
                    sqlCommand.Parameters.AddWithValue("@Zip", (object)Service.Zip);
                else
                    sqlCommand.Parameters.AddWithValue("@Zip", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Country))
                    sqlCommand.Parameters.AddWithValue("@Country", (object)Service.Country);
                else
                    sqlCommand.Parameters.AddWithValue("@Country", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.WebSite))
                    sqlCommand.Parameters.AddWithValue("@WebSite", (object)Service.WebSite);
                else
                    sqlCommand.Parameters.AddWithValue("@WebSite", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Service_Provider_Sublocation))
                    sqlCommand.Parameters.AddWithValue("@Service_Provider_Sublocation", (object)Service.Service_Provider_Sublocation);
                else
                    sqlCommand.Parameters.AddWithValue("@Service_Provider_Sublocation", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Related_To))
                    sqlCommand.Parameters.AddWithValue("@Related_To", (object)Service.Related_To);
                else
                    sqlCommand.Parameters.AddWithValue("@Related_To", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Short_Name))
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)Service.Short_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone1))
                    sqlCommand.Parameters.AddWithValue("@Phone1", (object)Service.Phone1);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone1", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone2))
                    sqlCommand.Parameters.AddWithValue("@Phone2", (object)Service.Phone2);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone2", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone3))
                    sqlCommand.Parameters.AddWithValue("@Phone3", (object)Service.Phone3);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone3", (object)string.Empty);

                //if (Service.Category == true)
                //    sqlCommand.Parameters.AddWithValue("@Category", (object)Service.Category);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Category", false);
                if (!string.IsNullOrEmpty(Service.Mobile_Collections))
                    sqlCommand.Parameters.AddWithValue("@Mobile_Collections", (object)Service.Mobile_Collections);
                else
                    sqlCommand.Parameters.AddWithValue("@Mobile_Collections", (object)string.Empty);
                if (Service.Servicing_which_clients == true)
                    sqlCommand.Parameters.AddWithValue("@Servicing_which_clients", 1);
                else
                    sqlCommand.Parameters.AddWithValue("@Servicing_which_clients", 0);
                if (!string.IsNullOrEmpty(Service.Formerly_Known_As))
                    sqlCommand.Parameters.AddWithValue("@Formerly_Known_As", (object)Service.Formerly_Known_As);
                else
                    sqlCommand.Parameters.AddWithValue("@Formerly_Known_As", (object)string.Empty);
                //if (Service.Address_Type == true)
                //    sqlCommand.Parameters.AddWithValue("@Address_Type", (object)Service.Address_Type);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Address_Type", false);
                if (!string.IsNullOrEmpty(Service.Address_Notes))
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", (object)Service.Address_Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", (object)string.Empty);
                if (file != null)
                {
                    if (!Directory.Exists(Server.MapPath("/SPImages")))
                    {
                        Directory.CreateDirectory(Server.MapPath("/SPImages"));
                    }
                    file.SaveAs(Server.MapPath("~/SPImages/SP_" +Path.GetFileName(file.FileName)));
                    Service.Logo_Image = "SP_"+Path.GetFileName(file.FileName);
                    sqlCommand.Parameters.AddWithValue("@Logo_Image", (object)Service.Logo_Image);
                }

                else
                {
                    if (!Directory.Exists(Server.MapPath("/SPImages")))
                    {
                        Directory.CreateDirectory(Server.MapPath("/SPImages"));
                    }
                    sqlCommand.Parameters.AddWithValue("@Logo_Image", (object)string.Empty);
                }
                if (!string.IsNullOrEmpty(Service.First_Name))
                    sqlCommand.Parameters.AddWithValue("@First_Name", (object)Service.First_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@First_Name", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Last_Name))
                    sqlCommand.Parameters.AddWithValue("@Last_Name", (object)Service.Last_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Last_Name", (object)string.Empty);
                //if (Service.Title == true)
                //    sqlCommand.Parameters.AddWithValue("@Title", (object)Service.Title);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Title", false);
                if (!string.IsNullOrEmpty(Service.Phone_Number_Type))
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_Type", (object)Service.Phone_Number_Type);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_Type", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Notes))
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)Service.Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Hours_of_Operation))
                    sqlCommand.Parameters.AddWithValue("@Hours_of_Operation", (object)Service.Hours_of_Operation);
                else
                    sqlCommand.Parameters.AddWithValue("@Hours_of_Operation", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Clinic))
                    sqlCommand.Parameters.AddWithValue("@Clinic", (object)Service.Clinic);
                else
                    sqlCommand.Parameters.AddWithValue("@Clinic", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Tractor_trailer_Parcking))
                    sqlCommand.Parameters.AddWithValue("@Tractor_trailer_Parcking", (object)Service.Tractor_trailer_Parcking);
                else
                    sqlCommand.Parameters.AddWithValue("@Tractor_trailer_Parcking", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Observed_collections))
                    sqlCommand.Parameters.AddWithValue("@Observed_collections", (object)Service.Observed_collections);
                else
                    sqlCommand.Parameters.AddWithValue("@Observed_collections", false);
                //if (!string.IsNullOrEmpty(Service.Services))
                //    sqlCommand.Parameters.AddWithValue("@Services", (object)Service.Services);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Services", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Reporting))
                    sqlCommand.Parameters.AddWithValue("@Reporting", (object)Service.Reporting);
                else
                    sqlCommand.Parameters.AddWithValue("@Reporting", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Billing_Details))
                    sqlCommand.Parameters.AddWithValue("@Billing_Details", (object)Service.Billing_Details);
                else
                    sqlCommand.Parameters.AddWithValue("@Billing_Details", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Credit_Card_Details))
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", (object)Service.Credit_Card_Details);
                else
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Notes_on_Billing))
                    sqlCommand.Parameters.AddWithValue("@Notes_on_Billing", (object)Service.Notes_on_Billing);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes_on_Billing", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Link_To_Collection_Protocols))
                    sqlCommand.Parameters.AddWithValue("@Link_To_Collection_Protocols", (object)Service.Link_To_Collection_Protocols);
                else
                    sqlCommand.Parameters.AddWithValue("@Link_To_Collection_Protocols", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Link_to_documents))
                    sqlCommand.Parameters.AddWithValue("@Link_to_documents", (object)Service.Link_to_documents);
                else
                    sqlCommand.Parameters.AddWithValue("@Link_to_documents", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Fees))
                    sqlCommand.Parameters.AddWithValue("@Fees", (object)Service.Fees);
                else
                    sqlCommand.Parameters.AddWithValue("@Fees", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone))
                    sqlCommand.Parameters.AddWithValue("@Phone", (object)Service.Phone);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Fax))
                    sqlCommand.Parameters.AddWithValue("@Fax", (object)Service.Fax);
                else
                    sqlCommand.Parameters.AddWithValue("@Fax", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Email))
                    sqlCommand.Parameters.AddWithValue("@Email", (object)Service.Email);
                else
                    sqlCommand.Parameters.AddWithValue("@Email", (object)string.Empty);



                string selected_cat = string.Empty;
                for (int i = 0; i < Service.Category1.Count; i++)
                {

                    if (Service.Category1[i].Selected == true)
                    {
                        if (string.IsNullOrEmpty(selected_cat))
                            selected_cat = Service.Category1[i].Text;
                        else
                            selected_cat = selected_cat + "," + Service.Category1[i].Text;
                    }
                }
                sqlCommand.Parameters.AddWithValue("@Category", selected_cat);

                string selected_title = string.Empty;
                for (int i = 0; i < Service.Title1.Count; i++)
                {

                    if (Service.Title1[i].Selected == true)
                    {
                        if (string.IsNullOrEmpty(selected_title))
                            selected_title = Service.Title1[i].Text;
                        else
                            selected_title = selected_title + "," + Service.Title1[i].Text;

                    }

                }


                sqlCommand.Parameters.AddWithValue("@Title", selected_title);


                string Selected_Address = string.Empty;
                for (int i = 0; i < Service.Address_Type1.Count; i++)
                {

                    if (Service.Address_Type1[i].Selected == true)
                    {
                        if (string.IsNullOrEmpty(Selected_Address))
                            Selected_Address = Service.Address_Type1[i].Text;
                        else
                            Selected_Address = Selected_Address + "," + Service.Address_Type1[i].Text;

                    }

                }

                sqlCommand.Parameters.AddWithValue("@Address_Type", Selected_Address);


                // sqlCommand.Parameters.AddWithValue("@Account_id", Session["Account_id"].ToString());
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return (ActionResult)this.RedirectToAction("ProviderList", "ServiceProvider");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [BreadCrumb(Label = "Update Service Provider")]
        [HttpGet]
        public ActionResult UpdateProvider(string id)
        {
            SqlCommand selectCommand = new SqlCommand("Tbl_ServiceProvider_Edit_by_id", new SqlConnection(this.TransCanadaConnection));
            selectCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@ServiceProvider_id", (object)id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            Service_provider serviceProvider = new Service_provider();
            List<SelectListItem> Sevice = new List<SelectListItem>()
            {
                  new SelectListItem {Text="Individual",Value="Individual",Selected=false },
                   new SelectListItem {Text="PSC",Value="PSC",Selected=false},
                   new SelectListItem {Text="PPN",Value="PPN",Selected=false },
                   new SelectListItem {Text="Third Party",Value="Third Party",Selected=false},

            };
            List<SelectListItem> Sevice1 = new List<SelectListItem>()
            {
                  new SelectListItem {Text="CEO",Value="CEO",Selected=false },
                   new SelectListItem {Text="Ops Manager",Value="Ops Manager",Selected=false},
                   new SelectListItem {Text="Collector",Value="Collector",Selected=false },
                    new SelectListItem {Text="Scheduling",Value="Scheduling",Selected=false },
                     new SelectListItem {Text="Invoicing",Value="Invoicing",Selected=false },

            };
            List<SelectListItem> Sevice2 = new List<SelectListItem>()
            {
                  new SelectListItem {Text="Mailing",Value="Mailing",Selected=false },
                   new SelectListItem {Text="On-Site",Value="On-Site",Selected=false},
                   new SelectListItem {Text="Billing",Value="Billing",Selected=false },
            };
            //List<SelectListItem> Sevice3 = new List<SelectListItem>()
            //{

            //};


            serviceProvider.Category1 = Sevice;
            serviceProvider.Title1 = Sevice1;
            serviceProvider.Address_Type1 = Sevice2;
            serviceProvider.List_Gender = Gender();
            if (dataTable.Rows.Count > 0)
            {
                serviceProvider.Serviceprovider_id = Convert.ToInt32(dataTable.Rows[0]["Serviceprovider_id"].ToString());
                serviceProvider.Serviceprovider_Name = string.IsNullOrEmpty(dataTable.Rows[0]["Serviceprovider_Name"].ToString()) ? string.Empty : dataTable.Rows[0]["Serviceprovider_Name"].ToString();
                serviceProvider.Contact_Person = string.IsNullOrEmpty(dataTable.Rows[0]["Contact_Person"].ToString()) ? string.Empty : dataTable.Rows[0]["Contact_Person"].ToString();
                serviceProvider.Address_1 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_1"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_1"].ToString();
                serviceProvider.Address_2 = string.IsNullOrEmpty(dataTable.Rows[0]["Address_2"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_2"].ToString();
                serviceProvider.City = string.IsNullOrEmpty(dataTable.Rows[0]["City"].ToString()) ? string.Empty : dataTable.Rows[0]["City"].ToString();
                serviceProvider.State = string.IsNullOrEmpty(dataTable.Rows[0]["State"].ToString()) ? string.Empty : dataTable.Rows[0]["State"].ToString();
                serviceProvider.Zip = string.IsNullOrEmpty(dataTable.Rows[0]["Zip"].ToString()) ? string.Empty : dataTable.Rows[0]["Zip"].ToString();
                serviceProvider.Country = string.IsNullOrEmpty(dataTable.Rows[0]["Country"].ToString()) ? string.Empty : dataTable.Rows[0]["Country"].ToString();
                serviceProvider.WebSite = string.IsNullOrEmpty(dataTable.Rows[0]["WebSite"].ToString()) ? string.Empty : dataTable.Rows[0]["WebSite"].ToString();
                serviceProvider.Service_Provider_Sublocation = string.IsNullOrEmpty(dataTable.Rows[0]["Service_Provider_Sublocation"].ToString()) ? string.Empty : dataTable.Rows[0]["Service_Provider_Sublocation"].ToString();
                serviceProvider.Related_To = string.IsNullOrEmpty(dataTable.Rows[0]["Related_To"].ToString()) ? string.Empty : dataTable.Rows[0]["Related_To"].ToString();
                serviceProvider.Short_Name = string.IsNullOrEmpty(dataTable.Rows[0]["Short_Name"].ToString()) ? string.Empty : dataTable.Rows[0]["Short_Name"].ToString();
                serviceProvider.Email = string.IsNullOrEmpty(dataTable.Rows[0]["Email"].ToString()) ? string.Empty : dataTable.Rows[0]["Email"].ToString();
                serviceProvider.Fax = string.IsNullOrEmpty(dataTable.Rows[0]["Fax"].ToString()) ? string.Empty : dataTable.Rows[0]["Fax"].ToString();
                serviceProvider.Observed_collections= string.IsNullOrEmpty(dataTable.Rows[0]["Observed_collections"].ToString()) ? string.Empty : dataTable.Rows[0]["Observed_collections"].ToString();
                if (dataTable.Rows[0]["Category"].ToString() != null)
                {
                    string cate = serviceProvider.Category = dataTable.Rows[0]["Category"].ToString();
                    string[] values = cate.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        foreach (var item in serviceProvider.Category1)
                        {
                            if (values[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    serviceProvider.Category = string.Empty;
                }
                serviceProvider.Mobile_Collections = string.IsNullOrEmpty(dataTable.Rows[0]["Mobile_Collections"].ToString()) ? string.Empty : dataTable.Rows[0]["Mobile_Collections"].ToString();
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Servicing_which_clients"].ToString()))
                {
                    serviceProvider.Servicing_which_clients = Convert.ToBoolean(dataTable.Rows[0]["Servicing_which_clients"].ToString());
                }
                else
                {
                    serviceProvider.Servicing_which_clients = false;
                }
                serviceProvider.Formerly_Known_As = string.IsNullOrEmpty(dataTable.Rows[0]["Formerly_Known_As"].ToString()) ? string.Empty : dataTable.Rows[0]["Formerly_Known_As"].ToString();
                if (dataTable.Rows[0]["Address_Type"].ToString() != null)
                {
                    string Address_Type = serviceProvider.Address_Type = dataTable.Rows[0]["Address_Type"].ToString();
                    string[] values = Address_Type.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        foreach (var item in serviceProvider.Address_Type1)
                        {
                            if (values[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                    serviceProvider.Address_Type = dataTable.Rows[0]["Address_Type"].ToString();
                }
                else
                {
                    serviceProvider.Address_Type = string.Empty;
                }
                serviceProvider.Address_Notes = string.IsNullOrEmpty(dataTable.Rows[0]["Address_Notes"].ToString()) ? string.Empty : dataTable.Rows[0]["Address_Notes"].ToString();
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Logo_Image"].ToString()))
                {
                    serviceProvider.Logo_Image = dataTable.Rows[0]["Logo_Image"].ToString();
                }
                else
                {
                    serviceProvider.Logo_Image = "No_Logo.png";
                }
                serviceProvider.Logoimage_src = dataTable.Rows[0]["Logo_Image"].ToString();
                serviceProvider.First_Name = string.IsNullOrEmpty(dataTable.Rows[0]["First_Name"].ToString()) ? string.Empty : dataTable.Rows[0]["First_Name"].ToString();
                serviceProvider.Last_Name = string.IsNullOrEmpty(dataTable.Rows[0]["Last_Name"].ToString()) ? string.Empty : dataTable.Rows[0]["Last_Name"].ToString();
                if (dataTable.Rows[0]["Title"].ToString() != null)
                {
                    string Title = serviceProvider.Title = dataTable.Rows[0]["Title"].ToString();
                    string[] values = Title.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {

                        foreach (var item in serviceProvider.Title1)
                        {
                            if (values[i] == item.Value.ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }

                }
                else
                {
                    serviceProvider.Title = string.Empty;
                }
                serviceProvider.Phone_Number_Type = string.IsNullOrEmpty(dataTable.Rows[0]["Phone_Number_Type"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone_Number_Type"].ToString();
                serviceProvider.Notes = string.IsNullOrEmpty(dataTable.Rows[0]["Notes"].ToString()) ? string.Empty : dataTable.Rows[0]["Notes"].ToString();
                serviceProvider.Hours_of_Operation = string.IsNullOrEmpty(dataTable.Rows[0]["Hours_of_Operation"].ToString()) ? string.Empty : dataTable.Rows[0]["Hours_of_Operation"].ToString();
                serviceProvider.Clinic = string.IsNullOrEmpty(dataTable.Rows[0]["Clinic"].ToString()) ? string.Empty : dataTable.Rows[0]["Clinic"].ToString();
                serviceProvider.Tractor_trailer_Parcking = string.IsNullOrEmpty(dataTable.Rows[0]["Tractor_trailer_Parcking"].ToString()) ? string.Empty : dataTable.Rows[0]["Tractor_trailer_Parcking"].ToString();
                if (dataTable.Rows[0]["Observed_collections"].ToString() != null)
                {
                    serviceProvider.Observed_collections = Convert.ToString(dataTable.Rows[0]["Observed_collections"].ToString());
                }
                else
                {
                    serviceProvider.Observed_collections = "";
                }
                //serviceProvider.Services = string.IsNullOrEmpty(dataTable.Rows[0]["Services"].ToString()) ? string.Empty : dataTable.Rows[0]["Services"].ToString();
                serviceProvider.Reporting = string.IsNullOrEmpty(dataTable.Rows[0]["Reporting"].ToString()) ? string.Empty : dataTable.Rows[0]["Reporting"].ToString();
                serviceProvider.Billing_Details = string.IsNullOrEmpty(dataTable.Rows[0]["Billing_Details"].ToString()) ? string.Empty : dataTable.Rows[0]["Billing_Details"].ToString();
                serviceProvider.Credit_Card_Details = string.IsNullOrEmpty(dataTable.Rows[0]["Credit_Card_Details"].ToString()) ? string.Empty : dataTable.Rows[0]["Credit_Card_Details"].ToString();
                serviceProvider.Notes_on_Billing = string.IsNullOrEmpty(dataTable.Rows[0]["Notes_on_Billing"].ToString()) ? string.Empty : dataTable.Rows[0]["Notes_on_Billing"].ToString();
                serviceProvider.Link_To_Collection_Protocols = string.IsNullOrEmpty(dataTable.Rows[0]["Link_To_Collection_Protocols"].ToString()) ? string.Empty : dataTable.Rows[0]["Link_To_Collection_Protocols"].ToString();
                serviceProvider.Link_to_documents = string.IsNullOrEmpty(dataTable.Rows[0]["Link_to_documents"].ToString()) ? string.Empty : dataTable.Rows[0]["Link_to_documents"].ToString();
                serviceProvider.Fees = string.IsNullOrEmpty(dataTable.Rows[0]["Fees"].ToString()) ? string.Empty : dataTable.Rows[0]["Fees"].ToString();
                serviceProvider.Phone = string.IsNullOrEmpty(dataTable.Rows[0]["Phone"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone"].ToString();
                serviceProvider.Phone1 = string.IsNullOrEmpty(dataTable.Rows[0]["Phone1"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone1"].ToString();
                serviceProvider.Phone2 = string.IsNullOrEmpty(dataTable.Rows[0]["Phone2"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone2"].ToString();
                serviceProvider.Phone3 = string.IsNullOrEmpty(dataTable.Rows[0]["Phone3"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone3"].ToString();


            }
            return (ActionResult)this.View((object)serviceProvider);
        }
        public List<string> Gender()
        {
            List<string> vs = new List<string>();
            vs.Add("Male");
            vs.Add("Female");
            return vs;
        }
        [HttpPost]
        public ActionResult UpdateProvider(Service_provider Service, HttpPostedFileBase file)
        {
            try
            {
                 if (!ModelState.IsValid)
                    {
                        Service.List_Gender = Gender();
                        return View(Service);
                    }
                string userName1 = IdentityExtensions.GetUserName(this.User.Identity);
                //string userName2 = IdentityExtensions.GetUserName(this.User.Identity);
                SqlConnection connection = new SqlConnection(TransCanadaConnection);
                SqlCommand sqlCommand = new SqlCommand("Tbl_ServiceProvider_Update", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //sqlCommand.Parameters.AddWithValue("@Createdby", userName1);
                sqlCommand.Parameters.AddWithValue("@Serviceprovider_id", (object)Service.Serviceprovider_id);
                sqlCommand.Parameters.AddWithValue("@Updatedby", userName1);
                //if (!string.IsNullOrEmpty(Service.Serviceprovider_Name))
                //    sqlCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)Service.Serviceprovider_Name);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Serviceprovider_Name", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Contact_Person))
                    sqlCommand.Parameters.AddWithValue("@Contact_Person", (object)Service.Contact_Person);
                else
                    sqlCommand.Parameters.AddWithValue("@Contact_Person", (object)string.Empty);


                if (!string.IsNullOrEmpty(Service.Address_1))
                    sqlCommand.Parameters.AddWithValue("@Address_1", (object)Service.Address_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_1", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Address_2))
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)Service.Address_2);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_2", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.City))
                    sqlCommand.Parameters.AddWithValue("@City", (object)Service.City);
                else
                    sqlCommand.Parameters.AddWithValue("@City", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.State))
                    sqlCommand.Parameters.AddWithValue("@State", (object)Service.State);
                else
                    sqlCommand.Parameters.AddWithValue("@State", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Zip))
                    sqlCommand.Parameters.AddWithValue("@Zip", (object)Service.Zip);
                else
                    sqlCommand.Parameters.AddWithValue("@Zip", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Country))
                    sqlCommand.Parameters.AddWithValue("@Country", (object)Service.Country);
                else
                    sqlCommand.Parameters.AddWithValue("@Country", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.WebSite))
                    sqlCommand.Parameters.AddWithValue("@WebSite", (object)Service.WebSite);
                else
                    sqlCommand.Parameters.AddWithValue("@WebSite", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Service_Provider_Sublocation))
                    sqlCommand.Parameters.AddWithValue("@Service_Provider_Sublocation", (object)Service.Service_Provider_Sublocation);
                else
                    sqlCommand.Parameters.AddWithValue("@Service_Provider_Sublocation", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Related_To))
                    sqlCommand.Parameters.AddWithValue("@Related_To", (object)Service.Related_To);
                else
                    sqlCommand.Parameters.AddWithValue("@Related_To", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Short_Name))
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)Service.Short_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone1))
                    sqlCommand.Parameters.AddWithValue("@Phone1", (object)Service.Phone1);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone1", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone2))
                    sqlCommand.Parameters.AddWithValue("@Phone2", (object)Service.Phone2);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone2", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone3))
                    sqlCommand.Parameters.AddWithValue("@Phone3", (object)Service.Phone3);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone3", (object)string.Empty);
                //if (Service.Category == true)
                //    sqlCommand.Parameters.AddWithValue("@Category", (object)Service.Category);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Category", false);
                if (!string.IsNullOrEmpty(Service.Mobile_Collections))
                    sqlCommand.Parameters.AddWithValue("@Mobile_Collections", (object)Service.Mobile_Collections);
                else
                    sqlCommand.Parameters.AddWithValue("@Mobile_Collections", (object)string.Empty);
                if (Service.Servicing_which_clients == true)
                    sqlCommand.Parameters.AddWithValue("@Servicing_which_clients", 1);
                else
                    sqlCommand.Parameters.AddWithValue("@Servicing_which_clients", 0);
                if (!string.IsNullOrEmpty(Service.Formerly_Known_As))
                    sqlCommand.Parameters.AddWithValue("@Formerly_Known_As", (object)Service.Formerly_Known_As);
                else
                    sqlCommand.Parameters.AddWithValue("@Formerly_Known_As", (object)string.Empty);
                //if (Service.Address_Type == true)
                //    sqlCommand.Parameters.AddWithValue("@Address_Type", (object)Service.Address_Type);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Address_Type", false);
                if (!string.IsNullOrEmpty(Service.Address_Notes))
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", (object)Service.Address_Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", (object)string.Empty);
                if (file != null)
                {
                    file.SaveAs(Server.MapPath("~/SPImages/SP_" + Path.GetFileName(file.FileName)));
                    Service.Logo_Image = "SP_" + Path.GetFileName(file.FileName) ;
                    sqlCommand.Parameters.AddWithValue("@Logo_Image", (object)Service.Logo_Image);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Logo_Image", (object)string.Empty);
                }
                if (!string.IsNullOrEmpty(Service.First_Name))
                    sqlCommand.Parameters.AddWithValue("@First_Name", (object)Service.First_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@First_Name", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Last_Name))
                    sqlCommand.Parameters.AddWithValue("@Last_Name", (object)Service.Last_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Last_Name", (object)string.Empty);
                //if (Service.Title == true)
                //    sqlCommand.Parameters.AddWithValue("@Title", (object)Service.Title);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Title", false);
                if (!string.IsNullOrEmpty(Service.Phone_Number_Type))
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_Type", (object)Service.Phone_Number_Type);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_Type", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Notes))
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)Service.Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Hours_of_Operation))
                    sqlCommand.Parameters.AddWithValue("@Hours_of_Operation", (object)Service.Hours_of_Operation);
                else
                    sqlCommand.Parameters.AddWithValue("@Hours_of_Operation", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Clinic))
                    sqlCommand.Parameters.AddWithValue("@Clinic", (object)Service.Clinic);
                else
                    sqlCommand.Parameters.AddWithValue("@Clinic", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Tractor_trailer_Parcking))
                    sqlCommand.Parameters.AddWithValue("@Tractor_trailer_Parcking", (object)Service.Tractor_trailer_Parcking);
                else
                    sqlCommand.Parameters.AddWithValue("@Tractor_trailer_Parcking", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Observed_collections))
                    sqlCommand.Parameters.AddWithValue("@Observed_collections", (object)Service.Observed_collections);
                else
                    sqlCommand.Parameters.AddWithValue("@Observed_collections", false);
                //if (!string.IsNullOrEmpty(Service.Services))
                //    sqlCommand.Parameters.AddWithValue("@Services", (object)Service.Services);
                //else
                //    sqlCommand.Parameters.AddWithValue("@Services", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Reporting))
                    sqlCommand.Parameters.AddWithValue("@Reporting", (object)Service.Reporting);
                else
                    sqlCommand.Parameters.AddWithValue("@Reporting", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Billing_Details))
                    sqlCommand.Parameters.AddWithValue("@Billing_Details", (object)Service.Billing_Details);
                else
                    sqlCommand.Parameters.AddWithValue("@Billing_Details", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Credit_Card_Details))
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", (object)Service.Credit_Card_Details);
                else
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Notes_on_Billing))
                    sqlCommand.Parameters.AddWithValue("@Notes_on_Billing", (object)Service.Notes_on_Billing);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes_on_Billing", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Link_To_Collection_Protocols))
                    sqlCommand.Parameters.AddWithValue("@Link_To_Collection_Protocols", (object)Service.Link_To_Collection_Protocols);
                else
                    sqlCommand.Parameters.AddWithValue("@Link_To_Collection_Protocols", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Link_to_documents))
                    sqlCommand.Parameters.AddWithValue("@Link_to_documents", (object)Service.Link_to_documents);
                else
                    sqlCommand.Parameters.AddWithValue("@Link_to_documents", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Fees))
                    sqlCommand.Parameters.AddWithValue("@Fees", (object)Service.Fees);
                else
                    sqlCommand.Parameters.AddWithValue("@Fees", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Phone))
                    sqlCommand.Parameters.AddWithValue("@Phone", (object)Service.Phone);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Fax))
                    sqlCommand.Parameters.AddWithValue("@Fax", (object)Service.Fax);
                else
                    sqlCommand.Parameters.AddWithValue("@Fax", (object)string.Empty);
                if (!string.IsNullOrEmpty(Service.Email))
                    sqlCommand.Parameters.AddWithValue("@Email", (object)Service.Email);
                else
                    sqlCommand.Parameters.AddWithValue("@Email", (object)string.Empty);



                string selected_cat = string.Empty;
                for (int i = 0; i < Service.Category1.Count; i++)
                {

                    if (Service.Category1[i].Selected == true)
                    {
                        if (string.IsNullOrEmpty(selected_cat))
                            selected_cat = Service.Category1[i].Text;
                        else
                            selected_cat = selected_cat + "," + Service.Category1[i].Text;
                    }
                }
                sqlCommand.Parameters.AddWithValue("@Category", selected_cat);

                string selected_title = string.Empty;
                for (int i = 0; i < Service.Title1.Count; i++)
                {

                    if (Service.Title1[i].Selected == true)
                    {
                        if (string.IsNullOrEmpty(selected_title))
                            selected_title = Service.Title1[i].Text;
                        else
                            selected_title = selected_title + "," + Service.Title1[i].Text;

                    }

                }


                sqlCommand.Parameters.AddWithValue("@Title", selected_title);


                string Selected_Address = string.Empty;
                for (int i = 0; i < Service.Address_Type1.Count; i++)
                {

                    if (Service.Address_Type1[i].Selected == true)
                    {
                        if (string.IsNullOrEmpty(Selected_Address))
                            Selected_Address = Service.Address_Type1[i].Text;
                        else
                            Selected_Address = Selected_Address + "," + Service.Address_Type1[i].Text;

                    }

                }

                sqlCommand.Parameters.AddWithValue("@Address_Type", Selected_Address);


                // sqlCommand.Parameters.AddWithValue("@Account_id", Session["Account_id"].ToString());
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return (ActionResult)this.RedirectToAction("ProviderList", "ServiceProvider");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        
        public ActionResult DeleteProvider(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransCanadaConnection);
            SqlCommand sqlCommand = new SqlCommand("Tbl_ServiceProvider_Delete_by_id", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Serviceprovider_id", (object)id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public ActionResult DeleteLocation(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransCanadaConnection);
            SqlCommand sqlCommand = new SqlCommand("delete from  Tbl_service_location where serviceprovider_id=@id ", connection);
            sqlCommand.Parameters.AddWithValue("@id", (object)id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [BreadCrumb(Label = "SP Contacts List")]
        public ActionResult SpContactlist(int id)
        {
            TempData["sp_Location_name"] = getlocationnamebyid(id);
            ViewBag.LocationId = id;
            SqlCommand selectCommand = new SqlCommand("select * from Tbl_Serviceprovider_Contact where sp_location_id=@id", new SqlConnection(this.TransCanadaConnection));
            selectCommand.Parameters.AddWithValue("@id", (object)id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            List<Lab_contact> labContactList = new List<Lab_contact>();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
                labContactList.Add(new Lab_contact()
                {
                    Sp_contact_id = Convert.ToInt32(dataTable.Rows[index]["Sp_contact_id"].ToString()),
                    Sp_location_id = Convert.ToInt32(dataTable.Rows[index]["Sp_location_id"].ToString()),
                    firstname = dataTable.Rows[index]["firstname"].ToString(),
                    Lastname = dataTable.Rows[index]["Lastname"].ToString(),
                    cell = dataTable.Rows[index]["cell"].ToString(),
                    email = dataTable.Rows[index]["email"].ToString(),
                    officephone = dataTable.Rows[index]["officephone"].ToString(),
                    Title = dataTable.Rows[index]["Title"].ToString(),
                    Role = dataTable.Rows[index]["Role"].ToString(),
                    Notes = dataTable.Rows[index]["Notes"].ToString(),
                    Email1 = dataTable.Rows[index]["Email1"].ToString(),
                    Phone1 = dataTable.Rows[index]["Phone1"].ToString()
                });
            SqlCommand sqlCommand1 = new SqlCommand("select Serviceprovider_Name from Tbl_service_location where Serviceprovider_id=@id", this.conn);
            sqlCommand1.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand1);
            DataTable dataTable1 = new DataTable();
            dataAdapter.Fill(dataTable1);
            ViewBag.LabName = dataTable1.Rows[0]["Serviceprovider_Name"].ToString();
            return (ActionResult)this.View((object)labContactList);
        }
        [BreadCrumb(Label = "New SP Contact")]
        public ActionResult SpContactinsert(string id)
        {
            return (ActionResult)this.View((object)new Lab_contact()
            {
                location_id = id
            });
        }

        [HttpPost]
        public ActionResult SpContactinsert(Lab_contact lc)
        {
            if (!ModelState.IsValid)
                return View(lc);
            SqlConnection connection = new SqlConnection(this.TransCanadaConnection);
            SqlCommand sqlCommand = new SqlCommand("Tbl_Sp_Contact_insert", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Sp_location_id", (object)lc.location_id);

            
        
            if (lc.email != null)
                sqlCommand.Parameters.AddWithValue("@email", (object)lc.email);
            else
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            if (lc.cell != null)
                sqlCommand.Parameters.AddWithValue("@cell", (object)lc.cell);
            else
                sqlCommand.Parameters.AddWithValue("@cell", string.Empty);
            if (lc.Lastname != null)
                sqlCommand.Parameters.AddWithValue("@Lastname", (object)lc.Lastname);
            else
                sqlCommand.Parameters.AddWithValue("@Lastname", string.Empty);
            if (lc.firstname != null)
                sqlCommand.Parameters.AddWithValue("@firstname", (object)lc.firstname);
            else
                sqlCommand.Parameters.AddWithValue("@firstname", string.Empty);
            if (lc.officephone != null)
                sqlCommand.Parameters.AddWithValue("@officephone", (object)lc.officephone);
            else
                sqlCommand.Parameters.AddWithValue("@officephone", string.Empty);
            if (lc.Title != null)
                sqlCommand.Parameters.AddWithValue("@Title", (object)lc.Title);
            else
                sqlCommand.Parameters.AddWithValue("@Title", string.Empty);
            if (lc.Role != null)
                sqlCommand.Parameters.AddWithValue("@Role", (object)lc.Role);
            else
                sqlCommand.Parameters.AddWithValue("@Role", string.Empty);
            if (lc.Notes != null)
                sqlCommand.Parameters.AddWithValue("@Notes", (object)lc.Notes);
            else
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            if (lc.Email1 != null)
                sqlCommand.Parameters.AddWithValue("@Email1", (object)lc.Email1);
            else
                sqlCommand.Parameters.AddWithValue("@Email1", string.Empty);
            if (lc.Phone1 != null)
                sqlCommand.Parameters.AddWithValue("@Phone1", (object)lc.Phone1);
            else
                sqlCommand.Parameters.AddWithValue("@Phone1", string.Empty);
            if (lc.Third_Phone != null)
                sqlCommand.Parameters.AddWithValue("@Third_Phone", (object)lc.Third_Phone);
            else
                sqlCommand.Parameters.AddWithValue("@Third_Phone", string.Empty);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.RedirectToAction("SpContactlist", (object)new
            {
                id = lc.location_id
            });
        }
        [BreadCrumb(Label = "Update SP Contact")]
        [HttpGet]
        public ActionResult UpdateSpContact(string id)
        {
            SqlCommand selectCommand = new SqlCommand("tbl_sp_contact_Edit", new SqlConnection(this.TransCanadaConnection));
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Sp_contact_id", (object)id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            Lab_contact labContact = new Lab_contact();
            labContact.Sp_contact_id = Convert.ToInt32(id);
            if (dataTable.Rows.Count > 0)
            {
                labContact.firstname = string.IsNullOrEmpty(dataTable.Rows[0]["firstname"].ToString()) ? string.Empty : dataTable.Rows[0]["firstname"].ToString();
                labContact.Sp_location_id = string.IsNullOrEmpty(dataTable.Rows[0]["Sp_Location_id"].ToString()) ? 0 : Convert.ToInt32(dataTable.Rows[0]["Sp_Location_id"].ToString());
                labContact.Lastname = string.IsNullOrEmpty(dataTable.Rows[0]["Lastname"].ToString()) ? string.Empty : dataTable.Rows[0]["Lastname"].ToString();
                labContact.cell = string.IsNullOrEmpty(dataTable.Rows[0]["cell"].ToString()) ? string.Empty : dataTable.Rows[0]["cell"].ToString();
                labContact.email = string.IsNullOrEmpty(dataTable.Rows[0]["email"].ToString()) ? string.Empty : dataTable.Rows[0]["email"].ToString();
                labContact.officephone = string.IsNullOrEmpty(dataTable.Rows[0]["officephone"].ToString()) ? string.Empty : dataTable.Rows[0]["officephone"].ToString();
                labContact.Title = string.IsNullOrEmpty(dataTable.Rows[0]["Title"].ToString()) ? string.Empty : dataTable.Rows[0]["Title"].ToString();
                labContact.Role = string.IsNullOrEmpty(dataTable.Rows[0]["Role"].ToString()) ? string.Empty : dataTable.Rows[0]["Role"].ToString();
                labContact.Notes = string.IsNullOrEmpty(dataTable.Rows[0]["Notes"].ToString()) ? string.Empty : dataTable.Rows[0]["Notes"].ToString();
                labContact.Email1 = string.IsNullOrEmpty(dataTable.Rows[0]["Email1"].ToString()) ? string.Empty : dataTable.Rows[0]["Email1"].ToString();
                labContact.Phone1 = string.IsNullOrEmpty(dataTable.Rows[0]["Phone1"].ToString()) ? string.Empty : dataTable.Rows[0]["Phone1"].ToString();
                labContact.Third_Phone = string.IsNullOrEmpty(dataTable.Rows[0]["Third_Phone"].ToString()) ? string.Empty : dataTable.Rows[0]["Third_Phone"].ToString();

            }
            return (ActionResult)this.View((object)labContact);
        }

        public ActionResult UpdateSpContact(Lab_contact lc)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)lc);
            SqlConnection connection = new SqlConnection(this.TransCanadaConnection);
            SqlCommand sqlCommand = new SqlCommand("tbl_sp_contact_Update", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Sp_contact_id", (object)lc.Sp_contact_id);
            if (lc.email != null)
                sqlCommand.Parameters.AddWithValue("@email", (object)lc.email);
            else
                sqlCommand.Parameters.AddWithValue("@email", string.Empty);
            if (lc.cell != null)
                sqlCommand.Parameters.AddWithValue("@cell", (object)lc.cell);
            else
                sqlCommand.Parameters.AddWithValue("@cell", string.Empty);
            if (lc.Lastname != null)
                sqlCommand.Parameters.AddWithValue("@Lastname", (object)lc.Lastname);
            else
                sqlCommand.Parameters.AddWithValue("@Lastname", string.Empty);
            if (lc.firstname != null)
                sqlCommand.Parameters.AddWithValue("@firstname", (object)lc.firstname);
            else
                sqlCommand.Parameters.AddWithValue("@firstname", string.Empty);
            if (lc.officephone != null)
                sqlCommand.Parameters.AddWithValue("@officephone", (object)lc.officephone);
            else
                sqlCommand.Parameters.AddWithValue("@officephone", string.Empty);
            if (lc.Title != null)
                sqlCommand.Parameters.AddWithValue("@Title", (object)lc.Title);
            else
                sqlCommand.Parameters.AddWithValue("@Title", string.Empty);
            if (lc.Role != null)
                sqlCommand.Parameters.AddWithValue("@Role", (object)lc.Role);
            else
                sqlCommand.Parameters.AddWithValue("@Role", string.Empty);
            if (lc.Notes != null)
                sqlCommand.Parameters.AddWithValue("@Notes", (object)lc.Notes);
            else
                sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
            if (lc.Email1 != null)
                sqlCommand.Parameters.AddWithValue("@Email1", (object)lc.Email1);
            else
                sqlCommand.Parameters.AddWithValue("@Email1", string.Empty);
            if (lc.Phone1 != null)
                sqlCommand.Parameters.AddWithValue("@Phone1", (object)lc.Phone1);
            else
                sqlCommand.Parameters.AddWithValue("@Phone1", string.Empty);
            if (lc.Third_Phone != null)
                sqlCommand.Parameters.AddWithValue("@Third_Phone", (object)lc.Third_Phone);
            else
                sqlCommand.Parameters.AddWithValue("@Third_Phone", string.Empty);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.RedirectToAction("SpContactlist", (object)new
            {
                id = lc.Sp_location_id
            });
        }

        public ActionResult deleteSp(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransCanadaConnection);
            SqlCommand sqlCommand = new SqlCommand("tbl_sp_contact_delete", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Sp_contact_id", (object)id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [BreadCrumb(Label = "Service Panels")]
        public ActionResult LabServices(string id)
        {
            ViewBag.LabName = id.Trim();
            List<Services> servicesList = new List<Services>();
            // ISSUE: reference to a compiler-generated field

            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field

            id = id.Trim();
            if (string.IsNullOrEmpty(id))
                return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
            SqlCommand sqlCommand = new SqlCommand("Sp_group_services_List", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SpName", (object)id);
            this.conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                    servicesList.Add(new Services()
                    {
                        Service_id = Convert.ToInt32(sqlDataReader["Sp_id"]),
                        Service_Name = Convert.ToString(sqlDataReader["service_grp_name"])
                    });
            }
            return (ActionResult)this.View((object)servicesList);
        }
        [BreadCrumb(Label = "New Service Panel")]
        public ActionResult CreateServiceGroup(string id)
        {
            return (ActionResult)this.View((object)new Services()
            {
                Labid = id.Trim()
            });
        }

        [HttpPost]
        public ActionResult CreateServiceGroup(Services services)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)services);
            SqlCommand sqlCommand = new SqlCommand("Sp_group_services_insert", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@SpName", (object)services.Labid);
            sqlCommand.Parameters.AddWithValue("@service_grp_name", (object)services.Service_Name);
            this.conn.Open();
            sqlCommand.ExecuteNonQuery();
            this.conn.Close();
            return (ActionResult)this.RedirectToAction("LabServices", (object)new
            {
                id = services.Labid
            });
        }
        [BreadCrumb(Label = "Update Service Panel & Sub Panels")]
        public ActionResult UpdateServiceGroup(int id)
        {
            List<SubServices> subServicesList = new List<SubServices>();
            Services services = new Services();
            services.Service_id = id;
            SqlCommand selectCommand = new SqlCommand("select service_grp_name,spname from sp_service_grp where sp_id=@id", this.conn);
            selectCommand.Parameters.AddWithValue("@id", (object)id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                services.Service_Name = string.IsNullOrEmpty(dataTable.Rows[0]["service_grp_name"].ToString()) ? string.Empty : dataTable.Rows[0]["service_grp_name"].ToString();
                services.Labid = string.IsNullOrEmpty(dataTable.Rows[0]["spname"].ToString()) ? string.Empty : dataTable.Rows[0]["spname"].ToString();
            }
            SqlCommand sqlCommand = new SqlCommand("select * from Sp_sub_services where Sp_group_id=@id", this.conn);
            sqlCommand.Parameters.AddWithValue("@id", (object)id);
            this.conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                    subServicesList.Add(new SubServices()
                    {
                        lab_services_description = sqlDataReader["Sp_services_description"].ToString(),
                        lab_service_id = Convert.ToInt32(sqlDataReader["Sp_service_id"].ToString()),
                        lab_services_ext_description = sqlDataReader["Sp_services_ext_description"].ToString(),
                        service_charges = Convert.ToDecimal(sqlDataReader["service_charges"].ToString()),
                        client_billing_charges = Convert.ToDecimal(sqlDataReader["client_billing_charges"].ToString())
                    });
                sqlDataReader.Close();
                this.conn.Close();
            }
            else
            {
                sqlDataReader.Close();
                this.conn.Close();
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
                SqlCommand sqlCommand = new SqlCommand("select * from Sp_sub_services where Sp_group_id=@id", this.conn);
                sqlCommand.Parameters.AddWithValue("@id", (object)services.Service_id);
                this.conn.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                        subServicesList.Add(new SubServices()
                        {
                            lab_services_description = sqlDataReader["Sp_services_description"].ToString(),
                            lab_service_id = Convert.ToInt32(sqlDataReader["Sp_service_id"].ToString()),
                            lab_services_ext_description = sqlDataReader["Sp_services_ext_description"].ToString(),
                            service_charges = Convert.ToDecimal(sqlDataReader["service_charges"].ToString()),
                            client_billing_charges = Convert.ToDecimal(sqlDataReader["client_billing_charges"].ToString())
                        });
                    sqlDataReader.Close();
                    this.conn.Close();
                }
                else
                {
                    sqlDataReader.Close();
                    this.conn.Close();
                }
                ViewData["Data"] = subServicesList;
                return (ActionResult)this.View((object)services);
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("update sp_service_grp set service_grp_name=@service_grp_name where Sp_id=@id", this.conn);
                sqlCommand.Parameters.AddWithValue("@id", (object)services.Service_id);
                sqlCommand.Parameters.AddWithValue("@service_grp_name", (object)services.Service_Name);
                this.conn.Open();
                sqlCommand.ExecuteNonQuery();
                this.conn.Close();
                return (ActionResult)this.RedirectToAction("LabServices", (object)new
                {
                    id = services.Labid
                });
            }
        }

        public ActionResult DeleteServiceGroup(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("delete from sp_service_grp where Sp_id=@service_grp_id", this.conn);
            sqlCommand.Parameters.AddWithValue("@service_grp_id", (object)id);
            this.conn.Open();
            sqlCommand.ExecuteNonQuery();
            this.conn.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        [BreadCrumb(Label = "New Sub Panel")]
        public ActionResult CreateSubService(int id)
        {
            return (ActionResult)this.View((object)new SubServices()
            {
                lab_group_id = id
            });
        }

        [HttpPost]
        public ActionResult CreateSubService(SubServices subServices)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)subServices);
            SqlCommand sqlCommand = new SqlCommand("Sp_sub_services_insert", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Sp_group_id", (object)subServices.lab_group_id);
            sqlCommand.Parameters.AddWithValue("@sp_services_description", (object)subServices.lab_services_description);
            sqlCommand.Parameters.AddWithValue("@sp_services_ext_description", (object)subServices.lab_services_ext_description);
            sqlCommand.Parameters.AddWithValue("@service_charges", (object)subServices.service_charges);
            sqlCommand.Parameters.AddWithValue("@client_billing_charges", (object)subServices.client_billing_charges);
            this.conn.Open();
            sqlCommand.ExecuteNonQuery();
            this.conn.Close();
            return (ActionResult)this.RedirectToAction("UpdateServiceGroup", (object)new
            {
                id = subServices.lab_group_id
            });
        }
        [BreadCrumb(Label = "Update Sub Panel")]
        public ActionResult UpdateSubService(int id)
        {
            SubServices subServices = new SubServices();
            subServices.lab_service_id = id;
            SqlCommand sqlCommand = new SqlCommand("Sp_sub_services_Edit", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Sp_service_id", (object)id);
            this.conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    subServices.lab_services_description = sqlDataReader["sp_services_description"].ToString();
                    subServices.lab_services_ext_description = sqlDataReader["sp_services_ext_description"].ToString();
                    subServices.client_billing_charges = Convert.ToDecimal(sqlDataReader["client_billing_charges"].ToString());
                    subServices.service_charges = Convert.ToDecimal(sqlDataReader["service_charges"].ToString());
                }
                sqlDataReader.Close();
                this.conn.Close();
            }
            else
            {
                sqlDataReader.Close();
                this.conn.Close();
            }
            return (ActionResult)this.View((object)subServices);
        }

        [HttpPost]
        public ActionResult UpdateSubService(SubServices subServices)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View(subServices);
            SqlCommand sqlCommand = new SqlCommand("Sp_sub_services_Update", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Sp_service_id", (object)subServices.lab_service_id);
            sqlCommand.Parameters.AddWithValue("@sp_services_description", (object)subServices.lab_services_description);
            sqlCommand.Parameters.AddWithValue("@sp_services_ext_description", (object)subServices.lab_services_ext_description);
            sqlCommand.Parameters.AddWithValue("@service_charges", (object)subServices.service_charges);
            sqlCommand.Parameters.AddWithValue("@client_billing_charges", (object)subServices.client_billing_charges);
            this.conn.Open();
            sqlCommand.ExecuteNonQuery();
            this.conn.Close();
            SqlCommand selectCommand = new SqlCommand("select Sp_group_id from Sp_sub_services where Sp_service_id=@id", this.conn);
            selectCommand.Parameters.AddWithValue("@id", (object)subServices.lab_service_id);
            DataTable dataTable = new DataTable();
            new SqlDataAdapter(selectCommand).Fill(dataTable);
            if (dataTable.Rows.Count > 0)
                subServices.lab_group_id = Convert.ToInt32(dataTable.Rows[0]["Sp_group_id"]);
            return (ActionResult)this.RedirectToAction("UpdateServiceGroup", (object)new
            {
                id = subServices.lab_group_id
            });
        }

        public ActionResult DeleteSubService(int id)
        {
            SqlCommand sqlCommand = new SqlCommand("Sp_sub_services_delete", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Sp_service_id", (object)id);
            this.conn.Open();
            sqlCommand.ExecuteNonQuery();
            this.conn.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public string getlocationnamebyid(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.Getlab_service_Location_nameById (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;

        }

    }
}
