using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
  public class SubServices
  {
        public int lab_service_id { get; set; }

        [Required]
        [Display(Name = "Panel")]
        public string lab_services_description { get; set; }

        [Required]
        [Display(Name = "Panel Description")]
        public string lab_services_ext_description { get; set; }

        [Required]
        [Display(Name = "Purchase Cost With MRO ($)")]
        [DataType(DataType.Currency)]
        public Decimal service_charges { get; set; }

        public int lab_group_id { get; set; }

        [Required]
        [Display(Name = "Lab Cost ($)")]
        [DataType(DataType.Currency)]
        public Decimal client_billing_charges { get; set; }
        [Required]
        [Display(Name = "Specimen Type")]
        public string Specimen_Type { get; set; }
        [Required]
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
        public string[] PS_Type { get; set; }
        public string PS_Id { get; set; }
        public List<SelectListItem> RemainProducts { get; set; }

        [Required]
        [Display(Name = "Billing Price ($)")]
        [DataType(DataType.Currency)]
        public Decimal Billing_Price { get; set; }

        [Required]
        [Display(Name = "Collection Cost ($)")]
        [DataType(DataType.Currency)]
        public Decimal Collection_Cost { get; set; }
    }
}
