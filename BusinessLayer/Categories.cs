using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Answer> Answers { get; set; } = new List<Answer>();

        public List<CategoriesValues> CategoriesValues { get; set; } = new List<CategoriesValues> { };

        public List<HeroProfile> HeroProfiles { get; set; } = new List<HeroProfile> { };

        private Categories()
        {
                
        }

        public Categories(string name)
        {
            this.Name = name;
        }
    }
}
