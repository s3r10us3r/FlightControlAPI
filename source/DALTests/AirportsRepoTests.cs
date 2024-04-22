using FlightControl.DAL;
using FlightControl.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DALTests
{
    [TestClass]
    public class AirportsRepoTests : BaseRepoTests<Airport>
    {
        public AirportsRepoTests() : base(6)
        {
        }

        protected override AirportsRepo CreateRepo(FlightDbContext dbContext)
        {
            return new AirportsRepo(dbContext);
        }

        [TestMethod]
        public override void AddTest()
        {
            Airport airport = new() { Name = "Dubai Airport" };
            int changes = Repo.Add(airport);

            Assert.AreEqual(1, changes);
            Airport? newAirport = Repo.GetOne(airport.Id);
            Assert.IsNotNull(newAirport);
        }

        [TestMethod]
        public override void AddRangeTest()
        {
            var airports = new List<Airport>()
            {
                new() {Name = "Dresden Airport"},
                new() {Name = "Madrid Airport"},
                new() {Name = "Beijing Airport"}
            };

            int changes = Repo.AddRange(airports);
            Assert.AreEqual(3, changes);

            foreach (Airport airport in airports)
            {
                Airport? entity = Repo.GetOne(airport.Id);
                Assert.IsNotNull(entity);
            }
        }

        [TestMethod]
        public override void SaveTest()
        {
            Airport? airport = Repo.GetOne(1);
            Assert.IsNotNull(airport);
            airport.Name = "Test name";

            int changes = Repo.Save(airport);
            Assert.AreEqual(1, changes);

            Airport? newAirport = Repo.GetOne(airport.Id);
            Assert.IsNotNull(newAirport);
            Assert.AreEqual(airport.Name, newAirport.Name);
        }

        [TestMethod]
        public void MatchByNameTest()
        {
            List<Airport> airports = ((AirportsRepo)Repo)
                .MatchByName("CPK")
                .ToList();

            Assert.AreEqual(1, airports.Count);
            Assert.AreEqual("CPK Airport", airports[0].Name);
        }

        [TestMethod]
        public override void ShouldThrowAnErrorWhenEntityWithNoRequiredPropertyAdded()
        {
            Airport airport = new() { };

            Assert.ThrowsException<DbUpdateException>(() =>
            {
                Repo.Add(airport);
            });
        }

        [TestMethod]
        public override void ShouldThrowAnErrorWhenDuplicateAdded()
        {
            Airport airport = new() { Name = "Warsaw Airport" };
            Assert.ThrowsException<DbUpdateException>(() =>
            {
                Repo.Add(airport);
            });
        }
    }
}
