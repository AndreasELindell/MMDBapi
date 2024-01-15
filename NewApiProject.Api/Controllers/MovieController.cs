using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewApiProject.Api.Entites;
using NewApiProject.Api.Models;
using NewApiProject.Api.Repositories;
using NewApiProject.Api.Services;
using System.Text.Json;

namespace NewApiProject.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IUserRepository userRepository, IMapper mapper)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper;
        }
        [HttpGet(Name = "Popular Movies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetPopularMovies()
        {
            var apiResponse = await TMDBService
                .GetMoviesFromTMDBUrl("https://api.themoviedb.org/3/movie/popular?language=en-US&page=1");

            return Ok(apiResponse.results);

        }

        [HttpGet("{searchQuery}/{pageNumber}")]
        public async Task<ActionResult<List<MovieDto>>> GetSearchMovies(string searchQuery, int pageNumber) 
        {

            var searchResult = await TMDBService.GetMoviesFromTMDBUrl($"https://api.themoviedb.org/3/search/movie?query={searchQuery}&include_adult=false&language=en-US&page={pageNumber}");

            var genres = await TMDBService.GetAllMovieGenresFromTMBD();

            var movies = searchResult.results!;

            if (movies is not null)
            {
                foreach (var movie in movies)
                {
                    var matchingGenres = genres.Where(genre => movie.genre_ids.Contains(genre.id)).ToList();
                    movie.genres.AddRange(matchingGenres);
                }

            }
            return Ok(new { PageData = new {searchResult.total_results, searchResult.total_pages}, movies});
        }

        [HttpGet("{movieId}", Name = "GetMovie")]
        public async Task<ActionResult<MovieDto>> GetMovie(int movieId)
        {
            var movie = await TMDBService.GetMovieByIDFromTMDB(movieId);

            if (movie == null) 
            { 
                return NotFound();
            }

            return Ok(movie);

        }
        [HttpGet("{movieId}/reviews", Name = "GetReviewsForMovie")]
        public async Task<ActionResult<List<ReviewDto>>> GetReviewsForMovies(int movieId)
        {
            var reviewsToReturn = new List<ReviewDto>();

            var reviewsFromTMBD = await TMDBService.GetReviewsForMovieFromTMDB(movieId) ?? new List<ReviewDto>();

            var reviewsFromDB = await _movieRepository.GetReviewsForMovie(movieId);

            if (reviewsFromTMBD is null && reviewsFromDB is null)
            {
                return NotFound();
            }

            reviewsToReturn = reviewsFromTMBD;

            if (reviewsFromDB is not null)
            {
                foreach (var review in reviewsFromDB)
                {
                    Author_details author = new Author_details()
                    {
                        id = review.userId,
                        rating = review.rating,
                        name = await _userRepository.GetUserNameById(review.userId)
                    };

                    ReviewDto reviewDto = new ReviewDto()
                    {
                        content = review.content,
                        updated_at = review.created.ToString(),
                        author_details = author,
                    };
                    reviewsToReturn.Add(reviewDto);
                }
            }

            return Ok(reviewsToReturn);
        }
        [HttpGet("reviews/{userId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetUsersReviews(int userId) 
        {
            var reviewsToReturn = new List<ReviewDto>();

            var reviewsFromDB = await _movieRepository.GetReviewsFromUser(userId);

            if(reviewsFromDB is null) 
            {
                return NotFound();
            }

            if(reviewsFromDB is not null) 
            {
                foreach (var review in reviewsFromDB)
                {
                    Author_details author = new Author_details()
                    {
                        id = review.userId,
                        rating = review.rating,
                        name = await _userRepository.GetUserNameById(review.userId)
                    };

                    ReviewDto reviewDto = new ReviewDto()
                    {
                        movieId = review.movieId,
                        movieTitle = review.movieTitle,
                        content = review.content,
                        updated_at = review.created.ToString(),
                        author_details = author,
                    };
                    reviewsToReturn.Add(reviewDto);
                }
            }

            return Ok(reviewsToReturn);
        }

        [HttpPost("{movieId}/reviews")]
        public async Task<ActionResult> AddUserReviewForMovie(int movieId, ReviewDto review) 
        {
            Review reviewToAdd = new Review()
            {
                movieTitle = review.movieTitle,
                movieId = movieId,
                userId = review.author_details!.id,
                content = review.content,
                rating = (float)review.author_details.rating!,
                created = DateTime.Now
            };

            await _movieRepository.AddReview(reviewToAdd);
            return Ok(reviewToAdd);
        }
        [HttpGet("wishlist/{userId}")]
        public async Task<ActionResult<List<WishlistItemDto>>> GetWishlistForUser(int userId) 
        {
            var wishlist = await _movieRepository.GetWishlistItemsForUser(userId);

            var wishlistToReturn = new List<WishlistItemDto>();


            if (wishlist is null) 
            { 
                return NotFound();
            }

            foreach (var wishlistItem in wishlist) 
            {
                var movie = new MovieDto()
                {
                    id = wishlistItem.MovieId,
                    title = wishlistItem.Title,
                    release_date = wishlistItem.Release_date,
                    overview = wishlistItem.Description,
                    vote_average = wishlistItem.vote_average,
                    vote_count = wishlistItem.vote_count,
                    poster_path = wishlistItem.Poster_path
                };

                var wlidto = new WishlistItemDto()
                {
                    Movie = movie,
                    UserId = wishlistItem.UserId,
                    Watched = wishlistItem.Watched
                };
                wishlistToReturn.Add(wlidto);
            }
            

            return Ok(wishlistToReturn);
        }
        [HttpPost("wishlist/")]
        public async Task<ActionResult<List<WishlistItemDto>>> AddWishlist(WishlistItemDto wishlistItem)
        {

            WishlistItem wishlistItemToAdd = new WishlistItem()
            {
                vote_average = wishlistItem.Movie.vote_average,
                vote_count = wishlistItem.Movie.vote_count,
                Poster_path = wishlistItem.Movie.poster_path,
                Release_date = wishlistItem.Movie.release_date,
                MovieId = wishlistItem.Movie.id,
                Description = wishlistItem.Movie.overview,
                Title = wishlistItem.Movie.title,
                UserId = wishlistItem.UserId,
                Watched = wishlistItem.Watched
            };

            await _movieRepository.AddWishlistItem(wishlistItemToAdd);
            return Ok(wishlistItemToAdd);
        }
    }
}
