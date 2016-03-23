using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;
using tupapi.Shared.Enums;

namespace tupapiService.Models
{
    public class User : EntityData
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     User Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     User Type
        /// </summary>
        /// <see cref="UserType">User Type Enum</see>
        public UserType Type { get; set; }

        /// <summary>
        ///     Salt for hash
        /// </summary>
        public byte[] Salt { get; set; }

        /// <summary>
        ///     Hashed Password with Salt
        /// </summary>
        public byte[] SaltedAndHashedPassword { get; set; }

        /// <summary>
        ///     Hashed Email with Salt
        /// </summary>
        public byte[] SaltedAndHashedEmail { get; set; }

        /// <summary>
        ///     Hashed Code with Salt for password recovery
        /// </summary>
        public byte[] SaltedAndHashedCode { get; set; }

        /// <summary>
        ///     Confirmation Email Sign
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        ///     Password Recovery Request Sign
        /// </summary>
        public bool IsResetRequested { get; set; }

        /// <summary>
        ///     User Blocked Sign
        /// </summary>
        public bool IsBlocked { get; set; }

        #region NavigataionProps
        public ICollection<Account>  Accounts { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Vote> Votes { get; set; } 
#endregion
    }
}