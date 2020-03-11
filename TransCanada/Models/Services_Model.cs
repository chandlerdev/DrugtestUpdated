// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.Services_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanadaDemo.Models
{
  public class Services_Model
  {
    [Display(Name = "Lab Name")]
    public List<SelectListItem> LabInfo_LabName { get; set; }

    [Display(Name = "Lab Account")]
    public string LabInfo_LabAccount { get; set; }

    [Display(Name = "Lab Panel")]
    public string LabInfo_LabPanel { get; set; }

    [Display(Name = "Company Name")]
    public List<SelectListItem> MRO_CompanyName { get; set; }

    [Display(Name = "MRO Name")]
    public string MRO_MRO_Name { get; set; }

    [Display(Name = "Address")]
    public string MRO_Address { get; set; }

    [Display(Name = "Primary Contact Person")]
    public string MRO_PrimaryContactPerson { get; set; }

    [Display(Name = "Phone Number with Extension")]
    public string MRO_PhoneNumberwithExtension { get; set; }

    [Display(Name = "Fax")]
    public string MRO_Fax { get; set; }

    [Display(Name = "Email")]
    public string MRO_Email { get; set; }

    [Display(Name = "Send CCF's")]
    public string MRO_SendCCF { get; set; }

    [Display(Name = "Send BAT's")]
    public string MRO_SendBAT { get; set; }

    [Display(Name = "Collections Only:")]
    public string CollectionsOnly { get; set; }

    [Display(Name = "DOT or Non DOT Urine Collection:")]
    public string ColOnly_DOTorNonDOTUrineCollection { get; set; }

    [Display(Name = "Oral Fluid Collection | not including devices - $4 each:")]
    public string ColOnly_OralFluidCollection { get; set; }

    [Display(Name = "Hair Collection:")]
    public string ColOnly_HairCollection { get; set; }

    [Display(Name = "Breath Alcohol Test - DOT Protocol:")]
    public string ColOnly_BreathAlcoholTest_DOTProtocol { get; set; }

    [Display(Name = "Breath Alcohol Test - Zero Tolerance:")]
    public string ColOnly_BreathAlcoholTest_ZeroTolerance { get; set; }

    [Display(Name = "Breath Alcohol Test - .08 BAC = Positive:")]
    public string ColOnly_BreathAlcoholTest_08_BAC_Positive { get; set; }

    [Display(Name = "DOT Breath Alcohol Test - DOT Protocol:")]
    public string ColOnly_DOTBreathAlcoholTest_DOTProtocol { get; set; }

    [Display(Name = "DOT Breath Alcohol Test - Zero Tolerance:")]
    public string ColOnly_DOTBreathAlcoholTest_ZeroTolerance { get; set; }

    [Display(Name = "Instant DT | 9 Panel with/ Expanded OPI:")]
    public string ColOnly_Instant_DT_9_Panel_with_Expanded_OPI { get; set; }

    [Display(Name = "Instant DT | 10 Panel:")]
    public string ColOnly_Instant_DT_10_Panel { get; set; }

    [Display(Name = "Instant DT | 12 Panel:")]
    public string ColOnly_Instant_DT_12_Panel { get; set; }

    [Display(Name = "Instant - Lab Confirmation - Per Drug:")]
    public string ColOnly_Instant_Lab_Confirmation_Per_Drug { get; set; }

    [Display(Name = "Refusal to Test Administration Fee:")]
    public string ColOnly_Refusal_to_Test_Administration_Fee { get; set; }

    [Display(Name = "Altering Chain of Custody Fee:")]
    public string ColOnly_Altering_Chain_of_Custody_Fee { get; set; }

    [Display(Name = "Direct Observation:")]
    public string ColOnly_Direct_Observation { get; set; }

    [Display(Name = "Sheduled On-Site Fee Structure:")]
    public string SheduledOnSiteFeeStructure { get; set; }

    [Display(Name = "Travel time, when more than 1.5 hour round trip:")]
    public string ShedOn_TravelTimeWhen_moreThan_15_Hour_Round_Trip { get; set; }

    [Display(Name = " Business Hours 8am - 5pm | Monday thru Friday:")]
    public string ShedOn_Business_Hours_8am_5pm_Monday_thru_Friday { get; set; }

    [Display(Name = " After Hours 5pm - 8am | Monday thru Friday, Weekends & Holidays:")]
    public string ShedOn_After_Hours_5pm_8am_Monday_thru_Friday_Weekends_Holidays { get; set; }

    [Display(Name = " Miles Charged Round Trip:")]
    public string ShedOn_Miles_Charged_Round_Trip { get; set; }

    [Display(Name = " Wait Time:")]
    public string ShedOn_Wait_Time { get; set; }

    [Display(Name = "Unscheduled Event Fee Structure:")]
    public string UnscheduledEventFeeStructure { get; set; }

    [Display(Name = "Minimum TWO HOURS per Event | Cancellation Fee $100 + Mileage:")]
    public string Unsched_Minimum_TWO_HOURS_per_event { get; set; }

    [Display(Name = "Business Hours 8am - 5pm | Monday thru Friday:")]
    public string Unsched_Business_Hours_8am_5pm { get; set; }

    [Display(Name = "After Hours 5pm - 8am | Monday thru Friday, Weekends:")]
    public string Unsched_After_Hours_5pm_8am { get; set; }

    [Display(Name = "Holidays:")]
    public string Unsched_Holidays { get; set; }

    [Display(Name = "Mileage Fee Per Mile – Current US Government Rate:")]
    public string Unsched_Mileage_Fee_Per_Mile { get; set; }

    [Display(Name = "Occupational Medicine:")]
    public string OccupationalMedicine { get; set; }

    [Display(Name = "DOT Physical:")]
    public string OccupMed_DOT_Physical { get; set; }

    [Display(Name = "Standard Physical:")]
    public string OccupMed_Standard_Physical { get; set; }

    [Display(Name = "Heavy Lift Physical:")]
    public string OccupMed_Heavy_Lift_Physical { get; set; }

    [Display(Name = "Audiograms:")]
    public string OccupMed_Audiograms { get; set; }

    [Display(Name = "Fit Test Medical (OSHA) Questionnaire:")]
    public string OccupMed_Fit_Test_Medical { get; set; }

    [Display(Name = "Pulmonary Function Test:")]
    public string OccupMed_Pulmonary_Function_Test { get; set; }

    [Display(Name = "Respirator Fit Test (per mask):")]
    public string OccupMed_Respirator_Fit_Test { get; set; }

    [Display(Name = "EKG:")]
    public string OccupMed_EKG { get; set; }

    [Display(Name = "Tuberculosis Test (TB) – Skin Test:")]
    public string OccupMed_Tuberculosis_Test_Skin_Test { get; set; }

    [Display(Name = "Flu Shot:")]
    public string OccupMed_Flu_Shot { get; set; }

    [Display(Name = "Blood Draw:")]
    public string OccupMed_Blood_Draw { get; set; }
  }
}
