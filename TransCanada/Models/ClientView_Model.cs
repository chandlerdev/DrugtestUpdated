using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransCanada.Models
{
    public class ClientView_Model
    {
        public Location location { get; set; }
        public Lab_contact Lab_contact { get; set; }
        public Lab_dd Services { get; set; }
        public string[] Sp_selected { get; set; }
        public List<SelectListItem> SP { get; set; }

        public List<SelectListItem> LabServices { get; set; }
        public List<Location> list_location { get; set; }

        public List<Lab_contact> List_Lab_contact { get; set; }

        public List<Lab_dd> sp_lab_list1 { get; set; }
        public List<Lab_dd> sp_lab_list2 { get; set; }
        public Event_Model Event { get; set; }
        public List<Event_Model> List_Events { get; set; }

        //[Required]
        //[Display(Name = "Name")]
        //public string Location_Name { get; set; }

        //[Required]
        //[Display(Name = "Address")]
        //public string Address_1 { get; set; }

        //[Display(Name = "Address 2")]
        //public string Address_2 { get; set; }

        //[Required]
        //public string City { get; set; }

        //[Required]
        //public string State { get; set; }

        //public int contact_id { get; set; }
        //public string location_id { get; set; }

        //[Required]
        //[Display(Name = "First Name")]
        //public string firstname { get; set; }

        //[Required]
        //[Display(Name = "Last Name")]
        //public string Lastname { get; set; }

        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string email { get; set; }


        //[Display(Name = "Mobile")]
        //public string cell { get; set; }

        //[Display(Name = "Office Phone")]
        //public string officephone { get; set; }
    }
}