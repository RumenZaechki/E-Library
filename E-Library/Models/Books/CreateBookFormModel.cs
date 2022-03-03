using System.ComponentModel.DataAnnotations;

namespace E_Library.Models.Books
{
    public class CreateBookFormModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //public string Category { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
        public string Release { get; set; }
        public string Author { get; set; }
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        public IEnumerable<BookCategoryViewModel> Categories { get; set; }
    }
}
