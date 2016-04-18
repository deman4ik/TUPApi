using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Mobile.Server;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;

namespace tupapiService.Models
{
    public class Post : EntityData
    {
        public Post()
        {
            Votes = new HashSet<Vote>();
        }

        /// <summary>
        ///     User ID
        /// </summary>
        /// <see cref="IUser">User</see>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        ///     Photo Url
        /// </summary>
        [Required]
        public string PhotoUrl { get; set; }

        /// <summary>
        ///     Photo Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Photo Type
        /// </summary>
        /// <see cref="PhotoType">Photo Type Enum</see>
        [Required]
        public PhotoType Type { get; set; }

        /// <summary>
        ///     Photo Current Status
        /// </summary>
        /// <see cref="PhotoStatus">Photo Status Enum</see>
        [Required]
        public PhotoStatus Status { get; set; }

        #region NavigataionProps

        public virtual User User { get; set; }
        public ICollection<Vote> Votes { get; set; }

        #endregion
    }
}