using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Game
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Colour { get; set; }

        public List<HeroProfile> HeroesProfiles { get; set; } = new List<HeroProfile>();

        public List<Answer> Answers { get; set; } = new List<Answer>();

        private Game()
        {

        }
        
        public Game(string name, int colour, string? description)
        {
            Name = name;
            Description = description;
            Colour = colour;
        }

    }
}