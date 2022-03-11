using E_Library.Data.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
