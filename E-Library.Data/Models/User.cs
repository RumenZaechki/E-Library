using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public Cart Cart { get; set; }
    }
}
