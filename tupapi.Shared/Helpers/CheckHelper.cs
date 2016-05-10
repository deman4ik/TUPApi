using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tupapi.Shared.Helpers
{
    public static class CheckHelper
    {
        public static bool IsNameValid(string name)
        {
            //TODO: Check this Regex
            return Regex.IsMatch(name, Const.NameRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, Const.EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static bool IsPasswordValid(string password)
        {
            //TODO: Check this Regex
            if (string.IsNullOrWhiteSpace(password))
                return false;
            return password.Length > 8;
        }
    }
}
