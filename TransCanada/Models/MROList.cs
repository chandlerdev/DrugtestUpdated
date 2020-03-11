// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.MROList
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.ComponentModel.DataAnnotations;

namespace TransCanadaDemo.Models
{
  public class MROList
  {
    [Display(Name = "MRO Company Name")]
    public string MROCompanyName { get; set; }

    [Display(Name = "Full Name")]
    public string ContactFullName1 { get; set; }

    [Display(Name = "Job Title")]
    public string ContactJobTitle1 { get; set; }

    [Display(Name = "Office Number")]
    public string ContactOfficeNumber1 { get; set; }

    [Display(Name = "Cell Phone")]
    public string ContactCellPhone1 { get; set; }

    [Display(Name = "Fax")]
    public string ContactFax1 { get; set; }

    [Display(Name = "Email")]
    public string ContactEmail1 { get; set; }

    [Display(Name = "Full Name")]
    public string ContactFullName2 { get; set; }

    [Display(Name = "Job Title")]
    public string ContactJobTitle2 { get; set; }

    [Display(Name = "Office Number")]
    public string ContactOfficeNumber2 { get; set; }

    [Display(Name = "Cell Phone")]
    public string ContactCellPhone2 { get; set; }

    [Display(Name = "Fax")]
    public string ContactFax2 { get; set; }

    [Display(Name = "Email")]
    public string ContactEmail2 { get; set; }

    [Display(Name = "Full Name")]
    public string ContactFullName3 { get; set; }

    [Display(Name = "Job Title")]
    public string ContactJobTitle3 { get; set; }

    [Display(Name = "Office Number")]
    public string ContactOfficeNumber3 { get; set; }

    [Display(Name = "Cell Phone")]
    public string ContactCellPhone3 { get; set; }

    [Display(Name = "Fax")]
    public string ContactFax3 { get; set; }

    [Display(Name = "Email")]
    public string ContactEmail3 { get; set; }

    [Display(Name = "Where to Send Documents")]
    public string WhereToSendDocuments { get; set; }

    [Display(Name = "CCF’s ")]
    public string CCFs { get; set; }

    [Display(Name = "BAT’s")]
    public string BATs { get; set; }

    [Display(Name = "Physicals")]
    public string Physicals { get; set; }
  }
}
