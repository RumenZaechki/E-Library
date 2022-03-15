using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Data.Models
{
    public class BookCart
    {
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
