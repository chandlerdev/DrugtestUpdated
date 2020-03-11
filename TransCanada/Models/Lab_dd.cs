using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TransCanada.Models
{
    public class Lab_dd
    {
        public string Client_Name { get; set; }

        public string src { get; set; }
        [Display(Name = "Lab/Manufacturer")]
        public string[] Lab_Name { get; set; }

        [Display(Name = "Panel Groups")]
        public string[] SubServices { get; set; }

        [Display(Name = "Panels")]
        public string[] Servicegroups { get; set; }

        
        public List<SelectListItem> Labs { get; set; }

        public List<SelectListItem> Labservices { get; set; }

        public List<SelectListItem> Labsubservices { get; set; }
        [Display(Name = "Service Provider")]
        public string[] SP_Names { get; set; }

        [Display(Name = "Panel Groups")]
        public string[] SP_Panel_Groups { get; set; }


        [Display(Name = "Panels")]
        public string[] SP_Panels { get; set; }
        public List<SelectListItem> List_SP { get; set; }

        public List<SelectListItem> List_Sp_Services { get; set; }

        public List<SelectListItem> List_Sp_Sub_Services { get; set; }



    }
}