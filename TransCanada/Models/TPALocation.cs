using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TransCanada.Models
{
    public class TPALocation
    {

        public int TPAClient_Id { get; set; }

        public int TPAClient_LocID { get; set; }
        [Required]
        [Display(Name = "Location Name")]
        public string Location_id { get; set; }


        //[Required]
        [Display(Name = "Address")]
        public string Address_1 { get; set; }

        public TPALocation()
        {
            Country = "US";
        }
        [Display(Name = "Address 2")]
        public string Address_2 { get; set; }

        //[Required]
        public string City { get; set; }

        //[Required]
        public string State { get; set; }

        //[Required]
        public string Zip { get; set; }

        //[Required]
        public string Country { get; set; }

        [Display(Name = "Website")]
        public string WebSite { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "Phone Number")]
        public string Phone_number { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

    }
}