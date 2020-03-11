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
    public class ProductServiceController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
        // GET: ProductService

        public ActionResult Index()
        {
            return View();
        }
        [BreadCrumb(Clear = true, Label = "Product and Services")]
        public ActionResult ServiceTypeList()
        {
            List<ServiceProd> ServiceType = new List<ServiceProd>();
            try
            {
                SqlCommand selectCommand = new SqlCommand("list_ServiceType ", this.conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    for (int index = 0; index < dataTable.Rows.Count; index++)
                        ServiceType.Add(new ServiceProd()
                        {
                            Id = dataTable.Rows[index]["Id"].ToString(),
                            LabType = string.IsNullOrEmpty(dataTable.Rows[index]["Service_Type"].ToString()) ? string.Empty : dataTable.Rows[index]["Service_Type"].ToString()
                        }); ;
                }
            }
            catch (Exception ex)
            {
            }
            return View(ServiceType);
        }
        [BreadCrumb(Label = "Add Services Products ")]
        public ActionResult AddServiceType()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddServiceType(ServiceProd addservice)
        {
            if (!ModelState.IsValid)
                return View(addservice);
            SqlCommand sqlCommand = new SqlCommand("add_ServiceType", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Service_Type", (object)addservice.LabType);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("ServiceTypeList");
        }
        [BreadCrumb(Label = "Update Services Products ")]
        public ActionResult UpdateServiceType(int id)
        {
            TempData["Product_service"] = getnamebyid(id);
            List<ProductSubservice> subServicesList = new List<ProductSubservice>();
            ServiceProd updateservice = new ServiceProd();
            SqlCommand selectCommand = new SqlCommand("edit_ServiceType", this.conn);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Id", (object)id);
            DataTable dataTable = new DataTable();
            new SqlDataAdapter(selectCommand).Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                int index = 0;
                updateservice.Id =id.ToString();
                updateservice.LabType = string.IsNullOrEmpty(dataTable.Rows[index]["Service_Type"].ToString()) ? string.Empty: dataTable.Rows[index]["Service_Type"].ToString();
            }

            SqlCommand sqlCommand1 = new SqlCommand("Proc_Productsubservice_List", this.conn);
            sqlCommand1.CommandType = CommandType.StoredProcedure;
            sqlCommand1.Parameters.AddWithValue("@Product_service_grp_id", id);
            this.conn.Open();
            SqlDataReader sqlDataReader = sqlCommand1.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                    subServicesList.Add(new ProductSubservice()
                    {
                        id = Convert.ToInt32(sqlDataReader["id"].ToString()),
                        lab_services_description = sqlDataReader["lab_services_description"].ToString(),
                        Product_service_grp_id = Convert.ToInt32(sqlDataReader[nameof(id)].ToString()),
                        lab_services_ext_description = sqlDataReader["lab_services_ext_description"].ToString(),
                        Lab_name= sqlDataReader["Lab_name"].ToString(),
                        service_charges = Convert.ToDecimal(sqlDataReader["service_charges"].ToString()),
                        Billing_Price = Convert.ToDecimal(sqlDataReader["Billing_Price"].ToString() == null ? 0 : Convert.ToDecimal(sqlDataReader["Billing_Price"].ToString()))
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
            return (ActionResult)this.View((object)updateservice);

        }
        [HttpPost]
        public ActionResult UpdateServiceType(ServiceProd updateservice)
        {
            if (!ModelState.IsValid)
                return View(updateservice);
            SqlCommand sqlCommand = new SqlCommand("Update_ServiceType", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", (object)updateservice.Id);
            sqlCommand.Parameters.AddWithValue("@Service_Type", (object)updateservice.LabType);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("ServiceTypeList");
        }

        [BreadCrumb(Label = "Create Sub-Panel")]
        public ActionResult AddProductSubService(int id)
        {
            return (ActionResult)this.View((object)new ProductSubservice()
            {
                Product_service_grp_id = id
            });
        }

        [HttpPost]
        public ActionResult AddProductSubService(ProductSubservice subServices)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)subServices);
            SqlCommand sqlCommand = new SqlCommand("Proc_Productsubservice_Insert", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(subServices.Lab_name))
                sqlCommand.Parameters.AddWithValue("@Lab_name", (object)subServices.Lab_name);
            else
                sqlCommand.Parameters.AddWithValue("@Lab_name", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Billing_Price).ToString()))
                sqlCommand.Parameters.AddWithValue("@Billing_Price", (object)subServices.Billing_Price);
            else
                sqlCommand.Parameters.AddWithValue("@Billing_Price", string.Empty);
            //if (!string.IsNullOrEmpty(subServices.Lab_name))
            //    sqlCommand.Parameters.AddWithValue("@Lab_name", (object)subServices.Lab_name);
            //else
            //    sqlCommand.Parameters.AddWithValue("@Lab_name", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToInt32(subServices.Product_service_grp_id).ToString()))
                sqlCommand.Parameters.AddWithValue("@Product_service_grp_id", (object)subServices.Product_service_grp_id);
            else
                sqlCommand.Parameters.AddWithValue("@Product_service_grp_id", string.Empty);
            if (!string.IsNullOrEmpty(subServices.lab_services_description))
                sqlCommand.Parameters.AddWithValue("@lab_services_description", (object)subServices.lab_services_description);
            else
                sqlCommand.Parameters.AddWithValue("@lab_services_description", string.Empty);
            if (!string.IsNullOrEmpty(subServices.lab_services_ext_description))
                sqlCommand.Parameters.AddWithValue("@lab_services_ext_description", (object)subServices.lab_services_ext_description);
            else
                sqlCommand.Parameters.AddWithValue("@lab_services_ext_description", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.service_charges).ToString()))
                sqlCommand.Parameters.AddWithValue("@service_charges", (object)subServices.service_charges);
            else
                sqlCommand.Parameters.AddWithValue("@service_charges", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.client_billing_charges).ToString()))
                sqlCommand.Parameters.AddWithValue("@client_billing_charges", (object)subServices.client_billing_charges);
            else
                sqlCommand.Parameters.AddWithValue("@client_billing_charges", string.Empty);
            if (!string.IsNullOrEmpty(subServices.Specimen_Type))
                sqlCommand.Parameters.AddWithValue("@Specimen_Type", (object)subServices.Specimen_Type);
            else
                sqlCommand.Parameters.AddWithValue("@Specimen_Type", string.Empty);
            if (!string.IsNullOrEmpty(subServices.Drugs))
                sqlCommand.Parameters.AddWithValue("@Drugs", (object)subServices.Drugs);
            else
                sqlCommand.Parameters.AddWithValue("@Drugs", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Medical_Review_Office_Cost).ToString()))
                sqlCommand.Parameters.AddWithValue("@Medical_Review_Office_Cost", (object)subServices.Medical_Review_Office_Cost);
            else
                sqlCommand.Parameters.AddWithValue("@Medical_Review_Office_Cost", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Vendor_management).ToString()))
                sqlCommand.Parameters.AddWithValue("@Vendor_management", (object)subServices.Vendor_management);
            else
                sqlCommand.Parameters.AddWithValue("@Vendor_management", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Document_Upload).ToString()))
                sqlCommand.Parameters.AddWithValue("@Document_Upload", (object)subServices.Document_Upload);
            else
                sqlCommand.Parameters.AddWithValue("@Document_Upload", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Collection_Cost).ToString()))
                sqlCommand.Parameters.AddWithValue("@Collection_Cost", (object)subServices.Collection_Cost);
            else
                sqlCommand.Parameters.AddWithValue("@Collection_Cost", string.Empty);
            conn.Open();
            int panel_id = Convert.ToInt32(sqlCommand.ExecuteScalar());
            conn.Close();
            int prod_id = 0;
            SqlCommand lab_service_grp_id = new SqlCommand("SELECT dbo.CheckServicegrp(@id,@labname)", conn);
            lab_service_grp_id.Parameters.AddWithValue("@id", subServices.Product_service_grp_id);
            lab_service_grp_id.Parameters.AddWithValue("@labname", subServices.Lab_name);
            conn.Open();
            prod_id =Convert.ToInt32(lab_service_grp_id.ExecuteScalar());
            conn.Close();
            if (prod_id == 0)
            {
                SqlCommand sqlCommand1 = new SqlCommand("AssignProductServicetoLabfrompanel", this.conn);
                sqlCommand1.CommandType = CommandType.StoredProcedure;
                sqlCommand1.Parameters.AddWithValue("@Labname", (object)subServices.Lab_name);
                sqlCommand1.Parameters.AddWithValue("@id", subServices.Product_service_grp_id);
                conn.Open();
                prod_id=Convert.ToInt32(sqlCommand1.ExecuteScalar());
                conn.Close();
            }
            SqlCommand sqlCommand24 = new SqlCommand("proc_assign_lab_sub_service", this.conn);
            sqlCommand24.CommandType = CommandType.StoredProcedure;
            sqlCommand24.Parameters.AddWithValue("@id", panel_id);
            sqlCommand24.Parameters.AddWithValue("@sp_id", prod_id);
            conn.Open();
            sqlCommand24.ExecuteNonQuery();
            conn.Close();
            return (ActionResult)this.RedirectToAction("UpdateServiceType", (object)new
            {
                id = subServices.Product_service_grp_id
            });
        }

        [BreadCrumb(Label = "Update Sub-Panel")]
        public ActionResult UpdateProductSubService(int id)
        {
            ProductSubservice subServices = new ProductSubservice();
            
            SqlCommand sqlCommand = new SqlCommand("Proc_Productsubservice_Edit", this.conn);
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
                    if (!string.IsNullOrEmpty(sqlDataReader["Lab_name"].ToString()))
                    {
                        subServices.Lab_name = sqlDataReader["Lab_name"].ToString();
                    }
                    else
                    {
                        subServices.Lab_name = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(sqlDataReader["Billing_Price"].ToString()))
                    {
                        subServices.Billing_Price = Convert.ToDecimal(sqlDataReader["Billing_Price"].ToString());
                    }
                    else
                    {
                        subServices.Billing_Price = 0;
                    }
                    if (!string.IsNullOrEmpty(sqlDataReader["Lab_name"].ToString()))
                    {
                        subServices.Lab_name = sqlDataReader["Lab_name"].ToString();
                    }
                    else
                    {
                        subServices.Lab_name = string.Empty;
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
                   
                     subServices.Product_service_grp_id =Convert.ToInt32(sqlDataReader["Product_service_grp_id"].ToString());
                    
                  
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
        public ActionResult UpdateProductSubService(ProductSubservice subServices)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View(subServices);
            SqlCommand sqlCommand = new SqlCommand("Proc_Productsubservice_Update", this.conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", (object)subServices.id);
            if (!string.IsNullOrEmpty(subServices.Lab_name))
                sqlCommand.Parameters.AddWithValue("@Lab_name", (object)subServices.Lab_name);
            else
                sqlCommand.Parameters.AddWithValue("@Lab_name", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Billing_Price).ToString()))
                sqlCommand.Parameters.AddWithValue("@Billing_Price", (object)subServices.Billing_Price);
            else
                sqlCommand.Parameters.AddWithValue("@Billing_Price", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToInt32(subServices.Product_service_grp_id).ToString()))
                sqlCommand.Parameters.AddWithValue("@Product_service_grp_id", (object)subServices.Product_service_grp_id);
            else
                sqlCommand.Parameters.AddWithValue("@Product_service_grp_id", string.Empty);

            //if (!string.IsNullOrEmpty(subServices.Lab_name))
            //    sqlCommand.Parameters.AddWithValue("@Lab_name", (object)subServices.Lab_name);
            //else
            //    sqlCommand.Parameters.AddWithValue("@Lab_name", string.Empty);
            if (!string.IsNullOrEmpty(subServices.lab_services_description))
                sqlCommand.Parameters.AddWithValue("@lab_services_description", (object)subServices.lab_services_description);
            else
                sqlCommand.Parameters.AddWithValue("@lab_services_description", string.Empty);
            if (!string.IsNullOrEmpty(subServices.lab_services_ext_description))
                sqlCommand.Parameters.AddWithValue("@lab_services_ext_description", (object)subServices.lab_services_ext_description);
            else
                sqlCommand.Parameters.AddWithValue("@lab_services_ext_description", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.service_charges).ToString()))
                sqlCommand.Parameters.AddWithValue("@service_charges", (object)subServices.service_charges);
            else
                sqlCommand.Parameters.AddWithValue("@service_charges", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.client_billing_charges).ToString()))
                sqlCommand.Parameters.AddWithValue("@client_billing_charges", (object)subServices.client_billing_charges);
            else
                sqlCommand.Parameters.AddWithValue("@client_billing_charges", string.Empty);
            if (!string.IsNullOrEmpty(subServices.Specimen_Type))
                sqlCommand.Parameters.AddWithValue("@Specimen_Type", (object)subServices.Specimen_Type);
            else
                sqlCommand.Parameters.AddWithValue("@Specimen_Type", string.Empty);
            if (!string.IsNullOrEmpty(subServices.Drugs))
                sqlCommand.Parameters.AddWithValue("@Drugs", (object)subServices.Drugs);
            else
                sqlCommand.Parameters.AddWithValue("@Drugs", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Medical_Review_Office_Cost).ToString()))
                sqlCommand.Parameters.AddWithValue("@Medical_Review_Office_Cost", (object)subServices.Medical_Review_Office_Cost);
            else
                sqlCommand.Parameters.AddWithValue("@Medical_Review_Office_Cost", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Vendor_management).ToString()))
                sqlCommand.Parameters.AddWithValue("@Vendor_management", (object)subServices.Vendor_management);
            else
                sqlCommand.Parameters.AddWithValue("@Vendor_management", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Document_Upload).ToString()))
                sqlCommand.Parameters.AddWithValue("@Document_Upload", (object)subServices.Document_Upload);
            else
                sqlCommand.Parameters.AddWithValue("@Document_Upload", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Collection_Cost).ToString()))
                sqlCommand.Parameters.AddWithValue("@Collection_Cost", (object)subServices.Collection_Cost);
            else
                sqlCommand.Parameters.AddWithValue("@Collection_Cost", string.Empty);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();

            return (ActionResult)this.RedirectToAction("UpdateServiceType", (object)new
            {
                id = subServices.Product_service_grp_id
            });
        }

        public ActionResult DeleteServiceType(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("delete_ServiceType", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ServiceTypeList");
        }
        public ActionResult DeleteProductsubservice(int id)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Proc_Productsubservice_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return (ActionResult)this.Redirect(Request.UrlReferrer.AbsolutePath);
        }

        public string getnamebyid(int id)
        {
            string name;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select dbo.GetProduct_ServiceByLId (@ID)", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            name = Convert.ToString(cmd.ExecuteScalar());
            con.Close();
            return name;

        }
        
    }
}