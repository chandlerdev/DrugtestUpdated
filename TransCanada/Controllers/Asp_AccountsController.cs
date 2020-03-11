using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TransCanada.Models;
using MvcBreadCrumbs;
using Microsoft.AspNet.Identity.Owin;

namespace TransCanada.Controllers
{
    [Authorize]
    [BreadCrumb]
    
    public class Asp_AccountsController : Controller
    {
        
        private string TransConnString = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        public string CheckAccountName(string Accountname)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("SELECT Accountid from AspnetAccounts where Accountid=@Accountid", connection);
            sqlCommand.Parameters.AddWithValue("@Accountid", (object)Accountname);
            connection.Open();
            if (sqlCommand.ExecuteReader().HasRows)
            {
                connection.Close();
                return "Failure";
            }
            connection.Close();
            return "Success";
        }
        [BreadCrumb(Clear =true, Label = "Client List")]
        public ActionResult AccountsList()
        {

            List<AspNetAccountsModel> netAccountsModelList = new List<AspNetAccountsModel>();
            SqlConnection connection = new SqlConnection(TransConnString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("Select * from AspNetAccounts", connection));

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            for (int index = 0; index < dataTable.Rows.Count; index++)
            {
                AspNetAccountsModel aspNetAccountsModel = new AspNetAccountsModel();
                aspNetAccountsModel.AccountId_PK = Convert.ToInt32(dataTable.Rows[index]["AccountId_PK"].ToString());
                if (!string.IsNullOrEmpty(dataTable.Rows[index]["AccountId"].ToString()))
                    aspNetAccountsModel.AccountId = dataTable.Rows[index]["AccountId"].ToString();
                else
                    aspNetAccountsModel.AccountId = string.Empty;
                if (!string.IsNullOrEmpty(dataTable.Rows[index]["LogoImage"].ToString()))
                    aspNetAccountsModel.LogoImage = dataTable.Rows[index]["LogoImage"].ToString();
                else
                    aspNetAccountsModel.LogoImage = "No_Logo.png";
                //aspNetAccountsModel.CreatedBy = dataTable.Rows[index]["CreatedBy"].ToString();
                //aspNetAccountsModel.CreatedDate = Convert.ToDateTime(dataTable.Rows[index]["CreatedDate"].ToString());
                //aspNetAccountsModel.UpdatedBy = dataTable.Rows[index]["UpdatedBy"].ToString();
                //aspNetAccountsModel.UpdatedDate = Convert.ToDateTime(dataTable.Rows[index]["UpdatedDate"].ToString());
                netAccountsModelList.Add(aspNetAccountsModel);
            }
            return View(netAccountsModelList);

        }

        [BreadCrumb(Label = "New Client")]
        public ActionResult InsertAccounts( )
        {
            //List<SelectListItem> Acc = new List<SelectListItem>()
            //{
            //      new SelectListItem {Text="FMCSA",Value="FMCSA",Selected=false },
            //       new SelectListItem {Text="FAA",Value="FAA",Selected=false},
            //       new SelectListItem {Text="FRA",Value="FRA",Selected=false },
            //       new SelectListItem {Text="FTA",Value="FTA",Selected=false},
            //       new SelectListItem {Text="PHMSA",Value="PHMSA",Selected=false},
            //       new SelectListItem {Text="USCG",Value="USCG",Selected=false},
            //};

            //      AspNetAccountsModel objBind = new AspNetAccountsModel();
            //      objBind.CheckBoxes = Acc;
            AspNetAccountsModel aspNetAccountsModel = new AspNetAccountsModel();
            aspNetAccountsModel.self_collect_list = ListSelfCollect();
            return View(aspNetAccountsModel);
        }
        public static List<CheckBox> ListSelfCollect()
        {

            List<CheckBox> At = new List<CheckBox>()
            {

                new CheckBox
                {
                    Text = "Yes",
                    Value = "1"
                },
                new CheckBox
                {
                    Text = "No",
                    Value = "2"
                }
            };
            return At;
        }
        [HttpPost]
        public ActionResult InsertAccounts(AspNetAccountsModel Acc, HttpPostedFileBase file)
        {
            DateTime now1 = DateTime.Now;
            DateTime now2 = DateTime.Now;
            if (Acc.Address == null)
                Acc.Address = string.Empty;
            if (Acc.Zip == null)
                Acc.Zip = string.Empty;
            if (Acc.City == null)
                Acc.City = string.Empty;
            if (Acc.State == null)
                Acc.State = string.Empty;
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)Acc);
            try
            {

                SqlConnection connection = new SqlConnection(this.TransConnString);
                //Acc.CheckBoxes = new List<SelectListItem>();
                //SqlCommand sqlCommand = new SqlCommand("Insert into  AspNetAccounts (TPA_Client,Category, Isactive,AccountId, LogoImage, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate,Short_Name,address1,address2,city,state,zip,FirstName,LastName,email,Phone,Location,Notes_1,Category_1,TPA_Category,Address_type,Address_Notes,Title,function_1,Phone_Number_type,Fax,Notes,POCT_Testing,Self_Collect,Clearing_House,Compliance_Support,Full_time,Background_Checks,Pool,Reporting,Billing_Details,Credit_Card_Details,Notes_On_Billing,POCT_Testing_Details,Self_Collect_Details,Clearing_House_Details,Compliance_Support_Details,Background_Checks_Details,Pool_Details,Card_Details) output INSERTED.AccountId_PK values(@TPA_Client,@Category, @Isactive,@AccountId, @LogoImage, @CreatedBy, @CreatedDate, @UpdatedBy, @UpdatedDate,@Short_Name,@address1,@address2,@city,@state,@zip,@FirstName,@LastName,@email,@Phone,@Location,@Notes_1,@Category_1,@TPA_Category,@Address_type,@Address_Notes,@Title,@function_1,@Phone_Number_type,@Fax,@Notes,@POCT_Testing,@Self_Collect,@Clearing_House,@Compliance_Support,@Full_time,@Background_Checks,@Pool,@Reporting,@Billing_Details,@Credit_Card_Details,@Notes_On_Billing,@POCT_Testing_Details,@Self_Collect_Details,@Clearing_House_Details,@Compliance_Support_Details,@Background_Checks_Details,@Pool_Details,@Card_Details)", connection);
                SqlCommand sqlCommand = new SqlCommand("Proc_Insert_AspNetAccounts", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                string self_col = string.Empty;
                for(int i=0;i<Acc.self_collect_list.Count;i++)
                {
                    if (Acc.self_collect_list[i].IsChecked)
                    {
                        if (i == 0)
                        {
                            self_col = Acc.self_collect_list[i].Value;
                        }
                        else
                        {
                            self_col = self_col + "," + Acc.self_collect_list[i].Value;
                        }
                    }
                }
                Acc.Self_Collect = self_col;
                if (file != null)
                {
                    file.SaveAs(Server.MapPath("~/Images/Account_" + Acc.AccountId + Path.GetExtension(file.FileName)));
                    Acc.LogoImage = "Account_" + Acc.AccountId + Path.GetExtension(file.FileName);
                    sqlCommand.Parameters.AddWithValue("@LogoImage", (object)Acc.LogoImage);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@LogoImage", (object)string.Empty);
                }
                if (Acc.Category != null)
                    sqlCommand.Parameters.AddWithValue("@Category", ConvertStringArrayToString(Acc.Category));
                else
                    sqlCommand.Parameters.AddWithValue("@Category", string.Empty);
                if (Acc.Isactive != 0)
                    sqlCommand.Parameters.AddWithValue("@Isactive", Convert.ToInt32(Acc.Isactive));
                else
                    sqlCommand.Parameters.AddWithValue("@Isactive", Convert.ToInt32(Acc.Isactive));

                if (!string.IsNullOrEmpty(Acc.AccountId))
                    sqlCommand.Parameters.AddWithValue("@AccountId", (object)Acc.AccountId);
                else
                    sqlCommand.Parameters.AddWithValue("@AccountId", (object)string.Empty);
                if (!string.IsNullOrEmpty(Acc.Short_Name))
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)Acc.Short_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)string.Empty);
                sqlCommand.Parameters.AddWithValue("@address1", Acc.Address);
                if (Acc.Address2 == null)
                    sqlCommand.Parameters.AddWithValue("@address2", string.Empty);

                else
                {
                    sqlCommand.Parameters.AddWithValue("@address2", Acc.Address2);
                }
                sqlCommand.Parameters.AddWithValue("@city", Acc.City);
                sqlCommand.Parameters.AddWithValue("@state", Acc.State);
                sqlCommand.Parameters.AddWithValue("@zip", Acc.Zip);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", User.Identity.Name);
                sqlCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", User.Identity.Name);
                sqlCommand.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                if (!string.IsNullOrEmpty(Acc.FirstName))
                    sqlCommand.Parameters.AddWithValue("@FirstName", Acc.FirstName);
                else
                    sqlCommand.Parameters.AddWithValue("@FirstName", string.Empty);
                if (!string.IsNullOrEmpty(Acc.LastName))
                    sqlCommand.Parameters.AddWithValue("@LastName", Acc.LastName);
                else
                    sqlCommand.Parameters.AddWithValue("@LastName", string.Empty);
                if (!string.IsNullOrEmpty(Acc.email))
                    sqlCommand.Parameters.AddWithValue("@email", Acc.email);
                else
                    sqlCommand.Parameters.AddWithValue("@email", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Phone))
                    sqlCommand.Parameters.AddWithValue("@Phone", Acc.Phone);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone", string.Empty);
                if (!string.IsNullOrEmpty(Acc.TPA_Client))
                    sqlCommand.Parameters.AddWithValue("@TPA_Client", Acc.TPA_Client);
                else
                    sqlCommand.Parameters.AddWithValue("@TPA_Client", string.Empty);

                if (!string.IsNullOrEmpty(Acc.Location))
                    sqlCommand.Parameters.AddWithValue("@Location", Acc.Location);
                else
                    sqlCommand.Parameters.AddWithValue("@Location", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Notes_1))
                    sqlCommand.Parameters.AddWithValue("@Notes_1", Acc.Notes_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes_1", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Category_1))
                    sqlCommand.Parameters.AddWithValue("@Category_1", Acc.Category_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Category_1", string.Empty);
                if (!string.IsNullOrEmpty(Acc.TPA_Category))
                    sqlCommand.Parameters.AddWithValue("@TPA_Category", Acc.TPA_Category);
                else
                    sqlCommand.Parameters.AddWithValue("@TPA_Category", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Address_type))
                    sqlCommand.Parameters.AddWithValue("@Address_type", Acc.Address_type);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_type", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Address_Notes))
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", Acc.Address_Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", string.Empty);
                if (Acc.Title1 != null)
                    sqlCommand.Parameters.AddWithValue("@Title", ConvertStringArrayToString(Acc.Title1));
                else
                    sqlCommand.Parameters.AddWithValue("@Title", 0);
                if (Acc.function_1!=null)
                    sqlCommand.Parameters.AddWithValue("@function_1", ConvertStringArrayToString(Acc.function_1));
                else
                    sqlCommand.Parameters.AddWithValue("@function_1", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Phone_Number_type))
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_type", Acc.Phone_Number_type);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_type", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Fax))
                    sqlCommand.Parameters.AddWithValue("@Fax", Acc.Fax);
                else
                    sqlCommand.Parameters.AddWithValue("@Fax", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Notes))
                    sqlCommand.Parameters.AddWithValue("@Notes", Acc.Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
                if (Acc.POCT_Testing == "1")

                {
                    sqlCommand.Parameters.AddWithValue("@POCT_Testing", Acc.POCT_Testing);
                    if (!string.IsNullOrEmpty(Acc.POCT_Testing_Details))
                    {

                        sqlCommand.Parameters.AddWithValue("@POCT_Testing_Details", Acc.POCT_Testing_Details);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@POCT_Testing_Details", string.Empty);
                    }
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@POCT_Testing", "0");
                    sqlCommand.Parameters.AddWithValue("@POCT_Testing_Details", string.Empty);
                }
                if (Acc.Self_Collect.Contains("1"))
                {
                    sqlCommand.Parameters.AddWithValue("@Self_Collect", Acc.Self_Collect);
                    if (!string.IsNullOrEmpty(Acc.Self_Collect_Details))
                    {
                        sqlCommand.Parameters.AddWithValue("@Self_Collect_Details", Acc.Self_Collect_Details);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Self_Collect_Details", string.Empty);
                    }
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Self_Collect", Acc.Self_Collect);
                    sqlCommand.Parameters.AddWithValue("@Self_Collect_Details", string.Empty);
                }

                if (Acc.Clearing_House == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Clearing_House", Acc.Clearing_House);

                    if (!string.IsNullOrEmpty(Acc.Clearing_House_Details))
                        sqlCommand.Parameters.AddWithValue("@Clearing_House_Details", Acc.Clearing_House_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Clearing_House_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Clearing_House", "0");
                    sqlCommand.Parameters.AddWithValue("@Clearing_House_Details", string.Empty);
                }
                if (Acc.Compliance_Support == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Compliance_Support", Acc.Compliance_Support);

                    if (!string.IsNullOrEmpty(Acc.Compliance_Support_Details))
                        sqlCommand.Parameters.AddWithValue("@Compliance_Support_Details", Acc.Compliance_Support_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Compliance_Support_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Compliance_Support", "0");
                    sqlCommand.Parameters.AddWithValue("@Compliance_Support_Details", string.Empty);
                }

                if (Acc.Background_Checks == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Background_Checks", Acc.Background_Checks);

                    if (!string.IsNullOrEmpty(Acc.Background_Checks_Details))
                        sqlCommand.Parameters.AddWithValue("@Background_Checks_Details", Acc.Background_Checks_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Background_Checks_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Background_Checks", "0");
                    sqlCommand.Parameters.AddWithValue("@Background_Checks_Details", string.Empty);
                }


                if (!string.IsNullOrEmpty(Acc.Full_time))
                    sqlCommand.Parameters.AddWithValue("@Full_time", Acc.Full_time);
                else
                    sqlCommand.Parameters.AddWithValue("@Full_time", string.Empty);
                if (Acc.Pool == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Pool", Acc.Pool);

                    if (!string.IsNullOrEmpty(Acc.Pool_Details))
                        sqlCommand.Parameters.AddWithValue("@Pool_Details", Acc.Pool_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Pool_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Pool", "0");
                    sqlCommand.Parameters.AddWithValue("@Pool_Details", string.Empty);
                }


                if (Acc.Reporting!=null)
                    sqlCommand.Parameters.AddWithValue("@Reporting", ConvertStringArrayToString(Acc.Reporting));
                else
                    sqlCommand.Parameters.AddWithValue("@Reporting", string.Empty);
                if (Acc.Billing_Details!=null)
                    sqlCommand.Parameters.AddWithValue("@Billing_Details", ConvertStringArrayToString(Acc.Billing_Details));
                else
                    sqlCommand.Parameters.AddWithValue("@Billing_Details",string.Empty);

                if (Acc.Credit_Card_Details != "0")
                {
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", Acc.Credit_Card_Details);

                    if (!string.IsNullOrEmpty(Acc.Card_Details))
                        sqlCommand.Parameters.AddWithValue("@Card_Details", Acc.Card_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Card_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", "0");
                    sqlCommand.Parameters.AddWithValue("@Card_Details", string.Empty);
                }

                if (!string.IsNullOrEmpty(Acc.Notes_On_Billing))
                    sqlCommand.Parameters.AddWithValue("@Notes_On_Billing", Acc.Notes_On_Billing);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes_On_Billing", string.Empty);
                if (!string.IsNullOrEmpty(Acc.Related_To))
                    sqlCommand.Parameters.AddWithValue("@Related_To", Acc.Related_To);
                else
                    sqlCommand.Parameters.AddWithValue("@Related_To", string.Empty);
                if (!string.IsNullOrEmpty(Acc.add_email))
                    sqlCommand.Parameters.AddWithValue("@add_email", Acc.add_email);
                else
                    sqlCommand.Parameters.AddWithValue("@add_email", string.Empty);


                connection.Open();
                sqlCommand.ExecuteNonQuery();

                //int AccountId_PK = (int)sqlCommand.ExecuteScalar();
                connection.Close();
                SqlCommand command = new SqlCommand("proc_new_demo_location", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Location_Name", Acc.AccountId);

                if (!string.IsNullOrEmpty(Acc.Address))
                {
                    command.Parameters.AddWithValue("@Address_1", Acc.Address);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address_1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Acc.Address2))
                {
                    command.Parameters.AddWithValue("@Address_2", Acc.Address2);
                }
                else
                {
                    command.Parameters.AddWithValue("@Address_2", string.Empty);
                }
                command.Parameters.AddWithValue("@City", Acc.City);
                command.Parameters.AddWithValue("@State", Acc.State);
                command.Parameters.AddWithValue("@Country", "USA");
                command.Parameters.AddWithValue("@Website", string.Empty);
                
                if (!string.IsNullOrEmpty(Acc.Location))
                {
                    command.Parameters.AddWithValue("@Location", Acc.Location);
                }
                else
                {
                    command.Parameters.AddWithValue("@Location", string.Empty);
                }
                if (!string.IsNullOrEmpty(Acc.Notes))
                {
                    command.Parameters.AddWithValue("@Notes", Acc.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", string.Empty);
                }
                if (!string.IsNullOrEmpty(Acc.Phone))
                {
                    command.Parameters.AddWithValue("@Phone_number", Acc.Phone);
                }
                else
                {
                    command.Parameters.AddWithValue("@Phone_number", string.Empty);
                }
                if (!string.IsNullOrEmpty(Acc.email))
                {
                    command.Parameters.AddWithValue("@email", Acc.email);
                }
                else
                {
                    command.Parameters.AddWithValue("@email", string.Empty);
                }
                command.Parameters.AddWithValue("@zip", Acc.Zip);
                command.Parameters.AddWithValue("@company_id", Acc.AccountId);
                command.Parameters.AddWithValue("@address_Type", "HEAD OFFICE");
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                if (User.IsInRole("Admin"))
                {
                    string userid = User.Identity.GetUserId();
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager
                                       .ConnectionStrings["TransCanadaConnection"].ConnectionString;
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "proc_update_Accounts";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = conn;
                            conn.Open();

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@userid", userid);
                            cmd.Parameters.AddWithValue("@AccountId", Acc.AccountId);
                            cmd.Parameters.AddWithValue("@user_account_status", 1);
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
                //if (Acc.Category == 1)
                //{

                //    SqlCommand sqlCommand1 = new SqlCommand("Insert into tbl_client_subcategory (Client_Id, Isactive, Subcategory) values(@Client_Id, @Isactive, @Subcategory)", connection);

                //    for (int i = 0; i < Acc.CheckBoxes.Count; i++)
                //    {
                //        connection.Open();

                //        if (Acc.CheckBoxes[i].Selected == true)
                //        {
                //            sqlCommand1.Parameters.Clear();
                //            sqlCommand1.Parameters.AddWithValue("@Subcategory", Acc.CheckBoxes[i].Text);
                //            sqlCommand1.Parameters.AddWithValue("@Isactive", 1);
                //            sqlCommand1.Parameters.AddWithValue("@Client_Id", AccountId_PK);

                //        }
                //        else
                //        {
                //            sqlCommand1.Parameters.Clear();
                //            sqlCommand1.Parameters.AddWithValue("@Subcategory", Acc.CheckBoxes[i].Text);
                //            sqlCommand1.Parameters.AddWithValue("@Isactive", 0);
                //            sqlCommand1.Parameters.AddWithValue("@Client_Id", AccountId_PK);
                //        }

                //        sqlCommand1.ExecuteNonQuery();
                //        connection.Close();

                //    }
                //}

                return (ActionResult)this.RedirectToAction("AccountsList", "Asp_Accounts");
            }


            catch (Exception ex)
            {
                return (ActionResult)this.RedirectToAction(ex.Message, "Errorpage", (object)"Asp_Accounts");
            }

        }

        [HttpGet]
        [BreadCrumb(Label = "Update Client")]
        public ActionResult UpdateAccounts(string id)
        {
            SqlCommand selectCommand = new SqlCommand("Select Related_To,TPA_Client,Category, Isactive,AccountId, LogoImage, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate,Short_Name,address1,address2,city,state,zip,FirstName,LastName,email,Phone,Location,Notes_1,Category_1,TPA_Category,Address_type,Address_Notes,Title,function_1,Phone_Number_type,Fax,Notes,POCT_Testing,Self_Collect,Clearing_House,Compliance_Support,Full_time,Background_Checks,Pool,Reporting,Billing_Details,Credit_Card_Details,Notes_On_Billing,POCT_Testing_Details,Self_Collect_Details,Clearing_House_Details,Compliance_Support_Details,Background_Checks_Details,Pool_Details,Card_Details,add_email from AspNetAccounts where AccountId_PK=@AccountId_PK", new SqlConnection(this.TransConnString));
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@AccountId_PK", (object)id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            Session["id"] = id;
            netAccountsModel.self_collect_list = ListSelfCollect();
            
            //List<SelectListItem> items = new List<SelectListItem>();
            //string constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    string query = "Proc_Update_category";
            //    using (SqlCommand cmd = new SqlCommand(query))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@client_id", id);
            //        cmd.Connection = con;
            //        con.Open();
            //        using (SqlDataReader sdr = cmd.ExecuteReader())
            //        {
            //            while (sdr.Read())
            //            {
            //                items.Add(new SelectListItem
            //                {
            //                    Text = sdr["Name"].ToString(),
            //                    Value = sdr["Name"].ToString(),
            //                    Selected = Convert.ToBoolean(sdr["user_account_status"])

            //                });
            //            }
            //        }
            //        con.Close();
            //    }
            //}
            //netAccountsModel.CheckBoxes = items;
            if (dataTable.Rows.Count > 0)
            {
                netAccountsModel.AccountId = dataTable.Rows[0]["AccountId"].ToString();
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["LogoImage"].ToString()))
                {
                    netAccountsModel.LogoImage = "../../../Images/" + dataTable.Rows[0]["LogoImage"].ToString();
                }
                else
                {
                    netAccountsModel.LogoImage = "../../../Images/No_Logo.png";
                }
                netAccountsModel.Logoimage_src = dataTable.Rows[0]["LogoImage"].ToString();
                //netAccountsModel.CreatedBy = dataTable.Rows[0]["CreatedBy"].ToString();
                //netAccountsModel.CreatedDate = Convert.ToDateTime(dataTable.Rows[0]["CreatedDate"].ToString());
                //netAccountsModel.UpdatedBy = dataTable.Rows[0]["UpdatedBy"].ToString();
                //netAccountsModel.UpdatedDate = Convert.ToDateTime(dataTable.Rows[0]["UpdatedDate"].ToString());
                netAccountsModel.Short_Name = dataTable.Rows[0]["Short_Name"].ToString();
                netAccountsModel.AccountId_PK = Convert.ToInt32(id);
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["address1"].ToString()))
                {
                    netAccountsModel.Address = dataTable.Rows[0]["address1"].ToString();
                }
                else
                {
                    netAccountsModel.Address = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["address2"].ToString()))
                {
                    netAccountsModel.Address2 = dataTable.Rows[0]["address2"].ToString();
                }
                else
                {
                    netAccountsModel.Address2 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["city"].ToString()))
                {
                    netAccountsModel.City = dataTable.Rows[0]["city"].ToString();
                }
                else
                {
                    netAccountsModel.City = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["state"].ToString()))
                {
                    netAccountsModel.State = dataTable.Rows[0]["state"].ToString();
                }
                else
                {
                    netAccountsModel.State = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["zip"].ToString()))
                {
                    netAccountsModel.Zip = dataTable.Rows[0]["zip"].ToString();
                }
                else
                {
                    netAccountsModel.Zip = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Category"].ToString()))
                {
                    netAccountsModel.Category = dataTable.Rows[0]["Category"].ToString().Split(',');
                }
                else
                {
                    netAccountsModel.Category = dataTable.Rows[0]["Category"].ToString().Split(',');
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Isactive"].ToString()))
                {
                    netAccountsModel.Isactive = Convert.ToInt32(dataTable.Rows[0]["Isactive"].ToString());
                }
                else
                {
                    netAccountsModel.Isactive = 0;
                }

                if (!string.IsNullOrEmpty(dataTable.Rows[0]["FirstName"].ToString()))
                {
                    netAccountsModel.FirstName = dataTable.Rows[0]["FirstName"].ToString();
                }
                else
                {
                    netAccountsModel.FirstName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["LastName"].ToString()))
                {
                    netAccountsModel.LastName = dataTable.Rows[0]["LastName"].ToString();
                }
                else
                {
                    netAccountsModel.LastName = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["email"].ToString()))
                {
                    netAccountsModel.email = dataTable.Rows[0]["email"].ToString();
                }
                else
                {
                    netAccountsModel.email = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Phone"].ToString()))
                {
                    netAccountsModel.Phone = dataTable.Rows[0]["Phone"].ToString();
                }
                else
                {
                    netAccountsModel.Phone = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["TPA_Client"].ToString()))
                {
                    netAccountsModel.TPA_Client = dataTable.Rows[0]["TPA_Client"].ToString();
                }
                else
                {
                    netAccountsModel.TPA_Client = string.Empty;
                }

                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Location"].ToString()))
                {
                    netAccountsModel.Location = dataTable.Rows[0]["Location"].ToString();
                }
                else
                {
                    netAccountsModel.Location = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Notes_1"].ToString()))
                {
                    netAccountsModel.Notes_1 = dataTable.Rows[0]["Notes_1"].ToString();
                }
                else
                {
                    netAccountsModel.Notes_1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Category_1"].ToString()))
                {
                    netAccountsModel.Category_1 = dataTable.Rows[0]["Category_1"].ToString();
                }
                else
                {
                    netAccountsModel.Category_1 = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["TPA_Category"].ToString()))
                {
                    netAccountsModel.TPA_Category = dataTable.Rows[0]["TPA_Category"].ToString();
                }
                else
                {
                    netAccountsModel.TPA_Category = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Address_type"].ToString()))
                {
                    netAccountsModel.Address_type = dataTable.Rows[0]["Address_type"].ToString();
                }
                else
                {
                    netAccountsModel.Address_type = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["add_email"].ToString()))
                {
                    netAccountsModel.add_email = dataTable.Rows[0]["add_email"].ToString();
                }
                else
                {
                    netAccountsModel.add_email = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Address_Notes"].ToString()))
                {
                    netAccountsModel.Address_Notes = dataTable.Rows[0]["Address_Notes"].ToString();
                }
                else
                {
                    netAccountsModel.Address_Notes = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Title"].ToString()))
                {
                    netAccountsModel.Title1 = dataTable.Rows[0]["Title"].ToString().Split(',');
                }
                else
                {
                    netAccountsModel.Title1 = dataTable.Rows[0]["Title"].ToString().Split(',');
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["function_1"].ToString()))
                {
                    netAccountsModel.function_1 = dataTable.Rows[0]["function_1"].ToString().Split(',');
                }
                else
                {
                    netAccountsModel.function_1 = dataTable.Rows[0]["function_1"].ToString().Split(',');
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Phone_Number_type"].ToString()))
                {
                    netAccountsModel.Phone_Number_type = dataTable.Rows[0]["Phone_Number_type"].ToString();
                }
                else
                {
                    netAccountsModel.Phone_Number_type = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Fax"].ToString()))
                {
                    netAccountsModel.Fax = dataTable.Rows[0]["Fax"].ToString();
                }
                else
                {
                    netAccountsModel.Fax = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Notes"].ToString()))
                {
                    netAccountsModel.Notes = dataTable.Rows[0]["Notes"].ToString();
                }
                else
                {
                    netAccountsModel.Notes = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["POCT_Testing"].ToString()))
                {
                    netAccountsModel.POCT_Testing = dataTable.Rows[0]["POCT_Testing"].ToString();
                }
                else
                {
                    netAccountsModel.POCT_Testing = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Self_Collect"].ToString()))
                {
                    netAccountsModel.Self_Collect = dataTable.Rows[0]["Self_Collect"].ToString();
                    string[] vs = netAccountsModel.Self_Collect.ToString().Split(',');
                    foreach (string val in vs)
                    {
                        for (int j = 0; j < netAccountsModel.self_collect_list.Count; j++)
                        {
                            if (val == netAccountsModel.self_collect_list[j].Value)
                            {
                                netAccountsModel.self_collect_list[j].IsChecked = true;
                            }
                        }
                    }
                }
                else
                {
                    netAccountsModel.Self_Collect = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Clearing_House"].ToString()))
                {
                    netAccountsModel.Clearing_House = dataTable.Rows[0]["Clearing_House"].ToString();
                }
                else
                {
                    netAccountsModel.Clearing_House = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Compliance_Support"].ToString()))
                {
                    netAccountsModel.Compliance_Support = dataTable.Rows[0]["Compliance_Support"].ToString();
                }
                else
                {
                    netAccountsModel.Compliance_Support = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Full_time"].ToString()))
                {
                    netAccountsModel.Full_time = dataTable.Rows[0]["Full_time"].ToString();
                }
                else
                {
                    netAccountsModel.Full_time = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Background_Checks"].ToString()))
                {
                    netAccountsModel.Background_Checks = dataTable.Rows[0]["Background_Checks"].ToString();
                }
                else
                {
                    netAccountsModel.Background_Checks = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Pool"].ToString()))
                {
                    netAccountsModel.Pool = dataTable.Rows[0]["Pool"].ToString();
                }
                else
                {
                    netAccountsModel.Pool = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Reporting"].ToString()))
                {
                    netAccountsModel.Reporting = dataTable.Rows[0]["Reporting"].ToString().Split(',');
                }
                else
                {
                    netAccountsModel.Reporting = dataTable.Rows[0]["Reporting"].ToString().Split(',');
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Billing_Details"].ToString()))
                {
                    netAccountsModel.Billing_Details = dataTable.Rows[0]["Billing_Details"].ToString().Split(',');
                }
                else
                {
                    netAccountsModel.Billing_Details = string.Empty.Split(',');
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Credit_Card_Details"].ToString()))
                {
                    netAccountsModel.Credit_Card_Details = dataTable.Rows[0]["Credit_Card_Details"].ToString();
                }
                else
                {
                    netAccountsModel.Credit_Card_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Notes_On_Billing"].ToString()))
                {
                    netAccountsModel.Notes_On_Billing = dataTable.Rows[0]["Notes_On_Billing"].ToString();
                }
                else
                {
                    netAccountsModel.Notes_On_Billing = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["POCT_Testing_Details"].ToString()))
                {
                    netAccountsModel.POCT_Testing_Details = dataTable.Rows[0]["POCT_Testing_Details"].ToString();
                }
                else
                {
                    netAccountsModel.POCT_Testing_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Self_Collect_Details"].ToString()))
                {
                    netAccountsModel.Self_Collect_Details = dataTable.Rows[0]["Self_Collect_Details"].ToString();
                    
                }
                else
                {
                    netAccountsModel.Self_Collect_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Clearing_House_Details"].ToString()))
                {
                    netAccountsModel.Clearing_House_Details = dataTable.Rows[0]["Clearing_House_Details"].ToString();
                }
                else
                {
                    netAccountsModel.Clearing_House_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Compliance_Support_Details"].ToString()))
                {
                    netAccountsModel.Compliance_Support_Details = dataTable.Rows[0]["Compliance_Support_Details"].ToString();
                }
                else
                {
                    netAccountsModel.Compliance_Support_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["TPA_Client"].ToString()))
                {
                    netAccountsModel.TPA_Client = dataTable.Rows[0]["TPA_Client"].ToString();
                }
                else
                {
                    netAccountsModel.TPA_Client = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Background_Checks_Details"].ToString()))
                {
                    netAccountsModel.Background_Checks_Details = dataTable.Rows[0]["Background_Checks_Details"].ToString();
                }
                else
                {
                    netAccountsModel.Background_Checks_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Pool_Details"].ToString()))
                {
                    netAccountsModel.Pool_Details = dataTable.Rows[0]["Pool_Details"].ToString();
                }
                else
                {
                    netAccountsModel.Pool_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Card_Details"].ToString()))
                {
                    netAccountsModel.Card_Details = dataTable.Rows[0]["Card_Details"].ToString();
                }
                else
                {
                    netAccountsModel.Card_Details = string.Empty;
                }
                if (!string.IsNullOrEmpty(dataTable.Rows[0]["Related_To"].ToString()))
                {
                    netAccountsModel.Related_To = dataTable.Rows[0]["Related_To"].ToString();
                }
                else
                {
                    netAccountsModel.Related_To = string.Empty;
                }

            }
            return View(netAccountsModel);
        }

        [HttpPost]
        public ActionResult UpdateAccounts(AspNetAccountsModel AccM, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View((object)AccM);
            try
            {
                string self_col = string.Empty;
                for (int i = 0; i < AccM.self_collect_list.Count; i++)
                {
                    if (AccM.self_collect_list[i].IsChecked)
                    {
                        if (i == 0)
                        {
                            self_col = AccM.self_collect_list[i].Value;
                        }
                        else
                        {
                            self_col = self_col + "," + AccM.self_collect_list[i].Value;
                        }
                    }
                }
                AccM.Self_Collect = self_col;
                if (AccM.Address == null)
                    AccM.Address = string.Empty;
                if (AccM.Zip == null)
                    AccM.Zip = string.Empty;
                if (AccM.City == null)
                    AccM.City = string.Empty;
                if (AccM.State == null)
                    AccM.State = string.Empty;
                SqlConnection connection = new SqlConnection(this.TransConnString);
                SqlCommand sqlCommand = new SqlCommand("update  AspNetAccounts set TPA_Client=@TPA_Client,Category=@Category, Isactive=@Isactive,AccountId=@AccountId, LogoImage=@LogoImage, UpdatedBy=@UpdatedBy, UpdatedDate=@UpdatedDate,Short_Name=@Short_Name,address1=@address1,address2=@address2,city=@city,state=@state,zip=@zip,FirstName=@FirstName, LastName=@LastName, email=@email, Phone=@Phone,Location=@Location,Notes_1=@Notes_1,Category_1=@Category_1,TPA_Category=@TPA_Category,Address_type=@Address_type,Address_Notes=@Address_Notes,Title=@Title,function_1=@function_1,Phone_Number_type=@Phone_Number_type,Fax=@Fax,Notes=@Notes,POCT_Testing=@POCT_Testing,Self_Collect=@Self_Collect,Clearing_House=@Clearing_House,Compliance_Support=@Compliance_Support,Full_time=@Full_time,Background_Checks=@Background_Checks,Pool=@Pool,Reporting=@Reporting,Billing_Details=@Billing_Details,Credit_Card_Details=@Credit_Card_Details,Notes_On_Billing=@Notes_On_Billing,POCT_Testing_Details=@POCT_Testing_Details,Self_Collect_Details=@Self_Collect_Details,Clearing_House_Details=@Clearing_House_Details,Compliance_Support_Details=@Compliance_Support_Details,Background_Checks_Details=@Background_Checks_Details,Pool_Details=@Pool_Details,Card_Details=@Card_Details,Related_To=@Related_To,add_email=@add_email where AccountId_PK=@AccountId_PK", connection);
                string accountId = AccM.AccountId;
                if (file != null)
                {
                    file.SaveAs(this.Server.MapPath("~/Images/Account_" + accountId + Path.GetExtension(file.FileName)));
                    AccM.LogoImage = "Account_" + accountId + Path.GetExtension(file.FileName);
                    sqlCommand.Parameters.AddWithValue("@LogoImage", (object)AccM.LogoImage);
                }
                else
                    sqlCommand.Parameters.AddWithValue("@LogoImage", AccM.LogoImage);
                sqlCommand.Parameters.AddWithValue("@AccountId_PK", (object)AccM.AccountId_PK);
                if (!string.IsNullOrEmpty(AccM.add_email))
                    sqlCommand.Parameters.AddWithValue("@add_email", (object)AccM.add_email);
                else
                    sqlCommand.Parameters.AddWithValue("@add_email", (object)string.Empty);
                if (!string.IsNullOrEmpty(AccM.AccountId))
                    sqlCommand.Parameters.AddWithValue("@AccountId", (object)AccM.AccountId);
                else
                    sqlCommand.Parameters.AddWithValue("@AccountId", (object)string.Empty);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", (object)IdentityExtensions.GetUserName(this.User.Identity));
                sqlCommand.Parameters.AddWithValue("@UpdatedDate", (object)Convert.ToDateTime(DateTime.Now));
                if (!string.IsNullOrEmpty(AccM.Short_Name))
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)AccM.Short_Name);
                else
                    sqlCommand.Parameters.AddWithValue("@Short_Name", (object)string.Empty);
                sqlCommand.Parameters.AddWithValue("@address1", AccM.Address);
                if (AccM.Address2 == null)
                    sqlCommand.Parameters.AddWithValue("@address2", string.Empty);
                else
                    sqlCommand.Parameters.AddWithValue("@address2", AccM.Address2);
                sqlCommand.Parameters.AddWithValue("@city", AccM.City);
                sqlCommand.Parameters.AddWithValue("@state", AccM.State);
                sqlCommand.Parameters.AddWithValue("@zip", AccM.Zip);
                if (AccM.Category !=null)
                {
                    sqlCommand.Parameters.AddWithValue("@Category", ConvertStringArrayToString(AccM.Category));
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Category", 0);
                }
                if (AccM.Isactive != 0)
                {
                    sqlCommand.Parameters.AddWithValue("@Isactive", AccM.Isactive);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Isactive", 0);
                }

                if (!string.IsNullOrEmpty(AccM.FirstName))
                    sqlCommand.Parameters.AddWithValue("@FirstName", AccM.FirstName);
                else
                    sqlCommand.Parameters.AddWithValue("@FirstName", string.Empty);
                if (!string.IsNullOrEmpty(AccM.LastName))
                    sqlCommand.Parameters.AddWithValue("@LastName", AccM.LastName);
                else
                    sqlCommand.Parameters.AddWithValue("@LastName", string.Empty);
                if (!string.IsNullOrEmpty(AccM.email))
                    sqlCommand.Parameters.AddWithValue("@email", AccM.email);
                else
                    sqlCommand.Parameters.AddWithValue("@email", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Phone))
                    sqlCommand.Parameters.AddWithValue("@Phone", AccM.Phone);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone", string.Empty);

                if (!string.IsNullOrEmpty(AccM.TPA_Client))
                    sqlCommand.Parameters.AddWithValue("@TPA_Client", AccM.TPA_Client);
                else
                    sqlCommand.Parameters.AddWithValue("@TPA_Client", string.Empty);

                if (!string.IsNullOrEmpty(AccM.Location))
                    sqlCommand.Parameters.AddWithValue("@Location", AccM.Location);
                else
                    sqlCommand.Parameters.AddWithValue("@Location", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Notes_1))
                    sqlCommand.Parameters.AddWithValue("@Notes_1", AccM.Notes_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes_1", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Category_1))
                    sqlCommand.Parameters.AddWithValue("@Category_1", AccM.Category_1);
                else
                    sqlCommand.Parameters.AddWithValue("@Category_1", string.Empty);
                if (!string.IsNullOrEmpty(AccM.TPA_Category))
                    sqlCommand.Parameters.AddWithValue("@TPA_Category", AccM.TPA_Category);
                else
                    sqlCommand.Parameters.AddWithValue("@TPA_Category", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Address_type))
                    sqlCommand.Parameters.AddWithValue("@Address_type", AccM.Address_type);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_type", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Address_Notes))
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", AccM.Address_Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Address_Notes", string.Empty);
                if (AccM.Title1 != null)
                    sqlCommand.Parameters.AddWithValue("@Title", ConvertStringArrayToString(AccM.Title1));
                else
                    sqlCommand.Parameters.AddWithValue("@Title", 0);
                if (AccM.function_1 != null)
                    sqlCommand.Parameters.AddWithValue("@function_1", ConvertStringArrayToString(AccM.function_1));
                else
                    sqlCommand.Parameters.AddWithValue("@function_1", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Phone_Number_type))
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_type", AccM.Phone_Number_type);
                else
                    sqlCommand.Parameters.AddWithValue("@Phone_Number_type", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Fax))
                    sqlCommand.Parameters.AddWithValue("@Fax", AccM.Fax);
                else
                    sqlCommand.Parameters.AddWithValue("@Fax", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Notes))
                    sqlCommand.Parameters.AddWithValue("@Notes", AccM.Notes);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes", string.Empty);
                if (AccM.POCT_Testing == "1")

                {
                    sqlCommand.Parameters.AddWithValue("@POCT_Testing", AccM.POCT_Testing);
                    if (!string.IsNullOrEmpty(AccM.POCT_Testing_Details))
                    {

                        sqlCommand.Parameters.AddWithValue("@POCT_Testing_Details", AccM.POCT_Testing_Details);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@POCT_Testing_Details", string.Empty);
                    }
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@POCT_Testing", "0");
                    sqlCommand.Parameters.AddWithValue("@POCT_Testing_Details", string.Empty);
                }
                if (AccM.Self_Collect.Contains("1"))
                {
                    sqlCommand.Parameters.AddWithValue("@Self_Collect", AccM.Self_Collect);
                    if (!string.IsNullOrEmpty(AccM.Self_Collect_Details))
                    {
                        sqlCommand.Parameters.AddWithValue("@Self_Collect_Details", AccM.Self_Collect_Details);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@Self_Collect_Details", string.Empty);
                    }
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Self_Collect", AccM.Self_Collect);
                    sqlCommand.Parameters.AddWithValue("@Self_Collect_Details", string.Empty);
                }

                if (AccM.Clearing_House == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Clearing_House", AccM.Clearing_House);

                    if (!string.IsNullOrEmpty(AccM.Clearing_House_Details))
                        sqlCommand.Parameters.AddWithValue("@Clearing_House_Details", AccM.Clearing_House_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Clearing_House_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Clearing_House", "0");
                    sqlCommand.Parameters.AddWithValue("@Clearing_House_Details", string.Empty);
                }
                if (AccM.Compliance_Support == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Compliance_Support", AccM.Compliance_Support);

                    if (!string.IsNullOrEmpty(AccM.Compliance_Support_Details))
                        sqlCommand.Parameters.AddWithValue("@Compliance_Support_Details", AccM.Compliance_Support_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Compliance_Support_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Compliance_Support", "0");
                    sqlCommand.Parameters.AddWithValue("@Compliance_Support_Details", string.Empty);
                }

                if (AccM.Background_Checks == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Background_Checks", AccM.Background_Checks);

                    if (!string.IsNullOrEmpty(AccM.Background_Checks_Details))
                        sqlCommand.Parameters.AddWithValue("@Background_Checks_Details", AccM.Background_Checks_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Background_Checks_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Background_Checks", "0");
                    sqlCommand.Parameters.AddWithValue("@Background_Checks_Details", string.Empty);
                }


                if (!string.IsNullOrEmpty(AccM.Full_time))
                    sqlCommand.Parameters.AddWithValue("@Full_time", AccM.Full_time);
                else
                    sqlCommand.Parameters.AddWithValue("@Full_time", string.Empty);
                if (AccM.Pool == "1")
                {
                    sqlCommand.Parameters.AddWithValue("@Pool", AccM.Pool);

                    if (!string.IsNullOrEmpty(AccM.Pool_Details))
                        sqlCommand.Parameters.AddWithValue("@Pool_Details", AccM.Pool_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Pool_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Pool", "0");
                    sqlCommand.Parameters.AddWithValue("@Pool_Details", string.Empty);
                }


                if (AccM.Reporting!=null)
                    sqlCommand.Parameters.AddWithValue("@Reporting", ConvertStringArrayToString(AccM.Reporting));
                else
                    sqlCommand.Parameters.AddWithValue("@Reporting", string.Empty);
                if (AccM.Billing_Details!=null)
                    sqlCommand.Parameters.AddWithValue("@Billing_Details", ConvertStringArrayToString(AccM.Billing_Details));
                else
                    sqlCommand.Parameters.AddWithValue("@Billing_Details", string.Empty);

                if (AccM.Credit_Card_Details.ToString() != "0")
                {
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", AccM.Credit_Card_Details);

                    if (!string.IsNullOrEmpty(AccM.Card_Details))
                        sqlCommand.Parameters.AddWithValue("@Card_Details", AccM.Card_Details);
                    else
                        sqlCommand.Parameters.AddWithValue("@Card_Details", string.Empty);
                }

                else
                {
                    sqlCommand.Parameters.AddWithValue("@Credit_Card_Details", "0");
                    sqlCommand.Parameters.AddWithValue("@Card_Details", string.Empty);
                }

                if (!string.IsNullOrEmpty(AccM.Notes_On_Billing))
                    sqlCommand.Parameters.AddWithValue("@Notes_On_Billing", AccM.Notes_On_Billing);
                else
                    sqlCommand.Parameters.AddWithValue("@Notes_On_Billing", string.Empty);
                if (!string.IsNullOrEmpty(AccM.Related_To))
                    sqlCommand.Parameters.AddWithValue("@Related_To", AccM.Related_To);
                else
                    sqlCommand.Parameters.AddWithValue("@Related_To", string.Empty);




                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();

                //if (AccM.Category == 1)
                //{
                //    if (AccM.CheckBoxes != null)
                //    {
                //        SqlCommand sqlCommand1 = new SqlCommand("proc_updateorin_subcategory", connection);
                //        sqlCommand1.CommandType = CommandType.StoredProcedure;

                //        for (int i = 0; i < AccM.CheckBoxes.Count; i++)
                //        {
                //            connection.Open();

                //            if (AccM.CheckBoxes[i].Selected == true)
                //            {
                //                sqlCommand1.Parameters.Clear();
                //                sqlCommand1.Parameters.AddWithValue("@Subcategory", AccM.CheckBoxes[i].Text);
                //                sqlCommand1.Parameters.AddWithValue("@Isactive", 1);
                //                sqlCommand1.Parameters.AddWithValue("@Client_Id", AccM.AccountId_PK);

                //            }
                //            else
                //            {
                //                sqlCommand1.Parameters.Clear();
                //                sqlCommand1.Parameters.AddWithValue("@Subcategory", AccM.CheckBoxes[i].Text);
                //                sqlCommand1.Parameters.AddWithValue("@Isactive", 0);
                //                sqlCommand1.Parameters.AddWithValue("@Client_Id", AccM.AccountId_PK);
                //            }

                //            sqlCommand1.ExecuteNonQuery();
                //            connection.Close();

                //        }
                //    }

                return (ActionResult)this.RedirectToAction("AccountsList", "Asp_Accounts");
            }
            catch (Exception ex)
            {
                return (ActionResult)this.RedirectToAction(ex.Message, "Errorpage");
            }
        }

        public ActionResult DetailsAccounts(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand selectCommand = new SqlCommand("Select * from AspNetAccounts where AccountId=@AccountId", connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
            selectCommand.Parameters.AddWithValue("@AccountId", (object)id);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            AspNetAccountsModel netAccountsModel = new AspNetAccountsModel();
            if (dataTable.Rows.Count > 0)
            {
                netAccountsModel.AccountId = dataTable.Rows[0]["AccountId"].ToString();
                netAccountsModel.LogoImage = dataTable.Rows[0]["LogoImage"].ToString();
                netAccountsModel.CreatedBy = dataTable.Rows[0]["CreatedBy"].ToString();
                netAccountsModel.CreatedDate = Convert.ToDateTime(dataTable.Rows[0]["CreatedDate"].ToString());
                netAccountsModel.UpdatedBy = dataTable.Rows[0]["UpdatedBy"].ToString();
                netAccountsModel.UpdatedDate = Convert.ToDateTime(dataTable.Rows[0]["UpdatedDate"].ToString());
            }
            connection.Close();
            connection.Dispose();
            return (ActionResult)this.View((object)netAccountsModel);
        }

        public ActionResult DeleteAccounts(string id)
        {
            SqlConnection connection = new SqlConnection(this.TransConnString);
            SqlCommand sqlCommand = new SqlCommand("Delete from AspNetAccounts where AccountId_PK=@AccountId_PK", connection);
            sqlCommand.Parameters.AddWithValue("@AccountId_PK", (object)id);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
            return (ActionResult)this.RedirectToAction("AccountsList", "Asp_Accounts");
        }

        public string ConvertStringArrayToString(string[] array)
        {
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(',');
            }
            return builder.ToString();
        }
        public ActionResult Addcontact(AspNetAccountsModel addcnt)
        {
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString);
            SqlCommand command = new SqlCommand("proc_insert_client_contact", con);
            command.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(addcnt.labContact.Email1))
            {
                command.Parameters.AddWithValue("@Email1", addcnt.labContact.Email1);
            }
            else
            {
                command.Parameters.AddWithValue("@Email1", string.Empty);
            }
            if (!string.IsNullOrEmpty(addcnt.labContact.Phone1))
            {
                command.Parameters.AddWithValue("@Phone1", addcnt.labContact.Phone1);
            }
            else
            {
                command.Parameters.AddWithValue("@Phone1", string.Empty);
            }
            if (!string.IsNullOrEmpty(addcnt.labContact.Role))
            {
                command.Parameters.AddWithValue("@Role", addcnt.labContact.Role);
            }
            else
            {
                command.Parameters.AddWithValue("@Role", string.Empty);
            }
            if (!string.IsNullOrEmpty(addcnt.labContact.Title))
            {
                command.Parameters.AddWithValue("@Title", addcnt.labContact.Title);
            }
            else
            {
                command.Parameters.AddWithValue("@Title", string.Empty);
            }
            if (!string.IsNullOrEmpty(addcnt.labContact.Notes))
            {
                command.Parameters.AddWithValue("@Notes", addcnt.labContact.Notes);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", string.Empty);
            }
            command.Parameters.AddWithValue("@location_id", addcnt.labContact.Address_1);
            if (!string.IsNullOrEmpty(addcnt.labContact.firstname))
            {
                command.Parameters.AddWithValue("@firstname", addcnt.labContact.firstname);
            }
            else
            {
                command.Parameters.AddWithValue("@firstname", string.Empty);
            }
            if (!string.IsNullOrEmpty(addcnt.labContact.Lastname))
            {
                command.Parameters.AddWithValue("@Lastname", addcnt.labContact.Lastname);
            }
            else
            {
                command.Parameters.AddWithValue("@Lastname", string.Empty);
            }
            if (!string.IsNullOrEmpty(addcnt.labContact.email))
            {
                command.Parameters.AddWithValue("@email", addcnt.labContact.email);
            }
            else
            {
                command.Parameters.AddWithValue("@email", string.Empty);
            }



            if (addcnt.labContact.officephone != null)
                command.Parameters.AddWithValue("@officephone", addcnt.labContact.officephone);
            else
                command.Parameters.AddWithValue("@officephone", string.Empty);
            if (!string.IsNullOrEmpty(addcnt.labContact.cell))
            {
                command.Parameters.AddWithValue("@cell", addcnt.labContact.cell);
            }
            else
            {
                command.Parameters.AddWithValue("@cell", string.Empty);
            }
            if (!string.IsNullOrEmpty(addcnt.labContact.Third_Phone))
            {
                command.Parameters.AddWithValue("@Third_Phone", addcnt.labContact.Third_Phone);
            }
            else
            {
                command.Parameters.AddWithValue("@Third_Phone", string.Empty);
            }
            //command.Parameters.AddWithValue("@location_name", Session["Client_Name"]);
            //if (!string.IsNullOrEmpty(addcnt.labContact.Address_1))
            //    command.Parameters.AddWithValue("@location_id", addcnt.labContact.Address_1);
            //else
            //    command.Parameters.AddWithValue("@location_id", string.Empty);
            //if (!string.IsNullOrEmpty(addcnt.labContact.firstname))
            //    command.Parameters.AddWithValue("@firstname", addcnt.labContact.firstname);
            //else
            //    command.Parameters.AddWithValue("@firstname", string.Empty);
            //if (!string.IsNullOrEmpty(addcnt.labContact.Lastname))

            //    command.Parameters.AddWithValue("@Lastname", addcnt.labContact.Lastname);
            //else
            //    command.Parameters.AddWithValue("@Lastname", string.Empty);
            //if (!string.IsNullOrEmpty(addcnt.labContact.email))
            //    command.Parameters.AddWithValue("@email", addcnt.labContact.email);
            //else
            //    command.Parameters.AddWithValue("@email", string.Empty);

            
            //if (addcnt.labContact.officephone != null)
            //    command.Parameters.AddWithValue("@officephone", addcnt.labContact.officephone);
            //else
            //    command.Parameters.AddWithValue("@officephone", string.Empty);
            //if (!string.IsNullOrEmpty(addcnt.labContact.cell))
            //{
            //    command.Parameters.AddWithValue("@cell", addcnt.labContact.cell);
            //}
            //else
            //{
            //    command.Parameters.AddWithValue("@cell", string.Empty);
            //}
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            return Redirect(Request.UrlReferrer.ToString());
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
