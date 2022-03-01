using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class BookCategory
    {
        [Required]
        public string BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
