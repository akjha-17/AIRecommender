using AIRecommender.DataCacher;
using AIRecommender.Entities;
using StackExchange.Redis;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.DataCacher
{
    public class RedisDataCacher : IDataCacher
    {
        private  ConnectionMultiplexer redisConnection;

        public RedisDataCacher()
        {
            redisConnection = ConnectionMultiplexer.Connect("localhost");
        }

        public BookDetails GetData()
        {
            
            var redisDatabase = redisConnection.GetDatabase();    // Connect to Redis database

            // Retrieve cached data from Redis
            string cachedDataString = redisDatabase.StringGet("bookDetails");

            if (cachedDataString == null)
            {
                return null; // cached data not found
            }

            BookDetails cachedData = JsonSerializer.Deserialize<BookDetails>(cachedDataString);

            return cachedData;
        }

        public void SetData(BookDetails bookDetails)
        {
            // Connect to Redis database
            var redisDatabase = redisConnection.GetDatabase();

            // Serialize bookDetails to JSON string
            string serializedData = JsonSerializer.Serialize(bookDetails);

            // Set cached data in Redis
            redisDatabase.StringSet("bookDetails", serializedData);
        }
    }
}



