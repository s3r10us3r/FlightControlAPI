using FlightControl.DAL;
using FlightControl.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DALTests
{
    [TestClass]
    public class FlightRepoTest : BaseRepoTests<Flight>
    {
        public FlightRepoTest() : base(9)
        {
        }

        [TestMethod]
        public override void AddRangeTest()
        {
            var flights = new List<Flight>()
            {
                new() { ArrivalAirportId = 3, DepartureAirportId = 4, DepartureDateTime = DateTime.Today, FlightNumber="TS 0000", PlaneType="TESTPLANE" },
                new() { ArrivalAirportId = 5, DepartureAirportId = 6, DepartureDateTime = DateTime.Today, FlightNumber="TS 0002", PlaneType="TESTAIRCRAFT" },
                new() { ArrivalAirportId = 2, DepartureAirportId = 1, DepartureDateTime = DateTime.Today, FlightNumber="TS 0003", PlaneType="TESTBOEING" }
            };

            int changes = Repo.AddRange(flights);
            Assert.AreEqual(3, changes);

            foreach (Flight flight in flights)
            {
                Flight? entity = Repo.GetOne(flight.Id);
                Assert.IsNotNull(entity);
            }
        }

        [TestMethod]
        public override void AddTest()
        {
            Flight flight = new() { ArrivalAirportId = 3, DepartureAirportId = 4, DepartureDateTime = DateTime.Today, FlightNumber = "TS 0000", PlaneType = "TESTPLANE" };
            int changes = Repo.Add(flight);

            Assert.AreEqual(1, changes);
            Flight? newAirport = Repo.GetOne(flight.Id);
            Assert.IsNotNull(newAirport);
        }

        [TestMethod]
        public override void SaveTest()
        {
            Flight? flight = Repo.GetOne(1);
            Assert.IsNotNull(flight);
            flight.FlightNumber = "TT 0007";

            int changes = Repo.Save(flight);
            Assert.AreEqual(1, changes);

            Flight? newFlight = Repo.GetOne(flight.Id);
            Assert.IsNotNull(newFlight);
            Assert.AreEqual(flight.FlightNumber, newFlight.FlightNumber);
        }

        [TestMethod]
        public void GetOneUsingFlightNumberTest()
        {
            Flight? flight = ((FlightRepo)Repo).GetOne("LT 0000");
            Assert.IsNotNull(flight);
        }

        [TestMethod]
        public void MatchByFlightNumberTest()
        {
            List<Flight> flights = ((FlightRepo)Repo).MatchByFlightNumber("LT").ToList();
            Assert.AreEqual(3, flights.Count);
        }

        [TestMethod]
        public void MatchByPlaneType()
        {
            List<Flight> flights = ((FlightRepo)Repo).MatchByPlaneType("Boein").ToList();
            Assert.AreEqual(4, flights.Count);
        }

        [TestMethod]
        public override void ShouldThrowAnErrorWhenEntityWithNoRequiredPropertyAdded()
        {
            Flight flight = new() { };

            Assert.ThrowsException<DbUpdateException>(() =>
            {
                Repo.Add(flight);
            });
        }

        [TestMethod]
        public override void ShouldThrowAnErrorWhenDuplicateAdded()
        {
            Assert.ThrowsException<DbUpdateException>(() =>
            {
                Repo.Add(new Flight {
                    FlightNumber = "LOT 00000",
                    DepartureDateTime = new DateTime(2024, 5, 6, 23, 34, 0),
                    DepartureAirportId = 4,
                    ArrivalAirportId = 3,
                    PlaneType = "Boeing"
                });
            });
        }

        protected override FlightRepo CreateRepo(FlightDbContext dbContext)
        {
            return new FlightRepo(dbContext);
        }
    }
}
