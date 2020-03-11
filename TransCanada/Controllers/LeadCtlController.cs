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
    public class LeadCtlController : Controller
    {
        // GET: LeadCtl
        string TransCanadaConnection = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
        [BreadCrumb(Label = "New Lead")]
        public ActionResult CreateLead()
        {
            Lead lead = new Lead();
            lead.IsActive = true;
            lead.Customer_Status = "2";
            lead.Received_on = DateTime.Now;
            lead.Next_Follow_Up = DateTime.Now;
            return View(lead);
        }
        [HttpPost]
        public ActionResult CreateLead(Lead LD)

        {
            try
            {
                if (!ModelState.IsValid)
                    return View(LD);

                SqlConnection connection = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_Lead_insert", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(LD.Client_Name))
                {
                    cmd.Parameters.AddWithValue("@Client_Name", LD.Client_Name);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Client_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Contact_FirstName))
                {
                    cmd.Parameters.AddWithValue("@Contact_First_Name", LD.Contact_FirstName);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Contact_First_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Contact_LastName))
                {
                    cmd.Parameters.AddWithValue("@Contact_Last_Name", LD.Contact_LastName);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Contact_Last_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Second_Contact_First_Name))
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_First_Name", LD.Second_Contact_First_Name);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_First_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Second_Contact_Last_Name))
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_Last_Name", LD.Second_Contact_Last_Name);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_Last_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", LD.Email);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Phone))
                {
                    cmd.Parameters.AddWithValue("@Phone", LD.Phone);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", string.Empty);
                }

                if (!string.IsNullOrEmpty(LD.Opportunity_Notes))
                {
                    cmd.Parameters.AddWithValue("@Opportunity_Notes", LD.Opportunity_Notes);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Opportunity_Notes", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Email_Ref))
                {
                    cmd.Parameters.AddWithValue("@Lead_Email_Ref", LD.Lead_Email_Ref);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Email_Ref", string.Empty);
                }
                if (LD.NDA == true)
                {
                    cmd.Parameters.AddWithValue("@NDA", 1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@NDA", 0);
                }
                if (!string.IsNullOrEmpty(LD.Priority))
                {
                    cmd.Parameters.AddWithValue("@Priority", LD.Priority);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Priority", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Address))
                {
                    cmd.Parameters.AddWithValue("@Address", LD.Address);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Address", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.City))
                {
                    cmd.Parameters.AddWithValue("@City", LD.City);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@City", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.State))
                {
                    cmd.Parameters.AddWithValue("@State", LD.State);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@State", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Zip_Code))
                {
                    cmd.Parameters.AddWithValue("@Zip_Code", LD.Zip_Code);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Zip_Code", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Country))
                {
                    cmd.Parameters.AddWithValue("@Country", LD.Country);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Country", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Mobile_Number))
                {
                    cmd.Parameters.AddWithValue("@Mobile_Number", LD.Mobile_Number);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile_Number", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Fax_Number))
                {
                    cmd.Parameters.AddWithValue("@Fax_Number", LD.Fax_Number);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Fax_Number", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Website))
                {
                    cmd.Parameters.AddWithValue("@Website", LD.Website);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Website", string.Empty);
                }

                cmd.Parameters.AddWithValue("@Received_on", LD.Received_on);

                if (!string.IsNullOrEmpty(LD.Customer_Status))
                {
                    cmd.Parameters.AddWithValue("@Customer_Status", LD.Customer_Status);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Customer_Status", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Source))
                {
                    cmd.Parameters.AddWithValue("@Lead_Source", LD.Lead_Source);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Source", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Received_Via))
                {
                    cmd.Parameters.AddWithValue("@Lead_Received_Via", LD.Lead_Received_Via);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Received_Via", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Proposal_Status))
                {
                    cmd.Parameters.AddWithValue("@Proposal_Status", LD.Proposal_Status);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Proposal_Status", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Eligibility))
                {
                    cmd.Parameters.AddWithValue("@Lead_Eligibility", LD.Lead_Eligibility);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Eligibility", string.Empty);
                }
                if (!string.IsNullOrEmpty(Convert.ToDateTime(LD.Next_Follow_Up).ToString()))
                {
                    cmd.Parameters.AddWithValue("@Next_Follow_Up", Convert.ToDateTime(LD.Next_Follow_Up));

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Next_Follow_Up", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Status))
                {
                    cmd.Parameters.AddWithValue("@Lead_Status", LD.Lead_Status);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Status", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Sales_Manager))
                {
                    cmd.Parameters.AddWithValue("@Sales_Manager", LD.Sales_Manager);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sales_Manager", string.Empty);
                }

                if (!string.IsNullOrEmpty(LD.Sales_Person))
                {
                    cmd.Parameters.AddWithValue("@Sales_Person", LD.Sales_Person);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sales_Person", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Notes))
                {
                    cmd.Parameters.AddWithValue("@Notes", LD.Notes);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Notes", string.Empty);
                }
                if (LD.IsActive == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Active", 1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Active", 0);
                }


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("Leadlist");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errorpage");
            }
        }

        [HttpGet]
        [BreadCrumb(Label = "Update Lead")]
        public ActionResult UpdateLead(string id)
        {
            try
            {
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("Tbl_Lead_Get_by_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Lead LD = new Lead();
                if (dt.Rows.Count > 0)
                {
                    LD.Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Client_Name"].ToString()))
                    {
                        LD.Client_Name = dt.Rows[0]["Client_Name"].ToString();
                    }
                    else
                    {
                        LD.Client_Name = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_First_Name"].ToString()))
                    {
                        LD.Contact_FirstName = dt.Rows[0]["Contact_First_Name"].ToString();
                    }
                    else
                    {
                        LD.Contact_FirstName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Contact_Last_Name"].ToString()))
                    {
                        LD.Contact_LastName = dt.Rows[0]["Contact_Last_Name"].ToString();
                    }
                    else
                    {
                        LD.Contact_LastName = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Second_Contact_First_Name"].ToString()))
                    {
                        LD.Second_Contact_First_Name = dt.Rows[0]["Second_Contact_First_Name"].ToString();
                    }
                    else
                    {
                        LD.Second_Contact_First_Name = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Second_Contact_Last_Name"].ToString()))
                    {
                        LD.Second_Contact_Last_Name = dt.Rows[0]["Second_Contact_Last_Name"].ToString();
                    }
                    else
                    {
                        LD.Second_Contact_Last_Name = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Email"].ToString()))
                    {
                        LD.Email = dt.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        LD.Email = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Phone"].ToString()))
                    {
                        LD.Phone = dt.Rows[0]["Phone"].ToString();
                    }
                    else
                    {
                        LD.Phone = string.Empty;
                    }
                    if (dt.Rows[0]["Is_Active"].ToString() != null)
                    {
                        LD.IsActive = Convert.ToBoolean(dt.Rows[0]["Is_Active"].ToString());
                    }
                    else
                    {
                        LD.IsActive = false;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Opportunity_Notes"].ToString()))
                    {
                        LD.Opportunity_Notes = dt.Rows[0]["Opportunity_Notes"].ToString();
                    }
                    else
                    {
                        LD.Opportunity_Notes = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Lead_Email_Ref"].ToString()))
                    {
                        LD.Lead_Email_Ref = dt.Rows[0]["Lead_Email_Ref"].ToString();
                    }
                    else
                    {
                        LD.Lead_Email_Ref = string.Empty;
                    }
                    if (dt.Rows[0]["NDA"].ToString() != null)
                    {
                        LD.NDA = Convert.ToBoolean(dt.Rows[0]["NDA"].ToString());
                    }
                    else
                    {
                        LD.NDA = false;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Priority"].ToString()))
                    {
                        LD.Priority = dt.Rows[0]["Priority"].ToString();
                    }
                    else
                    {
                        LD.Priority = "0";
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Address"].ToString()))
                    {
                        LD.Address = dt.Rows[0]["Address"].ToString();
                    }
                    else
                    {
                        LD.Address = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["City"].ToString()))
                    {
                        LD.City = dt.Rows[0]["City"].ToString();
                    }
                    else
                    {
                        LD.City = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["State"].ToString()))
                    {
                        LD.State = dt.Rows[0]["State"].ToString();
                    }
                    else
                    {
                        LD.State = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Zip_Code"].ToString()))
                    {
                        LD.Zip_Code = dt.Rows[0]["Zip_Code"].ToString();
                    }
                    else
                    {
                        LD.Zip_Code = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Country"].ToString()))
                    {
                        LD.Country = dt.Rows[0]["Country"].ToString();
                    }
                    else
                    {
                        LD.Country = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Mobile_Number"].ToString()))
                    {
                        LD.Mobile_Number = dt.Rows[0]["Mobile_Number"].ToString();
                    }
                    else
                    {
                        LD.Mobile_Number = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Fax_Number"].ToString()))
                    {
                        LD.Fax_Number = dt.Rows[0]["Fax_Number"].ToString();
                    }
                    else
                    {
                        LD.Fax_Number = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Website"].ToString()))
                    {
                        LD.Website = dt.Rows[0]["Website"].ToString();
                    }
                    else
                    {
                        LD.Website = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Received_on"].ToString()))
                    {
                        LD.Received_on = Convert.ToDateTime(dt.Rows[0]["Received_on"].ToString());
                    }
                    else
                    {
                        LD.Received_on = DateTime.Now;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Customer_Status"].ToString()))
                    {
                        LD.Customer_Status = dt.Rows[0]["Customer_Status"].ToString();
                    }
                    else
                    {
                        LD.Customer_Status = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Lead_Source"].ToString()))
                    {
                        LD.Lead_Source = dt.Rows[0]["Lead_Source"].ToString();
                    }
                    else
                    {
                        LD.Lead_Source = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Lead_Received_Via"].ToString()))
                    {
                        LD.Lead_Received_Via = dt.Rows[0]["Lead_Received_Via"].ToString();
                    }
                    else
                    {
                        LD.Lead_Received_Via = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Proposal_Status"].ToString()))
                    {
                        LD.Proposal_Status = dt.Rows[0]["Proposal_Status"].ToString();
                    }
                    else
                    {
                        LD.Proposal_Status = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Lead_Eligibility"].ToString()))
                    {
                        LD.Lead_Eligibility = dt.Rows[0]["Lead_Eligibility"].ToString();
                    }
                    else
                    {
                        LD.Lead_Eligibility = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Next_Follow_Up"].ToString()))
                    {
                        LD.Next_Follow_Up = Convert.ToDateTime(dt.Rows[0]["Next_Follow_Up"].ToString());
                    }
                    else
                    {
                        LD.Next_Follow_Up = DateTime.Now;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Lead_Status"].ToString()))
                    {
                        LD.Lead_Status = dt.Rows[0]["Lead_Status"].ToString();
                    }
                    else
                    {
                        LD.Lead_Status = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Sales_Manager"].ToString()))
                    {
                        LD.Sales_Manager = dt.Rows[0]["Sales_Manager"].ToString();
                    }
                    else
                    {
                        LD.Sales_Manager = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Sales_Person"].ToString()))
                    {
                        LD.Sales_Person = dt.Rows[0]["Sales_Person"].ToString();
                    }
                    else
                    {
                        LD.Sales_Person = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Notes"].ToString()))
                    {
                        LD.Notes = dt.Rows[0]["Notes"].ToString();
                    }
                    else
                    {
                        LD.Notes = string.Empty;
                    }

                }
                List<Callhist> calllist = new List<Callhist>();
                List<Callhist> calllist1 = new List<Callhist>();
                SqlCommand sqlCommand = new SqlCommand("select * from tbl_call_history where Lead_id=@Lead_id order by call_on desc", con);
                sqlCommand.Parameters.AddWithValue("@Lead_id", (object)id);
                DataTable table = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(table);
                if(table.Rows.Count>0)
                {
                    for (int i = 0; i < table.Rows.Count; i++) {
                        Callhist callhist1 = new Callhist();
                        callhist1.call_status = table.Rows[i]["call_status"].ToString();
                        callhist1.followupdate =Convert.ToDateTime(table.Rows[i]["followupdate"].ToString());
                        callhist1.followuptime= table.Rows[i]["followuptime"].ToString();
                        callhist1.reminderdate = Convert.ToDateTime(table.Rows[i]["remainderdate"].ToString());
                        callhist1.remindertime = table.Rows[i]["remaidertime"].ToString();
                        callhist1.remainder_by = table.Rows[i]["remainder_by"].ToString();
                        callhist1.Notes = table.Rows[i]["Notes"].ToString();
                        callhist1.Last_Call_on= Convert.ToDateTime(table.Rows[i]["call_on"].ToString());
                        callhist1.Duration = table.Rows[i]["duration"].ToString();
                        calllist.Add(callhist1);

                    }
                }
                else
                {

                }

                ViewData["Data"] = calllist;
                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = table.Select().Where(p => (Convert.ToDateTime(p["followupdate"]) >= Convert.ToDateTime(DateTime.Now))).CopyToDataTable();
                }
                catch
                {
                    dt1.Clear();
                    
                }
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        Callhist callhist11 = new Callhist();
                        callhist11.call_status = dt1.Rows[i]["call_status"].ToString();
                        callhist11.followupdate = Convert.ToDateTime(dt1.Rows[i]["followupdate"].ToString());
                        callhist11.followuptime = dt1.Rows[i]["followuptime"].ToString();
                        callhist11.reminderdate = Convert.ToDateTime(dt1.Rows[i]["remainderdate"].ToString());
                        callhist11.remindertime = dt1.Rows[i]["remaidertime"].ToString();
                        callhist11.remainder_by = dt1.Rows[i]["remainder_by"].ToString();
                        callhist11.Notes = dt1.Rows[i]["Notes"].ToString();
                        callhist11.Last_Call_on = Convert.ToDateTime(table.Rows[i]["call_on"].ToString());
                        callhist11.Duration = dt1.Rows[i]["duration"].ToString();
                        calllist1.Add(callhist11);

                    }
                }
                else
                {

                }
                ViewData["Data_follow"] = calllist1;
                //return filteredTable;
                return View(LD);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Errorpage");
            }
        }
        [HttpPost]
        public ActionResult UpdateLead(Lead LD)
        {
            try
            {

                if (!ModelState.IsValid)
                    return View(LD);
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_lead_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", LD.Id);


                if (!string.IsNullOrEmpty(LD.Client_Name))
                {
                    cmd.Parameters.AddWithValue("@Client_Name", LD.Client_Name);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Client_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Contact_FirstName))
                {
                    cmd.Parameters.AddWithValue("@Contact_First_Name", LD.Contact_FirstName);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Contact_First_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Contact_LastName))
                {
                    cmd.Parameters.AddWithValue("@Contact_Last_Name", LD.Contact_LastName);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Contact_Last_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Second_Contact_First_Name))
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_First_Name", LD.Second_Contact_First_Name);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_First_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Second_Contact_Last_Name))
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_Last_Name", LD.Second_Contact_Last_Name);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Second_Contact_Last_Name", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Email))
                {
                    cmd.Parameters.AddWithValue("@Email", LD.Email);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Phone))
                {
                    cmd.Parameters.AddWithValue("@Phone", LD.Phone);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Phone", string.Empty);
                }

                if (!string.IsNullOrEmpty(LD.Opportunity_Notes))
                {
                    cmd.Parameters.AddWithValue("@Opportunity_Notes", LD.Opportunity_Notes);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Opportunity_Notes", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Email_Ref))
                {
                    cmd.Parameters.AddWithValue("@Lead_Email_Ref", LD.Lead_Email_Ref);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Email_Ref", string.Empty);
                }
                if (LD.NDA == true)
                {
                    cmd.Parameters.AddWithValue("@NDA", 1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@NDA", 0);
                }
                if (!string.IsNullOrEmpty(LD.Priority))
                {
                    cmd.Parameters.AddWithValue("@Priority", LD.Priority);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Priority", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Address))
                {
                    cmd.Parameters.AddWithValue("@Address", LD.Address);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Address", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.City))
                {
                    cmd.Parameters.AddWithValue("@City", LD.City);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@City", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.State))
                {
                    cmd.Parameters.AddWithValue("@State", LD.State);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@State", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Zip_Code))
                {
                    cmd.Parameters.AddWithValue("@Zip_Code", LD.Zip_Code);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Zip_Code", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Country))
                {
                    cmd.Parameters.AddWithValue("@Country", LD.Country);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Country", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Mobile_Number))
                {
                    cmd.Parameters.AddWithValue("@Mobile_Number", LD.Mobile_Number);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile_Number", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Fax_Number))
                {
                    cmd.Parameters.AddWithValue("@Fax_Number", LD.Fax_Number);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Fax_Number", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Website))
                {
                    cmd.Parameters.AddWithValue("@Website", LD.Website);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Website", string.Empty);
                }

                cmd.Parameters.AddWithValue("@Received_on", LD.Received_on);

                if (!string.IsNullOrEmpty(LD.Customer_Status))
                {
                    cmd.Parameters.AddWithValue("@Customer_Status", LD.Customer_Status);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Customer_Status", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Source))
                {
                    cmd.Parameters.AddWithValue("@Lead_Source", LD.Lead_Source);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Source", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Received_Via))
                {
                    cmd.Parameters.AddWithValue("@Lead_Received_Via", LD.Lead_Received_Via);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Received_Via", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Proposal_Status))
                {
                    cmd.Parameters.AddWithValue("@Proposal_Status", LD.Proposal_Status);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Proposal_Status", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Eligibility))
                {
                    cmd.Parameters.AddWithValue("@Lead_Eligibility", LD.Lead_Eligibility);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Eligibility", string.Empty);
                }
                if (!string.IsNullOrEmpty(Convert.ToDateTime(LD.Next_Follow_Up).ToString()))
                {
                    cmd.Parameters.AddWithValue("@Next_Follow_Up", Convert.ToDateTime(LD.Next_Follow_Up));

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Next_Follow_Up", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Lead_Status))
                {
                    cmd.Parameters.AddWithValue("@Lead_Status", LD.Lead_Status);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Lead_Status", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Sales_Manager))
                {
                    cmd.Parameters.AddWithValue("@Sales_Manager", LD.Sales_Manager);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sales_Manager", string.Empty);
                }

                if (!string.IsNullOrEmpty(LD.Sales_Person))
                {
                    cmd.Parameters.AddWithValue("@Sales_Person", LD.Sales_Person);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Sales_Person", string.Empty);
                }
                if (!string.IsNullOrEmpty(LD.Notes))
                {
                    cmd.Parameters.AddWithValue("@Notes", LD.Notes);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Notes", string.Empty);
                }
                if (LD.IsActive == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Active", 1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Active", 0);
                }


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Leadlist");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errorpage");
            }
        }
        [HttpPost]
        public JsonResult Newcall(Callhist callhist)
        {
            try
            {

                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_call_history_new", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if(string.IsNullOrEmpty(callhist.call_status))
                    cmd.Parameters.AddWithValue("@call_status", "Follow Up");
                else
                    cmd.Parameters.AddWithValue("@call_status", callhist.call_status);
                cmd.Parameters.AddWithValue("@followupdate", callhist.followupdate);
                cmd.Parameters.AddWithValue("@followuptime", callhist.followuptime);
                cmd.Parameters.AddWithValue("@remainderdate", callhist.reminderdate);
                cmd.Parameters.AddWithValue("@remaidertime", callhist.remindertime);
                cmd.Parameters.AddWithValue("@remainder_by", callhist.remainder_by);
                if (string.IsNullOrEmpty(callhist.Notes))
                    cmd.Parameters.AddWithValue("@notes", string.Empty);
                else
                cmd.Parameters.AddWithValue("@notes", callhist.Notes);

                cmd.Parameters.AddWithValue("@Lead_id", callhist.leadid);
                cmd.Parameters.AddWithValue("@duration", callhist.Duration);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json("Newcallreg", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }


        [BreadCrumb(Clear = true, Label = "Leads")]
        public ActionResult Leadlist()
        {
            try
            {
                SqlConnection con = new SqlConnection(TransCanadaConnection);
                SqlCommand cmd = new SqlCommand("tbl_list_lead", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<Lead> LeadList = new List<Lead>();
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    Lead LD = new Lead();
                    LD.Id = Convert.ToInt32(dt.Rows[j]["Id"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Client_Name"].ToString()))
                    {
                        LD.Client_Name = dt.Rows[j]["Client_Name"].ToString();
                    }
                    else
                    {
                        LD.Client_Name = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Contact_First_Name"].ToString()))
                    {
                        LD.Contact_FirstName = dt.Rows[j]["Contact_First_Name"].ToString();
                    }
                    else
                    {
                        LD.Contact_FirstName = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[j]["Email"].ToString()))
                    {
                        LD.Email = dt.Rows[j]["Email"].ToString();
                    }
                    else
                    {
                        LD.Email = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["Phone"].ToString()))
                    {
                        LD.Phone = dt.Rows[j]["Phone"].ToString();
                    }
                    else
                    {
                        LD.Phone = string.Empty;
                    }

                    LeadList.Add(LD);
                }

                return View(LeadList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Errorpage");
            }
        }

        public static List<SelectListItem> ListCustomerStatus()
        {

            List<SelectListItem> Cs = new List<SelectListItem>() {

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
                    Text = "Customer-Prospect",
                    Value = "2"
                },
                   new SelectListItem
                {
                    Text = "Vendor",
                    Value = "3"
                },   new SelectListItem
                {
                    Text = "Vendor-Prospect",
                    Value = "4"
                },   new SelectListItem
                {
                    Text = "Free Lancer",
                    Value = "5"
                },
                    new SelectListItem
                {
                    Text = "Other",
                    Value = "6"
                },
              };
            return Cs;
        }
        public static List<SelectListItem> ListLeadSource()
        {

            List<SelectListItem> Ls = new List<SelectListItem>() {

                // Asign Value from database
                 new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Desss",
                    Value = "1"
                },


              };
            return Ls;
        }
        public static List<SelectListItem> ListLeadEligible()
        {

            List<SelectListItem> Ls = new List<SelectListItem>() {

                // Asign Value from database
                 new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Qualified",
                    Value = "1"
                },
                  new SelectListItem
                {
                    Text = "Disqualified",
                    Value = "2"
                },

              };
            return Ls;
        }
        public static List<SelectListItem> ListLeadStatus()
        {

            List<SelectListItem> Cs = new List<SelectListItem>() {

                // Asign Value from database
                   new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Lead recieved",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Lead reviewed",
                    Value = "2"
                },
                   new SelectListItem
                {
                    Text = "Acknowledgement Email Sent",
                    Value = "3"
                },   new SelectListItem
                {
                    Text = "Analytical Report",
                    Value = "4"
                },   new SelectListItem
                {
                    Text = "Follow Up",
                    Value = "5"
                },
                    new SelectListItem
                {
                    Text = "Meeting to be scheduled",
                    Value = "6"
                },
                     new SelectListItem
                {
                    Text = "Meeting Scheduled",
                    Value = "7"
                }, new SelectListItem
                {
                    Text = "Disqualified",
                    Value = "8"
                }, new SelectListItem
                {
                    Text = "Qualified",
                    Value = "9"
                }, new SelectListItem
                {
                    Text = "Converted as Oppurtunity",
                    Value = "10"
                }, new SelectListItem
                {
                    Text = "Time Estimation pending from Programmers",
                    Value = "11"
                },
                     new SelectListItem
                {
                    Text = "Time Estimation Completed By Programmers",
                    Value = "12"
                },new SelectListItem
                {
                    Text = "Site Map Created & Proposal Pending",
                    Value = "13"
                },new SelectListItem
                {
                    Text = "Proposal Given",
                    Value = "14"
                },new SelectListItem
                {
                    Text = "Proposal Pending",
                    Value = "15"
                },new SelectListItem
                {
                    Text = "Proposal Follow Up",
                    Value = "16"
                },new SelectListItem
                {
                    Text = "Project Lost",
                    Value = "17"
                },
                     new SelectListItem
                {
                    Text = "Awarded",
                    Value = "18"
                },new SelectListItem
                {
                    Text = "Discussing With Project Manager",
                    Value = "19"
                },new SelectListItem
                {
                    Text = "Requirements Gathering",
                    Value = "20"
                },new SelectListItem
                {
                    Text = "Vendor Discussion",
                    Value = "21"
                },
                     new SelectListItem
                {
                    Text = "Scope of Work in Progress",
                    Value = "22"
                },new SelectListItem
                {
                    Text = "Site Map in Progress",
                    Value = "23"
                },new SelectListItem
                {
                    Text = "Proposal Pending from Vendors",
                    Value = "24"
                },
              };
            return Cs;
        }
        public static List<SelectListItem> ListSalesManager()
        {

            List<SelectListItem> Sm = new List<SelectListItem>()
            {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Chandler Dev",
                    Value = "1"
                },
            };
            return Sm;

        }
        public static List<SelectListItem> ListSalesPerson()
        {

            List<SelectListItem> Sp = new List<SelectListItem>()
            {

                // Asign Value from database
                new SelectListItem
                {
                    Text = "--Select--",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Chandler Dev",
                    Value = "1"
                },
            };
            return Sp;

        }

        public static List<SelectListItem> ListLeadRecievedVia()
        {

            List<SelectListItem> Lr = new List<SelectListItem>() {

                // Asign Value from database
                 new SelectListItem
                {
                    Text = "Phone",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Call",
                    Value = "2"
                },

                 new SelectListItem
                {
                    Text = "Email",
                    Value = "3"
                },
                  new SelectListItem
                {
                    Text = "Text",
                    Value = "4"
                },
              };
            return Lr;
        }

        public static List<SelectListItem> Prioritylist()
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
                    Text = "1",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "2",
                    Value = "2"
                },
                 new SelectListItem
                {
                    Text = "3",
                    Value = "3"
                },
                  new SelectListItem
                {
                    Text = "4",
                    Value = "4"
                },
                     new SelectListItem
                {
                    Text = "5",
                    Value = "5"
                },


              };
            return ls;
        }
        public ActionResult Errorpage()
        {
            return View();
        }
        public JsonResult PrevNotes(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);

            List<string> Cn = new List<string>();
            if (!string.IsNullOrEmpty(id))
            {
                string query = "Get_prev_notes_by_lead";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@leadid", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Cn.Add(dt.Rows[i]["prevnotes"].ToString());
                }
            }

            return Json(Cn, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LeadtoClient(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
                string query = "Proc_client_from_lead";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@leadid", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Json("Success",JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }

        }
    }
}