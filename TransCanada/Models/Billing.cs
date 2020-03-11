using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransCanada.Models
{
    public class Billing
    {
        
        public string Lab_name { get; set; }
        [Display(Name = "Billing Price ($)")]
        [DataType(DataType.Currency)]
        public Decimal Billing_Price { get; set; }
        [Required]
        [Display(Name = "Client_id")]
        
        public int Client_id { get; set; }

        [Display(Name = "Panels")]
        public string lab_services_description { get; set; }
        public string lab_services_ext_description { get; set; }

        [Required]
        [Display(Name = "Purchase Cost With MRO ($)")]
        [DataType(DataType.Currency)]
        public Decimal service_charges { get; set; }


        [Required]
        [Display(Name = "Lab Cost ($)")]
        [DataType(DataType.Currency)]
        public Decimal client_billing_charges { get; set; }
       
        [Display(Name = "Specimen Type")]
        public string Specimen_Type { get; set; }
        
        [Display(Name = "Drugs")]
        public string Drugs { get; set; }
        [Required]
        [Display(Name = "Medical Review Office Cost ($)")]
        [DataType(DataType.Currency)]
        public Decimal Medical_Review_Office_Cost { get; set; }
        [Required]
        [Display(Name = "Vendor Management ($)")]
        public Decimal Vendor_management { get; set; }
        [Required]
        [Display(Name = "Document Upload ($)")]
        public Decimal Document_Upload { get; set; }

        [Required]
        [Display(Name = "Collection Cost ($)")]
        [DataType(DataType.Currency)]
        public Decimal Collection_Cost { get; set; }
    }
}