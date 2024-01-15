using Microsoft.EntityFrameworkCore;
using NewApiProject.Api.DbContext;
using NewApiProject.Api.Entites;
using NewApiProject.Api.Services;

namespace NewApiProject.Api.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task AddReview(Review review)
        {
            _context.Reviews.Add(review);
            await SaveChangesAsync();
        }

        public async Task<List<Review>> GetReviewsForMovie(int movieId)
        {
            return await _context.Reviews.Where(r => r.movieId == movieId).ToListAsync();
        }

        public async Task<List<Review>> GetReviewsFromUser(int userId)
        {
            return await _context.Reviews.Where(r => r.userId == userId).ToListAsync();
        }

        public async Task<List<WishlistItem>> GetWishlistItemsForUser(int userId)
        {
            return await _context.Wishlist.Where(w => w.UserId == userId).ToListAsync();
        }
        public async Task AddWishlistItem(WishlistItem wishlistItem)
        {
            _context.Wishlist.Add(wishlistItem);
            await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
