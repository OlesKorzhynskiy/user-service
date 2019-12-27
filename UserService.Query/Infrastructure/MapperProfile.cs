using AutoMapper;
using UserService.Domain.UserAggregate;
using UserService.Query.Contracts;

namespace UserService.Query.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadModel>();
        }
    }
}