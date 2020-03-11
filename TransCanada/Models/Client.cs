// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Client
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
  public class Client
  {
    [Required]
    [Display(Name = "Name")]
    public string Client_Name { get; set; }

    [Required]
    [Display(Name = "Contact Person")]
    public string Contact_Person { get; set; }

    [Required]
    [Display(Name = "Address")]
    public string Address_1 { get; set; }

    [Required]
    [Display(Name = "Address 2")]
    public string Address_2 { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string Zip { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Fax { get; set; }

    [Required]
    public string Country { get; set; }

    [Required]
    public string WebSite { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public int client_id { get; set; }

    public List<SelectListItem> Cities { get; set; }
  }
}
