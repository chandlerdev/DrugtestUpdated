// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.lab_location
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
    public class lab_location
    {
        public lab_location()
        {
            Country = "US";
        }

        [Required]
        [Display(Name = "Lab Name")]
        public string Lab_Name { get; set; }

        [Required]
        [Display(Name = "Location ID")]
        public string Id1 { get; set; }

       
        [Display(Name = "Location")]
        public string Location_Id { get; set; }

        //[Required]
        [Display(Name = "Address")]
        public string Address_1 { get; set; }


        [Display(Name = "Address 2")]
        public string Address_2 { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
   
        //[Required]
        public string City { get; set; }

        //[Required]
        public string State { get; set; }

        //[Required]
        public string Zip { get; set; }

        //[Required]
        public string Country { get; set; }

        public string hide_id { get; set; }

        public List<SelectListItem> Cities { get; set; }
    }
}
