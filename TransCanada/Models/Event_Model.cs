using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransCanada.Models
{
    public class Event_Model
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        private string Para_Event_id = "";
        [Display(Name = "Event Id")]
        public String Event_id
        {
            get { return Para_Event_id; }
            set { Para_Event_id = value; }
        }

        private string Para_Event_name = "";
        [Display(Name = "Event Name")]
        [Required]
        public String Event_name
        {
            get { return Para_Event_name; }
            set { Para_Event_name = value; }
        }

        [Display(Name = "Client Name")]
        [Required]
        public string Client_id { get; set; }


        private string Para_Client_Location = "";
        [Display(Name = "Client Location")]
        [Required]
        public String Client_Location
        {
            get { return Para_Client_Location; }
            set { Para_Client_Location = value; }
        }

        private string Para_Client_Contact = "";
        [Display(Name = "Client Contact")]
        [Required]
        public String Client_Contact
        {
            get { return Para_Client_Contact; }
            set { Para_Client_Contact = value; }
        }

        [Display(Name = "Event Start Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Event_Start_Date { get; set; }


        [Display(Name = "Event End Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Event_End_Date { get; set; }
        [Required]
        [Display(Name = "Event Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        
        
        public DateTime Event_Start_Time { get; set; }
        [Required]
        [Display(Name = "Event End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        
        public DateTime Event_End_Time { get; set; }

        private string Para_Document_Upload = "";
        [Display(Name = "Document")]
        public String Document_Upload
        {
            get { return Para_Document_Upload; }
            set { Para_Document_Upload = value; }
        }

        private string Para_Notes = "";
        [Display(Name = "Notes")]
        public String Notes
        {
            get { return Para_Notes; }
            set { Para_Notes = value; }
        }

        private string Para_Service_Location = "";
        [Display(Name = "Service Location")]
        [Required]
        public String Service_Location
        {
            get { return Para_Service_Location; }
            set { Para_Service_Location = value; }
        }

        [Display(Name = "Location Type")]
        public string Location_Type { get; set; }
        [Display(Name = "Service Type")]
        [Required]
        public string Service_Prov_Type { get; set; }
        [Display(Name = "Lab/Sp Name")]
        [Required]
        public string Service_Prov_Id { get; set; }

        private string Para_Service_Prov_Location = "";
        [Display(Name = "Lab/Sp Location")]
        [Required]
        public String Service_Prov_Location
        {
            get { return Para_Service_Prov_Location; }
            set { Para_Service_Prov_Location = value; }
        }

        private string Para_Service_Prov_Contact = "";
        [Display(Name = "Lab/Sp Contact")]
        [Required]
        public String Service_Prov_Contact
        {
            get { return Para_Service_Prov_Contact; }
            set { Para_Service_Prov_Contact = value; }
        }

        
        [Display(Name = "Lab Id")]
        public int Lab_Id { get; set; }

        [Display(Name = "Lab Location")]
        public List<SelectListItem> Lab_Location { get; set; }

        //private string Para_Event_Status = "";
        [Display(Name = "Event Status")]
        [Required]
        public string Event_Status { get; set; }


        [Display(Name = "Created By")]
        public int Created_By { get; set; }

        [Display(Name = "Created On")]
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Created_On { get; set; }


        [Display(Name = "Update By")]
        public int Update_By { get; set; }

        [Display(Name = "Update On")]
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Update_On { get; set; }

        [Display(Name = "Is Deleted")]
        public int Is_Deleted { get; set; }

        public string[] Servicegroups { get; set; }
        public IEnumerable<SelectListItem> Servicelist { get; set; }
        public string[] subServices { get; set; }
        public IEnumerable<SelectListItem> subServiceslist { get; set; }
        public string Servicegroup { get; set; }

        public string subService { get; set; }

        public string Event_type { get; set; }

        public string Lab_Name { get; set; }

        public string Lab_Locations { get; set; }

        public string Lab_Contacts { get; set; }

        public string Tpa_Client { get; set; }

        public string Tpa_Client_location { get; set; }

        public string Tpa_Client_Contact { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name ="Billing Price :")]
        public decimal Billing_price { get; set; }


        public List<Billing> List_price { get; set; }
    }
}