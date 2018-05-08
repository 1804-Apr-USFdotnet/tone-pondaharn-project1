using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int RestaurantId { get; set; }
        public string reviewer { get; set; }
        public string review1 { get; set; }
        public int rating { get; set; }
    }
}
