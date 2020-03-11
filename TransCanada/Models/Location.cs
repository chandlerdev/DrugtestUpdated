// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Location
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
    public class Location
    {
        public Location()
        {
            Country = "US";
        }
        [Required]
        [Display(Name = "Name")]
        public string Location_Name { get; set; }
        [Display(Name = "Location")]
        [Required]
        public string Location_1 { get; set; }
        ////[Required]
        //[Display(Name = "Contact Person")]
        //public string Location_Contact_Person { get; set; }
        [Display(Name = "Location")]
        public string Location_id1 { get; set; }
        //[Required]
        [Display(Name = "Address")]
        public string Address_1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address_2 { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "Phone Number")]
        public string Phone_number { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
        //[Required]
        public string City { get; set; }

        //[Required]
        public string State { get; set; }

        //[Required]
        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        //[Required]
        public string Country { get; set; }


        public string WebSite { get; set; }

        //public string Email { get; set; }

            [Display(Name = "Location")]
        public int Location_id { get; set; }

        [Display(Name ="Type")]
        public string ltype { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}
