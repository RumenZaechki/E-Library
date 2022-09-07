using E_Library.Services.Publishers.Models;

namespace E_Library.Services.Contracts
{
    public interface IPublishersService
    {
        public Task<PublisherServiceModel> DetailsAsync(string publisherId);
    }
}