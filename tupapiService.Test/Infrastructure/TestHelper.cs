using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Hosting;
using Newtonsoft.Json;
using tupapi.Shared.DataObjects;
using tupapiService.Controllers;
using tupapiService.Models;
using LoginResult = tupapiService.Controllers.LoginResult;

namespace tupapiService.Test.Infrastructure
{
    public class TestHelper
    {
        /// <summary>
        /// Provide Standart Authentication
        /// </summary>
        /// <param name="context">ITupapiContext</param>
        /// <param name="req">Creds</param>
        /// <returns></returns>
        public static LoginResult Authenticate(ITupapiContext context, StandartAuthRequest req)
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

        /// <summary>
        ///     Desitialize Controller Base Response
        /// </summary>
        /// <param name="response">Base Controller Response</param>
        /// <returns></returns>
        public static ControllerResult ParseBaseResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            string content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<BaseResponse>(response.Content.ReadAsStringAsync().Result);
            var controllerResult = new ControllerResult
            {
                StatusCode = response.StatusCode.ToString(),
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ApiResult = result.ApiResult,
                ErrorType = result.ErrorType,
                ResponseMessage = result.Message
            };
            LogBaseResponse(controllerResult);
            return controllerResult;
        }

        /// <summary>
        ///     Log Base Response to Console
        /// </summary>
        /// <param name="result"></param>
        public static void LogBaseResponse(ControllerResult result)
        {
            Console.WriteLine("# Status Code:");
            Console.WriteLine(result.StatusCode);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("# Is Success Status Code:");
            Console.WriteLine(result.IsSuccessStatusCode);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("# Api Result:");
            Console.WriteLine(result.ApiResult.ToString());
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("# Error Type:");
            Console.WriteLine(result.ErrorType.ToString());
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("# Response Message:");
            Console.WriteLine(result.ResponseMessage);
        }

        public static LoginResult ParseLoginResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result = JsonConvert.DeserializeObject<LoginResult>(response.Content.ReadAsStringAsync().Result);
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

        public static PostResponse ParsePostResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                Console.WriteLine("HttpResponseMessage is NULL");
                return null;
            }
            var result = JsonConvert.DeserializeObject<PostResponse>(response.Content.ReadAsStringAsync().Result);
            if (result != null)
            {
                LogPostResponse(result);
            }
            return result;
        }

        public static void LogPostResponse(PostResponse result)
        {
            Console.WriteLine("# Post Id:");
            Console.WriteLine(result.Id);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("# Sas:");
            Console.WriteLine(result.Sas);
        }
        public static ClaimsPrincipal GetUser(string id)
        {
            var identity = new GenericIdentity(id, "");
            var nameIdentifierClaim = new Claim(ClaimTypes.NameIdentifier, id);
            identity.AddClaim(nameIdentifierClaim);
            var principal = new GenericPrincipal(identity, roles: new string[] { });
            return new ClaimsPrincipal(principal);
        }
    }
}