using FlightControl.DAL.Interfaces;
using FlightControl.DAL.MockRepos;
using FlightControl.DAL.Models;

namespace FlightControl.ServiceLayer.Tests
{
    [TestClass]
    public class AirportServiceTest : BaseServiceTest<Airport, IAirportsRepo>
    {
        public AirportServiceTest() : base(new AirportService(new MockAirports()))
        {
        }

        [TestMethod]
        public void CreateOneShouldThrowWhenNameExists()
        {
            var func = () => ((AirportService)_service).CreateOne("Warsaw Airport");
            Assert.ThrowsException<InvalidOperationException>(func);
        }

        [TestMethod]
        public void ReadOneWithFlightsIdShouldReturnWithNotEmptyFlights()
        {
            var airport = ((AirportService)_service).ReadOneWithFlights(1);
            Assert.IsNotNull(airport);
            Assert.IsTrue(airport.ArrivalFlights.Count > 0);
            Assert.IsTrue(airport.DepartureFlights.Count > 0);
        }

        [TestMethod]
        public void ReadOneWithFlightsnameShouldReturnWithNotEmptyFlights()
        {
            var airport = ((AirportService)_service).ReadOneWithFlights("Paris Airport");
            Assert.IsNotNull(airport);
            Assert.IsTrue(airport.ArrivalFlights.Count > 0);
            Assert.IsTrue(airport.DepartureFlights.Count > 0);
        }

        public void ReadOneIdShouldReturnAirportWithEmptyFlights()
        {
            var airport = _service.ReadOne(1);
            Assert.IsNotNull(airport);
            Assert.IsTrue(airport.DepartureFlights.Count == 0);
            Assert.IsTrue(airport.ArrivalFlights.Count == 0);
        }

        [TestMethod]
        public void ChangeNameShouldThrowWhenNameExists()
        {
            var func = () => ((AirportService)_service).ChangeName("Warsaw Airport", 1);
            Assert.ThrowsException<InvalidOperationException>(func);
        }

        [TestMethod]
        public void MatchByNameShouldReturnANonEmptyList()
        {
            var matches = ((AirportService)_service).MatchByName("Wa");
            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Count > 0);
        }


        [TestMethod]
        public override void DeleteOneWithEntityShouldReturnOne()
        {
            Airport airport = new() { Id = 1, Name = "Tijuana Airport" };
            Assert.AreEqual(1, _service.DeleteOne(airport));
        }

        public void MatchByNameShouldReturnNotNullList()
        {
            var list = ((AirportService)_service).MatchByName("name");
            Assert.IsNotNull(list);
        }

        public void ChangeNameShouldThrowWhenAirportDoesNotExist()
        {
            var func = () => ((AirportService)_service).MatchByName("notname");
            Assert.ThrowsException<InvalidOperationException>(func);
        }

        public void ChangeNameShouldreturnAirport()
        {
            var airport = ((AirportService)_service).MatchByName("name");
            Assert.IsNotNull(airport);
        }
    }
}
