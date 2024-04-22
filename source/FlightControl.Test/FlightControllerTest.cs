using FlightControl.Controllers;
using FlightControl.DAL.MockRepos;
using FlightControl.DAL.Models;
using FlightControl.DTOs;
using FlightControl.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Test
{
    [TestClass]
    public class FlightControllerTest
    {
        private readonly FlightController controller;

        public FlightControllerTest()
        {
            var flightService = new FlightService(new MockFlight());
            var airportService = new AirportService(new MockAirports());
            controller = new FlightController(flightService, airportService, new MockLogger<FlightController>());
        }

        [TestMethod]
        public void GetByIdReturnsOkIfFlightExists()
        {
            var result = controller.Get(1);
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }

        [TestMethod]
        public void GetByIdReturnsNotFoundIfFlightDoesNotExist()
        {
            var result = controller.Get(100);
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void GetAllReturnsOkWithNotEmptyList()
        {
            var result = controller.GetAll();
            Assert.IsInstanceOfType<OkObjectResult>(result);
            Assert.IsNotNull(((OkObjectResult)result).Value);
            Assert.IsInstanceOfType<IEnumerable<FlightDTO>>(((OkObjectResult)result).Value);
        }

        [TestMethod]
        public void GetAllFromToReturnsBadRequestWhenAirportDoesNotExist()
        {
            var result = controller.GetAllFromTo("no", "no");
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void GetAllFromToReturnsOkObjectResultWhenThereAreValidFlights()
        {
            var result = controller.GetAllFromTo("Warsaw Airport", "To delete2");
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }

        [TestMethod]
        public void GetAllFromToReturnsNotFoundWhenAirportsExistsButThereAreNoRecords()
        {
            var result = controller.GetAllFromTo("To delete1", "CPK Airport");
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void GetAllInTimeFrameReturnsBadRequestIfLowerBoundIsHigher()
        {
            var result = controller.GetAllInTimeFrame(DateTime.MaxValue, DateTime.MinValue);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void GetAllInTimeFrameReturnsNotFoundIfNoFlightsWereFound()
        {
            var result = controller.GetAllInTimeFrame(new DateTime(1990, 1, 1), new DateTime(1991, 1, 1));
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void GetAllInTimeFrameReturnsOkIfFlightsWereFound()
        {
            var result = controller.GetAllInTimeFrame(DateTime.MinValue, DateTime.MaxValue);
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }

        [TestMethod]
        public void GetAllFromToInTimeFrameReturnsBadRequestIfAirportsDoNotExist()
        {
            var result = controller.GetAllFromToInTimeFrame("not an airport", "not an airport 2", DateTime.MinValue, DateTime.MaxValue);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void GetAllFromToInTimeFrameReturnBadRequestIfLowerBoundIsHigher()
        {
            var result = controller.GetAllFromToInTimeFrame("Warsaw Airport", "To delete2", DateTime.MaxValue, DateTime.MinValue);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void GetAllFromToInTimeFrameReturnsNotFoundIfNoFlightsWereFound()
        {
            var result = controller.GetAllFromToInTimeFrame("Warsaw Airport", "To delete2", new DateTime(1990, 1, 1), new DateTime(1991, 1, 1));
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void GetAllFromToReturnsOkObjectResultsIfFlightsWereFound()
        {
            var result = controller.GetAllFromToInTimeFrame("Warsaw Airport", "To delete2", DateTime.MinValue, DateTime.MaxValue);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<IEnumerable<FlightDTO>>(value);
        }

        [TestMethod]
        public void MatchByFlightNumberReturnsNotFoundIfNoFlightWasFound()
        {
            var result = controller.MatchByFlightNumber("XX");
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void MatchByFlightNumberCorrectlyReturnsIfFlightsWereFound()
        {
            var result = controller.MatchByFlightNumber("LT");
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<IEnumerable<FlightDTO>>(value);
        }

        [TestMethod]
        public void MatchByPlaneTypeReturnsNotFoundIfNoFlightsWereFound()
        {
            var result = controller.MatchByPlaneType("NOTAPLANETYPE");
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void MatchByPlaneTypeReturnsCorrectlyIfFlightsWereFound()
        {
            var result = controller.MatchByPlaneType("Bo");
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<IEnumerable<FlightDTO>>(value);
        }

        [TestMethod]
        public void GetByFlightNumberReturnsNotFoundIfFlightHasNotBeenFound()
        {
            var result = controller.Get("NOTAFLIGHTNUMBER");
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void GetByFlightNumberCorrectlyReturnsIfFlightWasFound()
        {
            var result = controller.Get("WZ 2015");
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<Flight>(value);
        }

        [TestMethod]
        public void CreateCorrectlyReturnsIfAirportsDoNotExist()
        {
            FlightModel model = new()
            {
                ArrivalAirportId = 100,
                DepartureAirportId = 100,
                FlightNumber = "TT 1234",
                DepartureDateTime = DateTime.Now,
                PlaneType = "Airbus"
            };

            var result = controller.Create(model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void CreateCorrectlyReturnsIfFlightNumberIsInvalid()
        {
            FlightModel model = new()
            {
                ArrivalAirportId = 3,
                DepartureAirportId = 4,
                FlightNumber = "TTT 1234",
                DepartureDateTime = DateTime.Now,
                PlaneType = "Airbus"
            };

            var result = controller.Create(model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void CreateCorrectlyReturnsIfFlightNumberAlreadyExists()
        {
            FlightModel model = new()
            {
                ArrivalAirportId = 3,
                DepartureAirportId = 4,
                FlightNumber = "WZ 2015",
                DepartureDateTime = DateTime.Now,
                PlaneType = "Airbus"
            };

            var result = controller.Create(model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void CreateCorrectlyReturnsIfModelIsValid()
        {
            FlightModel model = new()
            {
                ArrivalAirportId = 3,
                DepartureAirportId = 4,
                FlightNumber = "WZ 2017",
                DepartureDateTime = DateTime.Now,
                PlaneType = "Airbus"
            };

            var result = controller.Create(model);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<FlightDTO>(value);
        }

        [TestMethod]
        public void UpdateCorrectlyReturnsIfModelIsValid()
        {
            FlightModel model = new()
            {
                ArrivalAirportId = 3,
                DepartureAirportId = 4,
                FlightNumber = "WZ 2017",
                DepartureDateTime = DateTime.Now,
                PlaneType = "Airbus"
            };

            var result = controller.Update(1, model);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var value = ((OkObjectResult)result).Value;
            Assert.IsInstanceOfType<FlightDTO>(value);
        }

        [TestMethod]
        public void UpdateCorrectlyReturnsIfAirportsDoNotExist()
        {
            FlightModel model = new()
            {
                ArrivalAirportId = 100,
                DepartureAirportId = 100,
                FlightNumber = "WZ 2017",
                DepartureDateTime = DateTime.Now,
                PlaneType = "Airbus"
            };

            var result = controller.Update(1, model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void DeleteCorrectlyReturnsIfFlightHasNotBeenFound()
        {
            var result = controller.Delete(100);
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public void DeleteCorrectlyReturnsIfFlightHasBeenFound()
        {
            var result = controller.Delete(7);
            Assert.IsInstanceOfType<OkResult>(result);
        }
    }
}
