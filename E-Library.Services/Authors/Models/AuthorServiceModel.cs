﻿namespace E_Library.Services.Authors.Models
{
    public class AuthorServiceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<AuthorBookServiceModel> Books { get; set; } = new List<AuthorBookServiceModel>();
    }
}
