using tupapi.Shared.Enums;

namespace tupapi.Shared.Interfaces
{
    /// <summary>
    ///     Photo Post
    /// </summary>
    public interface IPost
    {
        /// <summary>
        ///     User ID
        /// </summary>
        /// <see cref="IUser">User</see>
        string UserId { get; set; }

        /// <summary>
        ///     User Name
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        ///     Photo Url
        /// </summary>
        string PhotoUrl { get; set; }

        /// <summary>
        ///     Photo Description
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Photo Type
        /// </summary>
        /// <see cref="PhotoType">Photo Type Enum</see>
        PhotoType Type { get; set; }

        /// <summary>
        ///     Photo Current Status
        /// </summary>
        /// <see cref="PhotoStatus">Photo Status Enum</see>
        PhotoStatus Status { get; set; }

        /// <summary>
        ///     Likes Count
        /// </summary>
        int Likes { get; set; }
    }
}