using System.Linq;
using AutoMapper;
using tupapi.Shared.Enums;
using tupapiService.DataObjects;
using tupapiService.Models;

namespace tupapiService.Mapping
{
    public class Mapping
    {
        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                // UserDTO Mapping
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                // PostDTO Mapping
                cfg.CreateMap<Post, PostDTO>()
                    .ForMember(dst => dst.UserName, map => map.MapFrom(src => src.User.Name))
                    .ForMember(dst => dst.Likes, map => map.MapFrom(src => src.Votes.Count(v => v.Type == VoteType.Up)));
                cfg.CreateMap<PostDTO, Post>();
            });
        }
    }
}