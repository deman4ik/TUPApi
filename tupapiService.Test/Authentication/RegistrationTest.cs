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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.Created.ToString(), result.StatusCode);
            Assert.AreEqual(true, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Created, result.ApiResult);
            Assert.AreEqual(ErrorType.None, result.ErrorType);
            Assert.IsNotNull(result.ResponseMessage);
        }

        [TestMethod]
        public void Registration_ShouldReturnErrorIfRequestIsNull()
        {
            HttpResponseMessage response = _controller.Registration(null);
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.ErrorType);
            Assert.AreEqual(result.ResponseMessage, "request");
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.ErrorType);
            Assert.AreEqual(nameof(req.Email), result.ResponseMessage);
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.IsNull, result.ErrorType);
            Assert.AreEqual(nameof(req.Name), result.ResponseMessage);
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest.ToString());
            Assert.AreEqual(result.IsSuccessStatusCode, false);
            Assert.AreEqual(result.ApiResult, ApiResult.Validation);
            Assert.AreEqual(result.ErrorType, ErrorType.IsNull);
            Assert.AreEqual(result.ResponseMessage, nameof(req.Password));
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.EmailInvalid, result.ErrorType);
            Assert.AreEqual(req.Email, result.ResponseMessage);
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.NameInvalid, result.ErrorType);
            Assert.AreEqual(req.Name, result.ResponseMessage);
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.PasswordInvalid, result.ErrorType);
            Assert.AreEqual(req.Password, result.ResponseMessage);
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.PasswordLength, result.ErrorType);
            Assert.AreEqual("7", result.ResponseMessage);
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.UserWithEmailExist, result.ErrorType);
            Assert.AreEqual(req.Email, result.ResponseMessage);
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
            var result = TestHelper.ParseBaseResponse(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.BadRequest.ToString(), result.StatusCode);
            Assert.AreEqual(false, result.IsSuccessStatusCode);
            Assert.AreEqual(ApiResult.Validation, result.ApiResult);
            Assert.AreEqual(ErrorType.UserWithNameExist, result.ErrorType);
            Assert.AreEqual(req.Name, result.ResponseMessage);
        }
    }
}