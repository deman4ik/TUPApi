using tupapi.Shared.Enums;

namespace tupapi.Shared.Interfaces
{
    /// <summary>
    ///     User
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// User Name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        ///     User Email
        /// </summary>
        string Email { get; set; }

        /// <summary>
        ///     User Type
        /// </summary>
        /// <see cref="UserType">User Type Enum</see>
        UserType Type { get; set; }

        /// <summary>
        ///     User Blocked Sign
        /// </summary>
        bool IsBlocked { get; set; }
    }
}
