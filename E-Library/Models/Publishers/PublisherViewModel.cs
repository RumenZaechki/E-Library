namespace E_Library.Models.Publishers
{
    public class PublisherViewModel
    {
        public string Name { get; set; }
        public ICollection<PublisherBookViewModel> Books { get; set; } = new List<PublisherBookViewModel>();
        public ICollection<PublisherAuthorViewModel> Authors { get; set; } = new List<PublisherAuthorViewModel>();
    }
}
