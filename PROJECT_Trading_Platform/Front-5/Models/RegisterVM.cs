using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Front_5.Models
{
    public class RegisterVM
    {
        //[Required(ErrorMessage = "Name is required")]
        //[StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string confirmpass { get; set; }
        [ValidateNever]
        public bool Isremember { get; set; }    
    }
}
