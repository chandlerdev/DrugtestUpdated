// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.Labs
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanadaDemo.Models
{
  public class Labs
  {
    [Display(Name = "Lab ID")]
    public int Lab_Id { get; set; }

    [Display(Name = "CreatedOn")]
    public DateTime CreatedOn { get; set; }

    [Display(Name = "UpdatedOn")]
    public DateTime UpdatedOn { get; set; }

    [Display(Name = "Accounts ID")]
    public string AccountsId { get; set; }

    [Display(Name = "Createdby")]
    public string Createdby { get; set; }

    [Display(Name = "Updatedby")]
    public string Updatedby { get; set; }

    [Display(Name = "IsDeleted")]
    public string IsDeleted { get; set; }

    [Required]
    [Display(Name = "Lab Name")]
    [StringLength(100)]
    public string LabsLabNameLabLocation { get; set; }

    [Display(Name = "Labs Address")]
    public string LabsStreetAddress { get; set; }

    [Display(Name = "Labs City")]
    public string LabsCity { get; set; }

    [Display(Name = "Labs State")]
    public List<SelectListItem> LabsState1 { get; set; }

    public string LabsState { get; set; }

    [Display(Name = "Labs Zip")]
    public string LabsZip { get; set; }

    [Display(Name = "Labs Number")]
    public string LabsMainNumber { get; set; }

    [Display(Name = "Labs Notes")]
    public string LabsNotes { get; set; }

    [Display(Name = "Full Name")]
    public string ContactFullName1 { get; set; }

    [Display(Name = "Job Function")]
    public string ContactJobFunction1 { get; set; }

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

    [Display(Name = "Job Function")]
    public string ContactJobFunction2 { get; set; }

    [Display(Name = "Office Number")]
    public string ContactOfficeNumber2 { get; set; }

    [Display(Name = "Cell Phone")]
    public string ContactCellPhone2 { get; set; }

    [Display(Name = "Fax")]
    public string ContactFax2 { get; set; }

    [Display(Name = "Email")]
    public string ContactEmail2 { get; set; }

    [Display(Name = "Attache CCF")]
    public List<SelectListItem> LAAttacheacopyofaCCF1 { get; set; }

    public string LAAttacheacopyofaCCF { get; set; }

    [Display(Name = "Lab")]
    public string LALab1 { get; set; }

    [Display(Name = "Account Number")]
    public string LAAccountNumber1 { get; set; }

    [Display(Name = "Pannel")]
    public string LAPannel1 { get; set; }

    [Display(Name = "TPA")]
    public string LATPA1 { get; set; }

    [Display(Name = "MRO")]
    public List<SelectListItem> LAMRO11 { get; set; }

    public string LAMRO1 { get; set; }

    [Display(Name = "Sample Type")]
    public List<SelectListItem> LASampleType11 { get; set; }

    public string LASampleType1 { get; set; }

    [Display(Name = "Attachment")]
    public string LAAttachment1 { get; set; }

    [Display(Name = "CCFs")]
    public List<SelectListItem> LACCFs11 { get; set; }

    public string LACCFs1 { get; set; }

    [Display(Name = "Lab")]
    public string LALab2 { get; set; }

    [Display(Name = "Account Number")]
    public string LAAccountNumber2 { get; set; }

    [Display(Name = "Pannel")]
    public string LAPannel2 { get; set; }

    [Display(Name = "TPA")]
    public string LATPA2 { get; set; }

    [Display(Name = "MRO")]
    public List<SelectListItem> LAMRO21 { get; set; }

    public string LAMRO2 { get; set; }

    [Display(Name = "Sample Type")]
    public List<SelectListItem> LASampleType21 { get; set; }

    public string LASampleType2 { get; set; }

    [Display(Name = "Attachment")]
    public string LAAttachment2 { get; set; }

    [Display(Name = "CCFs")]
    public List<SelectListItem> LACCFs21 { get; set; }

    public string LACCFs2 { get; set; }

    [Display(Name = "Lab")]
    public string LALab3 { get; set; }

    [Display(Name = "Account Number")]
    public string LAAccountNumber3 { get; set; }

    [Display(Name = "Pannel")]
    public string LAPannel3 { get; set; }

    [Display(Name = "TPA")]
    public string LATPA3 { get; set; }

    [Display(Name = "MRO")]
    public List<SelectListItem> LAMRO31 { get; set; }

    public string LAMRO3 { get; set; }

    [Display(Name = "Sample Type")]
    public List<SelectListItem> LASampleType31 { get; set; }

    public string LASampleType3 { get; set; }

    [Display(Name = "Attachment")]
    public string LAAttachment3 { get; set; }

    [Display(Name = "CCFs")]
    public List<SelectListItem> LACCFs31 { get; set; }

    public string LACCFs3 { get; set; }
  }
}
