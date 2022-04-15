using E_Library.Models.Publishers;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublishersService publishersService;
        public PublishersController(IPublishersService publishersService)
        {
            this.publishersService = publishersService;
        }
        public IActionResult Details(string publisherId)
        {
            var publisherServiceModel = this.publishersService.Details(publisherId);
            if (publisherServiceModel == null)
            {
                return NotFound();
            }
            var viewModel = new PublisherViewModel
            {
                Name = publisherServiceModel.Name,
                Books = publisherServiceModel.Books
                        .Select(b => new PublisherBookViewModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            ImageUrl = b.ImageUrl
                        })
                        .ToList(),
                Authors = publisherServiceModel.Authors
                          .Select(a => new PublisherAuthorViewModel
                          {
                              Id = a.Id,
                              Name = a.Name
                          })
                          .ToList()
            };
            return View(viewModel);
        }
    }
}
