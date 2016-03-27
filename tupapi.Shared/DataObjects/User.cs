﻿using System;
using tupapi.Shared.Enums;
using tupapi.Shared.Interfaces;

namespace tupapi.Shared.DataObjects
{
    public class User :  IUser
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
    }
}