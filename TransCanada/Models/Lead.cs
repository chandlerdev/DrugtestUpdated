using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransCanada.Models
{
    public class Lead
    {
        public Lead()
        {
            Country = "US";
        }
        public int Id { get; set; }
        [Display(Name="Client Name")]
        [Required]
        public string Client_Name { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string Contact_FirstName { get; set; }
            
        [Display(Name = "Last Name")]
        //[Required]

        public string Contact_LastName { get; set; }
        
        [Display(Name = "First Name 2")]
        public string Second_Contact_First_Name { get; set; }
        
        [Display(Name = "Last Name 2")]
        public string Second_Contact_Last_Name { get; set; }
        [EmailAddress]
        [Display(Name = "Email ")]
        

        public string Email { get; set; }
        
        [Display(Name = "Phone ")]
        //[Required]

        public string Phone { get; set; }

        public bool IsActive { get; set; }
        
        [Display(Name = "Opportunity Notes ")]
        public string Opportunity_Notes { get; set; }
        
        [Display(Name = "Lead Email Ref ")]
        public string Lead_Email_Ref { get; set; }
        
        [Display(Name = "NDA ")]
        public bool NDA { get; set; }
        
        [Display(Name = "Priority ")]

        public string Priority { get; set; }


        [Display(Name = "Address ")]
        public string Address { get; set; }
        
        [Display(Name = "City ")]
        //[Required]

        public string City { get; set; }
        
        [Display(Name = "State ")]
        //[Required]

        public string State { get; set; }
        
        [Display(Name = "Zip Code ")]
        public string Zip_Code { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Mobile Number  ")]
        public string Mobile_Number { get; set; }
        [Display(Name = "Fax Number ")]
        public string Fax_Number { get; set; }
        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Received On ")]
        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Received_on { get; set; }

        [Display(Name = "Customer Status ")]
        //[Required]

        public string Customer_Status { get; set; }

        [Display(Name = "Lead Source")]
        [Required]
        public string Lead_Source { get; set; }     

        [Display(Name = "Lead Received Via")]
        public string Lead_Received_Via { get; set; }

        [Display(Name = "Proposal Status")]
        public string Proposal_Status { get; set; }

        [Display(Name = "Lead Eligibility")]
        //[Required]
        public string Lead_Eligibility { get; set; }

        [Display(Name = "Next Follow Up")]
        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Next_Follow_Up { get; set; }

        [Display(Name = "Lead Status")]
        public string Lead_Status { get; set; }

        [Display(Name = "Sales Manager")]
       // [Required]

        public string Sales_Manager { get; set; }

        [Display(Name = "Sales Person")]
        //[Required]

        public string Sales_Person { get; set; }

        [Display(Name="Notes")]

        public string Notes { get; set; }



    }

   
}