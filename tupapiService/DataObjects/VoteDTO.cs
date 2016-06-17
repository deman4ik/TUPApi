using System;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;
using ITableData = Microsoft.Azure.Mobile.Server.Tables.ITableData;

namespace tupapiService.DataObjects
{
    public class VoteDTO : ITableData, IVote
    {
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public VoteType Type { get; set; }
    }
}