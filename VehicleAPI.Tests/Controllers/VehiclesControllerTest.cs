using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleAPI.Controllers;
using VehicleAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace VehicleAPI.Tests.Controllers
{
    [TestClass]
    public class VehiclesControllerTest
    {
        private VehiclesController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new VehiclesController();
        }

        [TestMethod]
        public void Test_GetById()
        {
            //ARRANGE
            Vehicle testVehicle = new Vehicle() { Id = 1, Make = "Toyota", Model = "CX500", Year = 1999 };

            var vehicle = controller.Get(1);

            Assert.AreEqual(testVehicle.Make, vehicle.Make);
            Assert.AreEqual(testVehicle.Model, vehicle.Model);
            Assert.AreEqual(testVehicle.Year, vehicle.Year);
            Assert.IsInstanceOfType(vehicle, typeof(Vehicle));
            Assert.IsTrue(vehicle.Id == 1);
        }

        [TestMethod]
        public void Test_Get()
        {
            //ARRANGE
            List<Vehicle> testList = new List<Vehicle>()
            {
                new Vehicle(){ Id=2, Make="Honda", Model="MX50", Year=2008 },
                new Vehicle(){ Id=5, Make="Honda", Model="MX60", Year=2004 }
            };
            Vehicle args = new Vehicle() { Make = "honda" };

            // ACT
            List<Vehicle> vehicles = controller.Get(args);

            //ASSERT
            Assert.AreEqual(testList.Count, vehicles.Count);
            Assert.IsNotNull(vehicles);
            Assert.IsInstanceOfType(vehicles, typeof(List<Vehicle>));


        }

        [TestMethod]
        public void Test_Post()
        {
            // ARRANGE
            controller.Request = new HttpRequestMessage();
            Vehicle testVehicle = new Vehicle() {  Make = "Toyota", Model = "CX550", Year = 2005 };

            //ACT
            var response = controller.Post(testVehicle);

            //ASSERT
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);

        }

        [TestMethod]
        public void Test_Put()
        {
            // ARRANGE
            Vehicle testVehicle = new Vehicle() { Id = 1, Make = "Honda", Model = "CX550", Year = 2005 };            
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //ACT
            var response = controller.Put(testVehicle);

            //ASSERT
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NoContent);
        }


        [TestMethod]
        public void Test_Delete()
        {
            //ARRANGE
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //ACT
            var response = controller.Delete(2);

            //ASSERT
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            Assert.AreEqual(2, response.Content.ReadAsAsync<Vehicle>().Result.Id);
        }
    }
}
