using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Front_5.Models
{
    public class LoginVM
    {
        [Required]

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [ValidateNever]
        public bool Isremember { get; set; }
    }
}
