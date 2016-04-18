using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Hosting;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tupapiService.Authentication;
using tupapiService.Controllers;
using tupapiService.DataObjects;
using tupapiService.Models;
using tupapiService.Test.Infrastructure;

namespace tupapiService.Test.Controllers
{
    [TestClass]
    public class PostApiControllerTest : BaseControllerTest
    {
        private PostApiController _controller;
        private Post _post;
        private MapperConfiguration _config;
        private IMapper _mapper;
        [TestInitialize]
        public void PostApiControllerTestInitialize()
        {
            _config = Mapping.Mapping.GetConfiguration();
            _mapper = _config.CreateMapper();
            TestDbPopulator.PopulateDb(2,1);
            string token = BaseAuth.CreateToken("u1");
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage { RequestUri = new Uri("http://localhost:50268/api/Post") };
            request.Headers.Add("x-zumo-auth", token);
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            _controller = new PostApiController(TestContext)
            {
                Request = request,
                User = TestHelper.GetUser("u1")
            };
        }

        [TestMethod]
        public void Post_ShouldReturnCreated()
        {
            _post = TestDbPopulator.GetPost(3, 1);
            PostDTO postDto = _mapper.Map<Post, PostDTO>(_post);
            var response = _controller.Post(postDto);
            var result = TestHelper.ParsePostResponse(response);
            Assert.AreEqual(HttpStatusCode.Created,response.StatusCode);
            Assert.IsNotNull(result.Id);
            Assert.IsNotNull(result.Sas);
        }

        [TestMethod]
        public void Post_ShouldReturnUnauth()
        {
            _post = TestDbPopulator.GetPost(3, 1);
            PostDTO postDto = _mapper.Map<Post, PostDTO>(_post);
            var req = new HttpRequestMessage { RequestUri = new Uri("http://localhost:50268/api/Post") };
            req.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();
            _controller = new PostApiController(TestContext)
            {
                Request = req
            };
            var response = _controller.Post(postDto);
            var result = TestHelper.ParseBaseResponse(response);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
