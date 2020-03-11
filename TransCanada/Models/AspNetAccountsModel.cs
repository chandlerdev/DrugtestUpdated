using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TransCanada.Models
{
  public class AspNetAccountsModel
  {
    public Lab_contact labContact { get; set; }

    [Display(Name = "Account ID")]
    public int AccountId_PK { get; set; }

    [Required]
    [Display(Name = "Client Name")]
    public string AccountId { get; set; }

    [Display(Name = "Logo Image")]
    public string LogoImage { get; set; }

    public HttpPostedFileBase ImageFile { get; set; }

    public string Logoimage_src { get; set; }

    [Display(Name = "Created By")]
    public string CreatedBy { get; set; }

    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Updated By")]
    public string UpdatedBy { get; set; }

    [Display(Name = "Updated Date")]
    public DateTime UpdatedDate { get; set; }

    public List<SelectListItem> Accounts_Id { get; set; }

    
    [Display(Name = "Short Name")]
        public string Short_Name { get; set; }

        //[Required]
        [Display(Name = "Address1")]
        public string Address { get; set; }

       
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        //[Required]
        [Display(Name = "City")]
        public string City { get; set; }

        //[Required]
        [Display(Name = "State")]
        public string State { get; set; }

        //[Required]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        
        [Display(Name = "Client Category")]
        public string[] Category { get; set; }
        public string Text { get; set; }

        [Display(Name = "IsActive")]
        public int Isactive { get; set; }

        public List<SelectListItem> CheckBoxes { get; set; }
        public bool Checked { get; set; }

        ////[Required]     
        public string FirstName { get; set; }

        ////[Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string Phone { get; set; }

        [Display(Name="TPA")]
        public string TPA_Client { get; set; }

        [Required]
        [Display(Name="Location")]       
        public string Location { get; set; }

        [Display(Name = "Notes")]
        public string Notes_1 { get; set; }

       
        [Display(Name = "Category")]
        public string Category_1 { get; set; }

        
        [Display(Name = "TPA Category")]
        public string TPA_Category { get; set; }

        
        [Display(Name = "Address Type")]
        public string Address_type { get; set; }

        [Display(Name = "Address Notes")]
        public string Address_Notes { get; set; }

        
        [Display(Name = "Title")]
        public string[] Title1 { get; set; }

       
        [Display(Name = "function")]
        public string[] function_1 { get; set; }

        
        [Display(Name = "Contact type")]
        public string Phone_Number_type { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        
        [Display(Name = "POCT Testing")]
        public string POCT_Testing { get; set; }

        
        [Display(Name = "Self Collect")]      
        public string Self_Collect { get; set; }

        
        [Display(Name = "Clearing House")]      
        public string Clearing_House { get; set; }

        
        [Display(Name = "Compliance Support")]      
        public string Compliance_Support { get; set; }

       
        [Display(Name = "24/7")]        
        public string Full_time { get; set; }

      
        [Display(Name = "Background Checks")]       
        public string Background_Checks { get; set; }

       
        [Display(Name = "Pool")]        
        public string Pool { get; set; }

        
        [Display(Name = "Reporting")]
        public string[] Reporting { get; set; }

        
        [Display(Name = "Billing Details")]      
        public string[] Billing_Details { get; set; }

        [Display(Name = "CreditCard Details")]
        public string Credit_Card_Details { get; set; }

        [Display(Name = "Notes On Billing")]
        public string Notes_On_Billing { get; set; }

        [Display(Name = "Details")]
        public string POCT_Testing_Details { get; set; }

        [Display(Name = "Details")]
        public string Self_Collect_Details { get; set; }

        [Display(Name = "Details")]

        public string Clearing_House_Details { get; set; }
        [Display(Name = "Details")]
        public string Compliance_Support_Details { get; set; }

        [Display(Name = "Details")]
        public string Background_Checks_Details { get; set; }

        [Display(Name = "Details")]
        public string Pool_Details { get; set; }

        [Display(Name = "Details")]
        public string Card_Details { get; set; }

        [Display(Name = "Related To")]
        public string Related_To { get; set; }


        public List<SelectListItem> Category_list { get; set; }

        public List<SelectListItem> Fucntion_list { get; set; }

        public List<SelectListItem> reporting_list { get; set; }
        
        public List<CheckBox> self_collect_list { get; set; }
        public List<SelectListItem> title_list { get; set; }

        public string add_email { get; set; }
        public string[] add_emails { get; set; }

    }

    //public class MainModel
    //{
    //    public List<AspNetAccountsModel> CheckBoxes { get; set; }
    //}
}
