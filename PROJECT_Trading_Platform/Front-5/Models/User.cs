using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Front_5.Models
{
    public class User : IdentityUser
    {
 
        public string Name { get; set; }    
        
        public string Surname{ get; set; }

        public int Age { get; set; }

    }
}
