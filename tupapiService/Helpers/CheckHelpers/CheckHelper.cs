using System;
using System.Text.RegularExpressions;
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

        public static void IsOwn(string objectUserId, string currentUserId)
        {
            if (objectUserId != currentUserId)
            {
                throw new ApiException(ApiResult.Denied,ErrorType.NotOwner,null);
            }
        }
        private static bool IsNameValid(string name)
        {
            //TODO: Check this Regex
            return Regex.IsMatch(name, Const.NameRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        private static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, Const.EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static void NameCheck(string name)
        {
            if (!IsNameValid(name))
                throw new ApiException(ApiResult.Validation, ErrorType.NameInvalid, name);
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