namespace E_Library.Models.Reviews
{
    public class ReviewViewModel
    {
        public ICollection<CommentsViewModel> Comments { get; set; } = new List<CommentsViewModel>();
        public string BookId { get; set; }
    }
}
