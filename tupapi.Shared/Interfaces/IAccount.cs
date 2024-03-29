﻿using tupapi.Shared.Enums.Auth;

namespace tupapi.Shared.Interfaces
{
    /// <summary>
    ///     User Authentication Accounts
    /// </summary>
    public interface IAccount : ITableData
    {
        /// <summary>
        ///     Unique Account ID (Provider code + unique identifier)
        /// </summary>
        string AccountId { get; set; }

        /// <summary>
        ///     Unique user ID
        /// </summary>
        /// <see cref="IUser">User</see>
        string UserId { get; set; }

        /// <summary>
        ///     Authentication Provider
        /// </summary>
        Provider Provider { get; set; }

        /// <summary>
        ///     Authentication Provider unique ID
        /// </summary>
        string ProviderId { get; set; }

        /// <summary>
        ///     Valid Access Token Sign
        /// </summary>
        bool IsAccessTokenValid { get; set; }
    }
}