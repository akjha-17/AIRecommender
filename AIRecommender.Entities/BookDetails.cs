using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.Entities
{
    public class BookDetails
    {
        public List<Book> Books { get; set; }
        public List<BookUserRating> Ratings { get; set; }
        public List<User> Users { get; set; }
    }
}
