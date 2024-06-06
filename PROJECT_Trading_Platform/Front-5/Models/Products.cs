using System.ComponentModel.DataAnnotations.Schema;

namespace Front_5.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double price { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public Category Category { get; set; }  
        public List<Images> Images { get; set; }
                
    }
}
