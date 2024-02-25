using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class Game
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Colour { get; set; } // maybe make a table with primary/secondary colour where we can store palletes and use them on the layout for the games

        [ForeignKey("GameId")]
        public List<HeroProfile> HeroesProfiles { get; set; } = new List<HeroProfile>();

        public List<Answer> Answers { get; set; } = new List<Answer>();

        public Game()
        {

        }
        
        public Game(string name, int colour, string description)
        {
            Name = name;
            Description = description;
            Colour = colour;
        }

    }
}