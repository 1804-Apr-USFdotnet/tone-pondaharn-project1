using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO;


namespace DataAccess
{

    [Serializable]
    partial class Restaurant
    {

    }

    [Serializable]
    partial class Reviews { }

    static public class Serializer
    {
        static public string SerializeToJSON()
        {
            var db = new RestaurantsDBEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            string json = JsonConvert.SerializeObject(db.Restaurants.ToList(), Formatting.Indented,
                new JsonSerializerSettings()
                {

                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return json;
        }

        static public void JSONToDeserialize(string fp)
        {
            var db = new RestaurantsDBEntities();
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(fp))
            {
                string temp = file.ReadToEnd();
                JsonSerializer serializer = new JsonSerializer();
                List<Restaurant> rest = JsonConvert.DeserializeObject<List<Restaurant>>(temp);
                foreach(var item in rest)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
