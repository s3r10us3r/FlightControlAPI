using FlightControl.DAL.Models;

namespace FlightControl.ServiceLayer.Tests
{
    [TestClass]
    public class TokenServiceTest
    {
        private static readonly TokenService _service = new(new MockConfig());

        [TestMethod]
        public void GenerateJWTokenTest()
        {
            string token = _service.GenerateJWToken(new User() { Id = 1, Login = "login", PasswordHash = "TRULYHASEDPASSWORD" });
            Assert.IsNotNull(token);
            Console.WriteLine($"Test token {token}");
        }
    }
}
