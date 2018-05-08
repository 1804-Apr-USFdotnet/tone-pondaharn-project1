using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ReviewCrud
    {
        public IEnumerable<Review> GetReviews()
        //public Restaurant GetRestaurants()
        {
            IEnumerable<Review> result;
            using (var db = new RestaurantsDBEntities())
            {
                var dataList = db.Reviews.ToList();
                result = dataList.Select(x => DataToLibraryReview(x)).ToList();
            }
            return result;
        }

        public IEnumerable<Review> GetRestaurantReviews(int restId)
        {
            IEnumerable<Review> result;
            using (var db = new RestaurantsDBEntities())
            {
                var dataList = db.Reviews.Where(r => r.RestaurantId == restId).ToList();
                result = dataList.Select(x => DataToLibraryReview(x)).ToList();
            }
            return result;
            //IEnumerable<Restaurant> result;
            //var db = new RestaurantsDBEntities();
            //var temp = db.Reviews.Where(r => r.RestaurantId == restId).ToList();
            //return LibraryToDataReview(temp);
        }
        //-----------------------------------------------
        public static Review DataToLibraryReview(DataLayer.Review dataModel)
        {
            var libModel = new Review()
            {
                ID = dataModel.ID,
                RestaurantId = dataModel.RestaurantId,
                reviewer = dataModel.reviewer,
                review1 = dataModel.review1,
                rating = dataModel.rating
            };
            return libModel;
        }

        public static DataLayer.Review LibraryToDataReview(Review libModel)
        {
            var dataModel = new DataLayer.Review()
            {
                ID = libModel.ID,
                RestaurantId = libModel.RestaurantId,
                reviewer = libModel.reviewer,
                review1 = libModel.review1,
                rating = libModel.rating
            };
            return dataModel;
        }
    }

}
