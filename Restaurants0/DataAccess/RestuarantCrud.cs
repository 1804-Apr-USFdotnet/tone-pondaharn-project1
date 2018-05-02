using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RestaurantCrud
    {
        public IEnumerable<Restaurant> ShowRestaurants()
        {
            using (var db = new RestaurantsDBEntities())
            {
                return db.Restaurants.ToList();
            }
        }

        public IEnumerable<Review> ShowRestaurantReviews(int restId)
        {
            var db = new RestaurantsDBEntities();
            return db.Reviews.Where(r => r.RestaurantId == restId);
        }

        public double ShowAverageRating(int restId)
        {
            using (var db = new RestaurantsDBEntities())
            {
                double RatingAverage = db.Reviews.Where(r => r.RestaurantId == restId).Average(r => r.rating);
                // INCORRECT: should update restaurant.avgRating
                return RatingAverage;
            }
        }

        public Restaurant FindRestById(int id)
        {
            using (var db = new RestaurantsDBEntities())
            {
                return db.Restaurants.Where(x => x.ID == id).ToList()[0];
            }
        }

        public IEnumerable<Restaurant> FindRestByName(string restName)
        {
            var db = new RestaurantsDBEntities();
            var rest = db.Restaurants.Where(b => b.name.Contains(restName));
            return rest;
        }
    }
}
