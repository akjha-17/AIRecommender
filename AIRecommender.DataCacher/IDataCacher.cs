using AIRecommender.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.DataCacher
{
    public interface IDataCacher
    {
        BookDetails GetData();
        void SetData(BookDetails bookDetails);
    }
}
