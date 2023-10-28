using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class HeroProfile
    {
        public int GameId { get; set; }

        public int CategoryId { get; set; }

        public int HeroId { get; set; }

        public int ValueId { get; set; }

        private HeroProfile()
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
