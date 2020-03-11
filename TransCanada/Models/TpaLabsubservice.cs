using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransCanada.Models
{
    public class TpaLabsubservice
    { 
        public int TPALab_SubService_Id { get; set; }

        [Display(Name = "TPALab Service Id")]
        public int TPALab_Service_Id { get; set; }

        [Display(Name = "TPALab Id")]
        public int TPALab_Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string TPAlab_Service_Description { get; set; }

        [Required]
        [Display(Name = "Extended Description")]
        public string TPAlab_Service_Ext_Description { get; set; }

        [Required]
        [Display(Name = "Service Charges")]
        [DataType(DataType.Currency)]
        public Decimal Service_Charges { get; set; }

      [Required]
        [Display(Name = "Billing Charges")]
        [DataType(DataType.Currency)]
        public Decimal Client_Billing_Charges { get; set; }

    }
}