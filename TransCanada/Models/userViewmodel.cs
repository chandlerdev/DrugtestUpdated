// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.userViewmodel
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class userViewmodel
  {
    public string Id { get; set; }

    public string username { get; set; }

    [Display(Name = "Email")]
    public string roleid { get; set; }

    [Display(Name = "Account Name")]
    public List<CheckBox> Accounts_Id { get; set; }

    [Display(Name = "Role")]
    public List<AspNetUserRoles> Category1 { get; set; }
  }
}
