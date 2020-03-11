using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TransCanada.Models
{
    public class LookUp
    { 
        
        public int Id { get; set; }


        [Required]
        [Display(Name ="Name")]
        public string Description { get; set; }
    }
} 