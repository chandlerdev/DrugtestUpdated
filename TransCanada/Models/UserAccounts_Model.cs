// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.UserAccounts_Model
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class UserAccounts_Model
  {
    [Display(Name = "Account Id PK")]
    public int AccountId_PK { get; set; }

    public int Account_id { get; set; }

    [Display(Name = "User Name")]
    public string UserId { get; set; }

    [Display(Name = "Account Id")]
    public string AccountId { get; set; }

    [Display(Name = "Logo Image")]
    public string LogoImage { get; set; }

    public List<UserAccounts_Model> Id { get; set; }

    public List<User_Mdl> Users { get; set; }
  }
}
