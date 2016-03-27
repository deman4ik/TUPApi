using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using tupapi.Shared.DataObjects;
using tupapiService.Authentication;
using tupapiService.Controllers;
using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;
using tupapiService.Test.Infrastructure;

namespace tupapiService.Test.Authentication
{
    [TestClass]
    public class LoginTest
    {
        private readonly ITupapiContext _testContext;
        private TestDbPopulator _testDbPopulator;
        private LoginController _controller;

        public LoginTest()
        {
            _testContext = new TestTupContext();
            _testDbPopulator = new TestDbPopulator(_testContext);
        }

        public LoginResult ParseLoginResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result =  JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
            if (result != null)
            {
                LogLoginResult(result);
            }
            return result;
        }

        public static void LogLoginResult(LoginResult result)
        {
            Console.WriteLine("# Authentication Token:");
            Console.WriteLine(result.AuthenticationToken);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("# User Id:");
            Console.WriteLine(result.User.Id);
        }
        [TestInitialize]
        public void LoginTestInitialize()
        {
            _testDbPopulator.PopulateUsers(2);
            _testDbPopulator.PopulateStandartAccounts(2);
            var config = new HttpConfiguration();
            _controller = new LoginController(_testContext)
            {
                Request = new HttpRequestMessage()
            };
          
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void Login_ShouldReturnToken()
        {
            var req = new StandartAuthRequest
            {
                Email = "user1@example.com",
                Password = "user1pwd"
            };
            HttpResponseMessage response = _controller.Login(req);
            LoginResult result = ParseLoginResponse(response);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.AuthenticationToken.ToString());
            Assert.AreEqual(result.User.Id, "u1");
        }
        [TestMethod]
        public void ShouldReturnAuthToken()
        {
            BaseAuth auth = new BaseAuth(_testContext);
            var token = auth.CreateToken("standart:u1");
            Debug.WriteLine("# Token : ");
            Debug.WriteLine(token);
        }

    }
}
