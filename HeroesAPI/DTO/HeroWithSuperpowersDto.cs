using HeroesAPI.Models;

namespace HeroesAPI.DTOs
{
    public class HeroWithSuperpowersDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HeroName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<SuperpowerDto> Superpowers { get; set; } = new List<SuperpowerDto>();
    }

    public class SuperpowerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}