namespace E_Library.Services.Publishers.Models
{
    public class PublisherServiceModel
    {
        public string Name { get; set; }
        public ICollection<PublisherBookServiceModel> Books { get; set; } = new List<PublisherBookServiceModel>();
        public ICollection<PublisherAuthorServiceModel> Authors { get; set; } = new List<PublisherAuthorServiceModel>();
    }
}
