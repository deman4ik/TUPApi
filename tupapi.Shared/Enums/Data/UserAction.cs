namespace tupapi.Shared.Enums
{
    /// <summary>
    ///     User Actions for Global Logging
    /// </summary>
    public enum UserAction
    {
        /// <summary>
        ///     User created new account
        /// </summary>
        SignUp,

        /// <summary>
        ///     User logged in
        /// </summary>
        SignIn,

        /// <summary>
        ///     User posted new photo
        /// </summary>
        PostPhoto,

        /// <summary>
        ///     User liked photo
        /// </summary>
        LikePhoto,

        /// <summary>
        ///     User dislike photo
        /// </summary>
        DislikePhoto
    }
}