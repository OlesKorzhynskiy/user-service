using System;
using AutoMapper;
using Gateway.Contracts.UserService;
using UserService.Command.Contracts;

namespace Gateway.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateUserRequest, CreateUser>()
                .ForMember(command => command.Id, options => options.MapFrom(request => Guid.NewGuid()));

            CreateMap<UpdateUserRequest, UpdateUser>();
        }
    }
}