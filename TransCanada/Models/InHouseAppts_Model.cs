// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.InHouseAppts_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.ComponentModel.DataAnnotations;

namespace TransCanadaDemo.Models
{
  public class InHouseAppts_Model
  {
    [Display(Name = "Patient")]
    [StringLength(150)]
    public string Patient { get; set; }

    [Display(Name = "Client Associated")]
    [StringLength(150)]
    public string ClientAssociated { get; set; }

    [Display(Name = "Reason For Test")]
    [StringLength(150)]
    public string ReasonForTest { get; set; }

    [Display(Name = "Services Completed")]
    [StringLength(150)]
    public string ServicesCompleted { get; set; }

    [Display(Name = "Clinic Completed At")]
    [StringLength(150)]
    public string ClinicCompletedAt { get; set; }

    [Display(Name = "Patient Event")]
    [StringLength(150)]
    public string PatientEvent { get; set; }

    [Display(Name = "Client")]
    [StringLength(150)]
    public string Client { get; set; }

    [Display(Name = "Patient Full Name")]
    [StringLength(150)]
    public string PatientFullName { get; set; }

    [Display(Name = "Reason")]
    [StringLength(150)]
    public string Reason { get; set; }

    [Display(Name = "Cleared by Medical Examiner")]
    [StringLength(150)]
    public string ClearedByMedicalExaminer { get; set; }

    [Display(Name = "Form Attached")]
    [StringLength(150)]
    public string FormAttached { get; set; }

    [Display(Name = "Scheduled")]
    public DateTime Scheduled { get; set; }

    [Display(Name = "Testing Completed")]
    public DateTime TestingCompleted { get; set; }

    [Display(Name = "Clinic Authorization Sent")]
    public DateTime ClinicAuthorizationSent { get; set; }

    [Display(Name = "Client Authorization Sent")]
    public DateTime ClientAuthorizationSent { get; set; }

    [Display(Name = "Notes:")]
    [StringLength(150)]
    public string Notes { get; set; }
  }
}
