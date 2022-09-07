
namespace E_Library.Services.Contracts
{
    public interface IStatisticsService
    {
        public Task<int> GetTotalUsersAsync();
        public Task<int> GetTotalBooksAsync();
    }
}
