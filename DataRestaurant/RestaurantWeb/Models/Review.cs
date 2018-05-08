using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataLayer;

namespace RestaurantWeb.Models
{
    public class Review
    {
        private RestaurantsDBEntities _db = new RestaurantsDBEntities();

        public int ID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Must be greater than zero")]
        public int RestaurantId { get; set; }
        public string reviewer { get; set; }
        public string review1 { get; set; }
 
        [Range(1,10)]
        public int rating { get; set; }

        public IEnumerable<Review> GetReviews()
        //public Restaurant GetRestaurants()
        {
            IEnumerable<Review> result;

                var dataList = _db.Reviews.ToList();
                result = dataList.Select(x => ToWebReview(x)).ToList();
            
            return result;
        }
        public Review GetReviewById(int id)
        {
            var rev = ToWebReview(_db.Reviews.Where(x => x.ID == id).ToList()[0]);
            return rev;
        }
        public void AddNewReview(Review newReview)
        {
            _db.Reviews.Add(ToDataReview(newReview));

            _db.SaveChanges();
        }
        public void UpdateReview(Review item)
        {
                Review rev = new Review();
                var temp = _db.Reviews.Find(item.ID);
                rev.ID = temp.ID;
                rev.RestaurantId = temp.RestaurantId;
                rev.reviewer = temp.reviewer;
                rev.review1 = temp.review1;
                rev.rating = temp.rating;
                _db.Reviews.Add(ToDataReview(rev));
                _db.SaveChanges();
        }
        public void RemoveReview(int id)
        {
                var tempReview =_db.Reviews.Where(x => x.ID == id).ToList()[0];
                _db.Reviews.Remove(tempReview);
                _db.SaveChanges();
        }
        public IEnumerable<Review> GetRestaurantReviews(int restId)
        {
            IEnumerable<Review> result;
                var dataList = _db.Reviews.Where(r => r.RestaurantId == restId).ToList();
                result = dataList.Select(x => ToWebReview(x)).ToList();
            return result;
            //IEnumerable<Restaurant> result;
            //var db = new RestaurantsDBEntities();
            //var temp = db.Reviews.Where(r => r.RestaurantId == restId).ToList();
            //return LibraryToDataReview(temp);
        }

        // MAPPING---------------------------------------------------------------
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