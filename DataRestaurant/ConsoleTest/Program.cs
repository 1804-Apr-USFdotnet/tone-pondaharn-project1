using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantCrud crud = new RestaurantCrud();
            var rests = crud.GetRestaurants();
            Console.WriteLine("\n");
            foreach (var item in rests)
            {
                Console.WriteLine(item.ID + "||" + item.name);
            }
            Console.WriteLine("\n");
            

            var d = crud.ShowRestaurantById(1);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine(d.name + "\n" + d.address + "\n" + d.city + ", " + d.state + " " + d.zipCode + "\n" + d.phoneNumber);
            crud.ShowAverageRating(1);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++\n");
            Console.ReadKey();
        }
    }
}
