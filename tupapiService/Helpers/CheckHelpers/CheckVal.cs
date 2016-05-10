using System;
using System.Text.RegularExpressions;
using tupapi.Shared.Enums;
using tupapi.Shared.Helpers;
using tupapiService.Helpers.ExceptionHelpers;

namespace tupapiService.Helpers.CheckHelpers
{
    public static class CheckVal
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
                throw new ApiException(ApiResult.Denied, ErrorType.NotOwner, null);
            }
        }

       

        public static void NameCheck(string name)
        {
            if (!CheckHelper.IsNameValid(name))
                throw new ApiException(ApiResult.Validation, ErrorType.NameInvalid, name);
        }

        public static void EmailCheck(string email)
        {
            if (!CheckHelper.IsEmailValid(email))
                throw new ApiException(ApiResult.Validation, ErrorType.EmailInvalid, email);
        }

        public static void PasswordCheck(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ApiException(ApiResult.Validation, ErrorType.IsNull, "password");

            if (!CheckHelper.IsPasswordValid(password))
                throw new ApiException(ApiResult.Validation, ErrorType.PasswordLength, password.Length.ToString());
            //TODO: Regular Expression for Password Validation
        }
    }
}