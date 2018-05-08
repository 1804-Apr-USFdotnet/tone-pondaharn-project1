using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantWeb.Models;

namespace RestaurantWeb.Controllers
{
    public class ReviewController : Controller
    {
        Review review = new Review();
        
        // GET: Review
        public ActionResult Index()
        {
            return View(review.GetReviews());
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(Review newReview)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    review.AddNewReview(newReview);
                }
                
                return RedirectToAction("Index");
            }
            catch(RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
                return View(newReview);
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View(review.GetReviewById(id));
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Review updateReview)
        {
            try
            {
                // TODO: Add update logic here

                review.UpdateReview(updateReview);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Review deleteReview)
        {
            try
            {
                // TODO: Add delete logic here
                review.RemoveReview(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
