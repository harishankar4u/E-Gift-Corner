using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LG_10_Exercise_01.Models
{
    public class Feedback
    {
        public long ID { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string Customer_Name { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Please enter the Email address in the correct format")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{3}\-[0-9]{3}\-[0-9]{4}", ErrorMessage = "Please enter a valid Phone Number in the format, XXX-XXX-XXXX")]
        [Display(Name = "Phone Number")]
        public string Phone_Number { get; set; }
        [Required]
        [Display(Name = "Message Type")]
        public bool Message_Type { get; set; }
        [Required]
        public string Regarding { get; set; }
        [Required]
        [Display(Name = "Your Message")]
        public string Your_Message { get; set; }
    }
}