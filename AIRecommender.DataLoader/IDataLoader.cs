using AIRecommender.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender_DataLoader
{
    public interface IDataLoader
    {
          BookDetails Load();
    }
}
