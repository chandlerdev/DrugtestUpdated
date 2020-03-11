using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransCanada.Models
{
    public class TpaLabservice
    {
        public TpaLabsubservice sub { get; set; }
        [Display(Name = "Service Id ")]
        public int TPALab_Service_Id { get; set; }

        [Display(Name = "TPALab Id")]
        public int TPALab_Id { get; set; }

        [Required]
        [Display(Name = "Service Group")]
        public string Service_Grp_Name { get; set; }

      
    }
}