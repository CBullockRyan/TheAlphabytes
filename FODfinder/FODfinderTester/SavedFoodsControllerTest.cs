using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FODfinder;
using FODfinder.Controllers;
using FODfinder.Models.SavedFood;

namespace FODfinderTester
{
    [TestClass]
    public class SavedFoodsControllerTest
    {
        

        [TestMethod]
        public void TestCanCount_Regular()
        {
            //Arrange
            SavedFoodsController c = new SavedFoodsController();
            //Act
            List<SavedFood> testList = new List<SavedFood>();
            int testCount = c.countSavedFoods(testList);
            int expectedCount = 3;

            //Assert
        }

        [TestMethod]
        public void TestCanCount_EmptyList()
        {

        }
    }
}
