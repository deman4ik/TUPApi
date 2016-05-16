using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Hosting;
using Newtonsoft.Json;
using tupapi.Shared.DataObjects;
using tupapiService.Controllers;
using tupapiService.DataObjects;
using tupapiService.Models;
using LoginResult = tupapiService.DataObjects.LoginResult;

namespace tupapiService.Test.Infrastructure
{
    public class TestHelper
    {
        /// <summary>
        ///     Provide Standart Authentication
        /// </summary>
        /// <param name="context">ITupapiContext</param>
        /// <param name="req">Creds</param>
        /// <returns></returns>
        public static TestResult<LoginResult> Authenticate(ITupapiContext context, StandartAuthRequest req)
        {
            var config = new HttpConfiguration();
            LoginController controller = new LoginController(context)
            {
                Request = new HttpRequestMessage()
            };
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            HttpResponseMessage response = controller.Login(req);
            return ParseLoginResponse(response);
        }


        public static TestResult<LoginResult> ParseLoginResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result =
                JsonConvert.DeserializeObject<Response<LoginResult>>(response.Content.ReadAsStringAsync().Result);

            var testResult = new TestResult<LoginResult>
            {
                StatusCode = response.StatusCode.ToString(),
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ApiResult = result.ApiResult,
                Data = result.Data,
                Error = result.Error
            };
            Console.WriteLine(testResult.ToString());

            return testResult;
        }

        public static TestResult<string> ParseRegistrationResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result = JsonConvert.DeserializeObject<Response<string>>(response.Content.ReadAsStringAsync().Result);
            var testResult = new TestResult<string>
            {
                StatusCode = response.StatusCode.ToString(),
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ApiResult = result.ApiResult,
                Data = result.Data,
                Error = result.Error
            };
            Console.WriteLine(testResult.ToString());
            return testResult;
        }


        public static TestResult<PostResponse> ParsePostResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result =
                JsonConvert.DeserializeObject<Response<PostResponse>>(response.Content.ReadAsStringAsync().Result);
            var testResult = new TestResult<PostResponse>
            {
                StatusCode = response.StatusCode.ToString(),
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ApiResult = result.ApiResult,
                Data = result.Data,
                Error = result.Error
            };
            Console.WriteLine(testResult.ToString());
            return testResult;
        }

        public static TestResult<UserDTO> ParseUserResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result = JsonConvert.DeserializeObject<Response<UserDTO>>(response.Content.ReadAsStringAsync().Result);
            var testResult = new TestResult<UserDTO>
            {
                StatusCode = response.StatusCode.ToString(),
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ApiResult = result.ApiResult,
                Data = result.Data,
                Error = result.Error
            };
            Console.WriteLine(testResult.ToString());
            return testResult;
        }

        public static ClaimsPrincipal GetUser(string id)
        {
            var identity = new GenericIdentity(id, "");
            var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, id);
            identity.AddClaim(nameIdentifierClaim);
            var principal = new GenericPrincipal(identity, new string[] {});
            return new ClaimsPrincipal(principal);
        }
    }
}