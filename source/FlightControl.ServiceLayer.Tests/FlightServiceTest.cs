using FlightControl.DAL.Interfaces;
using FlightControl.DAL.MockRepos;
using FlightControl.DAL.Models;

namespace FlightControl.ServiceLayer.Tests
{
    [TestClass]
    public class FlightServiceTest : BaseServiceTest<Flight, IFlightRepo>
    {
        public FlightServiceTest() : base(new FlightService(new MockFlight()))
        {
        }

        [TestMethod]
        public void CreateOneShouldThrowForInvalidFlightNumber()
        {
            var func = () => ((FlightService)_service).CreateOne("", DateTime.Now, new Airport() { Id = 1, Name = "test" }, new Airport() { Id = 2, Name = "test2" }, "PlaneType");
            Assert.ThrowsException<InvalidOperationException>(func);
        }

        [TestMethod]
        public void CreateOneShouldReturnNotNullFlight()
        {
            Flight flight = ((FlightService)_service).CreateOne("TT 123", DateTime.Now, new Airport() { Id = 1, Name = "test" }, new Airport() { Id = 2, Name = "test2" }, "Test plane");
            Assert.IsNotNull(flight);
        }

        [TestMethod]
        public override void DeleteOneWithEntityShouldReturnOne()
        {
            Flight? flight = _service.ReadOne(1);
            Assert.IsNotNull(flight);
            Assert.AreEqual(1, _service.DeleteOne(flight));
        }

        [TestMethod]
        public void UpdateShouldThrowWhenInvalidFlightNumber()
        {
            var func = () =>
            {
                var service = (FlightService)_service;
                service.Update(
                    new Flight
                    {
                        FlightNumber = "LOT 010231",
                        DepartureDateTime = DateTime.Now,
                        DepartureAirport = new Airport { Name = "Warsaw airport" },
                        ArrivalAirport = new Airport { Name = "London Airport" },
                        PlaneType = "Boeing"
                    });
            };
            Assert.ThrowsException<InvalidOperationException>(func);
        }

        [TestMethod]
        public void GetDeparturesInTimeFrameShouldReturnNotNull()
        {
            var flights = ((FlightService)_service).GetDeparturesInTimeFrame(DateTime.MinValue, DateTime.MaxValue);
            Assert.IsNotNull(flights);
            Assert.IsTrue(flights.Count > 0);
        }

        [TestMethod]
        public void GetDeparturesInTimeFrameShouldThrowWhenLowerBoundIsHigherThanHigherBound()
        {
            var func = () => ((FlightService)_service).GetDeparturesInTimeFrame(DateTime.MaxValue, DateTime.MinValue);
            Assert.ThrowsException<InvalidOperationException>(func);
        }

        [TestMethod]
        public void UpdateShouldReturnFlight()
        {
            ((FlightService)_service)
                .Update(
                    new Flight
                    {
                        FlightNumber = "LT 1234",
                        DepartureDateTime = DateTime.Now,
                        DepartureAirport = new Airport { Name = "Warsaw airport" },
                        ArrivalAirport = new Airport { Name = "London Airport" },
                        PlaneType = "Boeing"
                    });
        }

        [TestMethod]
        public void ReadOneByFlightNumberShouldReturnNotNullEntity()
        {
            Flight? flight = ((FlightService)_service).ReadOne("RN 1234");
            Assert.IsNotNull(flight);
        }

        [TestMethod]
        public void GetAllFromToShouldCorrectlyReturn()
        {
            IEnumerable<Flight> flights = ((FlightService)_service).GetAllFromTo(3, 2);
            Assert.AreEqual(3, flights.Count());
        }

        [TestMethod]
        public void GetAllFromToInTimeFrameShouldCorrectlyReturn()
        {
            IEnumerable<Flight> flights = ((FlightService)_service).GetAllFromToInTimeFrame(3, 2, new DateTime(2022, 1, 1), new DateTime(2025, 1, 1));
            Assert.AreEqual(2, flights.Count());
        }

        [TestMethod]
        public void MatchByFlightNumberShouldCorrectlyReturn()
        {
            IEnumerable<Flight> flights = ((FlightService)_service).MatchByFlightNumber("LT");
            Assert.AreEqual(3, flights.Count());
        }

        [TestMethod]
        public void MatchByPlaneTypeShouldCorrectlyReturn()
        {
            IEnumerable<Flight> flights = ((FlightService)_service).MatchByPlaneType("Com");
            Assert.AreEqual(2, flights.Count());
        }

        [TestMethod]
        public void GetAllByPlaneTypeShouldCorrectlyReturn()
        {
            IEnumerable<Flight> flights = ((FlightService)_service).GetAllByPlaneType("Boeing");
            Assert.AreEqual(4, flights.Count());
        }
    }
}
