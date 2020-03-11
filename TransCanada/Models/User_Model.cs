// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.User_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TransCanada.Models
{
  public class User_Model
  {
    [Display(Name = "User Id")]
    public string Id { get; set; }

    [Display(Name = "User Name")]
    public string Email { get; set; }

    [Display(Name = "Role")]
    public string RoleId { get; set; }

    [Display(Name = "Accounts")]
    public string Accountsid { get; set; }

    public List<SelectListItem> Roles { get; set; }

    public List<SelectListItem> Accounts { get; set; }

    [Display(Name = "User Name")]
    public string UserName { get; set; }
  }
}
