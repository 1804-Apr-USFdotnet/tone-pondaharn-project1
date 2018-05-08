using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string phoneNumber { get; set; }
        public Nullable<double> avgRating { get; set; }
        public bool Active { get; set; }
    }
}
