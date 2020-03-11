using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TransCanada.Models
{
    public class TPAClient
    {
        public int TPAClient_Id { get; set; }

        [Required]
        [Display(Name = "Tpa Client")]
        public string TPA_Client { get; set; }


    }
}