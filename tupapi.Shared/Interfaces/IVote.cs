using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupapi.Shared.Enums;

namespace tupapi.Shared.Interfaces
{
   public interface IVote : ITableData
    {
         string UserId { get; set; }
         string PostId { get; set; }
         VoteType Type { get; set; }
    }
}
