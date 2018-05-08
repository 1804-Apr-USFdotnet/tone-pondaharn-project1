using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    [TestClass()]
    public class RestaurantCrudTests
    {
        [TestMethod()]
        public void FindRestByIdTest()
        {
            int i = 1;
            RestaurantCrud crud = new RestaurantCrud();
            Restaurant rest = crud.FindRestById(i);
            Assert.AreEqual(i, rest.ID);
        }

        [TestMethod()]
        public void FindRestByNameTest()
        {
            string a = "a";
            RestaurantCrud crud = new RestaurantCrud();
            List<Restaurant> rests = crud.FindRestByName(a).ToList();
            int count = rests.Count();
            for (int i=0; i <count; i++)
            {
                StringAssert.Contains(rests.ElementAt(i).name, a);
            }
        }
    }
}