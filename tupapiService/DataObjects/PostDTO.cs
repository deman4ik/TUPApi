using System;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;
using ITableData = Microsoft.Azure.Mobile.Server.Tables.ITableData;

namespace tupapiService.DataObjects
{
    public class PostDTO : ITableData, IPost
    {
        public string Id { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public PhotoType Type { get; set; }
        public PhotoStatus Status { get; set; }
        public int Likes { get; set; }

        public override string ToString()
        {
            return "# Post Id:" + Environment.NewLine + Id + Environment.NewLine +
                   "# Post CreatedAt:" + Environment.NewLine + CreatedAt + Environment.NewLine +
                   "# Post UpdatedAt:" + Environment.NewLine + UpdatedAt + Environment.NewLine +
                   "# Post Version:" + Environment.NewLine + Version + Environment.NewLine +
                   "# Post Deleted:" + Environment.NewLine + Deleted + Environment.NewLine +
                   "# Post UserId:" + Environment.NewLine + UserId + Environment.NewLine +
                   "# Post UserName:" + Environment.NewLine + UserName + Environment.NewLine +
                   "# Post PhotoUrl:" + Environment.NewLine + PhotoUrl + Environment.NewLine +
                   "# Post Description:" + Environment.NewLine + Description + Environment.NewLine +
                   "# Post Type:" + Environment.NewLine + Type.ToString() + Environment.NewLine +
                   "# Post Status:" + Environment.NewLine + Status + Environment.NewLine +
                   "# Post Likes:" + Environment.NewLine + Likes;
        }
    }
}