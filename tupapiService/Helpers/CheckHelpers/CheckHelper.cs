using System;
using System.Net.Mail;
using tupapi.Shared.Enums;
using tupapiService.Helpers.ExceptionHelpers;

namespace tupapiService.Helpers.CheckHelpers
{
    public static class CheckHelper
    {
        /// <summary>
        ///     Checks object is null
        /// </summary>
        /// <param name="req">any object</param>
        /// <param name="propertyName">property name for error</param>
        public static void IsNull(object req, string propertyName = null)
        {
            if (req == null)
            {
                throw new ApiException(ApiResult.Validation, ErrorType.IsNull, propertyName);
            }
        }

        private static bool IsEmailValid(string email)
        {
            try
            {
                //TODO: Better Email check ( в данный момент не проверяется точка на конце ) 
                var m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static void EmailCheck(string email)
        {
            if (!IsEmailValid(email))
                throw new ApiException(ApiResult.Validation, ErrorType.EmailInvalid, email);
        }

        public static void PasswordCheck(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ApiException(ApiResult.Validation, ErrorType.IsNull, "password");

            if (password.Length < 8)
                throw new ApiException(ApiResult.Validation, ErrorType.PasswordLength, password.Length.ToString());
            //TODO: Regular Expression for Password Validation
        }
    }
}