using NewApiProject.Api.Entites;

namespace NewApiProject.Api.Models
{
    public class MovieDto
    {

        public int id { get; set; }
        public string? title { get; set; }
        public string? overview { get; set; }
        public string? release_date { get; set; }
        public string? poster_path { get; set; }
        public string? backdrop_path { get; set; }
        public decimal vote_average { get; set; }
        public int vote_count { get; set; }
        public List<Genre>? genres { get; set; } = new List<Genre>();
        public List<int>? genre_ids { get; set; }
        public double budget { get; set; }
        public double revenue { get; set; }
        public float popularity { get; set; }
        public int runtime { get; set; }

    }
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
    }
    public class ApiGenreResponse 
    { 
        public List<Genre>? genres { get; set; }
    }
    public class ApiResponse
    {
        public int? total_pages { get; set; }
        public int? total_results { get; set; }
        public List<MovieDto>? results { get; set; }
    }
}
