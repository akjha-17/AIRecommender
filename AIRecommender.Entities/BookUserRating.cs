using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.Entities
{
    public class BookUserRating
    {
        public string UserID  {  get; set; }
        public string ISBN { get; set; }
        public string Rating { get; set; }
    }
}
