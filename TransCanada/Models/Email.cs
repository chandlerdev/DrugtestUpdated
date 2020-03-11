// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Email
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class Email
  {
    [Required]
    [Display(Name = "LabName")]
    public string _LabName { get; set; }

    [Required]
    [Display(Name = "EmployeeName")]
    public string _EmpName { get; set; }
  }
}
