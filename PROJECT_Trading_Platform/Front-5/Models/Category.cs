using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Front_5.Models
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public bool ischeck { get; set; } 
       // public List<Sport_pro> Sport_Pros{ get; set; }
        //  public List<Products> Products { get; set; }
    }

}
