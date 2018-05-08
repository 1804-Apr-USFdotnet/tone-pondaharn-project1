using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWeb.Models
{
    public class Restaurant
    {
        private RestaurantsDBEntities _db = new RestaurantsDBEntities();


        public int ID { get; set; }
        [Required]
        [StringLength(100), MinLength(5)]
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string phoneNumber { get; set; }
        public Nullable<double> avgRating { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public IEnumerable<Restaurant> ShowRestaurant()
        {
            Restaurant restaurant = new Restaurant();
         
            var rests = _db.Restaurants.ToList();
            var result = rests.Select(x => ToWeb(x));
            return result;
        }

        public void AddNewReview(int id, Review newReview)
        {
            newReview.RestaurantId = id;
            newReview.reviewer = "";
            _db.Reviews.Add(ToDataReview(newReview));
            //_db.SaveChanges();
            try
            {
                _db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public IEnumerable<Restaurant> ShowTopThree()
        {
            using (var db = new RestaurantsDBEntities())
            {
                var dataList = db.Restaurants.ToList();
                var topThree = (IEnumerable<Restaurant>)dataList.Select(x => ToWeb(x)).ToList();
                topThree.OrderByDescending(x => x.avgRating);
                int numItems = topThree.Count();
                var returnThree = new List<Restaurant>();
                returnThree.Add((topThree.ToList()[numItems - 3]));
                returnThree.Add((topThree.ToList()[numItems - 2]));
                returnThree.Add((topThree.ToList()[numItems - 1]));
                //var temp = DataToLibrary(topThree.ToList()[numItems - 1]);
                //dataList.Add((IEnumerable<Restaurant>)(topThree.ToList()[numItems-1]));
                return returnThree;
            }
        }
            public IEnumerable<Review> GetRestaurantReviews(int restId)
        {
            IEnumerable<Review> result;
            var dataList = _db.Reviews.Where(r => r.RestaurantId == restId).ToList();
            result = dataList.Select(x => ToWebReview(x)).ToList();
            return result;
        }
            public Restaurant ShowRestaurantById(int id)
        {
            var rests = _db.Restaurants.ToList();
            var results = ToWeb(_db.Restaurants.Where(x => x.ID == id).ToList()[0]);
            return results;
        }

        public void AddRestaurant(Restaurant newRestaurant)
        {
            _db.Restaurants.Add(ToData(newRestaurant));
            _db.SaveChanges();
        }
        public void UpdateRestaurant(Restaurant item)
        {

            var oldRest = _db.Restaurants.Find(item.ID);
            _db.Entry(oldRest).CurrentValues.SetValues(item);

            //Restaurant restaurant = new Restaurant();
            //var temp = _db.Restaurants.Find(item.ID);
            //restaurant.ID = temp.ID;
            //restaurant.name = temp.name;
            //restaurant.address = temp.address;
            //restaurant.city = temp.city;
            //restaurant.state = temp.state;
            //restaurant.zipCode = temp.zipCode;
            //restaurant.phoneNumber = temp.phoneNumber;
            //restaurant.avgRating = temp.avgRating;
            _db.Restaurants.Remove(oldRest);
            _db.Restaurants.Add(ToData(item));
            _db.SaveChanges();
        }
        public void RemoveRestaurant(int id)
        {
            var tempRestaurant = _db.Restaurants.Where(x => x.ID == id).ToList()[0];
            _db.Restaurants.Remove(tempRestaurant);
            _db.SaveChanges();
        }
        public IEnumerable<Restaurant> ShowRestaurantsByName(string name)
        {
            using (var db = new RestaurantsDBEntities())
            {
                return (IEnumerable<Restaurant>)db.Restaurants.Where(b => b.name.Contains(name)).ToList();
            }
        }
        public double UpdateAverageRating(int restId)
        {
            using (var db = new RestaurantsDBEntities())
            {
                double RatingAverage = db.Reviews.Where(r => r.RestaurantId == restId).Average(r =>(double?) r.rating) ?? 0;
                var temp = db.Restaurants.Where(x => x.ID == restId).First();
                temp.avgRating = RatingAverage;
                db.SaveChanges();
                return RatingAverage;
            }

            //double cafeSales = db.InvoiceLines
            //         .Where(x =>
            //                    x.UserId == user.UserId &&
            //                    x.DateCharged >= dateStart &&
            //                    x.DateCharged <= dateEnd)
            //         .Sum(x => (double?)(x.Quantity * x.Price)) ?? 0;
        }

        //var db = new RestaurantsDBEntities();
        //    return db.Reviews.Where(r => r.RestaurantId == restId);
        //public IEnumerable<Review> ShowRestaurantReviews(int restId)
        //{
        //    IEnumerable<Review> result;


        //        var dataList = _db.Reviews.ToList();
        //    Reviews = (dataList.Where(x => x.RestaurantId == restId));

        //        //result = dataList.Select(x => ToWebReview(x)).ToList();

        //    return result;
        //}
        // MAPPING---------------------------------------------------------------
        public static Restaurant ToWeb(DataLayer.Restaurant dataRestaurant)
        {
            var webRestaurant = new Restaurant()
            {
                ID = dataRestaurant.ID,
                name = dataRestaurant.name,
                address = dataRestaurant.address,
                city = dataRestaurant.city,
                state = dataRestaurant.state,
                zipCode = dataRestaurant.zipCode,
                phoneNumber = dataRestaurant.phoneNumber,
                avgRating = dataRestaurant.avgRating
            };
            return webRestaurant;
        }
        public static DataLayer.Restaurant ToData(Restaurant webRestaurant)
        {
            var dataRestaurant = new DataLayer.Restaurant()
            {
                ID = webRestaurant.ID,
                name = webRestaurant.name,
                address = webRestaurant.address,
                city = webRestaurant.city,
                state = webRestaurant.state,
                zipCode = webRestaurant.zipCode,
                phoneNumber = webRestaurant.phoneNumber,
                avgRating = webRestaurant.avgRating
            };
            return dataRestaurant;
        }
        public static Review ToWebReview(DataLayer.Review dataReview)
        {
            var webReview = new Review()
            {
                ID = dataReview.ID,
                RestaurantId = dataReview.RestaurantId,
                reviewer = dataReview.reviewer,
                review1 = dataReview.review1,
                rating = dataReview.rating
            };
            return webReview;
        }
        public static DataLayer.Review ToDataReview(Review webReview)
        {
            var dataReview = new DataLayer.Review()
            {
                ID = webReview.ID,
                RestaurantId = webReview.RestaurantId,
                reviewer = webReview.reviewer,
                review1 = webReview.review1,
                rating = webReview.rating
            };
            return dataReview;
        }
    }
}