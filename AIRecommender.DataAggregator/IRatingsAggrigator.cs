using AIRecommender.DTO;
using AIRecommender.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.DataAggregator
{
    public interface IRatingsAggrigator
    {
        Dictionary<string, List<int>> Aggrigate(BookDetails bookDetails,Preference preference);
    }
}
