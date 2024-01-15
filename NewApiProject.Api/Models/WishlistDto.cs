using NewApiProject.Api.Entites;
using System.ComponentModel.DataAnnotations;

namespace NewApiProject.Api.Models
{
    public class WishlistItemDto
    {
        public int Id { get; set; }
        public MovieDto Movie { get; set; }
        public int UserId { get; set; }
        public bool Watched { get; set; }
    }
}
