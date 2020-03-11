// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.AspNetRolesModel
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class AspNetRolesModel
  {
    [Display(Name = "Role Id")]
    public string Id { get; set; }

    [Display(Name = "Role Name")]
    public string Name { get; set; }

    [Display(Name = "Created By")]
    public string CreatedBy { get; set; }

    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Updated By")]
    public string UpdatedBy { get; set; }

    [Display(Name = "Updated Date")]
    public DateTime UpdatedDate { get; set; }

    public List<AspNetRolesModel> Category { get; set; }

    public List<UserModel> Users { get; set; }
  }
}
