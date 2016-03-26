using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Mobile.Server;
using tupapi.Shared.Enums;

namespace tupapiService.Models
{
    public class Vote : EntityData
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string PostId { get; set; }

        [Required]
        public VoteType Type { get; set; }

        #region NavigataionProps

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }

        #endregion
    }
}