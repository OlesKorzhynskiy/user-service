using System;
using System.Threading.Tasks;
using Contracts.Contracts;
using Mediator.Handler;

namespace Consumer
{
    public class UserHandler :
        IHandleMessages<CreateUser>,
        IHandleMessages<UpdateUser>
    {
        public Task Handle(CreateUser message)
        {
            Console.WriteLine($"Create user with name: {message.Name}");
            return Task.CompletedTask;
        }

        public Task Handle(UpdateUser message)
        {
            Console.WriteLine($"Update user with name: {message.Name}");
            return Task.CompletedTask;
        }
    }
}