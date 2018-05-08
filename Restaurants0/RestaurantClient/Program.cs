using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using NLog;

namespace RestaurantClient
{
    class Program
    {
        static public void SerializeRestaurants()
        {
            string temp = Serializer.SerializeToJSON();
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\Restaraunts.txt", temp);
        }

        static public void DeserializeRestaurants()
        {
            string filePath = @"C:\Users\Public\TestFolder\Restaraunts.txt";
            Serializer.JSONToDeserialize(filePath);
        }

        static public void ShowDetails()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Please enter the restaurant id and press Enter:");
            int restId = Convert.ToInt32(Console.ReadLine());
            RestaurantCrud crud = new RestaurantCrud();
            var d = crud.FindRestById(restId);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine(d.name + "\n" + d.address + "\n" + d.city + ", " + d.state + " " + d.zipCode + "\n" + d.phoneNumber);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
        }
        static public void ShowDetails(int restId)
        {
            RestaurantCrud crud = new RestaurantCrud();
            var d = crud.FindRestById(restId);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine(d.name + "\n" + d.address + "\n" + d.city + ", " + d.state + " " + d.zipCode + "\n" + d.phoneNumber);
            ShowAveRating(restId);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
        }

        static public void ShowReviews()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Please enter the restaurant id and press Enter:");
            int restId = Convert.ToInt32(Console.ReadLine());
            ShowDetails(restId);

            RestaurantCrud crud = new RestaurantCrud();
            var revs = crud.ShowRestaurantReviews(restId);

            foreach (var item in revs)
            {
                Console.WriteLine(item.reviewer + "\n" + item.review1 + "\n" + item.rating);
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine("\n");
        }

        static public void ShowAveRating(int restId)
        {
            RestaurantCrud crud = new RestaurantCrud();
            double revs = crud.ShowAverageRating(restId);
            Console.WriteLine("Ave.Rating: " + revs + "\n");
        }

        static public void SearchRestaurants()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Please enter a name or part of a name then Enter");
            string searchStr = Console.ReadLine();

            RestaurantCrud crud = new RestaurantCrud();
            var rests = crud.FindRestByName(searchStr);
            foreach (var item in rests)
            {
                Console.WriteLine(item.ID + "||" + item.name);
            }
            Console.WriteLine("\n");
        }

        static public void ShowAllRestaurants()
        {
            RestaurantCrud crud = new RestaurantCrud();
            var rests = crud.ShowRestaurants();
            Console.WriteLine("\n");
            foreach (var item in rests)
            {
                Console.WriteLine(item.ID + "||" + item.name);
            }
            Console.WriteLine("\n");
        }

        public static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            //display top 3 restaurants by average rating
            //display all restaurants
            //should allow more than one method of sorting
            //display details of a restaurant
            //display all the reviews of a restaurant
            //search restaurants(e.g.by partial name), and display all matching results
            //quit application

            char input = 'A';
            while (input != 'q' || input != 'Q')
            {
                Console.WriteLine("Display (T)op three restaurants by rating");
                Console.WriteLine("Display (A)ll restaurants");
                Console.WriteLine("Display (D)etails of a restaurant");
                Console.WriteLine("Display all (R)eviews of a restaurant");
                Console.WriteLine("(S)earch for a restaurant");
                Console.WriteLine("Serialize restaurants to (J)SON");
                Console.WriteLine("Deseriali(Z)e from 'C:\\Users\\Public\\TestFolder\\Restaraunts.txt");
                Console.WriteLine("(Q)uit\n");

                input = Console.ReadKey().KeyChar;
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Info($"User pressed {input}");
                switch (input)
                {
                    case 'A':
                    case 'a':
                        ShowAllRestaurants();
                        break;
                    case 'D':
                    case 'd':
                        ShowDetails();
                        break;
                    case 'J':
                    case 'j':
                        SerializeRestaurants();
                        break;
                    case 'Q':
                    case 'q':
                        Environment.Exit(0);
                        break;
                    case 'R':
                    case 'r':
                        ShowReviews();
                        break;
                    case 'S':
                    case 's':
                        SearchRestaurants();
                        break;
                    case 'Z':
                    case 'z':
                        DeserializeRestaurants();
                        break;
                }
            }

        }
    }
}
