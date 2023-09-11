using System.ComponentModel.DataAnnotations;

namespace Movie_MVC.Models
{
    public class Movie
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]

        public DateTime ReleaseDate { get; set; }
        [Required]
        public string? MovieType { get; set; }
        [Required]
        public string? StarName { get; set; }
        [ScaffoldColumn(false)]
        public int isActive { get; set; }

    }
}
