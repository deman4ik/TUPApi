namespace tupapi.Shared.Interfaces
{
    public interface ILoginResult
    {
        string AuthenticationToken { get; }
        IUser User { get; }
    }
}