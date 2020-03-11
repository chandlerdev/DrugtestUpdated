// Decompiled with JetBrains decompiler
// Type: TransCanada.Models.Lab_loc
// Assembly: TransCanada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78AEA9CA-12BE-44D1-9407-D806EB0929A5
// Assembly location: C:\Users\Admin\Documents\TransCanada.dll

using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class Lab_loc
  {
    public int Id { get; set; }

    [Display(Name = "Id")]
    public int Lab_Id { get; set; }

    [Display(Name = "Name")]
    public string Lab_Name { get; set; }
  }
}
