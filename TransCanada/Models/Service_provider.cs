// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Service_provider
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TransCanada.Models
{
    public class Service_provider
    {
        public Service_provider()
        {
            Country = "US";
        }
        [Display(Name = "Id")]
        public int Serviceprovider_id { get; set; }
        [Display(Name = "Location")]
        public string Location_1 { get; set; }
        public string Location { get; set; }
        public string Createdby { get; set; }

        public string Updatedby { get; set; }

        public DateTime Createdon { get; set; }

        public DateTime Updatedon { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Serviceprovider_Name { get; set; }

        [Required]
        [Display(Name = "Contact Person")]
        public string Contact_Person { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string Address_1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address_2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Required]
        public string Country { get; set; }

        public string WebSite { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<SelectListItem> Cities1 { get; set; }

        [Required]
        [Display(Name = "Sp Sublocation")]
        public string Service_Provider_Sublocation { get; set; }

        [Display(Name = "Related To")]
        public string Related_To { get; set; }

        [Required]
        [Display(Name = "Short Name")]
        public string Short_Name { get; set; }

        //[Required]
        //[Display(Name = "Category")]
        //public bool Category { get; set; }

        [Required]
        [Display(Name = "Mobile Collections")]
        public string Mobile_Collections { get; set; }

        [Required]
        [Display(Name = "Servicing clients")]
        public bool Servicing_which_clients { get; set; }


        [Display(Name = "Formerly Known As")]
        public string Formerly_Known_As { get; set; }

        //[Required]
        //[Display(Name = "Address Type")]
        //public bool Address_Type { get; set; }

        [Display(Name = "Address Notes")]
        public string Address_Notes { get; set; }

        [Display(Name = "Logo Image")]
        public string Logo_Image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public string Logoimage_src { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        //[Required]
        //[Display(Name = "Title")]
        //public bool Title { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string Phone_Number_Type { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Hours of Operation")]
        public string Hours_of_Operation { get; set; }

        [Required]
        [Display(Name = "Clinic")]
        public string Clinic { get; set; }

        [Required]
        [Display(Name = "Tractor trailer Parcking")]
        public string Tractor_trailer_Parcking { get; set; }

        [Required]
        [Display(Name = "Observed collections")]
        public string Observed_collections { get; set; }

        
        [Display(Name = "Services")]
        public string Services { get; set; }

        [Required]
        [Display(Name = "Reporting")]
        public string Reporting { get; set; }

        [Required]
        [Display(Name = "Billing Details")]
        public string Billing_Details { get; set; }

        [Display(Name = "Credit Card Details")]
        public string Credit_Card_Details { get; set; }

        [Display(Name = "Notes on Billing")]
        public string Notes_on_Billing { get; set; }

        [Display(Name = "Collection Protocols")]
        public string Link_To_Collection_Protocols { get; set; }

        [Display(Name = "documents")]
        public string Link_to_documents { get; set; }

        [Display(Name = "Fees")]
        public string Fees { get; set; }

        public List<SelectListItem> Category1 { get; set; }
        public string Category { get; set; }

        //public List<SelectListItem> Servicing_which_clients { get; set; }
        //public bool Checked1 { get; set; }
         
        public List<SelectListItem> Address_Type1 { get; set; }
        [Display(Name ="Address Type")]
        public string Address_Type { get; set; }

        public List<SelectListItem> Title1 { get; set; }
        public string Title { get; set; }

        public List<string> List_Gender { get; set; }

        [Display(Name = "Phone 1")]
        public string Phone1 { get; set; }
        [Display(Name = "Phone 2")]
        public string Phone2 { get; set; }
        [Display(Name = "Phone 3")]
        public string Phone3 { get; set; }

    }


    public class Service_provider1
    {
        [Required]
        [Display(Name = "Name")]
        public string Serviceprovider_Name { get; set; }
    }
}
