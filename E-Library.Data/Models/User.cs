using E_Library.Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(UserConstants.UsernameMaxLength)]
        public string Username { get; set; }
        [Required]
        [StringLength(UserConstants.EmailMaxLength)]
        public string Email { get; set; }
        [Required]
        [StringLength(UserConstants.PasswordMaxLength)]
        public string Password { get; set; }
        [Required]
        public string CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
    }
}
