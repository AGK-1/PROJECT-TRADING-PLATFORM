using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using X.PagedList;

namespace Front_5.Models
{
    public class Sport_pro
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string? photo1 { get; set; }
        public string? photo2 { get; set; }
        public string price { get; set; }

        public string description { get; set; }

        public bool Ischeck { get; set; } = true;
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        //public int ColorId { get; set; }
        //[ForeignKey("ColorId")]
       
        public Category? Category { get; set; }
        //public Colors? Colors { get; set; }
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public Colors? Color { get; set; }

        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public Size? Size { get; set; }
        public int CategorytwoId { get; set; }
        [ForeignKey("CategorytwoId")]
        public Category_two? Category_two { get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile file1{ get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile? file2 { get; set; }

        //public bool? IsCheck { get; set; } = true;
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; } // List of files to be uploaded

        // Navigation property for related images
        public List<Images> Images { get; set; } = new List<Images>();

      
    }
}
