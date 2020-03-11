using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransCanada.Models
{
    public class TpaLabLocation
    {
        public int TPALab_Id { get; set; }

        public int TPALAB_LocID { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address_1 { get; set; }


        [Display(Name = "Address 2")]
        public string Address_2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Country { get; set; }

        //[Display(Name = "Website")]
        //public string WebSite { get; set; }
    }
}