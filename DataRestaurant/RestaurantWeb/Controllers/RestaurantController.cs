using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantWeb.Models;

namespace RestaurantWeb.Controllers
{
    public class RestaurantController : Controller
    {
        //IEnumerable<Restaurant> restaurants = new Restaurant();
        Restaurant restaurant = new Restaurant();
        Review review = new Review();

        public ActionResult AddReview(int id, Review newReview)
        {
            //var item = restaurant ;
            //return this.RedirectToAction("Create", "Review", new { id = item.ID });
            restaurant.AddNewReview(id, newReview);
            return View();
        }

        public ActionResult ShowReviews(int id)
        {
            return View(restaurant.GetRestaurantReviews(id));
        }
        // GET: Restaurant
        public ActionResult Index()
        {
            return View(restaurant.ShowRestaurant());
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int id)
        {
            restaurant.UpdateAverageRating(id);
            return View(restaurant.ShowRestaurantById(id));
        }

        public ActionResult TopThree()
        {
            return View(restaurant.ShowTopThree());
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "name, address, city, state, zipCode, phoneNumber")] Restaurant newRestaurant)
        {
            try
            {
                // TODO: Add insert logic here
                restaurant.AddRestaurant(newRestaurant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            return View(restaurant.ShowRestaurantById(id));
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Restaurant updateRestaurant)
        //public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                restaurant.UpdateRestaurant(updateRestaurant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Restaurant deleteRestaurant)
        {
            try
            {
                // TODO: Add delete logic here
                restaurant.RemoveRestaurant(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
