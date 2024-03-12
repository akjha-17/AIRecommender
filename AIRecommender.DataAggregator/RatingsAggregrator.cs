using AgeGroup;
using AIRecommender.DTO;
using AIRecommender.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.DataAggregator
{
    public class RatingsAggregrator: IRatingsAggrigator
    {
        public Dictionary<string, List<int>> Aggrigate(BookDetails bookDetails, Preference preference)
        {
            Dictionary<string, List<int>> bookRating = new Dictionary<string, List<int>>();
            int ageGroup = AgeGroupCategory.FindAgeGroup(preference.Age);
            string state=preference.State;
            List<User> UserList=new List<User>();
            //UserList = bookDetails.Users.Where(r => ((r.State == state) && (AgeGroupCategory.FindAgeGroup(int.Parse(r.Age)) == ageGroup))).ToList();
            foreach(User r in bookDetails.Users) {
                if((r.State == state)&& (AgeGroupCategory.FindAgeGroup(int.Parse(r.Age)) == ageGroup))
                {
                    UserList.Add(r);
                }
            }
            //foreach(var user in UserList)
            Parallel.ForEach(UserList, user =>
            {
                foreach (var rating in bookDetails.Ratings)
                {
                    if (user.UserID == rating.UserID)
                    {
                        if (!bookRating.ContainsKey(rating.ISBN))
                        {
                            // If  ISBN not in  dictionary, add with an empty list
                            bookRating[rating.ISBN] = new List<int>();
                        }

                        // Add  rating to  list of ratings for this ISBN
                        bookRating[rating.ISBN].Add(int.Parse(rating.Rating));
                    }
                }
            }
            );
            return bookRating;
        }
        

    }
}
