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
    public class PanelPriceController : Controller
    {
        private string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: PanelPrice
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Panels(int id)
        {
            Session["Client_idPK"] = id.ToString();
            List<PriceModel> PanelCostList = new List<PriceModel>();
            SqlConnection con = new SqlConnection(this.TransConnString);
            SqlCommand selectCommand = new SqlCommand(" select * from Lab_demo where Client_Name=@Client_Name ", con);
            selectCommand.Parameters.AddWithValue("@Client_Name", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                string a = dataTable.Rows[0]["SubServices"].ToString().Trim(',');
                string[] b = a.Split(',');
                for (int j = 0; j < b.Length; j++)
                {
                    SqlCommand selectCommand1 = new SqlCommand("SELECT tbl_lab_sub_service.id,tbl_lab_sub_service.lab_services_description FROM tbl_lab_sub_service left join lab_service_grp on lab_service_grp.id = tbl_lab_sub_service.lab_service_grp_id where tbl_lab_sub_service.id in (@id) ", con);
                    selectCommand1.Parameters.AddWithValue("@id", b[j]);
                    SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter(selectCommand1);
                    DataTable dataTable1 = new DataTable();
                    sqlDataAdapter1.Fill(dataTable1);

                    for (int i = 0; i < dataTable1.Rows.Count; i++)
                        PanelCostList.Add(new PriceModel()
                        {
                            id = Convert.ToInt32(dataTable1.Rows[i]["id"].ToString()),
                            lab_services_description = dataTable1.Rows[i]["lab_services_description"].ToString()
                        });

                }
            }
            return (ActionResult)this.View((object)PanelCostList);

        }
        public ActionResult Price(string id)
        {

            Billing subServices = new Billing();
            subServices.lab_services_description = id.Trim();
            SqlConnection conn = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Proc_AssignPrice", conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@lab_services_description", (object)id);
            sqlCommand.Parameters.AddWithValue("@Client_id", Session["Client_idPK"].ToString());
           

            conn.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    if (!string.IsNullOrEmpty(sqlDataReader["lab_services_description"].ToString()))
                    {
                        subServices.lab_services_description = id;
                    }
                    else
                    {
                        subServices.lab_services_description = string.Empty;
                    }
                    //if (!string.IsNullOrEmpty(sqlDataReader["Lab_name"].ToString()))
                    //{
                    //    subServices.Lab_name = sqlDataReader["Lab_name"].ToString();
                    //}
                    //else
                    //{
                    //    subServices.Lab_name = string.Empty;
                    //}

                    if (!string.IsNullOrEmpty(sqlDataReader["Billing_Price"].ToString()))
                    {
                        subServices.Billing_Price = Convert.ToDecimal(sqlDataReader["Billing_Price"].ToString());
                    }
                    else
                    {
                        subServices.Billing_Price = 0;
                    }
                    //if (!string.IsNullOrEmpty(sqlDataReader["Lab_name"].ToString()))
                    //{
                    //    subServices.Lab_name = sqlDataReader["Lab_name"].ToString();
                    //}
                    //else
                    //{
                    //    subServices.Lab_name = string.Empty;
                    //}
                   
                    //if (!string.IsNullOrEmpty(sqlDataReader["lab_services_ext_description"].ToString()))
                    //{
                    //    subServices.lab_services_ext_description = sqlDataReader["lab_services_ext_description"].ToString();
                    //}
                    //else
                    //{
                    //    subServices.lab_services_ext_description = string.Empty;
                    //}
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



                    //if (!string.IsNullOrEmpty(sqlDataReader["Specimen_Type"].ToString()))
                    //{
                    //    subServices.Specimen_Type = sqlDataReader["Specimen_Type"].ToString();
                    //}
                    //else
                    //{
                    //    subServices.Specimen_Type = string.Empty;
                    //}
                    //if (!string.IsNullOrEmpty(sqlDataReader["Drugs"].ToString()))
                    //{
                    //    subServices.Drugs = sqlDataReader["Drugs"].ToString();
                    //}
                    //else
                    //{
                    //    subServices.Drugs = string.Empty;
                    //}
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
        public ActionResult Price(Billing subServices)
        {
            if(!ModelState.IsValid)
                return (ActionResult)this.View(subServices);
            SqlConnection conn = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Proc_Billing_Price_Update", conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(subServices.Lab_name))
                sqlCommand.Parameters.AddWithValue("@Lab_name", (object)subServices.Lab_name);
            else
                sqlCommand.Parameters.AddWithValue("@Lab_name", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToDecimal(subServices.Billing_Price).ToString()))
                sqlCommand.Parameters.AddWithValue("@Billing_Price", (object)subServices.Billing_Price);
            else
                sqlCommand.Parameters.AddWithValue("@Billing_Price", string.Empty);
            if (!string.IsNullOrEmpty(Convert.ToInt32(subServices.Client_id).ToString())) 
                sqlCommand.Parameters.AddWithValue("@Client_id", Session["Client_idPK"].ToString());
            else
                sqlCommand.Parameters.AddWithValue("@Client_id", Session["Client_idPK"].ToString());
            
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

            return Json(new { Data = subServices, success = ModelState.IsValid ? true : false, error = ViewData }, JsonRequestBehavior.AllowGet);

        }
    }
}