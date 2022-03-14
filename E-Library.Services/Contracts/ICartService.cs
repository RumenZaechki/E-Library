using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Services.Contracts
{
    public interface ICartService
    {
        public void AddBookToCart(string userId, string bookId);
    }
}
