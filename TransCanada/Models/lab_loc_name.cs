// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.lab_loc_name
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace TransCanada.Models
{
    public class lab_loc_name
    {
        [Required]
        [Display(Name = "Lab/Manufacturer Name")]
        public string Lab_Name { get; set; }
        [Required]
        [Display(Name = "Product Services")]
        public string[] Lab_Type { get; set; }

        public string LabType { get; set; }
        public List<SelectListItem> RemainProducts { get; set; }
        public string Id { get; set; }
        [Required]
        [Display(Name = "Lab Type")]
        public string Type { get; set; }
    }

    public class ServiceProd
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Product Services")]
        public string LabType { get; set; }
    }
}
