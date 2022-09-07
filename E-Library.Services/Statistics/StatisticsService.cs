using E_Library.Data;
using E_Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly LibraryDbContext data;
        public StatisticsService(LibraryDbContext data)
        {
            this.data = data;
        }
        public async Task<int> GetTotalUsersAsync()
        {
            return await this.data.Users.CountAsync();
        }
        public async Task<int> GetTotalBooksAsync()
        {
            return await this.data.Books.CountAsync();
        }
    }
}
