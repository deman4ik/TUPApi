using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tupapi.Shared.DataObjects;
using tupapi.Shared.Enums;
using tupapiService.Controllers;
using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;
using tupapiService.Test.Infrastructure;

namespace tupapiService.Test.Authentication
{
    [TestClass]
    public class RegistrationTest
    {
        private readonly ITupapiContext _testContext;
        private TestDbPopulator _testDbPopulator;
        private RegistrationController _controller;

        public RegistrationTest()
        {
            _testContext = new TestTupContext();
            _testDbPopulator = new TestDbPopulator(_testContext);
        }

        [TestInitialize]
        public void RegistrationTestInitialize()
        {
            _testDbPopulator.PopulateUsers(2);
            var config = new HttpConfiguration();
            _controller = new RegistrationController(_testContext)
            {
                Request = new HttpRequestMessage()
            };
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void Registration_ShouldReturnOk()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "test@test.ru",
                Name = "test",
                Password = "abcdefghigklmpnop094"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, true);
            Assert.AreEqual(result.ApiResult, ApiResult.Created);
            Assert.AreEqual(result.ErrorType, ErrorType.None);
            Assert.IsNotNull(result.ResponseMessage);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfRequestIsNull()
        {
            HttpResponseMessage response = _controller.Registration(null);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.IsNull);
            Assert.AreEqual(result.ResponseMessage, "request");
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfEmailIsNull()
        {
            var req = new StandartRegistrationRequest
            {
                Name = "test",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.IsNull);
            Assert.AreEqual(result.ResponseMessage, req.EmailPropertyName);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfNameIsNull()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "test@test.ru",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.IsNull);
            Assert.AreEqual(result.ResponseMessage, req.NamePropertyName);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfPasswordIsNull()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "test@test.ru",
                Name = "test"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.IsNull);
            Assert.AreEqual(result.ResponseMessage, req.PasswordPropertyName);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfEmailIsInvalid()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "testtestru",
                Name = "test",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.EmailInvalid);
            Assert.AreEqual(result.ResponseMessage, req.Email);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfNameIsInvalid()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "test@test.ru",
                Name = "06test",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.NameInvalid);
            Assert.AreEqual(result.ResponseMessage, req.Name);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfPasswordIsInvalid()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "test@test.ru",
                Name = "test",
                Password = "12345670"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.PasswordInvalid);
            Assert.AreEqual(result.ResponseMessage, req.Password);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfPasswordIsShort()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "test@test.ru",
                Name = "test",
                Password = "1234567"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.PasswordLength);
            Assert.AreEqual(result.ResponseMessage, "7");
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfUserWithSameEmailExist()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "user1@example.com",
                Name = "user123",
                Password = "user1pwd"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.UserWithEmailExist);
            Assert.AreEqual(result.ResponseMessage, req.Email);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfUserWithSameNameExist()
        {
            var req = new StandartRegistrationRequest
            {
                Email = "user1232@example.com",
                Name = "user1",
                Password = "user1pwd"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.UserWithNameExist);
            Assert.AreEqual(result.ResponseMessage, req.Name);
        }
    }
}