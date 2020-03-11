// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.EmployeeLocation
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
  public class EmployeeLocation
  {
    [Display(Name = "ID")]
    [Required]
    public int Id { get; set; }

    [Display(Name = "Employee Name")]
    [Required]
    public string Employee_Name { get; set; }

    [Display(Name = "Employee Test")]
    [Required]
    public string Employee_Test { get; set; }

    [Display(Name = "Address 1")]
    [Required]
    public string Address_1 { get; set; }

    [Display(Name = "Address 2")]
    [Required]
    public string Address_2 { get; set; }

    [Display(Name = "city")]
    [Required]
    public string city { get; set; }

    [Display(Name = "State")]
    [Required]
    public string State { get; set; }

    [Display(Name = "Country")]
    [Required]
    public string Country { get; set; }

    [Display(Name = "Email")]
    [Required]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
    public string Email { get; set; }

    [Display(Name = "Phone")]
    [Required]
    public string Phone { get; set; }

    [Display(Name = "Mobile")]
    [Required]
    public string Mobile { get; set; }

    [Display(Name = "Priority")]
    [Required]
    public string Priority { get; set; }

    public List<SelectListItem> Cities { get; set; }
  }
}
