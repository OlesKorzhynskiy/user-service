using UserService.Command.Contracts;
using UserService.Mediator.Dispatcher;

namespace Gateway.Infrastructure
{
    public static class TransportRoutingConfiguration
    {
        public static void RegisterUserServiceRoutes(IDispatcher dispatcher, string route)
        {
            dispatcher.RegisterRoute(typeof(CreateUser), route);
            dispatcher.RegisterRoute(typeof(UpdateUser), route);
        }
    }
}