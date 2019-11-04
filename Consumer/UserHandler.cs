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
            return Task.CompletedTask;
        }

        public Task Handle(UpdateUser message)
        {
            return Task.CompletedTask;
        }
    }
}