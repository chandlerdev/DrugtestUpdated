// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.PatientDonor_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanadaDemo.Models
{
  public class PatientDonor_Model
  {
    [Display(Name = "Full Name")]
    [StringLength(150)]
    public string FullName { get; set; }

    [Display(Name = "Job Title")]
    [StringLength(200)]
    public string JobTitle { get; set; }

    [Display(Name = "Client")]
    [StringLength(150)]
    public string Client { get; set; }

    [Display(Name = "SSNumber")]
    [StringLength(150)]
    public string SSNumber { get; set; }

    [Display(Name = "Birth Date")]
    public DateTime Birthdate { get; set; }

    [Display(Name = "Age")]
    public Decimal Age { get; set; }

    [Display(Name = "Mobile")]
    [StringLength(15)]
    public string Mobile { get; set; }

    [Display(Name = "Alt Phone")]
    [StringLength(15)]
    public string AltPhone { get; set; }

    [Display(Name = "SMS Address")]
    [StringLength(150)]
    public string SMSAddress { get; set; }

    [Display(Name = "Email | Personal")]
    [StringLength(150)]
    public string EmailPersonal { get; set; }

    [Display(Name = "Email | Business")]
    [StringLength(150)]
    public string EmailBusiness { get; set; }

    [Display(Name = "Address")]
    [StringLength(250)]
    public string Address { get; set; }

    [Display(Name = "Connect to ShareFile Folder")]
    [StringLength(500)]
    public string ConnecttoShareFileFolder { get; set; }

    [Display(Name = "Medical Information")]
    [StringLength(500)]
    public string MedicalInformation { get; set; }

    [Display(Name = "Clinic Notes")]
    [StringLength(500)]
    public string ClinicNotes { get; set; }

    [Display(Name = "DOT Mode:")]
    public List<SelectListItem> DOTMode { get; set; }

    [Display(Name = "Show Expiration of:")]
    public List<SelectListItem> ShowExpirationOf { get; set; }

    [Display(Name = "Expiration Date:")]
    public DateTime ExpirationDate { get; set; }

    [Display(Name = "Show all Testing Event")]
    [StringLength(500)]
    public string ShowallTestingEvent { get; set; }
  }
}
