namespace tupapi.Shared.Enums
{
    public enum ErrorType
    {
        None = 0,

        /// <summary>
        ///     Property Is Null
        /// </summary>
        IsNull = 1,

        /// <summary>
        ///     An entity Not Found
        /// </summary>
        NotFound = 2,

        Internal = 3,
        //Login and Registration
        EmailInvalid = 100,
        EmailWrong = 101,
        UserWithEmailExist = 102,
        NameInvalid = 103,
        NameWrong = 104,
        UserWithNameExist = 105,
        PasswordInvalid = 106,
        PasswordLength = 107,
        PasswordWrong = 108

        //User
    }
}