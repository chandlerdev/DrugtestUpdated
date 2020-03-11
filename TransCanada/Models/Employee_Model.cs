// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Employee_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class Employee_Model
  {
    [Display(Name = "Id")]
    [Required(ErrorMessage = "Id Required")]
    public int Id { get; set; }

    [Display(Name = "Event id")]
    [Required(ErrorMessage = "Event Id Required")]
    public string Event_id { get; set; }

    [Display(Name = "Event Name")]
    [Required(ErrorMessage = "Event Name Required")]
    public string Event_name { get; set; }

    [Display(Name = "Client Id")]
    [Required(ErrorMessage = "Client Id Required")]
    public int Client_id { get; set; }

    [Display(Name = "client Location")]
    [Required(ErrorMessage = "Client Location Required")]
    public string clientLocation { get; set; }

    [Display(Name = "client contact")]
    [Required(ErrorMessage = "client contact Required")]
    public string clientcontact { get; set; }

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "Date Required")]
    public DateTime Start_Date { get; set; }

    [Display(Name = "End Date")]
    [Required(ErrorMessage = "Date Required")]
    public DateTime End_date { get; set; }

    [Display(Name = "Start Time")]
    [Required(ErrorMessage = "Time Required")]
    public DateTime Start_time { get; set; }

    [Display(Name = "End Time")]
    [Required(ErrorMessage = "Time Required")]
    public DateTime End_time { get; set; }

    [Display(Name = "Contact Id")]
    [Required(ErrorMessage = "Contact Id Required")]
    public int Contact_id { get; set; }

    [Display(Name = "Documents")]
    [Required(ErrorMessage = "Documents Required")]
    public string Documents { get; set; }

    [Display(Name = "Notes")]
    [Required(ErrorMessage = "Notes Required")]
    public string Notes { get; set; }

    [Display(Name = "Responsibilty")]
    [Required(ErrorMessage = "Responsibilty Required")]
    public string Responsibilty { get; set; }

    [Display(Name = "Location")]
    [Required(ErrorMessage = "Location Required")]
    public string Location { get; set; }

    [Display(Name = "Contact")]
    [Required(ErrorMessage = "Contact Required")]
    public string Contact { get; set; }
  }
}
