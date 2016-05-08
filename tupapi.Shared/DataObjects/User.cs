using System;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;

namespace tupapi.Shared.DataObjects
{
    public class User : IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public bool IsBlocked { get; set; }

        #region ITableData

        public string Id { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public byte[] Version { get; set; }
        public bool Deleted { get; set; }

        #endregion

        public override string ToString()
        {
            return "# User Id:" + Environment.NewLine + Id + Environment.NewLine +
                   "# User CreatedAt:" + Environment.NewLine + CreatedAt + Environment.NewLine +
                   "# User UpdatedAt:" + Environment.NewLine + UpdatedAt + Environment.NewLine +
                   "# User Version:" + Environment.NewLine + Version + Environment.NewLine +
                   "# User Deleted:" + Environment.NewLine + Deleted + Environment.NewLine +
                   "# User Name:" + Environment.NewLine + Name + Environment.NewLine +
                   "# User Email:" + Environment.NewLine + Email + Environment.NewLine +
                   "# User Type:" + Environment.NewLine + Type + Environment.NewLine +
                   "# User IsBlocked:" + Environment.NewLine + IsBlocked + Environment.NewLine;
        }
    }
}