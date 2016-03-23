using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;
using tupapi.Shared.Interfaces;

namespace tupapiService.Models
{
    public class Account : EntityData
    {
        /// <summary>
        ///     Unique Account ID (Provider code + unique identifier)
        /// </summary>
        [Required]
        public string AccountId { get; set; }

        /// <summary>
        ///     Unique user ID
        /// </summary>
        /// <see cref="IUser">User</see>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        ///     Authentication Provider Name
        /// </summary>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        ///     Authentication Provider unique ID
        /// </summary>
        [Required]
        public string ProviderId { get; set; }

        /// <summary>
        /// Access Token for Provider
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Valid Access Token Sign 
        /// </summary>
        public bool IsAccessTokenValid { get; set; }

        public virtual User User { get; set; }
    }
}