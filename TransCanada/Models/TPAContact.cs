using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TransCanada.Models
{
    public class TPAContact
    {
        public int Id { get; set; }

        public int TPAClient_Id { get; set; }
        public int TPAClient_LocID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstname { get; set; }

        //[Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        //[Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }


        [Display(Name = "Mobile")]
        public string cell { get; set; }

        [Display(Name = "Office Phone")]
        public string officephone { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Display(Name = "Notes")]


        public string Notes { get; set; }
        [EmailAddress]
        [Display(Name = "Secondary Email")]
        public string Email1 { get; set; }
        [Display(Name = "Secondary Phone")]
        public string Phone1 { get; set; }
        [Display(Name = "Third Phone")]
        public string Third_Phone { get; set; }
    }
}