using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class HeroProfile
    {
        public int GameId { get; set; } // FK to the HeroProfile list in Game 

        public int CategoryId { get; set; } // FK to the HeroProfile list in categories 

        public int HeroId { get; set; } // gives the info for the hero - HeroMetadata

        public int ValueId { get; set; } // is here to account for the possibility of a hero having more than one values, like "top and mid"
        
        [ForeignKey("ValueId")]
        public CategoriesValues Value { get; set; }

        public HeroProfile()
        {
                
        }

        public HeroProfile(int gameId, int categoryId, int heroId, int valueId)
        {
            GameId = gameId;
            CategoryId = categoryId;
            HeroId = heroId;
            ValueId = valueId;
        }
    }
}
