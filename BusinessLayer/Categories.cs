using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // name like "gender" 

        [ForeignKey("CategoryId")]
        public List<Answer> Answers { get; set; } = new List<Answer>(); // list of the answers that were randomly chosen for a specific date and game

        public List<CategoriesValues> CategoriesValues { get; set; } = new List<CategoriesValues> { }; // list of all values [male, female, other]

        [ForeignKey("CategoryId")]
        public List<HeroProfile> HeroProfiles { get; set; } = new List<HeroProfile> { }; // heroes that fall into this category

        public Categories()
        {
                
        }

        public Categories(string name)
        {
            this.Name = name;
        }
    }
}
