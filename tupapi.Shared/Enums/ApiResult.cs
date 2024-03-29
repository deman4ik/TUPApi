﻿namespace tupapi.Shared.Enums
{
    /// <summary>
    ///     Possible Error Types
    /// </summary>
    public enum ApiResult
    {
        /// <summary>
        ///     Good Response
        /// </summary>
        Ok = 0,

        /// <summary>
        ///     Object Vreated
        /// </summary>
        Created = 1,

        /// <summary>
        ///     Object Updated
        /// </summary>
        Updated = 2,

        /// <summary>
        ///     Object Deleted
        /// </summary>
        Deleted = 3,

        /// <summary>
        ///     Unknown Error
        /// </summary>
        Unknown = 4,

        /// <summary>
        ///     Validation Error
        /// </summary>
        Validation = 5,

        /// <summary>
        ///     SQL Error
        /// </summary>
        Sql = 6,

        /// <summary>
        ///     Access Denied
        /// </summary>
        Denied = 7,

        /// <summary>
        ///     Unauthenticated
        /// </summary>
        Unauth = 8,


        /// <summary>
        ///     An entity Not Found
        /// </summary>
        NotFound = 9,

        NoConnection = 10
    }
}