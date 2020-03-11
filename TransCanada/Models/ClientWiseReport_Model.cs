// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.ClientWiseReport_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class ClientWiseReport_Model
  {
    private string clientname = "DESSS Applying Technologies Pvt Ltd";
    private string employeename = "Dev Chandler";
    private string checkuptype = "General Checkup";
    private Decimal actualcost = new Decimal(10);
    private Decimal wsscost = new Decimal(5);
    private Decimal totalcost = new Decimal(15);

    [Display(Name = "Client Name")]
    public string ClientName
    {
      get
      {
        return this.clientname;
      }
      set
      {
        this.clientname = value;
      }
    }

    [Display(Name = "Employee Name")]
    public string EmployeeName
    {
      get
      {
        return this.employeename;
      }
      set
      {
        this.employeename = value;
      }
    }

    [Display(Name = "Checkup Type")]
    public string CheckupType
    {
      get
      {
        return this.checkuptype;
      }
      set
      {
        this.checkuptype = value;
      }
    }

    [Display(Name = "Actual Cost")]
    public Decimal ActualCost
    {
      get
      {
        return this.actualcost;
      }
      set
      {
        this.actualcost = value;
      }
    }

    [Display(Name = "WSS Cost")]
    public Decimal WSSCost
    {
      get
      {
        return this.wsscost;
      }
      set
      {
        this.wsscost = value;
      }
    }

    [Display(Name = "Total Cost")]
    public Decimal TotalCost
    {
      get
      {
        return this.totalcost;
      }
      set
      {
        this.totalcost = value;
      }
    }
  }
}
