using System.Linq;
using tupapi.Shared.Enums;
using tupapiService.Helpers.ExceptionHelpers;
using tupapiService.Models;

namespace tupapiService.Helpers.CheckHelpers
{
    public class CheckData
    {
        /// <summary>
        ///     Checks if user exist
        /// </summary>
        /// <param name="context">ITupapiContext</param>
        /// <param name="exception">Throw exception if found for email and name search</param>
        /// <param name="id">User Id</param>
        /// <param name="email">User Email</param>
        /// <param name="name">User Name</param>
        /// <returns>User</returns>
        public static User UserExist(ITupapiContext context, bool exception, string id = null, string email = null,
            string name = null)
        {
            User user = null;
            if (!string.IsNullOrWhiteSpace(id))
            {
                user = context.Users.SingleOrDefault(u => u.Id == id);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                user = context.Users.SingleOrDefault(u => u.Email == email);
                if (user != null && exception)
                    throw new ApiException(ApiResult.Validation, ErrorType.UserWithEmailExist, email);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                user = context.Users.SingleOrDefault(u => u.Name == name);
                if (user != null && exception)
                    throw new ApiException(ApiResult.Validation, ErrorType.UserWithNameExist, name);
            }

            return user;
        }
    }
}