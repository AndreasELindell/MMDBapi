using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewApiProject.Api.Entites
{
    public class WishlistItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required] 
        public string Description { get; set; }
        public string? Release_date { get; set; }
        public string? Poster_path { get; set; }
        public decimal vote_average { get; set; }
        public int vote_count { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool Watched { get; set; }
    }
}
