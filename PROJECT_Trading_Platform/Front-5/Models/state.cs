using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Front_5.Models
{
    public class state
    {
        public int Id { get; set; }
        public string ad { get; set; }
        public string? sekil { get; set; }
        public string stat { get; set; }


        [NotMapped]
        [ValidateNever]
        public IFormFile file3 { get; set; }
    }
}
