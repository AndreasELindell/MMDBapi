using NewApiProject.Api.Models;
using RestSharp;
using System.Text.Json;

namespace NewApiProject.Api.Services;

public static class TMDBService
{
    public static async Task<ApiResponse> GetMoviesFromTMDBUrl(string fullUrl)
    {
        var options = new RestClientOptions(fullUrl);
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJlYTBjNWI5ZTI2YzMyNThmNzYyODNmN2Y0YThiYmM0MyIsInN1YiI6IjY1ODQ0MDUzODgwNTUxNDE0YjI3ZDc5MiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.9lQZyvqWTIuS2-JLNsn7CJkKhiYVwNFREHzzjPBbgoM");
        var response = await client.GetAsync(request);
        ApiResponse testnewmovie = JsonSerializer.Deserialize<ApiResponse>(response.Content)!;

        return testnewmovie;
    }
    public static async Task<MovieDto> GetMovieByIDFromTMDB(int id)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJlYTBjNWI5ZTI2YzMyNThmNzYyODNmN2Y0YThiYmM0MyIsInN1YiI6IjY1ODQ0MDUzODgwNTUxNDE0YjI3ZDc5MiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.9lQZyvqWTIuS2-JLNsn7CJkKhiYVwNFREHzzjPBbgoM");
        var response = await client.GetAsync(request);
        var testnewmovie = JsonSerializer.Deserialize<MovieDto>(response.Content)!;

        if(testnewmovie != null)
        {
            return testnewmovie;
        }

        return null;
    }
    public static async Task<List<ReviewDto>> GetReviewsForMovieFromTMDB(int id)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{id}/reviews");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJlYTBjNWI5ZTI2YzMyNThmNzYyODNmN2Y0YThiYmM0MyIsInN1YiI6IjY1ODQ0MDUzODgwNTUxNDE0YjI3ZDc5MiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.9lQZyvqWTIuS2-JLNsn7CJkKhiYVwNFREHzzjPBbgoM");
        var response = await client.GetAsync(request);
        ReviewApiRespones reviews = JsonSerializer.Deserialize<ReviewApiRespones>(response.Content)!;

        if (reviews != null)
        {
            return reviews.results!;
        }

        return null;
    }
    public static async Task<List<Genre>> GetAllMovieGenresFromTMBD() 
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/genre/movie/list");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJlYTBjNWI5ZTI2YzMyNThmNzYyODNmN2Y0YThiYmM0MyIsInN1YiI6IjY1ODQ0MDUzODgwNTUxNDE0YjI3ZDc5MiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.9lQZyvqWTIuS2-JLNsn7CJkKhiYVwNFREHzzjPBbgoM");
        var response = await client.GetAsync(request);
        ApiGenreResponse genres = JsonSerializer.Deserialize<ApiGenreResponse>(response.Content)!;

        if (genres != null)
        {
            return genres.genres!;
        }

        return null;
    }
}