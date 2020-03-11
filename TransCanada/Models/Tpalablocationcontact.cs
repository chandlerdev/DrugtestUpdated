using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransCanada.Models
{
    public class Tpalablocationcontact
    {

        public int TPALab_Contact_Id { get; set; }
        public int TPALab_LocId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }


        [Display(Name = "Mobile")]
        public string cell { get; set; }

        [Display(Name = "Office Phone")]
        public string officephone { get; set; }

    }
}