using AIRecommender.CoreEngine;
using AIRecommender.DataAggregator;
using AIRecommender_DataLoader;
using AIRecommender.DTO;
using AIRecommender.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.UIClient;

namespace AIRecommenderUIClient
{
    public class AIRecommendationEngine
    {
        private readonly BookDataService bookDataService;
        //private IDataLoader dataLoader;
        private IRecommender recommender;
        private IRatingsAggrigator aggregator;
        //public AIRecommendationEngine(IDataLoader dataLoader, IRecommender recommender, IRatingsAggrigator aggregator)
        public AIRecommendationEngine(BookDataService bookDataService, IRecommender recommender, IRatingsAggrigator aggregator)
        
        {
            this.bookDataService = bookDataService;
            //this.dataLoader = dataLoader;
            this.recommender = recommender;
            this.aggregator = aggregator;
        }
        public IList<Book> Recommend(Preference preference, int limit)
        {
            string myPref = preference.ISBN;
            //BookDetails bookDetails = dataLoader.Load();
            BookDetails bookDetails = bookDataService.GetBookDetails();
            Dictionary<string, List<int>> aggregatedRatings = aggregator.Aggrigate(bookDetails, preference);
            IList<Book> recommendedBooks = new List<Book>();
            int[] baseRating = aggregatedRatings[myPref].ToArray();
            Dictionary<Book, double> bookCorrelationMap = new Dictionary<Book, double>();
            foreach (var isbn in aggregatedRatings.Keys)
           // Parallel.ForEach(aggregatedRatings.Keys, isbn =>
            {
                if (isbn == myPref)
                    continue; // return; 
                int[] otherRating = aggregatedRatings[isbn].ToArray();
                double correlation = recommender.GetCorellation(baseRating, otherRating);
                Book b = (bookDetails.Books.Find(book => book.ISBN == isbn));
                /*if(correlation!=null)*/
                if (b == null)
                    continue;// return; 
                bookCorrelationMap[b] = correlation;
            }
            //);
            recommendedBooks = bookCorrelationMap.OrderByDescending(kv => kv.Value)
                                              .Select(kv => kv.Key)
                                              .ToList();
            if (recommendedBooks.Count > limit)
            {
                recommendedBooks = recommendedBooks.Take(limit).ToList();
            }
            return recommendedBooks;

        }
    }
}
