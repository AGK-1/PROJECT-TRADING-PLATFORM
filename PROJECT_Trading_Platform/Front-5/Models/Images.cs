using System.ComponentModel.DataAnnotations.Schema;

namespace Front_5.Models
{
    public class Images
    {
        public int Id { get; set; } 

        public string ImaUrl { get; set; }

        public int CardId { get; set; }
        [ForeignKey("CardId")]
        public Sport_pro Cards { get; set; }

    }
}
