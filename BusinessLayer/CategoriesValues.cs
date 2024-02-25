using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class CategoriesValues
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; } // for example male or female

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; } // which category does it belong to
        public Categories Category { get; set; }  // the category

        public List<HeroProfile> HeroProfiles { get; set; } = new List<HeroProfile>();

        public List<Answer> Answers { get; set; } = new List<Answer>();

        public CategoriesValues()
        {
                
        }

        public CategoriesValues(int categoryId, string value)
        {
            CategoryId = categoryId;
            Value = value;
        }

    }
}
