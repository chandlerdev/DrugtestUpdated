// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Employeetolab
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
  public class Employeetolab
  {
    [Required]
    public string Labname { get; set; }

    [Required]
    public string EmployeeName { get; set; }

    [Required]
    [Display(Name = "City")]
    public string Location { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string Address { get; set; }

    public List<SelectListItem> EmployeeNameList { get; set; }

    public List<SelectListItem> LabNameList { get; set; }

    public List<SelectListItem> LocationList { get; set; }

    public List<SelectListItem> AddressList { get; set; }

    public List<SelectListItem> StateList { get; set; }

    public int id { get; set; }
  }
}
