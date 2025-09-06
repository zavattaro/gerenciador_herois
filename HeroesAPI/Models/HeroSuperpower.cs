namespace HeroesAPI.Models
{
    public class HeroSuperpower
    {
        public int HeroId { get; set; }
        public Hero Hero { get; set; } = null!;
        public int SuperpowerId { get; set; }
        public Superpower Superpower { get; set; } = null!;
    }
}