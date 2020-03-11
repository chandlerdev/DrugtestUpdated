// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.TPAs
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanadaDemo.Models
{
  public class TPAs
  {
    private bool formfox = true;
    private bool wantustohouseccfs = true;

    [Display(Name = "Name")]
    public string TPANameName { get; set; }

    [Display(Name = "Street Address")]
    public string TPANameStreetAddress { get; set; }

    [Display(Name = "City")]
    public string TPANameCity { get; set; }

    [Display(Name = "State")]
    public List<SelectListItem> TPANameState { get; set; }

    [Display(Name = "Zip")]
    public string TPANameZip { get; set; }

    [Display(Name = "Main Number")]
    public string TPANameMainNumber { get; set; }

    [Display(Name = "Full Name")]
    public string SchedulingContactFullName { get; set; }

    [Display(Name = "Title")]
    public string SchedulingContactJobTitle { get; set; }

    [Display(Name = "Office Number")]
    public string SchedulingContactOfficeNumber { get; set; }

    [Display(Name = "Cell Phone")]
    public string SchedulingContactCellPhone { get; set; }

    [Display(Name = "Fax")]
    public string SchedulingContactFax { get; set; }

    [Display(Name = "Email")]
    public string SchedulingContactEmail { get; set; }

    [Display(Name = "Full Name")]
    public string BillingContactFullName { get; set; }

    [Display(Name = "Title")]
    public string BillingContactJobTitle { get; set; }

    [Display(Name = "Office Number")]
    public string BillingContactOfficeNumber { get; set; }

    [Display(Name = "Cell Phone")]
    public string BillingContactCellPhone { get; set; }

    [Display(Name = "Fax")]
    public string BillingContactFax { get; set; }

    [Display(Name = "Email")]
    public string BillingContactEmail { get; set; }

    [Display(Name = "Bill TPA or Client")]
    public List<SelectListItem> BillTPAorClient { get; set; }

    [Display(Name = "Full Name")]
    public string SalesPersonFullName { get; set; }

    [Display(Name = "Title")]
    public string SalesPersonTitle { get; set; }

    [Display(Name = "Office Number")]
    public string SalesPersonOfficeNumber { get; set; }

    [Display(Name = "Cell Phone")]
    public string SalesPersonCellPhone { get; set; }

    [Display(Name = "Fax")]
    public string SalesPersonFax { get; set; }

    [Display(Name = "Email")]
    public string SalesPersonEmail { get; set; }

    [Display(Name = "Form Fox")]
    public bool FormFox
    {
      get
      {
        return this.formfox;
      }
      set
      {
        this.formfox = value;
      }
    }

    [Display(Name = "Want us to House CCF's")]
    public bool WantustoHouseCCFs
    {
      get
      {
        return this.wantustohouseccfs;
      }
      set
      {
        this.wantustohouseccfs = value;
      }
    }

    [Display(Name = "Contact the CTPA  with ANY:")]
    public string ContacttheCTPAwithANY { get; set; }

    [Display(Name = "Shy Bladders Lung with no sample")]
    public string ShyBladdersLungwithnosample { get; set; }

    [Display(Name = "Refusal to Test")]
    public string RefusaltoTest { get; set; }

    [Display(Name = "BAT wConfirmed Positive")]
    public string BATwConfirmedPositive { get; set; }

    [Display(Name = "Cancelled or Incomplete Tests")]
    public string CancelledorIncompleteTests { get; set; }

    [Display(Name = "Authorization Forms Sent Via:")]
    public string AuthorizationFormsSentVia { get; set; }

    [Display(Name = "Company Name")]
    public string MROCompanyName { get; set; }

    [Display(Name = "Full Name")]
    public string MROFullName { get; set; }

    [Display(Name = "Job Title")]
    public string MROJobTitle { get; set; }

    [Display(Name = "Office Number")]
    public string MROOfficeNumber { get; set; }

    [Display(Name = "Cell Phone")]
    public string MROCellPhone { get; set; }

    [Display(Name = "Fax")]
    public string MROFax { get; set; }

    [Display(Name = "Email")]
    public string MROEmail { get; set; }

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
