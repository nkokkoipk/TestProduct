using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Student
    {
        public string Id { get; set; }

        [Display(Name="Full Name")]
        [Required(ErrorMessage = "Please input Full Name")]
        [StringLength(10,ErrorMessage = "Name should be between 1 and 10 characters")]
        public string Name { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "Please input Description")]
        public string Description { get; set; }

        public DateTime? DateBirth { get; set; }

        public int Score { get; set; }
    }
}