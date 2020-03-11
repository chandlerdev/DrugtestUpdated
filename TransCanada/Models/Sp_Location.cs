using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransCanada.Models
{
    public class Sp_Location
    {   
        public int Serviceprovider_id { get; set; }
        public string Serviceprovider_Name { get; set; }
        [Display(Name = "Address")]
        public string Address_1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address_2 { get; set; }
        [Display(Name = "City")]

        public string City { get; set; }
        [Display(Name = "State")]

        public string State { get; set; }
        [Display(Name = "Zip")]

        public string Zip { get; set; }
        public Sp_Location()
        {
            Country = "US";
        }
       
        public string Country { get; set; }
        public string WebSite { get; set; }
        public string Notes { get; set; }
        [Required]
        [Display(Name="Location")]
        public string Location_1 { get; set; }


    }
}