using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer
{
    public class Answer
    {
        public DateTime Date { get; set; } // when was this an answer

        public int GameId { get; set; } // for which game

        public int CategoryId { get; set; } // for which category

        public int CategoryValueId { get; set; } // the id of the answer

        [ForeignKey("CategoryValueId")]
        public CategoriesValues CategoryValue { get; set; }

        public Answer()
        {
              
        }

        public Answer(DateTime date, int gameId, int categoryId)
        {
            Date = date;
            GameId = gameId;
            CategoryId = categoryId;
        }
    }
}
