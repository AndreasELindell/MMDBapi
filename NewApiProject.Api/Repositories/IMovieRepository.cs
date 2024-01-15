using NewApiProject.Api.Entites;
using NewApiProject.Api.Services;

namespace NewApiProject.Api.Repositories
{
    public interface IMovieRepository
    {
        Task AddReview(Review review);
        Task<bool> SaveChangesAsync();
        Task<List<Review>> GetReviewsForMovie(int movieId);
        Task<List<Review>> GetReviewsFromUser(int userId);
        Task AddWishlistItem(WishlistItem wishlistItem);

        Task<List<WishlistItem>> GetWishlistItemsForUser(int userId);
    }
}
