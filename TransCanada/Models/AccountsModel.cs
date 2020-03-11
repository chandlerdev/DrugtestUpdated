using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
  public class AccountsModel
  {
    [Display(Name = "Account Id")]
    [StringLength(10)]
    public string AccountId { get; set; }

    [Display(Name = "Account Name")]
    [StringLength(250)]
    public string AccountName { get; set; }
  }
}
