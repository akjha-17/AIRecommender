using AIRecommender.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender_DataLoader
{
    public class CSVDataLoader:IDataLoader
    {
        string BookPath = "C:\\Users\\Aman kumar jha\\Desktop\\Recommender\\BX-CSV-Dump\\BX-Books.csv";
        string RatingPath = "C:\\Users\\Aman kumar jha\\Desktop\\Recommender\\BX-CSV-Dump\\BX-Book-Ratings.csv";
        string UsersPath = "C:\\Users\\Aman kumar jha\\Desktop\\Recommender\\BX-CSV-Dump\\BX-Users.csv";

        public BookDetails Load()
        {

            List<BookUserRating> ratings = new List<BookUserRating>();
            List<User> users = new List<User>();
            List<Book> books = new List<Book>();
            // BookDetails bookDetails = new BookDetails();
            try
            {
                using (StreamReader reader = new StreamReader(BookPath))
                {
                    reader.ReadLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');

                        //for (int j = 0; j < parts.Length; j++)
                        Parallel.For(0, parts.Length, j =>
                        {
                            parts[j] = parts[j].Trim('"');
                        });
                        books.Add(new Book
                        {
                            ISBN = parts[0],
                            BookTitle = parts[1],
                            BookAuthor = parts[2],
                            YearOfPublication = parts[3],
                            Publisher = parts[4],
                            ImageUrlSmall = parts[5],
                            ImageUrlMedium = parts[6],
                            ImageUrlLarge = parts[7]
                        });
                        // Book book = null;
                        //Console.WriteLine(" ISBN"+parts[0]+" "+parts[1]+ " " + parts[2]+ " " + parts[3]+ " " + parts[4]+ " " + parts[5]+ " " + parts[6]+ " " + parts[7]);
                        //Book b = new Book { ISBN = "hi" };
                        //book=new Book { ISBN = parts[0], BookTitle = parts[1], BookAuthor = parts[2], YearOfPublication = parts[3], Publisher = parts[4], ImageUrlSmall = parts[5], ImageUrlMedium = parts[6], ImageUrlLarge = parts[7] };
                        //bookDetails.Books.Add(b);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data from CSV in Books: " + ex.Message);
            }
            try
            {
                using (StreamReader reader = new StreamReader(RatingPath))
                {
                    reader.ReadLine();
                    string RatingLine;
                    while ((RatingLine = reader.ReadLine()) != null)
                    {
                        string[] parts = RatingLine.Split(';');

                        //for (int j = 0; j < parts.Length; j++)
                        Parallel.For(0, parts.Length, j =>
                        {
                            parts[j] = parts[j].Trim('"');
                        });
                        ratings.Add(new BookUserRating
                        {
                            UserID = parts[0],
                            ISBN = parts[1],
                            Rating = parts[2]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data from CSV in Ratings: " + ex.Message);
            }
            try
            {

                using (StreamReader reader = new StreamReader(UsersPath))
                {
                    reader.ReadLine();
                    string UserLine;
                    
                    while ((UserLine = reader.ReadLine()) != null)
                    {
                        string[] parts = UserLine.Split(';');

                        if (parts.Length != 3)
                            continue;
                        if (parts[2] == "NULL")
                            continue;
                        //for (int j = 0; j < 3; j++)
                        Parallel.For(0, 3, j =>
                        {
                            parts[j] = parts[j].Trim('"');
                            //Console.WriteLine(parts[j]);
                        });

                        string[] RegionPart = parts[1].Split(',');
                        if (RegionPart.Length != 3)
                            continue;
                        //Console.WriteLine($"{parts[0]} {RegionPart[0]} {RegionPart[1]} {RegionPart[2]} {parts[2]}");

                        parts[0] = parts[0].Trim();
                        if (RegionPart[1].Length == 0 || (RegionPart[1].Length == 1 && RegionPart[1][0] == ' '))
                            continue;
                        if (RegionPart[2].Length == 0 || (RegionPart[2].Length == 1 && RegionPart[2][0] == ' '))
                            continue;

                        if (RegionPart[1][0] == ' ')
                            RegionPart[1] = RegionPart[1].Substring(1);
                        if (RegionPart[2][0] == ' ')
                            RegionPart[2] = RegionPart[2].Substring(1);
                        parts[2] = parts[2].Trim();
                        users.Add(new User
                        {
                            UserID = parts[0],
                            City = RegionPart[0],
                            State = RegionPart[1],
                            Country = RegionPart[2],
                            Age = parts[2]
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data from CSV in Users: " + ex.Message);

            }
            return new BookDetails { Books = books, Users = users, Ratings = ratings };

        }
    }
}
