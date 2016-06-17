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