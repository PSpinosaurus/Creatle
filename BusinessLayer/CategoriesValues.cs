using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class CategoriesValues
    {
        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Value { get; set; }

        private CategoriesValues()
        {
                
        }

        public CategoriesValues(int categoryId, string value)
        {
            CategoryId = categoryId;
            Value = value;
        }

    }
}
