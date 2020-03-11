using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransCanadaDemo.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNet.Identity;



namespace TransCanadaDemo.Controllers
{
    public class LabsController : Controller
    {
        String constr = ConfigurationManager.ConnectionStrings["TransCanadaConnection"].ConnectionString;

        // GET: Labs
        public ActionResult Index()
        {
            Labs labs = new Labs();

            return View(labs);
        }


        public static List<SelectListItem> ListSampleType()
        {

            List<SelectListItem> ls = new List<SelectListItem>()
            { 
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

        public static List<SelectListItem> ListCCFs()
        {

            List<SelectListItem> ls = new List<SelectListItem>()
            {
            new SelectListItem
            {
                Text = "--Select--",
                Value = "0"
            },
                new SelectListItem
                {
                    Text = " Use FormFox",
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
                    Value = "3"
                },
            };
            return ls;
        }

        public static List<SelectListItem> ListsampleType()
        {

            List<SelectListItem> ls = new List<SelectListItem>()
            {
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

        public static List<SelectListItem> ListCCFS()
        {

            List<SelectListItem> ls = new List<SelectListItem>()
            {
            new SelectListItem
            {
                Text = "--Select--",
                Value = "0"
            },
                new SelectListItem
                {
                    Text = " Use FormFox",
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
                    Value = "3"
                },
            };
            return ls;
        }
        public static List<SelectListItem> Listsample()
        {

            List<SelectListItem> ls = new List<SelectListItem>()
            {
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

        public static List<SelectListItem> ListCCF()
        {

            List<SelectListItem> ls = new List<SelectListItem>()
            {
            new SelectListItem
            {
                Text = "--Select--",
                Value = "0"
            },
                new SelectListItem
                {
                    Text = " Use FormFox",
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
                    Value = "3"
                },
            };
            return ls;
        }


        public ActionResult Lablist()
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("ListLab", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Accounts_id", System.Web.HttpContext.Current.Session["Account_id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<Labs> LabList = new List<Labs>();
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    Labs LB = new Labs();
                    LB.Lab_Id = Convert.ToInt32(dt.Rows[j]["Lab_Id"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[j]["LabsLabNameLabLocation"].ToString()))
                    {
                        LB.LabsLabNameLabLocation = dt.Rows[j]["LabsLabNameLabLocation"].ToString();
                    }
                    else
                    {
                        LB.LabsLabNameLabLocation = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["LabsStreetAddress"].ToString()))
                    {
                        LB.LabsStreetAddress = dt.Rows[j]["LabsStreetAddress"].ToString();
                    }
                    else
                    {
                        LB.LabsStreetAddress = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["ContactCellPhone1"].ToString()))
                    {
                        LB.ContactCellPhone1 = dt.Rows[j]["ContactCellPhone1"].ToString();
                    }
                    else
                    {
                        LB.ContactCellPhone1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[j]["LabsState"].ToString()))
                    {
                        LB.LabsState = dt.Rows[j]["LabsState"].ToString();
                    }
                    else
                    {
                        LB.LabsState = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(dt.Rows[j]["ContactEmail1"].ToString()))
                    {
                        LB.ContactEmail1 = dt.Rows[j]["ContactEmail1"].ToString();
                    }
                    else
                    {
                        LB.ContactEmail1 = string.Empty;
                    }

                    LabList.Add(LB);



                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return View(LabList);
            }
            catch
            {
                return RedirectToAction("Error", "Labs");

            }
        }

            public ActionResult InsertLab()
        {

            return View();
        }
        [HttpPost]
        public ActionResult InsertLab(Labs Lab)
        {
            try
            {
                //DateTime CreatedOn = System.DateTime.Now;
                //DateTime UpdatedOn = System.DateTime.Now;

                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("InsertLab", con);
                cmd.CommandType = CommandType.StoredProcedure;


                //cmd.Parameters.AddWithValue("@Lab_Id", Lab.Lab_Id);

                if (System.Web.HttpContext.Current.Session["Account_id"]!=null)
                {

                    cmd.Parameters.AddWithValue("@AccountsId", System.Web.HttpContext.Current.Session["Account_id"]);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccountsId", string.Empty);
                }

                //cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                //cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);


               cmd.Parameters.AddWithValue("@Updatedby", System.Web.HttpContext.Current.User.Identity.GetUserName());

                 
                
               cmd.Parameters.AddWithValue("@Createdby", System.Web.HttpContext.Current.User.Identity.GetUserName());

               
                if (!string.IsNullOrEmpty(Lab.LabsLabNameLabLocation))
                {
                    cmd.Parameters.AddWithValue("@LabsLabNameLabLocation", Lab.LabsLabNameLabLocation);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsLabNameLabLocation", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LabsStreetAddress))
                {
                    cmd.Parameters.AddWithValue("@LabsStreetAddress", Lab.LabsStreetAddress);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsStreetAddress", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LabsCity))
                {
                    cmd.Parameters.AddWithValue("@LabsCity", Lab.LabsCity);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsCity", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LabsState))
                {
                    cmd.Parameters.AddWithValue("@LabsState", Lab.LabsState);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@LabsState", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LabsZip))
                {
                    cmd.Parameters.AddWithValue("@LabsZip", Lab.LabsZip);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsZip", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LabsMainNumber))
                {
                    cmd.Parameters.AddWithValue("@LabsMainNumber", Lab.LabsMainNumber);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsMainNumber", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LabsNotes))
                {


                    cmd.Parameters.AddWithValue("@LabsNotes", Lab.LabsNotes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsNotes", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactFullName1))
                {
                    cmd.Parameters.AddWithValue("@ContactFullName1", Lab.ContactFullName1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFullName1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactJobFunction1))
                {
                    cmd.Parameters.AddWithValue("@ContactJobFunction1", Lab.ContactJobFunction1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactJobFunction1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactOfficeNumber1))
                {

                    cmd.Parameters.AddWithValue("@ContactOfficeNumber1", Lab.ContactOfficeNumber1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactOfficeNumber1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactCellPhone1))
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone1", Lab.ContactCellPhone1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactFax1))
                {


                    cmd.Parameters.AddWithValue("@ContactFax1", Lab.ContactFax1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFax1", string.Empty);
                }

                if (!string.IsNullOrEmpty(Lab.ContactEmail1))
                {

                    cmd.Parameters.AddWithValue("@ContactEmail1", Lab.ContactEmail1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactEmail1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactFullName2))
                {
                    cmd.Parameters.AddWithValue("@ContactFullName2", Lab.ContactFullName2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFullName2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactJobFunction2))
                {

                    cmd.Parameters.AddWithValue("@ContactJobFunction2", Lab.ContactJobFunction2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactJobFunction2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactOfficeNumber2))
                {
                    cmd.Parameters.AddWithValue("@ContactOfficeNumber2", Lab.ContactOfficeNumber2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactOfficeNumber2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactCellPhone2))
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone2", Lab.ContactCellPhone2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactFax2))

                {
                    cmd.Parameters.AddWithValue("@ContactFax2", Lab.ContactFax2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFax2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactEmail2))
                {
                    cmd.Parameters.AddWithValue("@ContactEmail2", Lab.ContactEmail2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactEmail2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAttacheacopyofaCCF))
                {
                    cmd.Parameters.AddWithValue("@LAAttacheacopyofaCCF", Lab.LAAttacheacopyofaCCF);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttacheacopyofaCCF", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LALab1))
                {
                    cmd.Parameters.AddWithValue("@LALab1", Lab.LALab1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LALab1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAccountNumber1))
                {

                    cmd.Parameters.AddWithValue("@LAAccountNumber1", Lab.LAAccountNumber1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LAPannel1))
                {
                    cmd.Parameters.AddWithValue("@LAPannel1", Lab.LAPannel1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAPannel1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LATPA1))
                {
                    cmd.Parameters.AddWithValue("@LATPA1", Lab.LATPA1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LATPA1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAMRO1))
                {
                    cmd.Parameters.AddWithValue("@LAMRO1", Lab.LAMRO1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAMRO1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LASampleType1))
                {
                    cmd.Parameters.AddWithValue("@LASampleType1", Lab.LASampleType1);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@LASampleType1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAttachment1))
                {
                    cmd.Parameters.AddWithValue("@LAAttachment1", Lab.LAAttachment1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttachment1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LACCFs1))
                {
                    cmd.Parameters.AddWithValue("@LACCFs1", Lab.LACCFs1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LACCFs1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LALab2))
                {
                    cmd.Parameters.AddWithValue("@LALab2", Lab.LALab2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LALab2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAccountNumber2))
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber2", Lab.LAAccountNumber2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAPannel2))
                {
                    cmd.Parameters.AddWithValue("@LAPannel2", Lab.LAPannel2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAPannel2", string.Empty);
                }


                if (!string.IsNullOrEmpty(Lab.LATPA2))
                {
                    cmd.Parameters.AddWithValue("@LATPA2", Lab.LATPA2);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LATPA2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAMRO2))
                {
                    cmd.Parameters.AddWithValue("@LAMRO2", Lab.LAMRO2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAMRO2", string.Empty);
                }

                if (!string.IsNullOrEmpty(Lab.LASampleType2))
                {
                    cmd.Parameters.AddWithValue("@LASampleType2", Lab.LASampleType2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LASampleType2", string.Empty);
                }


                if (!string.IsNullOrEmpty(Lab.LAAttachment2))
                {
                    cmd.Parameters.AddWithValue("@LAAttachment2", Lab.LAAttachment2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttachment2", string.Empty);
                }

                if (!string.IsNullOrEmpty(Lab.LACCFs2))
                {
                    cmd.Parameters.AddWithValue("@LACCFs2", Lab.LACCFs2);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LACCFs2", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LALab3))
                {
                    cmd.Parameters.AddWithValue("@LALab3", Lab.LALab3);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@LALab3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAccountNumber3))
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber3", Lab.LAAccountNumber3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAPannel3))
                {
                    cmd.Parameters.AddWithValue("@LAPannel3", Lab.LAPannel3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAPannel3", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LATPA3))
                {
                    cmd.Parameters.AddWithValue("@LATPA3", Lab.LATPA3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LATPA3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAMRO3))
                {
                    cmd.Parameters.AddWithValue("@LAMRO3", Lab.LAMRO3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAMRO3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LASampleType3))
                {
                    cmd.Parameters.AddWithValue("@LASampleType3", Lab.LASampleType3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LASampleType3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAttachment3))
                {
                    cmd.Parameters.AddWithValue("@LAAttachment3", Lab.LAAttachment3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttachment3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LACCFs3))
                {
                    cmd.Parameters.AddWithValue("@LACCFs3", Lab.LACCFs3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LACCFs3", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Lablist", "Labs");

            }

            catch
            {
                return RedirectToAction("Error", "Labs");
            }
        }
           

        public ActionResult Error()
        {
            return View();
        }



        [HttpGet]
        public ActionResult UpdateLabs(string id)
        {
            try
            {

                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("EditLabs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@Lab_Id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Labs LB = new Labs();
                if (dt.Rows.Count > 0)
                {
                    LB.Lab_Id = Convert.ToInt32(dt.Rows[0]["Lab_Id"].ToString());
                    
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Updatedby"].ToString()))
                    {
                        LB.Updatedby = dt.Rows[0]["Updatedby"].ToString();
                    }
                    else
                    {
                        LB.Updatedby = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabsLabNameLabLocation"].ToString()))
                    {
                        LB.LabsLabNameLabLocation = dt.Rows[0]["LabsLabNameLabLocation"].ToString();
                    }
                    else
                    {
                        LB.LabsLabNameLabLocation = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabsStreetAddress"].ToString()))
                    {
                        LB.LabsStreetAddress = dt.Rows[0]["LabsStreetAddress"].ToString();
                    }
                    else
                    {
                        LB.LabsStreetAddress = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabsCity"].ToString()))
                    {
                        LB.LabsCity = dt.Rows[0]["LabsCity"].ToString();
                    }
                    else
                    {
                        LB.LabsCity = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabsState"].ToString()))
                    {
                        LB.LabsState = dt.Rows[0]["LabsState"].ToString();
                    }
                    else
                    {
                        LB.LabsState = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabsZip"].ToString()))
                    {
                        LB.LabsZip = dt.Rows[0]["LabsZip"].ToString();
                    }
                    else
                    {
                        LB.LabsZip = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabsMainNumber"].ToString()))
                    {
                        LB.LabsMainNumber = dt.Rows[0]["LabsMainNumber"].ToString();
                    }
                    else
                    {
                        LB.LabsMainNumber = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LabsNotes"].ToString()))
                    {
                        LB.LabsNotes = dt.Rows[0]["LabsNotes"].ToString();
                    }
                    else
                    {
                        LB.LabsNotes = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactFullName1"].ToString()))
                    {
                        LB.ContactFullName1 = dt.Rows[0]["ContactFullName1"].ToString();
                    }
                    else
                    {
                        LB.ContactFullName1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactJobFunction1"].ToString()))
                    {
                        LB.ContactJobFunction1 = dt.Rows[0]["ContactJobFunction1"].ToString();
                    }
                    else
                    {
                        LB.ContactJobFunction1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactOfficeNumber1"].ToString()))
                    {
                        LB.ContactOfficeNumber1 = dt.Rows[0]["ContactOfficeNumber1"].ToString();
                    }
                    else
                    {
                        LB.ContactOfficeNumber1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactCellPhone1"].ToString()))
                    {
                        LB.ContactCellPhone1 = dt.Rows[0]["ContactCellPhone1"].ToString();
                    }
                    else
                    {
                        LB.ContactCellPhone1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactFax1"].ToString()))
                    {
                        LB.ContactFax1 = dt.Rows[0]["ContactFax1"].ToString();
                    }
                    else
                    {
                        LB.ContactFax1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactEmail1"].ToString()))
                    {
                        LB.ContactEmail1 = dt.Rows[0]["ContactEmail1"].ToString();
                    }
                    else
                    {
                        LB.ContactEmail1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactFullName2"].ToString()))
                    {
                        LB.ContactFullName2 = dt.Rows[0]["ContactFullName2"].ToString();
                    }
                    else
                    {
                        LB.ContactFullName2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactJobFunction2"].ToString()))
                    {
                        LB.ContactJobFunction2 = dt.Rows[0]["ContactJobFunction2"].ToString();
                    }
                    else
                    {
                        LB.ContactJobFunction2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactOfficeNumber2"].ToString()))
                    {
                        LB.ContactOfficeNumber2 = dt.Rows[0]["ContactOfficeNumber2"].ToString();
                    }
                    else
                    {
                        LB.ContactOfficeNumber2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactCellPhone2"].ToString()))
                    {
                        LB.ContactCellPhone2 = dt.Rows[0]["ContactCellPhone2"].ToString();
                    }
                    else
                    {
                        LB.ContactCellPhone2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactFax2"].ToString()))
                    {
                        LB.ContactFax2 = dt.Rows[0]["ContactFax2"].ToString();
                    }
                    else
                    {
                        LB.ContactFax2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContactEmail2"].ToString()))
                    {
                        LB.ContactEmail2 = dt.Rows[0]["ContactEmail2"].ToString();
                    }
                    else
                    {
                        LB.ContactEmail2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAAttacheacopyofaCCF"].ToString()))
                    {
                        LB.LAAttacheacopyofaCCF = dt.Rows[0]["LAAttacheacopyofaCCF"].ToString();
                    }
                    else
                    {
                        LB.LAAttacheacopyofaCCF = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LALab1"].ToString()))
                    {
                        LB.LALab1 = dt.Rows[0]["LALab1"].ToString();
                    }
                    else
                    {
                        LB.LALab1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAAccountNumber1"].ToString()))
                    {
                        LB.LAAccountNumber1 = dt.Rows[0]["LAAccountNumber1"].ToString();
                    }
                    else
                    {
                        LB.LAAccountNumber1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAPannel1"].ToString()))
                    {
                        LB.LAPannel1 = dt.Rows[0]["LAPannel1"].ToString();
                    }
                    else
                    {
                        LB.LAPannel1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LATPA1"].ToString()))
                    {
                        LB.LATPA1 = dt.Rows[0]["LATPA1"].ToString();
                    }
                    else
                    {
                        LB.LATPA1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAMRO1"].ToString()))
                    {
                        LB.LAMRO1 = dt.Rows[0]["LAMRO1"].ToString();
                    }
                    else
                    {
                        LB.LAMRO1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LASampleType1"].ToString()))
                    {
                        LB.LASampleType1 = dt.Rows[0]["LASampleType1"].ToString();
                    }
                    else
                    {
                        LB.LASampleType1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAAttachment1"].ToString()))
                    {
                        LB.LAAttachment1 = dt.Rows[0]["LAAttachment1"].ToString();
                    }
                    else
                    {
                        LB.LAAttachment1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LACCFs1"].ToString()))
                    {
                        LB.LACCFs1 = dt.Rows[0]["LACCFs1"].ToString();
                    }
                    else
                    {
                        LB.LACCFs1 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LALab2"].ToString()))
                    {
                        LB.LALab2 = dt.Rows[0]["LALab2"].ToString();
                    }
                    else
                    {
                        LB.LALab2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAAccountNumber2"].ToString()))
                    {
                        LB.LAAccountNumber2 = dt.Rows[0]["LAAccountNumber2"].ToString();
                    }
                    else
                    {
                        LB.LAAccountNumber2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAPannel2"].ToString()))
                    {
                        LB.LAPannel2 = dt.Rows[0]["LAPannel2"].ToString();
                    }
                    else
                    {
                        LB.LAPannel2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LATPA2"].ToString()))
                    {
                        LB.LATPA2 = dt.Rows[0]["LATPA2"].ToString();
                    }
                    else
                    {
                        LB.LATPA2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAMRO2"].ToString()))
                    {
                        LB.LAMRO2 = dt.Rows[0]["LAMRO2"].ToString();
                    }
                    else
                    {
                        LB.LAMRO2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LASampleType2"].ToString()))
                    {
                        LB.LASampleType2 = dt.Rows[0]["LASampleType2"].ToString();
                    }
                    else
                    {
                        LB.LASampleType2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAAttachment2"].ToString()))
                    {
                        LB.LAAttachment2 = dt.Rows[0]["LAAttachment2"].ToString();
                    }
                    else
                    {
                        LB.LAAttachment2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LACCFs2"].ToString()))
                    {
                        LB.LACCFs2 = dt.Rows[0]["LACCFs2"].ToString();
                    }
                    else
                    {
                        LB.LACCFs2 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LALab3"].ToString()))
                    {
                        LB.LALab3 = dt.Rows[0]["LALab3"].ToString();
                    }
                    else
                    {
                        LB.LALab3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAAccountNumber3"].ToString()))
                    {
                        LB.LAAccountNumber3 = dt.Rows[0]["LAAccountNumber3"].ToString();
                    }
                    else
                    {
                        LB.LAAccountNumber3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAPannel3"].ToString()))
                    {
                        LB.LAPannel3 = dt.Rows[0]["LAPannel3"].ToString();
                    }
                    else
                    {
                        LB.LAPannel3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LATPA3"].ToString()))
                    {
                        LB.LATPA3 = dt.Rows[0]["LATPA3"].ToString();
                    }
                    else
                    {
                        LB.LATPA3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAMRO3"].ToString()))
                    {
                        LB.LAMRO3 = dt.Rows[0]["LAMRO3"].ToString();
                    }
                    else
                    {
                        LB.LAMRO3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LASampleType3"].ToString()))
                    {
                        LB.LASampleType3 = dt.Rows[0]["LASampleType3"].ToString();
                    }
                    else
                    {
                        LB.LASampleType3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LAAttachment3"].ToString()))
                    {
                        LB.LAAttachment3 = dt.Rows[0]["LAAttachment3"].ToString();
                    }
                    else
                    {
                        LB.LAAttachment3 = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["LACCFs3"].ToString()))
                    {
                        LB.LACCFs3 = dt.Rows[0]["LACCFs3"].ToString();
                    }
                    else
                    {
                        LB.LACCFs3 = string.Empty;
                    }
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return View(LB);
            }

            catch
            {
                return RedirectToAction("Error", "Labs");

            }
        }
        [HttpPost]
        public ActionResult UpdateLabs(Labs Lab)
        {
            try
            {

                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("UpdateLab", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Lab_Id", Convert.ToInt32(Lab.Lab_Id));
                //cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
              
                cmd.Parameters.AddWithValue("@Updatedby", System.Web.HttpContext.Current.User.Identity.GetUserName());

                
                if (!string.IsNullOrEmpty(Lab.LabsLabNameLabLocation))
                {
                    cmd.Parameters.AddWithValue("@LabsLabNameLabLocation", Lab.LabsLabNameLabLocation);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsLabNameLabLocation", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LabsStreetAddress))
                {
                    cmd.Parameters.AddWithValue("@LabsStreetAddress", Lab.LabsStreetAddress);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsStreetAddress", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LabsCity))
                {
                    cmd.Parameters.AddWithValue("@LabsCity", Lab.LabsCity);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsCity", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LabsState))
                {
                    cmd.Parameters.AddWithValue("@LabsState", Lab.LabsState);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@LabsState", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LabsZip))
                {
                    cmd.Parameters.AddWithValue("@LabsZip", Lab.LabsZip);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsZip", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LabsMainNumber))
                {
                    cmd.Parameters.AddWithValue("@LabsMainNumber", Lab.LabsMainNumber);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsMainNumber", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LabsNotes))
                {


                    cmd.Parameters.AddWithValue("@LabsNotes", Lab.LabsNotes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LabsNotes", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactFullName1))
                {
                    cmd.Parameters.AddWithValue("@ContactFullName1", Lab.ContactFullName1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFullName1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactJobFunction1))
                {
                    cmd.Parameters.AddWithValue("@ContactJobFunction1", Lab.ContactJobFunction1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactJobFunction1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactOfficeNumber1))
                {

                    cmd.Parameters.AddWithValue("@ContactOfficeNumber1", Lab.ContactOfficeNumber1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactOfficeNumber1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.ContactCellPhone1))
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone1", Lab.ContactCellPhone1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactFax1))
                {


                    cmd.Parameters.AddWithValue("@ContactFax1", Lab.ContactFax1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFax1", string.Empty);
                }

                if (!string.IsNullOrEmpty(Lab.ContactEmail1))
                {

                    cmd.Parameters.AddWithValue("@ContactEmail1", Lab.ContactEmail1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactEmail1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactFullName2))
                {
                    cmd.Parameters.AddWithValue("@ContactFullName2", Lab.ContactFullName2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFullName2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactJobFunction2))
                {

                    cmd.Parameters.AddWithValue("@ContactJobFunction2", Lab.ContactJobFunction2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactJobFunction2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactOfficeNumber2))
                {
                    cmd.Parameters.AddWithValue("@ContactOfficeNumber2", Lab.ContactOfficeNumber2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactOfficeNumber2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactCellPhone2))
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone2", Lab.ContactCellPhone2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactCellPhone2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactFax2))

                {
                    cmd.Parameters.AddWithValue("@ContactFax2", Lab.ContactFax2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactFax2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.ContactEmail2))
                {
                    cmd.Parameters.AddWithValue("@ContactEmail2", Lab.ContactEmail2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactEmail2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAttacheacopyofaCCF))
                {
                    cmd.Parameters.AddWithValue("@LAAttacheacopyofaCCF", Lab.LAAttacheacopyofaCCF);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttacheacopyofaCCF", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LALab1))
                {
                    cmd.Parameters.AddWithValue("@LALab1", Lab.LALab1);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LALab1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAccountNumber1))
                {

                    cmd.Parameters.AddWithValue("@LAAccountNumber1", Lab.LAAccountNumber1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LAPannel1))
                {
                    cmd.Parameters.AddWithValue("@LAPannel1", Lab.LAPannel1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAPannel1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LATPA1))
                {
                    cmd.Parameters.AddWithValue("@LATPA1", Lab.LATPA1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LATPA1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAMRO1))
                {
                    cmd.Parameters.AddWithValue("@LAMRO1", Lab.LAMRO1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAMRO1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LASampleType1))
                {
                    cmd.Parameters.AddWithValue("@LASampleType1", Lab.LASampleType1);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@LASampleType1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAttachment1))
                {
                    cmd.Parameters.AddWithValue("@LAAttachment1", Lab.LAAttachment1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttachment1", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LACCFs1))
                {
                    cmd.Parameters.AddWithValue("@LACCFs1", Lab.LACCFs1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LACCFs1", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LALab2))
                {
                    cmd.Parameters.AddWithValue("@LALab2", Lab.LALab2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LALab2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAccountNumber2))
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber2", Lab.LAAccountNumber2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAPannel2))
                {
                    cmd.Parameters.AddWithValue("@LAPannel2", Lab.LAPannel2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAPannel2", string.Empty);
                }


                if (!string.IsNullOrEmpty(Lab.LATPA2))
                {
                    cmd.Parameters.AddWithValue("@LATPA2", Lab.LATPA2);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LATPA2", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAMRO2))
                {
                    cmd.Parameters.AddWithValue("@LAMRO2", Lab.LAMRO2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAMRO2", string.Empty);
                }

                if (!string.IsNullOrEmpty(Lab.LASampleType2))
                {
                    cmd.Parameters.AddWithValue("@LASampleType2", Lab.LASampleType2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LASampleType2", string.Empty);
                }


                if (!string.IsNullOrEmpty(Lab.LAAttachment2))
                {
                    cmd.Parameters.AddWithValue("@LAAttachment2", Lab.LAAttachment2);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttachment2", string.Empty);
                }

                if (!string.IsNullOrEmpty(Lab.LACCFs2))
                {
                    cmd.Parameters.AddWithValue("@LACCFs2", Lab.LACCFs2);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LACCFs2", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LALab3))
                {
                    cmd.Parameters.AddWithValue("@LALab3", Lab.LALab3);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@LALab3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAccountNumber3))
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber3", Lab.LAAccountNumber3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAccountNumber3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAPannel3))
                {
                    cmd.Parameters.AddWithValue("@LAPannel3", Lab.LAPannel3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAPannel3", string.Empty);

                }
                if (!string.IsNullOrEmpty(Lab.LATPA3))
                {
                    cmd.Parameters.AddWithValue("@LATPA3", Lab.LATPA3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LATPA3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAMRO3))
                {
                    cmd.Parameters.AddWithValue("@LAMRO3", Lab.LAMRO3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAMRO3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LASampleType3))
                {
                    cmd.Parameters.AddWithValue("@LASampleType3", Lab.LASampleType3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LASampleType3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LAAttachment3))
                {
                    cmd.Parameters.AddWithValue("@LAAttachment3", Lab.LAAttachment3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LAAttachment3", string.Empty);
                }
                if (!string.IsNullOrEmpty(Lab.LACCFs3))
                {
                    cmd.Parameters.AddWithValue("@LACCFs3", Lab.LACCFs3);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LACCFs3", string.Empty);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Lablist", "Labs");

            }
            catch
            {
                return RedirectToAction("Error", "Labs");

            }
        }


        public ActionResult DeleteLab(string id)
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("DeleteLab", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Lab_Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToAction("Lablist", "Labs");

            }
            catch
            {
                return RedirectToAction("Error", "Labs");

            }
        }

        }
}