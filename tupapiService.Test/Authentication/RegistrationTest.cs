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
    public class RegistrationTest : BaseControllerTest
    {
        private RegistrationController _controller;


        [TestInitialize]
        public void RegistrationTestInitialize()
        {
            TestDbPopulator.PopulateUsers(2);
            var config = new HttpConfiguration();
            _controller = new RegistrationController(TestContext)
            {
                Request = new HttpRequestMessage()
            };
            _controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void Registration_ShouldReturnOk()
        {
            var req = new StandartAuthRequest
            {
                Email = "test@test.ru",
                Name = "test",
                Password = "abcdefghigklmpnop094"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseRegistrationResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Created.ToString(), result.StatusCode);
            Assert.AreEqual(true, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Created, result.ApiResult);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfRequestIsNull()
        {
            HttpResponseMessage response = _controller.Registration(null);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.Data.ErrorType);
            Assert.AreEqual(result.Data.Message, "request");
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfEmailIsNull()
        {
            var req = new StandartAuthRequest
            {
                Name = "test",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.Data.ErrorType);
            Assert.AreEqual(nameof(req.Email), result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfNameIsNull()
        {
            var req = new StandartAuthRequest
            {
                Email = "test@test.ru",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.Data.ErrorType);
            Assert.AreEqual(nameof(req.Name), result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfPasswordIsNull()
        {
            var req = new StandartAuthRequest
            {
                Email = "test@test.ru",
                Name = "test"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.Data.ErrorType);
            Assert.AreEqual(nameof(req.Password), result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfEmailIsInvalid()
        {
            var req = new StandartAuthRequest
            {
                Email = "test@testru",
                Name = "test",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.EmailInvalid, result.Data.ErrorType);
            Assert.AreEqual(req.Email, result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfNameIsInvalid()
        {
            var req = new StandartAuthRequest
            {
                Email = "test@test.ru",
                Name = "4deman4ik",
                Password = "abcdefghigklmpnop"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.NameInvalid, result.Data.ErrorType);
            Assert.AreEqual(req.Name, result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfPasswordIsInvalid()
        {
            var req = new StandartAuthRequest
            {
                Email = "test@test.ru",
                Name = "test",
                Password = "12345670"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.PasswordInvalid, result.Data.ErrorType);
            Assert.AreEqual(req.Password, result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfPasswordIsShort()
        {
            var req = new StandartAuthRequest
            {
                Email = "test@test.ru",
                Name = "test",
                Password = "1234567"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.PasswordLength, result.Data.ErrorType);
            Assert.AreEqual("7", result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfUserWithSameEmailExist()
        {
            var req = new StandartAuthRequest
            {
                Email = "user1@example.com",
                Name = "user123",
                Password = "user1pwd"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.UserWithEmailExist, result.Data.ErrorType);
            Assert.AreEqual(req.Email, result.Data.Message);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfUserWithSameNameExist()
        {
            var req = new StandartAuthRequest
            {
                Email = "user1232@example.com",
                Name = "user1",
                Password = "user1pwd"
            };
            HttpResponseMessage response = _controller.Registration(req);
            var result = TestHelper.ParseErorResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Unauthorized.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.UserWithNameExist, result.Data.ErrorType);
            Assert.AreEqual(req.Name, result.Data.Message);
        }
    }
}