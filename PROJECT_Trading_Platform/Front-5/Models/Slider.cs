using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Front_5.Models
{
    public class Slider
    {
            public int Id { get; set; }
            public string text2 { get; set; }
            public string? picture { get; set; }

        [NotMapped]
        [ValidateNever]
        public IFormFile file { get; set; }


    }
}
