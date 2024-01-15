using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewApiProject.Api.Entites;

namespace NewApiProject.Api.DbContext;

public class MovieContext : IdentityDbContext
{
    public MovieContext(DbContextOptions options) : base(options){}


    public DbSet<Review> Reviews { get; set; }
    public DbSet<Movie> Movies { get; set;}
    public DbSet<User> Users {  get; set; }
    public DbSet<WishlistItem> Wishlist { get; set; }

}
