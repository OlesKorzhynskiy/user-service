using System;
using AutoMapper;
using UserService.Command.Contracts;
using UserService.Domain.UserAggregate;

namespace UserService.Command.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateUser, User>();

            CreateMap<UpdateUser, User>();
        }
    }
}