using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class RestaurantCrud
    {
        public IEnumerable<Restaurant> GetRestaurants()
        //public Restaurant GetRestaurants()
        {
            IEnumerable<Restaurant> result;
            using (var db = new RestaurantsDBEntities())
            {
                var dataList = db.Restaurants.ToList();
                result = dataList.Select(x => DataToLibrary(x)).ToList();
            }
            return result;
        }
        public void AddRestaurant(Restaurant item)
        {
            using (var db = new RestaurantsDBEntities())
            {
                db.Restaurants.Add(LibraryToData(item));
                db.SaveChanges();
            }
        }
        public void UpdateRestaurant(Restaurant item)
        {
            using (var db = new RestaurantsDBEntities())
            {
                Restaurant restaurant = new Restaurant();
                var temp = db.Restaurants.Find(item.ID);
                restaurant.ID = temp.ID;
                restaurant.name = temp.name;
                restaurant.address = temp.address;
                restaurant.city = temp.city;
                restaurant.state = temp.state;
                restaurant.zipCode = temp.zipCode;
                restaurant.phoneNumber = temp.phoneNumber;
                restaurant.avgRating = temp.avgRating;
                db.Restaurants.Add(LibraryToData(restaurant));
                db.SaveChanges();
            }

        }
        public void RemoveRestaurant(int id)
        {
            using (var db = new RestaurantsDBEntities())
            {
                var tempRestaurant = db.Restaurants.Where(x => x.ID == id).ToList()[0];
                db.Restaurants.Remove(tempRestaurant);
                db.SaveChanges();
            }
        }
        public Restaurant ShowRestaurantById(int id)
        {
            using (var db = new RestaurantsDBEntities())
            {
                return DataToLibrary(db.Restaurants.Where(x => x.ID == id).ToList()[0]);
            }
        }
        public IEnumerable<Restaurant> ShowRestaurantsByName(string name)
        {
            using (var db = new RestaurantsDBEntities())
            {
                return (IEnumerable<Restaurant>) db.Restaurants.Where(b => b.name.Contains(name)).ToList();
            }
        }
        public double ShowAverageRating(int restId)
        {
            using (var db = new RestaurantsDBEntities())
            {
                double RatingAverage = db.Reviews.Where(r => r.RestaurantId == restId).Average(r => r.rating);
                var temp = db.Restaurants.Where(x => x.ID == restId).First();
                temp.avgRating = RatingAverage;
                db.SaveChanges();
                return RatingAverage;
            }
        }
        public IEnumerable<Restaurant> ShowTopThree()
        {
            using (var db = new RestaurantsDBEntities())
            {
                var dataList = db.Restaurants.ToList();
                var topThree=  (IEnumerable<Restaurant>)dataList.Select(x => DataToLibrary(x)).ToList();
                topThree.OrderByDescending(x => x.avgRating);
                int numItems = topThree.Count();
                var returnThree = new List<Restaurant>();
                returnThree.Add((topThree.ToList()[numItems - 1]));
                returnThree.Add((topThree.ToList()[numItems - 2]));
                returnThree.Add((topThree.ToList()[numItems - 3]));
                //var temp = DataToLibrary(topThree.ToList()[numItems - 1]);
                //dataList.Add((IEnumerable<Restaurant>)(topThree.ToList()[numItems-1]));
                return returnThree;
            }
            
        }
        // MAPPING------------------------------------------------------------
        public static Restaurant DataToLibrary(DataLayer.Restaurant dataModel)
        {
            var libModel = new Restaurant()
            {
            ID = dataModel.ID,
            name = dataModel.name,
            address = dataModel.address,
            city = dataModel.city,
            state = dataModel.state,
            zipCode = dataModel.zipCode,
            phoneNumber = dataModel.phoneNumber,
            avgRating = dataModel.avgRating
        };
            return libModel;
        }

        public static DataLayer.Restaurant LibraryToData(Restaurant libModel)
        {
            var dataModel = new DataLayer.Restaurant()
            {
                ID = libModel.ID,
                name = libModel.name,
                address = libModel.address,
                city = libModel.city,
                state = libModel.state,
                zipCode = libModel.zipCode,
                phoneNumber = libModel.phoneNumber,
                avgRating = libModel.avgRating
            };
            return dataModel;
        }
    }
}
