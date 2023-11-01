using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Answer
    {
        public DateTime Date { get; set; }

        public int GameId { get; set; }

        public int CategoryId { get; set; }

        private Answer()
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
