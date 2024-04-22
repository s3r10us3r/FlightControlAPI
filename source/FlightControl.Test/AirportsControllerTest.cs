using FlightControl.Controllers;
using FlightControl.DAL.MockRepos;
using FlightControl.DTOs;
using FlightControl.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Test
{
    [TestClass]
    public class AirportsControllerTest
    {
        private readonly AirportController controller;

        public AirportsControllerTest()
        {
            var airportService = new AirportService(new MockAirports());
            controller = new AirportController(airportService, new MockLogger<AirportController>());
        }

        [TestMethod]
        public void GetByIdCorrectlyReturns()
        {
            var result = controller.Get(1);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<AirportDTOFlights>(value);
        }

        [TestMethod]
        public void GetByIdReturnsNotFoundIfAirprotHasNotBeenFound()
        {
            var result = controller.Get(100);
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void GetByNameCorrectlyReturns()
        {
            var result = controller.Get("Warsaw Airport");
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<AirportDTOFlights>(value);
        }

        [TestMethod]
        public void GetByNameReturnsNotFoundIfAirprotHasNotBeenFound()
        {
            var result = controller.Get("NOT AN AIRPORT");
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void GetAllCorrectlyReturns()
        {
            var result = controller.GetAll();
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<IEnumerable<AirportDTO>>(value);
        }

        [TestMethod]
        public void MatchByNameCorrectlyReturnsIfNoAirportWasFound()
        {
            var result = controller.MatchByName("NOTANAIRPORT");
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void MatchByNameCorrectlyReturns()
        {
            var result = controller.MatchByName("C");
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<IEnumerable<AirportDTO>>(value);
        }

        [TestMethod]
        public void DeleteCorrectlyReturnsIfObjectDoesNotExist()
        {
            var result = controller.Delete(100);
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void DeleteCorrectlyReturnsIfAirportHasFlightsConnected()
        {
            var result = controller.Delete(1);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void UpdateShouldCorrectlyReturnIfIdDoesNotExist()
        {
            AirportModel model = new()
            {
                Name = "New Airport"
            };

            var result = controller.Update(100, model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void UpdateShouldCorrectlyReturnIfNameAlreadyExists()
        {
            AirportModel model = new()
            {
                Name = "Warsaw Airport"
            };

            var result = controller.Update(1, model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void UpdateShouldReturnCorrectly()
        {
            AirportModel model = new()
            {
                Name = "New Airport"
            };

            var result = controller.Update(1, model);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<AirportDTO>(value);
        }

        [TestMethod]
        public void CreateShouldCorrectlyReturnIfNameAlreadyExists()
        {
            AirportModel model = new()
            {
                Name = "Warsaw Airport"
            };

            var result = controller.Create(model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void CreateShouldCorrectlyReturn()
        {
            AirportModel model = new()
            {
                Name = "New Airport"
            };

            var result = controller.Create(model);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<AirportDTO>(value);
        }
    }
}
