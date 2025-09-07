using System.ComponentModel.DataAnnotations;

namespace HeroesAPI.DTOs
{
    public class CreateHeroRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string HeroName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public double Height { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public List<int> SuperpowerIds { get; set; } = new List<int>();
    }

}