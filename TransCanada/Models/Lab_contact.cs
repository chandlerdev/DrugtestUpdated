// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Lab_contact
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
    public class Lab_contact
    {
        [Display(Name = "Name")]
        public string Location_Name { get; set; }
        public string Address_1 { get; set; }
        public int contact_id { get; set; }

        public string location_id { get; set; }
        [EmailAddress]
        [Display(Name = "Secondary Email")]
        public string Email1 { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Secondary Phone")]
        public string Phone1 { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstname { get; set; }

        //[Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        //[Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile")]
        public string cell { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Office Phone")]
        public string officephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Third Phone")]
        public string Third_Phone { get; set; }

        public int Sp_location_id { get; set; }

        public int Sp_contact_id { get; set; }
    }
}
