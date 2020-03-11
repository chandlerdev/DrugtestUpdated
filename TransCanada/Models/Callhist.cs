using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TransCanada.Models
{
    public class Callhist
    {
        public int leadid { get; set; }
        [Display(Name ="Call Status")]
        public string call_status { get; set; }
        [Display(Name = "Follow Up Date")]
        public DateTime followupdate { get; set; }
        [Display(Name = "Follow Up Time")]
        public string followuptime { get; set; }
        [Display(Name = "Reminder Date")]
        public DateTime reminderdate { get; set; }
        [Display(Name = "Reminder Time")]
        public string remindertime { get; set; }
        [Display(Name = "Reminder By")]
        public string remainder_by { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "Date")]
        public DateTime Last_Call_on { get; set; }
        [Display(Name = "Duration")]
        public string Duration { get; set; }



    }
}