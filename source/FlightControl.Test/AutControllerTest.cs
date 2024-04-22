using FlightControl.Controllers;
using FlightControl.DAL.MockRepos;
using FlightControl.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace FlightControl.Test
{
    [TestClass]
    public class AuthControllerTest
    {
        private readonly AuthController controller;

        public AuthControllerTest()
        {
            var service = new UserService(new MockUsers());
            var tokenService = new TokenService(new MockConfig());
            controller = new AuthController(service, tokenService, new MockLogger<AuthController>());
        }

        [TestMethod]
        public void RegisterShouldReturnCorrectlyIfLoginAlreadyExists()
        {
            LoginModel model = new()
            {
                Login = "TestUser1",
                Password = "TestPassword"
            };

            var result = controller.Register(model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void RegisterShouldReturnCorrectlyForValidNewUser()
        {
            LoginModel model = new()
            {
                Login = "TestUser4",
                Password = "TestPassword"
            };

            var result = controller.Register(model);
            Assert.IsInstanceOfType<OkResult>(result);
        }

        [TestMethod]
        public void LoginShouldReturnCorrectlyIfUserDoesNotExist()
        {
            LoginModel model = new()
            {
                Login = "TestUser4",
                Password = "TestPassword"
            };

            var result = controller.Login(model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void LoginShouldReturnCorrectlyIfPasswordIsIncorrect()
        {
            LoginModel model = new()
            {
                Login = "TestUser1",
                Password = "NotAPassword"
            };

            var result = controller.Login(model);
            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }

        [TestMethod]
        public void LoginShouldReturnCorrectly()
        {
            LoginModel model = new()
            {
                Login = "TestUser1",
                Password = "TestUser1"
            };

            var result = controller.Login(model);
            Assert.IsInstanceOfType<OkObjectResult>(result);
        }
    }
}
