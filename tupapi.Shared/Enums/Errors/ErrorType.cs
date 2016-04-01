namespace tupapi.Shared.Enums
{
    public enum ErrorType
    {
        None = 0,

        /// <summary>
        ///     Property Is Null
        /// </summary>
        IsNull = 1,


        Internal = 3,
        //Login and Registration
        EmailInvalid = 100,
        EmailWrong = 101,
        NameInvalid = 103,
        NameWrong = 104,
        PasswordInvalid = 108,
        PasswordLength = 109,
        PasswordWrong = 110,

        //User
        UserWithEmailExist = 200,
        UserWithNameExist = 201,
        UserWithEmailorNameNotFound = 202,
        UserNotFound = 203,
        UserBlocked = 204,
        UserNoPassword = 205,

        //Account
        AccountNotFound = 300,

        //Authorization
        NotOwner = 400,
        ClaimNotFound = 401,
    }
}