using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class HeroMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Ashe, Braum (the name of the champ)

        public string Avatar { get; set; } // the small icon that appears 

        [ForeignKey("HeroId")]
        public List<HeroProfile> HeroProfiles { get; set; } // beacause the champ can appear in many games/categories and can have several values

        public HeroMetadata()
        {

        }

        public HeroMetadata(string name, string avatar)
        {
            Name = name;
            Avatar = avatar;
        }

    }
}
