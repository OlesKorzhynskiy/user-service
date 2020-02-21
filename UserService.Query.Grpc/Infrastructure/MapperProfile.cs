using AutoMapper;
using GrpcUserService;
using UserService.Domain.UserAggregate;

namespace UserService.Query.Grpc.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadModel>();
        }
    }
}