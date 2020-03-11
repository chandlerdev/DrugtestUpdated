// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.TcClient
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
  public class TcClient
  {
    private string clientzip = "77450";
    private string mainnumber = "465-485-653";
    private string locationspecificnotes = "Notes";
    private string conatctwithany = "Dev Chandler";
    private string clientsample = "Sample";
    private string refusaltotest = "";
    private string batconfirmedpostive = "";
    private string cancelledorincompletetests = "";
    private string authorizationformssentvia = "";
    private string derfullname = "";
    private string derjobtitle = "";
    private string derofficenumber = "";
    private string dercellphone = "";
    private string derfax = "";
    private string deremail = "";
    private string derstreetaddress = "";
    private string dercity = "";
    private string derzip = "";
    private string der2fullname = "";
    private string der2jobtitle = "";
    private string der2officenumber = "";
    private string der2cellphone = "";
    private string der2fax = "";
    private string der2email = "";
    private string der2streetaddress = "";
    private string der2city = "";
    private string der2zip = "";
    private string contact1fullname = "";
    private string contact1jobtitle = "";
    private string contact1officenumber = "";
    private string contact1cellphone = "";
    private string contact1fax = "";
    private string contact1email = "";
    private string contact1streetaddress = "";
    private string contact1city = "";
    private string contact1zip = "";
    private string contact2fullname = "";
    private string contact2jobtitle = "";
    private string contact2officenumber = "";
    private string contact2cellphone = "";
    private string contact2fax = "";
    private string contact2email = "";
    private string contact2streetaddress = "";
    private string contact2city = "";
    private string contact2zip = "";
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
    private string locationslocationname = "";
    private string locationsstreetaddress = "";
    private string locationscity = "";
    private string locationszip = "";
    private string locationsmainnumber = "";
    private string locationsnotes = "";
    private string locationscontactfullname = "";
    private string locationscontactofficenumber = "";
    private string locationscontactcellphone = "";
    private string locationscontactfax = "";
    private string locationscontactemail = "";
    private string companyprotocolsnotes = "";
    private string companyprotocolsprotocols = "All Collections are handled in accordance with 49 CFR Part 40. All lab based urine collections must be split specimens (minimum of 45ml) All CCF forms must have a complete SSN number and Donors telephone number.";
    private string companypartnerprotocols = "Payment will be rendered if (1) authorization is given, (2) valid test results, (3) proper protocols used when testing, and(4) documentation provided timely after testing. Payment may be delayed if completed record is not returned timely, and bills with dates of service older than six months will be denied payment. Contact the CTPA (832 572 5577) with ANY: Shy Bladders/Lung with no sample Refusal to Test BAT w/Confirmed Positive Cancelled or Incomplete Tests. By close of business of a collection send all documentation to: Results@WorkplaceSafetyScreenings.com";
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
    private string rtpalternatesfordrug = "";
    private string rtppercent2 = "";
    private string rtpalternatesforalcohol = "";
    private string rtppercent3 = "";
    private string rtpnotes = "";
    private string accountsid = "";
    private string shybladderslugwithno;

    public string clientid { get; set; }

    [Required]
    [Display(Name = "Client Type")]
    public List<SelectListItem> ClientType_LI1 { get; set; }

    public string ClientType_LI { get; set; }

    [Required]
    [Display(Name = "Street Address")]
    [StringLength(100)]
    public string ClientStreetAddress { get; set; }

    [Display(Name = "Client City")]
    public string ClientCity { get; set; }

    [Display(Name = "Client State")]
    public List<SelectListItem> ClientState1 { get; set; }

    public string ClientState { get; set; }

    [Display(Name = "Client Zip")]
    public string ClientZip
    {
      get
      {
        return this.clientzip;
      }
      set
      {
        this.clientzip = value;
      }
    }

    [Display(Name = "Main Number")]
    public string MainNumber
    {
      get
      {
        return this.mainnumber;
      }
      set
      {
        this.mainnumber = value;
      }
    }

    [Display(Name = "Location Notes")]
    public string LocationSpecificNotes
    {
      get
      {
        return this.locationspecificnotes;
      }
      set
      {
        this.locationspecificnotes = value;
      }
    }

    [Display(Name = "Conatct ")]
    [StringLength(50)]
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

    [Display(Name = "Shy Bladders")]
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

    [Display(Name = "Refusal ")]
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

    [Display(Name = "Bat")]
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

    [Display(Name = "Incomplete Tests")]
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

    [Display(Name = "Authorization")]
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

    [Display(Name = "DER Full Name")]
    public string DerFullName
    {
      get
      {
        return this.derfullname;
      }
      set
      {
        this.derfullname = value;
      }
    }

    [Display(Name = "Job Title")]
    public string DerJobTitle
    {
      get
      {
        return this.derjobtitle;
      }
      set
      {
        this.derjobtitle = value;
      }
    }

    [Display(Name = "Office Number")]
    public string DerOfficeNumber
    {
      get
      {
        return this.derofficenumber;
      }
      set
      {
        this.derofficenumber = value;
      }
    }

    [Display(Name = "Cell Phone")]
    public string DerCellPhone
    {
      get
      {
        return this.dercellphone;
      }
      set
      {
        this.dercellphone = value;
      }
    }

    [Display(Name = "Fax")]
    public string DerFax
    {
      get
      {
        return this.derfax;
      }
      set
      {
        this.derfax = value;
      }
    }

    [Display(Name = "Email")]
    public string DerEmail
    {
      get
      {
        return this.deremail;
      }
      set
      {
        this.deremail = value;
      }
    }

    [Display(Name = "Street Address")]
    public string DerStreetAddress
    {
      get
      {
        return this.derstreetaddress;
      }
      set
      {
        this.derstreetaddress = value;
      }
    }

    [Display(Name = "City")]
    public string DerCity
    {
      get
      {
        return this.dercity;
      }
      set
      {
        this.dercity = value;
      }
    }

    [Display(Name = "State")]
    public List<SelectListItem> DerState1 { get; set; }

    public string DerState { get; set; }

    [Display(Name = "Zip")]
    public string DerZip
    {
      get
      {
        return this.derzip;
      }
      set
      {
        this.derzip = value;
      }
    }

    [Display(Name = "i3 Screen Access")]
    public List<SelectListItem> Deri3ScreenAccess1 { get; set; }

    public string Deri3ScreenAccess { get; set; }

    [Display(Name = "Screening Access")]
    public List<SelectListItem> DerBackgroundScreeningAccess1 { get; set; }

    public string DerBackgroundScreeningAccess { get; set; }

    [Display(Name = "Medicine Access")]
    public List<SelectListItem> DerOccupationalMedicineAccess1 { get; set; }

    public string DerOccupationalMedicineAccess { get; set; }

    [Display(Name = "DER-2nd Full Name")]
    public string Der2FullName
    {
      get
      {
        return this.der2fullname;
      }
      set
      {
        this.der2fullname = value;
      }
    }

    [Display(Name = "Job Title")]
    public string Der2JobTitle
    {
      get
      {
        return this.der2jobtitle;
      }
      set
      {
        this.der2jobtitle = value;
      }
    }

    [Display(Name = "Office Number")]
    public string Der2OfficeNumber
    {
      get
      {
        return this.der2officenumber;
      }
      set
      {
        this.der2officenumber = value;
      }
    }

    [Display(Name = "Cell Phone")]
    public string Der2CellPhone
    {
      get
      {
        return this.der2cellphone;
      }
      set
      {
        this.der2cellphone = value;
      }
    }

    [Display(Name = "Fax")]
    public string Der2Fax
    {
      get
      {
        return this.der2fax;
      }
      set
      {
        this.der2fax = value;
      }
    }

    [Display(Name = " Email")]
    public string Der2Email
    {
      get
      {
        return this.der2email;
      }
      set
      {
        this.der2email = value;
      }
    }

    [Display(Name = "Street Address")]
    public string Der2StreetAddress
    {
      get
      {
        return this.der2streetaddress;
      }
      set
      {
        this.der2streetaddress = value;
      }
    }

    [Display(Name = "City")]
    public string Der2City
    {
      get
      {
        return this.der2city;
      }
      set
      {
        this.der2city = value;
      }
    }

    [Display(Name = "State")]
    public List<SelectListItem> Der2State1 { get; set; }

    public string Der2State { get; set; }

    [Display(Name = "Zip")]
    public string Der2Zip
    {
      get
      {
        return this.der2zip;
      }
      set
      {
        this.der2zip = value;
      }
    }

    [Display(Name = "i3 Screen Access")]
    public List<SelectListItem> Der2i3ScreenAccess1 { get; set; }

    public string Der2i3ScreenAccess { get; set; }

    [Display(Name = "Background")]
    public List<SelectListItem> Der2BackgroundScreeningAccess1 { get; set; }

    public string Der2BackgroundScreeningAccess { get; set; }

    [Display(Name = "Medicine Access")]
    public List<SelectListItem> Der2OccupationalMedicineAccess1 { get; set; }

    public string Der2OccupationalMedicineAccess { get; set; }

    [Display(Name = " Full Name")]
    public string Contact1FullName
    {
      get
      {
        return this.contact1fullname;
      }
      set
      {
        this.contact1fullname = value;
      }
    }

    [Display(Name = "Job Title")]
    public string Contact1JobTitle
    {
      get
      {
        return this.contact1jobtitle;
      }
      set
      {
        this.contact1jobtitle = value;
      }
    }

    [Display(Name = "Office Number")]
    public string Contact1OfficeNumber
    {
      get
      {
        return this.contact1officenumber;
      }
      set
      {
        this.contact1officenumber = value;
      }
    }

    [Display(Name = "Cell Phone")]
    public string Contact1CellPhone
    {
      get
      {
        return this.contact1cellphone;
      }
      set
      {
        this.contact1cellphone = value;
      }
    }

    [Display(Name = "Fax")]
    public string Contact1Fax
    {
      get
      {
        return this.contact1fax;
      }
      set
      {
        this.contact1fax = value;
      }
    }

    [Display(Name = "Email")]
    public string Contact1Email
    {
      get
      {
        return this.contact1email;
      }
      set
      {
        this.contact1email = value;
      }
    }

    [Display(Name = "Street Address")]
    public string Contact1StreetAddress
    {
      get
      {
        return this.contact1streetaddress;
      }
      set
      {
        this.contact1streetaddress = value;
      }
    }

    [Display(Name = " City")]
    public string Contact1City
    {
      get
      {
        return this.contact1city;
      }
      set
      {
        this.contact1city = value;
      }
    }

    [Display(Name = "State")]
    public List<SelectListItem> Contact1State1 { get; set; }

    public string Contact1State { get; set; }

    [Display(Name = " Zip")]
    public string Contact1Zip
    {
      get
      {
        return this.contact1zip;
      }
      set
      {
        this.contact1zip = value;
      }
    }

    [Display(Name = "i3 Screen Access")]
    public List<SelectListItem> Contact1i3ScreenAccess1 { get; set; }

    public string Contact1i3ScreenAccess { get; set; }

    [Display(Name = "Screening Access")]
    public List<SelectListItem> Contact1BackgroundScreeningAccess1 { get; set; }

    public string Contact1BackgroundScreeningAccess { get; set; }

    [Display(Name = "Medicine Access")]
    public List<SelectListItem> Contact1OccupationalMedicineAccess1 { get; set; }

    public string Contact1OccupationalMedicineAccess { get; set; }

    [Display(Name = "Full Name")]
    public string Contact2FullName
    {
      get
      {
        return this.contact2fullname;
      }
      set
      {
        this.contact2fullname = value;
      }
    }

    [Display(Name = "Job Title")]
    public string Contact2JobTitle
    {
      get
      {
        return this.contact2jobtitle;
      }
      set
      {
        this.contact2jobtitle = value;
      }
    }

    [Display(Name = "Office Number")]
    public string Contact2OfficeNumber
    {
      get
      {
        return this.contact2officenumber;
      }
      set
      {
        this.contact2officenumber = value;
      }
    }

    [Display(Name = "Cell Phone")]
    public string Contact2CellPhone
    {
      get
      {
        return this.contact2cellphone;
      }
      set
      {
        this.contact2cellphone = value;
      }
    }

    [Display(Name = "Fax")]
    public string Contact2Fax
    {
      get
      {
        return this.contact2fax;
      }
      set
      {
        this.contact2fax = value;
      }
    }

    [Display(Name = "Email")]
    public string Contact2Email
    {
      get
      {
        return this.contact2email;
      }
      set
      {
        this.contact2email = value;
      }
    }

    [Display(Name = "Street Address")]
    public string Contact2StreetAddress
    {
      get
      {
        return this.contact2streetaddress;
      }
      set
      {
        this.contact2streetaddress = value;
      }
    }

    [Display(Name = "City")]
    public string Contact2City
    {
      get
      {
        return this.contact2city;
      }
      set
      {
        this.contact2city = value;
      }
    }

    [Display(Name = "State")]
    public List<SelectListItem> Contact2State1 { get; set; }

    public string Contact2State { get; set; }

    [Display(Name = "Zip")]
    public string Contact2Zip
    {
      get
      {
        return this.contact2zip;
      }
      set
      {
        this.contact2zip = value;
      }
    }

    [Display(Name = "i3 Screen Access")]
    public List<SelectListItem> Contact2i3ScreenAccess1 { get; set; }

    public string Contact2i3ScreenAccess { get; set; }

    [Display(Name = "Screening Access")]
    public List<SelectListItem> Contact2BackgroundScreeningAccess1 { get; set; }

    public string Contact2BackgroundScreeningAccess { get; set; }

    [Display(Name = "Medicine Access")]
    public List<SelectListItem> Contact2OccupationalMedicineAccess1 { get; set; }

    public string Contact2OccupationalMedicineAccess { get; set; }

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

    [Display(Name = "Office Number")]
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

    [Display(Name = "Fax")]
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

    [Display(Name = "Email")]
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

    [Display(Name = "Street Address")]
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

    [Display(Name = "City")]
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

    [Display(Name = "State")]
    public List<SelectListItem> BillingContactState1 { get; set; }

    public string BillingContactState { get; set; }

    [Display(Name = "Zip")]
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

    [Display(Name = "Options")]
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

    [Display(Name = "Location Name")]
    public string LocationsLocationName
    {
      get
      {
        return this.locationslocationname;
      }
      set
      {
        this.locationslocationname = value;
      }
    }

    [Display(Name = "Street Address")]
    public string LocationsStreetAddress
    {
      get
      {
        return this.locationsstreetaddress;
      }
      set
      {
        this.locationsstreetaddress = value;
      }
    }

    [Display(Name = " City")]
    public string LocationsCity
    {
      get
      {
        return this.locationscity;
      }
      set
      {
        this.locationscity = value;
      }
    }

    [Display(Name = " State")]
    public List<SelectListItem> LocationsState1 { get; set; }

    public string LocationsState { get; set; }

    [Display(Name = " Zip")]
    public string LocationsZip
    {
      get
      {
        return this.locationszip;
      }
      set
      {
        this.locationszip = value;
      }
    }

    [Display(Name = " Main Number")]
    public string LocationsMainNumber
    {
      get
      {
        return this.locationsmainnumber;
      }
      set
      {
        this.locationsmainnumber = value;
      }
    }

    [Display(Name = " Notes")]
    public string LocationsNotes
    {
      get
      {
        return this.locationsnotes;
      }
      set
      {
        this.locationsnotes = value;
      }
    }

    [Display(Name = " Full Name")]
    public string LocationsContactFullName
    {
      get
      {
        return this.locationscontactfullname;
      }
      set
      {
        this.locationscontactfullname = value;
      }
    }

    [Display(Name = " Office Number")]
    public string LocationsContactOfficeNumber
    {
      get
      {
        return this.locationscontactofficenumber;
      }
      set
      {
        this.locationscontactofficenumber = value;
      }
    }

    [Display(Name = " Cell Phone")]
    public string LocationsContactCellPhone
    {
      get
      {
        return this.locationscontactcellphone;
      }
      set
      {
        this.locationscontactcellphone = value;
      }
    }

    [Display(Name = "Contact Fax")]
    public string LocationsContactFax
    {
      get
      {
        return this.locationscontactfax;
      }
      set
      {
        this.locationscontactfax = value;
      }
    }

    [Display(Name = "Contact Email")]
    public string LocationsContactEmail
    {
      get
      {
        return this.locationscontactemail;
      }
      set
      {
        this.locationscontactemail = value;
      }
    }

    [Display(Name = "Locations i3 Screen Access")]
    public List<SelectListItem> Locationsi3ScreenAccess1 { get; set; }

    public string Locationsi3ScreenAccess { get; set; }

    [Display(Name = "Company Protocols Policies")]
    public List<SelectListItem> CompanyProtocolsPolicies1 { get; set; }

    public string CompanyProtocolsPolicies { get; set; }

    [Display(Name = "Company Protocols Notes")]
    public string CompanyProtocolsNotes
    {
      get
      {
        return this.companyprotocolsnotes;
      }
      set
      {
        this.companyprotocolsnotes = value;
      }
    }

    [Display(Name = "Protocols")]
    public string CompanyProtocolsProtocols
    {
      get
      {
        return this.companyprotocolsprotocols;
      }
      set
      {
        this.companyprotocolsprotocols = value;
      }
    }

    [Display(Name = "Partner Protocols")]
    public string CompanyPartnerProtocols
    {
      get
      {
        return this.companypartnerprotocols;
      }
      set
      {
        this.companypartnerprotocols = value;
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

    [Display(Name = "Lab 2")]
    public List<SelectListItem> LabAccLab21 { get; set; }

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

    [Display(Name = "Lab 3")]
    public List<SelectListItem> LabAccLab31 { get; set; }

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

    [Display(Name = "Package-Pre Employ")]
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

    public string RPTOwnerType { get; set; }

    [Display(Name = "DOT/NonDOT")]
    public List<SelectListItem> RPTdotnondot1 { get; set; }

    public string RPTdotnondot { get; set; }

    [Display(Name = "DOT Agency")]
    public List<SelectListItem> RPTdotagency1 { get; set; }

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

    [Display(Name = "Selection Level")]
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

    [Display(Name = "Drug")]
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

    [Display(Name = "Alcohol")]
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

    [Display(Name = "Accounts Id")]
    public string AccountsId
    {
      get
      {
        return this.accountsid;
      }
      set
      {
        this.accountsid = value;
      }
    }
  }
}
