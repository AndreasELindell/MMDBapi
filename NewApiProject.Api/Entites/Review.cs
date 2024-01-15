using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewApiProject.Api.Entites
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int movieId { get; set; }
        [Required]
        public string movieTitle { get; set; } = string.Empty;
        [Required]
        public int userId { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        [Range(1, 10)]
        public float rating { get; set; }
        [Required]
        public DateTime created { get; set; }

    }
}
