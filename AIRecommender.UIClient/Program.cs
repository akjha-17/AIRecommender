using AIRecommender.CoreEngine;
using AIRecommender_DataLoader;
using AIRecommender.DataAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.DTO;
using AIRecommender.Entities;
using AIRecommender.DataLoader;
using AIRecommender.UIClient;
using AIRecommender.DataCacher;

namespace AIRecommenderUIClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            string ISBN; //= "0140434801";
            string State; //= "british columbia";
            int Age; //= 23;
            string cont;
            //Preference p=new Preference { ISBN = ISBN, State = State, Age = Age };
            IList<Book> books = null;
           //IDataLoader dataLoad = new CSVDataLoader();

            DataLoaderFactory factory = DataLoaderFactory.Instance;
            IRecommender recommender = new PearsonRecommender();
            IRatingsAggrigator aggrigator = new RatingsAggregrator();
           // IDataCacher dataCacher=new MemDataCacher();
            IDataCacher dataCacher = new RedisDataCacher();
            BookDataService bookDataService=new BookDataService(factory, dataCacher);
            //books = new AIRecommendationEngine(dataLoad, recommender, aggrigator).Recommend(p,10);
            //books = new AIRecommendationEngine(factory.GetDataLoader(), recommender, aggrigator).Recommend(p, 10);

            AIRecommendationEngine recommend=new AIRecommendationEngine(bookDataService, recommender, aggrigator);
            
            //books = new AIRecommendationEngine(bookDataService, recommender, aggrigator).Recommend(p, 10);
            


            do
            {
                Preference p = new Preference();
                Console.WriteLine("Enter ISBN");
                ISBN= Console.ReadLine();
                Console.WriteLine("Enter State");
                State= Console.ReadLine();
                Console.WriteLine("Enter Age");
                Age=int.Parse(Console.ReadLine());
                p.ISBN = ISBN;
                p.State = State;
                p.Age = Age ;
                books = recommend.Recommend(p, 10);
                foreach (Book book in books)
                {
                    Console.WriteLine(book.ISBN + " " + book.BookTitle);
                }
                Console.WriteLine("Continue ? enter 'y' or enter 'n'");
                cont=Console.ReadLine();
            }
            while (cont=="y");
        }
    }
}
