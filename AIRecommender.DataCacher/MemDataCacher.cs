using AIRecommender.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.DataCacher
{
    public class MemDataCacher:IDataCacher
    {
        private BookDetails cachedData;

        public MemDataCacher()
        {
            cachedData = null;
        }

        public BookDetails GetData()
        {
            return cachedData;
        }

        public void SetData(BookDetails bookDetails)
        {
            cachedData = bookDetails;
        }
    }
}
