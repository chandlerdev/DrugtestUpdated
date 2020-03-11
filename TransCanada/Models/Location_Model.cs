// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.Location_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanadaDemo.Models
{
  public class Location_Model
  {
    private string locations_locationname = "";
    private string locations_streetaddress = "";
    private string locations_city = "";
    private string locations_zip = "";
    private string locations_main_number = "";
    private string locations_notes = "";
    private string conatctwithany = "Dev Chandler";
    private string clientsample = "Sample";
    private string refusaltotest = "";
    private string batconfirmedpostive = "";
    private string cancelledorincompletetests = "";
    private string authorizationformssentvia = "";
    private string contact_fullname = "";
    private string contact_jobtitle = "";
    private string contact_officenumber = "";
    private string contact_cellphone = "";
    private string contact_fax = "";
    private string contact_email = "";
    private string billingcontactfullname = "";
    private string billingcontactofficenumber = "";
    private string billingcontactfax = "";
    private string billingcontactemail = "";
    private string billingcontactstreetaddress = "";
    private string billingcontactcity = "";
    private string billingcontactzip = "";
    private string billingcontactemailinvoices = "";
    private string billingcontactnotes = "";
    private string billingcontactbillingoptions = "";
    private string labaccattachecopyccf = "";
    private string labaccaccountnumber1 = "";
    private string labaccpannel = "";
    private string labacctpa1 = "";
    private string labaccmro1 = "";
    private string labaccattachment1 = "";
    private string labaccaccountnumber2 = "";
    private string labaccpannel2 = "";
    private string labacctpa2 = "";
    private string labaccmro2 = "";
    private string labaccattachment2 = "";
    private string labaccaccountnumber3 = "";
    private string labaccpannel3 = "";
    private string labacctpa3 = "";
    private string labaccmro3 = "";
    private string labaccattachment3 = "";
    private string servicesprovided = "";
    private string sppreemployment = "";
    private string sprandom = "";
    private string sppostaccident = "";
    private string spreasonablesuspicion = "";
    private string spfollowup = "";
    private string spreturntoduty = "";
    private string spannual = "";
    private string spnegativedilute = "";
    private string rptpoolname = "";
    private string rptowner = "";
    private string rptpoolmanager = "";
    private bool rtpalcoholgetsdrug = true;
    private string rtpselectionlevelfordrug = "";
    private string rtppercent1 = "";
    private string rtpselectionlevelforalcohol = "";
    private string rtppercent2 = "";
    private string rtpalternatesfordrug = "";
    private string rtppercent3 = "";
    private string rtpalternatesforalcohol = "";
    private string rtppercent4 = "";
    private string rtpnotes = "";
    private string shybladderslugwithno;

    [Display(Name = "Location Id")]
    public int Location_Id { get; set; }

    [Display(Name = "Created By")]
    public string Createdby { get; set; }

    [Display(Name = "Created On")]
    public DateTime Createdon { get; set; }

    [Display(Name = "Updated By")]
    public string Updatedby { get; set; }

    [Display(Name = "Updated On")]
    public DateTime Updatedon { get; set; }

    [Display(Name = "Deleted")]
    public string IsDeleted { get; set; }

    [Required]
    [Display(Name = "Location Name")]
    public string Locations_LocationName
    {
      get
      {
        return this.locations_locationname;
      }
      set
      {
        this.locations_locationname = value;
      }
    }

    [Display(Name = "Street Address")]
    [Required]
    public string Locations_StreetAddress
    {
      get
      {
        return this.locations_streetaddress;
      }
      set
      {
        this.locations_streetaddress = value;
      }
    }

    [Display(Name = "City")]
    [Required]
    public string Locations_City
    {
      get
      {
        return this.locations_city;
      }
      set
      {
        this.locations_city = value;
      }
    }

    [Display(Name = "State")]
    public List<SelectListItem> Locations_State1 { get; set; }

    [Display(Name = "State")]
    [Required]
    public string Locations_State { get; set; }

    [Display(Name = "Zip")]
    [Required]
    public string Locations_Zip
    {
      get
      {
        return this.locations_zip;
      }
      set
      {
        this.locations_zip = value;
      }
    }

    [Display(Name = "Main Number")]
    [Required]
    public string Locations_Main_Number
    {
      get
      {
        return this.locations_main_number;
      }
      set
      {
        this.locations_main_number = value;
      }
    }

    [Display(Name = "Notes")]
    public string Locations_Notes
    {
      get
      {
        return this.locations_notes;
      }
      set
      {
        this.locations_notes = value;
      }
    }

    [Display(Name = "Conatct With ANY")]
    [StringLength(50)]
    [Required]
    public string ConatctWithANY
    {
      get
      {
        return this.conatctwithany;
      }
      set
      {
        this.conatctwithany = value;
      }
    }

    [Display(Name = "Shy Bladders Lug With No")]
    public string ShyBladdersLugWithNo
    {
      get
      {
        return this.shybladderslugwithno;
      }
      set
      {
        this.shybladderslugwithno = value;
      }
    }

    [Display(Name = "Sample")]
    public string ClientSample
    {
      get
      {
        return this.clientsample;
      }
      set
      {
        this.clientsample = value;
      }
    }

    [Display(Name = "Refusal to Test")]
    public string RefusalToTest
    {
      get
      {
        return this.refusaltotest;
      }
      set
      {
        this.refusaltotest = value;
      }
    }

    [Display(Name = "Bat w/Confirmed Postive")]
    public string BatConfirmedPostive
    {
      get
      {
        return this.batconfirmedpostive;
      }
      set
      {
        this.batconfirmedpostive = value;
      }
    }

    [Display(Name = "Cancelled Or Incomplete Tests")]
    public string CancelledOrIncompleteTests
    {
      get
      {
        return this.cancelledorincompletetests;
      }
      set
      {
        this.cancelledorincompletetests = value;
      }
    }

    [Display(Name = "Authorization Forms Sent Via")]
    public string AuthorizationFormsSentVia
    {
      get
      {
        return this.authorizationformssentvia;
      }
      set
      {
        this.authorizationformssentvia = value;
      }
    }

    [Display(Name = "Contact Full Name")]
    public string Contact_FullName
    {
      get
      {
        return this.contact_fullname;
      }
      set
      {
        this.contact_fullname = value;
      }
    }

    [Display(Name = "Contact Job Title")]
    public string Contact_JobTitle
    {
      get
      {
        return this.contact_jobtitle;
      }
      set
      {
        this.contact_jobtitle = value;
      }
    }

    [Display(Name = "Contact Office Number")]
    public string Contact_OfficeNumber
    {
      get
      {
        return this.contact_officenumber;
      }
      set
      {
        this.contact_officenumber = value;
      }
    }

    [Display(Name = "Contact Cell Phone")]
    public string Contact_CellPhone
    {
      get
      {
        return this.contact_cellphone;
      }
      set
      {
        this.contact_cellphone = value;
      }
    }

    [Display(Name = "Contact Fax")]
    public string Contact_Fax
    {
      get
      {
        return this.contact_fax;
      }
      set
      {
        this.contact_fax = value;
      }
    }

    [Display(Name = "Contact Email")]
    [Required]
    public string Contact_Email
    {
      get
      {
        return this.contact_email;
      }
      set
      {
        this.contact_email = value;
      }
    }

    [Display(Name = "i3 Screen Access")]
    public List<SelectListItem> Locations_i3ScreenAccess1 { get; set; }

    [Display(Name = "i3 Screen Access")]
    public string Locations_i3ScreenAccess { get; set; }

    [Display(Name = "Background Screening Access")]
    public List<SelectListItem> Locations_BackgroundScreeningAccess1 { get; set; }

    [Display(Name = "Background Screening Access")]
    public string Locations_BackgroundScreeningAccess { get; set; }

    [Display(Name = "Occupational Medicine Access")]
    public List<SelectListItem> Locations_OccupationalMedicineAccess1 { get; set; }

    [Display(Name = "Occupational Medicine Access")]
    public string Locations_OccupationalMedicineAccess { get; set; }

    [Display(Name = "Billing Contact Full Name")]
    public string BillingContactFullName
    {
      get
      {
        return this.billingcontactfullname;
      }
      set
      {
        this.billingcontactfullname = value;
      }
    }

    [Display(Name = "Billing Contact Office Number")]
    public string BillingContactOfficeNumber
    {
      get
      {
        return this.billingcontactofficenumber;
      }
      set
      {
        this.billingcontactofficenumber = value;
      }
    }

    [Display(Name = "Billing Contact Fax")]
    public string BillingContactFax
    {
      get
      {
        return this.billingcontactfax;
      }
      set
      {
        this.billingcontactfax = value;
      }
    }

    [Display(Name = "Billing Contact Email")]
    public string BillingContactEmail
    {
      get
      {
        return this.billingcontactemail;
      }
      set
      {
        this.billingcontactemail = value;
      }
    }

    [Display(Name = "Billing Contact Street Address")]
    public string BillingContactStreetAddress
    {
      get
      {
        return this.billingcontactstreetaddress;
      }
      set
      {
        this.billingcontactstreetaddress = value;
      }
    }

    [Display(Name = "Billing Contact City")]
    public string BillingContactCity
    {
      get
      {
        return this.billingcontactcity;
      }
      set
      {
        this.billingcontactcity = value;
      }
    }

    [Display(Name = "Billing Contact State")]
    public List<SelectListItem> BillingContactState1 { get; set; }

    [Display(Name = "Billing Contact State")]
    public string BillingContactState { get; set; }

    [Display(Name = "Billing Contact Zip")]
    public string BillingContactZip
    {
      get
      {
        return this.billingcontactzip;
      }
      set
      {
        this.billingcontactzip = value;
      }
    }

    [Display(Name = "Email Invoices")]
    public string BillingContactEmailInvoices
    {
      get
      {
        return this.billingcontactemailinvoices;
      }
      set
      {
        this.billingcontactemailinvoices = value;
      }
    }

    [Display(Name = "Notes")]
    public string BillingContactNotes
    {
      get
      {
        return this.billingcontactnotes;
      }
      set
      {
        this.billingcontactnotes = value;
      }
    }

    [Display(Name = "Billing Options")]
    public string BillingContactBillingOptions
    {
      get
      {
        return this.billingcontactbillingoptions;
      }
      set
      {
        this.billingcontactbillingoptions = value;
      }
    }

    [Display(Name = "Attache a copy of a CCF")]
    public string LabAccAttacheCopyCCF
    {
      get
      {
        return this.labaccattachecopyccf;
      }
      set
      {
        this.labaccattachecopyccf = value;
      }
    }

    [Display(Name = "Lab 1")]
    public List<SelectListItem> LabAccLab11 { get; set; }

    [Display(Name = "LabAccLab1")]
    public string LabAccLab1 { get; set; }

    [Display(Name = "Account Number 1")]
    public string LabAccAccountNumber1
    {
      get
      {
        return this.labaccaccountnumber1;
      }
      set
      {
        this.labaccaccountnumber1 = value;
      }
    }

    [Display(Name = "Pannel 1")]
    public string LabAccPannel
    {
      get
      {
        return this.labaccpannel;
      }
      set
      {
        this.labaccpannel = value;
      }
    }

    [Display(Name = "TPA 1")]
    public string LabAccTpa1
    {
      get
      {
        return this.labacctpa1;
      }
      set
      {
        this.labacctpa1 = value;
      }
    }

    [Display(Name = "MRO 1")]
    public string LabAccMro1
    {
      get
      {
        return this.labaccmro1;
      }
      set
      {
        this.labaccmro1 = value;
      }
    }

    [Display(Name = "Sample Type 1")]
    public List<SelectListItem> LabAccSampleType11 { get; set; }

    [Display(Name = "Sample Type 1")]
    public string LabAccSampleType1 { get; set; }

    [Display(Name = "Attachment 1")]
    public string LabAccAttachment1
    {
      get
      {
        return this.labaccattachment1;
      }
      set
      {
        this.labaccattachment1 = value;
      }
    }

    [Display(Name = "CCF 1")]
    public List<SelectListItem> LabAccCcf11 { get; set; }

    [Display(Name = "CCF 1")]
    public string LabAccCcf1 { get; set; }

    [Display(Name = "Lab 2")]
    public List<SelectListItem> LabAccLab21 { get; set; }

    [Display(Name = "Lab 2")]
    public string LabAccLab2 { get; set; }

    [Display(Name = "Account Number 2")]
    public string LabAccAccountNumber2
    {
      get
      {
        return this.labaccaccountnumber2;
      }
      set
      {
        this.labaccaccountnumber2 = value;
      }
    }

    [Display(Name = "Pannel 2")]
    public string LabAccPannel2
    {
      get
      {
        return this.labaccpannel2;
      }
      set
      {
        this.labaccpannel2 = value;
      }
    }

    [Display(Name = "TPA 2")]
    public string LabAccTpa2
    {
      get
      {
        return this.labacctpa2;
      }
      set
      {
        this.labacctpa2 = value;
      }
    }

    [Display(Name = "MRO 2")]
    public string LabAccMro2
    {
      get
      {
        return this.labaccmro2;
      }
      set
      {
        this.labaccmro2 = value;
      }
    }

    [Display(Name = "Sample Type 2")]
    public List<SelectListItem> LabAccSampleType21 { get; set; }

    [Display(Name = "Sample Type 2")]
    public string LabAccSampleType2 { get; set; }

    [Display(Name = "Attachment 2")]
    public string LabAccAttachment2
    {
      get
      {
        return this.labaccattachment2;
      }
      set
      {
        this.labaccattachment2 = value;
      }
    }

    [Display(Name = "CCF 2")]
    public List<SelectListItem> LabAccCcf21 { get; set; }

    [Display(Name = "CCF 2")]
    public string LabAccCcf2 { get; set; }

    [Display(Name = "Lab 3")]
    public List<SelectListItem> LabAccLab31 { get; set; }

    [Display(Name = "Lab 3")]
    public string LabAccLab3 { get; set; }

    [Display(Name = "Account Number 3")]
    public string LabAccAccountNumber3
    {
      get
      {
        return this.labaccaccountnumber3;
      }
      set
      {
        this.labaccaccountnumber3 = value;
      }
    }

    [Display(Name = "Pannel 3")]
    public string LabAccPannel3
    {
      get
      {
        return this.labaccpannel3;
      }
      set
      {
        this.labaccpannel3 = value;
      }
    }

    [Display(Name = "TPA 3")]
    public string LabAccTpa3
    {
      get
      {
        return this.labacctpa3;
      }
      set
      {
        this.labacctpa3 = value;
      }
    }

    [Display(Name = "MRO 3")]
    public string LabAccMro3
    {
      get
      {
        return this.labaccmro3;
      }
      set
      {
        this.labaccmro3 = value;
      }
    }

    [Display(Name = "Sample Type 3")]
    public List<SelectListItem> LabAccSampleType31 { get; set; }

    [Display(Name = "Sample Type 3")]
    public string LabAccSampleType3 { get; set; }

    [Display(Name = "Attachment 3")]
    public string LabAccAttachment3
    {
      get
      {
        return this.labaccattachment3;
      }
      set
      {
        this.labaccattachment3 = value;
      }
    }

    [Display(Name = "CCF 3")]
    public List<SelectListItem> LabAccCcf31 { get; set; }

    [Display(Name = "CCF 3")]
    public string LabAccCcf3 { get; set; }

    [Display(Name = "Services Provided")]
    public string ServicesProvided
    {
      get
      {
        return this.servicesprovided;
      }
      set
      {
        this.servicesprovided = value;
      }
    }

    [Display(Name = "Pre-Employment")]
    public string SPPreEmployment
    {
      get
      {
        return this.sppreemployment;
      }
      set
      {
        this.sppreemployment = value;
      }
    }

    [Display(Name = "Background Package-Pre Employ")]
    public List<SelectListItem> SPBackgroundPackagePreEmploy1 { get; set; }

    [Display(Name = "Background Package-Pre Employ")]
    public string SPBackgroundPackagePreEmploy { get; set; }

    [Display(Name = "Random")]
    public string SPRandom
    {
      get
      {
        return this.sprandom;
      }
      set
      {
        this.sprandom = value;
      }
    }

    [Display(Name = "PostAccident")]
    public string SPPostAccident
    {
      get
      {
        return this.sppostaccident;
      }
      set
      {
        this.sppostaccident = value;
      }
    }

    [Display(Name = "ReasonableSuspicion")]
    public string SPReasonableSuspicion
    {
      get
      {
        return this.spreasonablesuspicion;
      }
      set
      {
        this.spreasonablesuspicion = value;
      }
    }

    [Display(Name = "Follow-up")]
    public string SPFollowUp
    {
      get
      {
        return this.spfollowup;
      }
      set
      {
        this.spfollowup = value;
      }
    }

    [Display(Name = "Return to Duty")]
    public string SPReturntoDuty
    {
      get
      {
        return this.spreturntoduty;
      }
      set
      {
        this.spreturntoduty = value;
      }
    }

    [Display(Name = "Annual")]
    public string SPAnnual
    {
      get
      {
        return this.spannual;
      }
      set
      {
        this.spannual = value;
      }
    }

    [Display(Name = "Background Package -Annual")]
    public List<SelectListItem> SPBackgroundPackageAnnual1 { get; set; }

    [Display(Name = "Background Package -Annual")]
    public string SPBackgroundPackageAnnual { get; set; }

    [Display(Name = "Negative-Dilute")]
    public string SPNegativeDilute
    {
      get
      {
        return this.spnegativedilute;
      }
      set
      {
        this.spnegativedilute = value;
      }
    }

    [Display(Name = "Pool Name")]
    public string RPTPoolName
    {
      get
      {
        return this.rptpoolname;
      }
      set
      {
        this.rptpoolname = value;
      }
    }

    [Display(Name = "Pool Type")]
    public List<SelectListItem> RPTPoolType1 { get; set; }

    [Display(Name = "Pool Type")]
    public string RPTPoolType { get; set; }

    [Display(Name = "Owner")]
    public string RPTOwner
    {
      get
      {
        return this.rptowner;
      }
      set
      {
        this.rptowner = value;
      }
    }

    [Display(Name = "Pool Manager")]
    public string RPTPoolManager
    {
      get
      {
        return this.rptpoolmanager;
      }
      set
      {
        this.rptpoolmanager = value;
      }
    }

    [Display(Name = "Owner Type")]
    public List<SelectListItem> RPTOwnerType1 { get; set; }

    [Display(Name = "Owner Type")]
    public string RPTOwnerType { get; set; }

    [Display(Name = "DOT/NonDOT")]
    public List<SelectListItem> RPTdotnondot1 { get; set; }

    [Display(Name = "DOT/NonDOT")]
    public string RPTdotnondot { get; set; }

    [Display(Name = "DOT Agency")]
    public List<SelectListItem> RPTdotagency1 { get; set; }

    [Display(Name = "DOT Agency")]
    public string RPTdotagency { get; set; }

    [Display(Name = "Alcohol Gets Drug")]
    public bool RTPAlcoholGetsDrug
    {
      get
      {
        return this.rtpalcoholgetsdrug;
      }
      set
      {
        this.rtpalcoholgetsdrug = value;
      }
    }

    [Display(Name = "Selection Level for Drug")]
    public string RTPSelectionLevelforDrug
    {
      get
      {
        return this.rtpselectionlevelfordrug;
      }
      set
      {
        this.rtpselectionlevelfordrug = value;
      }
    }

    [Display(Name = "Percent")]
    public string RTPPercent1
    {
      get
      {
        return this.rtppercent1;
      }
      set
      {
        this.rtppercent1 = value;
      }
    }

    [Display(Name = "Selection Level for Alcohol")]
    public string RTPSelectionLevelforAlcohol
    {
      get
      {
        return this.rtpselectionlevelforalcohol;
      }
      set
      {
        this.rtpselectionlevelforalcohol = value;
      }
    }

    [Display(Name = "Percent")]
    public string RTPPercent2
    {
      get
      {
        return this.rtppercent2;
      }
      set
      {
        this.rtppercent2 = value;
      }
    }

    [Display(Name = "Alternates for Drug")]
    public string RTPAlternatesforDrug
    {
      get
      {
        return this.rtpalternatesfordrug;
      }
      set
      {
        this.rtpalternatesfordrug = value;
      }
    }

    [Display(Name = "Percent")]
    public string RTPPercent3
    {
      get
      {
        return this.rtppercent3;
      }
      set
      {
        this.rtppercent3 = value;
      }
    }

    [Display(Name = "Alternates for Alcohol")]
    public string RTPAlternatesforAlcohol
    {
      get
      {
        return this.rtpalternatesforalcohol;
      }
      set
      {
        this.rtpalternatesforalcohol = value;
      }
    }

    [Display(Name = "Percent")]
    public string RTPPercent4
    {
      get
      {
        return this.rtppercent4;
      }
      set
      {
        this.rtppercent4 = value;
      }
    }

    [Display(Name = "Notes")]
    public string RTPNotes
    {
      get
      {
        return this.rtpnotes;
      }
      set
      {
        this.rtpnotes = value;
      }
    }

    public List<Location_Model> ListLab { get; set; }

    public string LabsLabNameLabLocation { get; set; }

    public int Lab_Id { get; set; }
  }
}
