// Decompiled with JetBrains decompiler
// Type: TransCanadaDemo.Models.iThreeScreen
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.ComponentModel.DataAnnotations;

namespace TransCanadaDemo.Models
{
  public class iThreeScreen
  {
    private string billtocustomerid = "542891";
    private string billto = "DRUG TEST COMPLIANCE";
    private string customerofid = "542891";
    private string customerof = "DRUG TEST COMPLIANCE";
    private string customerid = "547241";
    private string customer = "Client A";
    private string locationname = "Client A";
    private string locationid = "275830";
    private string locationcode = "";
    private string casenumber = "2018021013004";
    private string donorccf = "3021601";
    private string donorfirstname = "LEVIS";
    private string donorlastname = "rtbvsw";
    private string donorid = "582670662";
    private string reasonfortest = "POST-ACCIDENT";
    private string collectiondate = "2/9/2018  9:45:00 AM";
    private string receivedfromlab = "2/10/2018  10:56:21 AM";
    private string labaccountnumber = "10684717";
    private string lab = "Quest Diagnostics";
    private string program = "DOT";
    private string specimentype = "URINE";
    private string paneltype = "DOT DRUG PANEL";
    private string panel = "65304N";
    private string collectionsite = "USA MOBILE DRUG TESTING OF WESTCHESTER";
    private string collectionsitetype = "THIRD";
    private string feeamount = "13.00";
    private string manuallyentered = "0";
    private string glaccount = "SCR-4130-001";
    private string Itemdescription = "DOT Urine Drug Screen Full Service ( Quest Diagnostics THIRD )";
    private string service = "Urine Drug Screen Full Service";
    private string additionalnotes = "";

    [Required]
    [Display(Name = "Bill To Customer Id")]
    public string BillToCustomerId
    {
      get
      {
        return this.billtocustomerid;
      }
      set
      {
        this.billtocustomerid = value;
      }
    }

    [Display(Name = "Bill To")]
    public string BillTo
    {
      get
      {
        return this.billto;
      }
      set
      {
        this.billto = value;
      }
    }

    [Display(Name = "Customer Of Id")]
    public string CustomerOfId
    {
      get
      {
        return this.customerofid;
      }
      set
      {
        this.customerofid = value;
      }
    }

    [Display(Name = "Customer Of")]
    public string CustomerOf
    {
      get
      {
        return this.customerof;
      }
      set
      {
        this.customerof = value;
      }
    }

    [Display(Name = "Customer Id")]
    public string CustomerId
    {
      get
      {
        return this.customerid;
      }
      set
      {
        this.customerid = value;
      }
    }

    [Display(Name = "Customer")]
    public string Customer
    {
      get
      {
        return this.customer;
      }
      set
      {
        this.customer = value;
      }
    }

    [Display(Name = "LocationName")]
    public string LocationName
    {
      get
      {
        return this.locationname;
      }
      set
      {
        this.locationname = value;
      }
    }

    [Display(Name = "Location Id")]
    public string LocationId
    {
      get
      {
        return this.locationid;
      }
      set
      {
        this.locationid = value;
      }
    }

    [Display(Name = "Location Code")]
    public string LocationCode
    {
      get
      {
        return this.locationcode;
      }
      set
      {
        this.locationcode = value;
      }
    }

    public string CaseNumber
    {
      get
      {
        return this.casenumber;
      }
      set
      {
        this.casenumber = value;
      }
    }

    public string DonorCCF
    {
      get
      {
        return this.donorccf;
      }
      set
      {
        this.donorccf = value;
      }
    }

    public string DonorFirstName
    {
      get
      {
        return this.donorfirstname;
      }
      set
      {
        this.donorfirstname = value;
      }
    }

    public string DonorLastName
    {
      get
      {
        return this.donorlastname;
      }
      set
      {
        this.donorlastname = value;
      }
    }

    public string DonorID
    {
      get
      {
        return this.donorid;
      }
      set
      {
        this.donorid = value;
      }
    }

    public string ReasonForTest
    {
      get
      {
        return this.reasonfortest;
      }
      set
      {
        this.reasonfortest = value;
      }
    }

    public string CollectionDate
    {
      get
      {
        return this.collectiondate;
      }
      set
      {
        this.collectiondate = value;
      }
    }

    public string ReceivedFromLab
    {
      get
      {
        return this.receivedfromlab;
      }
      set
      {
        this.receivedfromlab = value;
      }
    }

    public string LabAccountNumber
    {
      get
      {
        return this.labaccountnumber;
      }
      set
      {
        this.labaccountnumber = value;
      }
    }

    public string Lab
    {
      get
      {
        return this.lab;
      }
      set
      {
        this.lab = value;
      }
    }

    public string Program
    {
      get
      {
        return this.program;
      }
      set
      {
        this.program = value;
      }
    }

    public string SpecimenType
    {
      get
      {
        return this.specimentype;
      }
      set
      {
        this.specimentype = value;
      }
    }

    public string PanelType
    {
      get
      {
        return this.paneltype;
      }
      set
      {
        this.paneltype = value;
      }
    }

    public string Panel
    {
      get
      {
        return this.panel;
      }
      set
      {
        this.panel = value;
      }
    }

    public string CollectionSite
    {
      get
      {
        return this.collectionsite;
      }
      set
      {
        this.collectionsite = value;
      }
    }

    public string CollectionSiteType
    {
      get
      {
        return this.collectionsitetype;
      }
      set
      {
        this.collectionsitetype = value;
      }
    }

    public string FeeAmount
    {
      get
      {
        return this.feeamount;
      }
      set
      {
        this.feeamount = value;
      }
    }

    public string ManuallyEntered
    {
      get
      {
        return this.manuallyentered;
      }
      set
      {
        this.manuallyentered = value;
      }
    }

    public string GLAccount
    {
      get
      {
        return this.glaccount;
      }
      set
      {
        this.glaccount = value;
      }
    }

    public string ItemDescription
    {
      get
      {
        return this.Itemdescription;
      }
      set
      {
        this.Itemdescription = value;
      }
    }

    public string Service
    {
      get
      {
        return this.service;
      }
      set
      {
        this.service = value;
      }
    }

    public string AdditionalNotes
    {
      get
      {
        return this.additionalnotes;
      }
      set
      {
        this.additionalnotes = value;
      }
    }
  }
}
