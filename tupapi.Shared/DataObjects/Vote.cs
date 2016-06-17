using System;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;

namespace tupapi.Shared.DataObjects
{
    public class Vote : IVote
    {
        public string Id { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public VoteType Type { get; set; }
    }
}