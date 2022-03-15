using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public Cart Cart { get; set; }
    }
}
