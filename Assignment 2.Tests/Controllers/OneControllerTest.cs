using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// required references
using System.Web.Mvc;
using Assignment_2.Controllers;
using Moq;
using Assignment_2.Models;
using System.Linq;

namespace Assignment_2.Tests.Controllers
{
    [TestClass]
    public class OneControllerTest
    {
        // globals
        oneController controller;
        Mock<IOneControllerRepository> mock;
        List<phone> phones;

        [TestInitialize]

        public void TestInitialize()
        {
            // arrange
            mock = new Mock<IOneControllerRepository>();

            // mock phone data
            phones = new List<phone>
            {
                new phone{price = 200, phones = "apple"},
                new phone{price = 100, phones = "samsung"},
                new phone{price = 150, phones = "Blackberry"},
                new phone{price = 90, phones = "LG"},

            };

            // add phone data to the mock object
            mock.Setup(m => m.phones).Returns(phones.AsQueryable());


            // pass the mock to the controller
            controller = new oneController(mock.Object);
        }
        [TestMethod]

        public void IndexLoadsValid()
        {

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void IndexShowsValidphones()
        {
            // act

            var actual = (List<phone>)controller.Index().Model;

            // assert
            CollectionAssert.AreEqual(phones, actual);

        }
        [TestMethod]

        public void DetailsValidphone()
        {
            // act
            var actual = (phone)controller.Details(200).Model;

            //assert
            Assert.AreEqual(phones.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            //act
            ViewResult actual = controller.Details(1);
            // assert

            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]
        public void DetailsInvalidNoId()
        {
            //act
            ViewResult actual = controller.Details(null);
            // assert

            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]
         public void DeleteConfirmedNoId()
        {
            //act
            ViewResult actual = controller.DeleteConfirmed(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            //act
            ViewResult actual = controller.DeleteConfirmed(1);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            //act
            ViewResult actual = controller.DeleteConfirmed(150);

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }
    }
}
