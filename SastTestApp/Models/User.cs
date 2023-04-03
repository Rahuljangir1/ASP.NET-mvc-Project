using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SastTestApp.Models
{
    public class User
    {
        [Required]
        [Display(Name ="UserName")]
        [MaxLength(15)]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MaxLength(15)]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}