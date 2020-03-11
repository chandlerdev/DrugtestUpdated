// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Events_mdl
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class Events_mdl
  {
    [Display(Name = "ID")]
    [Required(ErrorMessage = "Event id required")]
    public int Id { get; set; }

    [Display(Name = "Event id")]
    [Required(ErrorMessage = "Event Eventid required")]
    public string Eventid { get; set; }

    [Display(Name = "Eventbilling id")]
    [Required(ErrorMessage = "Event Eventbillingid required")]
    public string Eventbillingid { get; set; }

    [Display(Name = "Main service")]
    [Required(ErrorMessage = "Event Main_service required")]
    public string Main_service { get; set; }

    [Display(Name = "Sub services")]
    [Required(ErrorMessage = "Event Sub_services required")]
    public string Sub_services { get; set; }

    [Display(Name = "Cost")]
    [Required(ErrorMessage = "Event Cost required")]
    public string Cost { get; set; }

    [Display(Name = "Billing cost")]
    [Required(ErrorMessage = "Event Billing_cost required")]
    public string Billing_cost { get; set; }
  }
}
