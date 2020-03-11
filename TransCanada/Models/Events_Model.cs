// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.Events_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanadaDemo.Models
{
  public class Events_Model
  {
    private string oselocationstreet = "";
    private string oselocationcity = "";
    private string oselocationzip = "";
    private string osecontactname = "";
    private string osecontactmobile = "";
    private DateTime osedatetimeofcollection = DateTime.Now;
    private string osenumbertobetested = "";
    private string osetestinglist = "";
    private string osereasonfortest = "";
    private string osechainofcustody = "";
    private bool osecollectiononly = true;
    private string osecollectors = "";
    private string osespecialinstructions = "";
    private DateTime osecollectionconfirmed = DateTime.Now;
    private bool osecollectioncancelled = true;
    private string osecancelledreason = "";
    private string pe_client = "";
    private string pe_patientfullname = "";
    private string pe_reason = "";
    private string pe_clearedbymedicalexaminer = "";
    private string pe_formattached = "";
    private DateTime pe_scheduled = DateTime.Now;
    private string pe_testingcompleted = "";
    private DateTime pe_clinicauthorizationsent = DateTime.Now;
    private DateTime pe_clientauthorizationsent = DateTime.Now;
    private string pe_notes = "";

    [Display(Name = "Urgency")]
    public List<SelectListItem> Urgency { get; set; }

    [Display(Name = "Location")]
    public List<SelectListItem> Location { get; set; }

    [Display(Name = "Partnering Facility")]
    public List<SelectListItem> PartneringFacility { get; set; }

    [Display(Name = "OSE Client Name")]
    public List<SelectListItem> OSEClientName { get; set; }

    [Display(Name = "OSE Location")]
    public string OSELocation { get; set; }

    [Display(Name = "OSE Location Street")]
    public string OSELocationStreet
    {
      get
      {
        return this.oselocationstreet;
      }
      set
      {
        this.oselocationstreet = value;
      }
    }

    [Display(Name = "OSE Location City")]
    public string OSELocationCity
    {
      get
      {
        return this.oselocationcity;
      }
      set
      {
        this.oselocationcity = value;
      }
    }

    [Display(Name = "OSE Location State")]
    public List<SelectListItem> OSELocationState { get; set; }

    [Display(Name = "OSE Location Zip")]
    public string OSELocationZip
    {
      get
      {
        return this.oselocationzip;
      }
      set
      {
        this.oselocationzip = value;
      }
    }

    [Display(Name = "OSE Contact Name")]
    public string OSEContactName
    {
      get
      {
        return this.osecontactname;
      }
      set
      {
        this.osecontactname = value;
      }
    }

    [Display(Name = "OSE Contact Mobile")]
    public string OSEContactMobile
    {
      get
      {
        return this.osecontactmobile;
      }
      set
      {
        this.osecontactmobile = value;
      }
    }

    [Display(Name = "OSE Date Time of Collection")]
    public DateTime OSEDateTimeofCollection
    {
      get
      {
        return this.osedatetimeofcollection;
      }
      set
      {
        this.osedatetimeofcollection = value;
      }
    }

    [Display(Name = "OSE Number to be Tested")]
    public string OSENumbertobeTested
    {
      get
      {
        return this.osenumbertobetested;
      }
      set
      {
        this.osenumbertobetested = value;
      }
    }

    [Display(Name = "OSE Testing List")]
    public string OSETestingList
    {
      get
      {
        return this.osetestinglist;
      }
      set
      {
        this.osetestinglist = value;
      }
    }

    [Display(Name = "OSE Reason for Test")]
    public string OSEReasonforTest
    {
      get
      {
        return this.osereasonfortest;
      }
      set
      {
        this.osereasonfortest = value;
      }
    }

    [Display(Name = "OSE Chain of Custody")]
    public string OSEChainofCustody
    {
      get
      {
        return this.osechainofcustody;
      }
      set
      {
        this.osechainofcustody = value;
      }
    }

    [Display(Name = "OSE Collection Only")]
    public bool OSECollectionOnly
    {
      get
      {
        return this.osecollectiononly;
      }
      set
      {
        this.osecollectiononly = value;
      }
    }

    [Display(Name = "OSE Collectors")]
    public string OSECollectors
    {
      get
      {
        return this.osecollectors;
      }
      set
      {
        this.osecollectors = value;
      }
    }

    [Display(Name = "OSE Special Instructions")]
    public string OSESpecialInstructions
    {
      get
      {
        return this.osespecialinstructions;
      }
      set
      {
        this.osespecialinstructions = value;
      }
    }

    [Display(Name = "OSE Collection Confirmed")]
    public DateTime OSECollectionConfirmed
    {
      get
      {
        return this.osecollectionconfirmed;
      }
      set
      {
        this.osecollectionconfirmed = value;
      }
    }

    [Display(Name = "OSE Collection Cancelled")]
    public bool OSECollectionCancelled
    {
      get
      {
        return this.osecollectioncancelled;
      }
      set
      {
        this.osecollectioncancelled = value;
      }
    }

    [Display(Name = "OSE Cancelled by and reason for cancellation")]
    public string OSECancelledReason
    {
      get
      {
        return this.osecancelledreason;
      }
      set
      {
        this.osecancelledreason = value;
      }
    }

    [Display(Name = "Client")]
    public string PE_Client
    {
      get
      {
        return this.pe_client;
      }
      set
      {
        this.pe_client = value;
      }
    }

    [Display(Name = "Patient Full Name")]
    public string PE_PatientFullName
    {
      get
      {
        return this.pe_patientfullname;
      }
      set
      {
        this.pe_patientfullname = value;
      }
    }

    [Display(Name = "Reason")]
    public string PE_Reason
    {
      get
      {
        return this.pe_reason;
      }
      set
      {
        this.pe_reason = value;
      }
    }

    [Display(Name = "Cleared by Medical Examiner")]
    public string PE_ClearedbyMedicalExaminer
    {
      get
      {
        return this.pe_clearedbymedicalexaminer;
      }
      set
      {
        this.pe_clearedbymedicalexaminer = value;
      }
    }

    [Display(Name = "Form Attached")]
    public string PE_FormAttached
    {
      get
      {
        return this.pe_formattached;
      }
      set
      {
        this.pe_formattached = value;
      }
    }

    [Display(Name = "Scheduled")]
    public DateTime PE_Scheduled
    {
      get
      {
        return this.pe_scheduled;
      }
      set
      {
        this.pe_scheduled = value;
      }
    }

    [Display(Name = "Testing Completed")]
    public string PE_TestingCompleted
    {
      get
      {
        return this.pe_testingcompleted;
      }
      set
      {
        this.pe_testingcompleted = value;
      }
    }

    [Display(Name = "Clinic Authorization Sent")]
    public DateTime PE_ClinicAuthorizationSent
    {
      get
      {
        return this.pe_clinicauthorizationsent;
      }
      set
      {
        this.pe_clinicauthorizationsent = value;
      }
    }

    [Display(Name = "Client Authorization Sent")]
    public DateTime PE_ClientAuthorizationSent
    {
      get
      {
        return this.pe_clientauthorizationsent;
      }
      set
      {
        this.pe_clientauthorizationsent = value;
      }
    }

    [Display(Name = "Notes")]
    public string PE_Notes
    {
      get
      {
        return this.pe_notes;
      }
      set
      {
        this.pe_notes = value;
      }
    }
  }
}
