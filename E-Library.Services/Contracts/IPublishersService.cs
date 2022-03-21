using E_Library.Services.Publishers.Models;

namespace E_Library.Services.Contracts
{
    public interface IPublishersService
    {
        public PublisherServiceModel Details(string publisherId);
    }
}
