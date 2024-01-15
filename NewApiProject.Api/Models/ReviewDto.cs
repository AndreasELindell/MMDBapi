namespace NewApiProject.Api.Models
{
    public class ReviewDto
    {
        public Author_details? author_details { get; set; }
        public string content { get; set; } = string.Empty;
        public string updated_at { get; set; } = string.Empty;
        public int movieId { get; set; }
        public string movieTitle { get; set; } = string.Empty;

    }
    public class ReviewApiRespones
    {
        public List<ReviewDto>? results { get; set; }
    }
    public class Author_details
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? avatar_path { get; set; }
        public float? rating { get; set; }
    }
}
