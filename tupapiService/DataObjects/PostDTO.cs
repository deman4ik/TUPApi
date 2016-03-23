using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;
using ITableData = Microsoft.Azure.Mobile.Server.Tables.ITableData;

namespace tupapiService.DataObjects
{
    public class PostDTO : ITableData, IPost
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public PhotoType Type { get; set; }
        public PhotoStatus Status { get; set; }
        public int Likes { get; set; }
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }
        
}