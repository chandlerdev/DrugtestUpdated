using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransCanada.Models
{
    public class TpaLab_model
    {
        public int TPALab_Id { get; set; }

        [Required]
        [Display(Name = "Tpa Lab")]
        public string TPALab_Name { get; set; }
    }
}