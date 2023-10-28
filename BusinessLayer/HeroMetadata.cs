using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class HeroMetadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Avatar { get; set; }

        public List<HeroProfile> HeroProfiles { get; set; }

        private HeroMetadata()
        {

        }

        public HeroMetadata(string name, string avatar)
        {
            Name = name;
            Avatar = avatar;
        }

    }
}
