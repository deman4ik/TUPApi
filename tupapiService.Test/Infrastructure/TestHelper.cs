using System;
using System.Net.Http;
using Newtonsoft.Json;
using tupapi.Shared.DataObjects;

namespace tupapiService.Test.Infrastructure
{
    public class TestHelper
    {
        /// <summary>
        ///     Desitialize Controller Response
        /// </summary>
        /// <param name="response">Base Controller Response</param>
        /// <returns></returns>
        public static ControllerResult ParseResponse(HttpResponseMessage response)
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
            Log(controllerResult);
            return controllerResult;
        }

        /// <summary>
        ///     Log response to Console
        /// </summary>
        /// <param name="result"></param>
        public static void Log(ControllerResult result)
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
    }
}