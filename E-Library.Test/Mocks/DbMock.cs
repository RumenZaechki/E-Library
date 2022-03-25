using E_Library.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_Library.Test.Mocks
{
    public static class DbMock
    {
        public static LibraryDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

                return new LibraryDbContext(dbContextOptions);
            }
        }
    }
}
