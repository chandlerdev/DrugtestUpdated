// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.Partner
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanadaDemo.Models
{
  public class Partner
  {
    [Display(Name = "Name of Clinic")]
    public string PartnerNameNameName { get; set; }

    [Display(Name = "Street Address")]
    public string PartnerNameStreetAddress { get; set; }

    [Display(Name = "City")]
    public string PartnerNameCity { get; set; }

    [Display(Name = "State")]
    public List<SelectListItem> PartnerNameState { get; set; }

    [Display(Name = "Zip")]
    public string PartnerNameZip { get; set; }

    [Display(Name = "Main Number")]
    public string PartnerNameMainNumber { get; set; }

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

    [Display(Name = "DOT Breath Alcohol Test")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingDOTBreathAlcoholTest { get; set; }

    [Display(Name = "DOT BAT Confirmation")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingDOTBATConfirmation { get; set; }

    [Display(Name = "Non-DOT BAT")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingNonDOTBAT { get; set; }

    [Display(Name = "Non-DOT DOT Confirmation")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingNonDOTDOTConfirmation { get; set; }

    [Display(Name = "DOT Like Panel Rapid")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingDOTLikePanelRapid { get; set; }

    [Display(Name = "12 Panel Rapid")]
    [DataType(DataType.Currency)]
    public float? PartnerPricing12PanelRapid { get; set; }

    [Display(Name = "Collection only DOT Urine")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingCollectiononlyDOTUrine { get; set; }

    [Display(Name = "Collection only Non-DOT Urine")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingCollectiononlyNonDOTUrine { get; set; }

    [Display(Name = "Collection only Hair")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingCollectiononlyHair { get; set; }

    [Display(Name = "Collection only Oral Fluid")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingCollectiononlyOralFluid { get; set; }

    [Display(Name = "Cost for oral fluid devices")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingCostfororalfluiddevices { get; set; }

    [Display(Name = "Alter Chain of Custody Form")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingAlterChainofCustodyForm { get; set; }

    [Display(Name = "Direct Observation Fee")]
    [DataType(DataType.Currency)]
    public float? PartnerPricingDirectObservationFee { get; set; }

    [Display(Name = "Blood Draw")]
    [DataType(DataType.Currency)]
    public float? LaboratoryAnalysisBloodDraw { get; set; }

    [Display(Name = "Urinalysis Panel 5463")]
    [DataType(DataType.Currency)]
    public float? LaboratoryAnalysisUrinalysisPanel5463 { get; set; }

    [Display(Name = "CBC Blood Panel 6399")]
    [DataType(DataType.Currency)]
    public float? LaboratoryAnalysisCBCBloodPanel6399 { get; set; }

    [Display(Name = "Lead Blood Panel 599")]
    [DataType(DataType.Currency)]
    public float? LaboratoryAnalysisLeadBloodPanel599 { get; set; }

    [Display(Name = "Heavy Metals Panel 7655")]
    [DataType(DataType.Currency)]
    public float? LaboratoryAnalysisHeavyMetalsPanel7655 { get; set; }

    [Display(Name = "Flu Shot")]
    [DataType(DataType.Currency)]
    public float? VaccinesTBServicesFluShot { get; set; }

    [Display(Name = "Hepatitis A")]
    [DataType(DataType.Currency)]
    public float? VaccinesTBServicesHepatitisA { get; set; }

    [Display(Name = "Hepatitis B")]
    [DataType(DataType.Currency)]
    public float? VaccinesTBServicesHepatitisB { get; set; }

    [Display(Name = "Hepatitis C")]
    [DataType(DataType.Currency)]
    public float? VaccinesTBServicesHepatitisC { get; set; }

    [Display(Name = "TBTestPPD1StepTest)")]
    [DataType(DataType.Currency)]
    public float? VaccinesTBServicesTBTestPPD1StepTest { get; set; }

    [Display(Name = "TBTestPPD2StepTest)")]
    [DataType(DataType.Currency)]
    public float? VaccinesTBServicesTBTestPPD2StepTest { get; set; }
  }
}
