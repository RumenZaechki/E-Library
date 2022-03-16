using E_Library.Data;
using E_Library.Services.Contracts;

namespace E_Library.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly LibraryDbContext data;
        public StatisticsService(LibraryDbContext data)
        {
            this.data = data;
        }
        public int GetTotalUsers()
        {
            return this.data.Users.Count();
        }
        public int GetTotalBooks()
        {
            return this.data.Books.Count();
        }
    }
}
