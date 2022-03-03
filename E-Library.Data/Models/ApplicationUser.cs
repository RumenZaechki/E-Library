using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } 
    }
}
