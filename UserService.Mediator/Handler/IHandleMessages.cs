using System.Threading.Tasks;

namespace UserService.Mediator.Handler
{
    public interface IHandleMessages<T>
    {
        Task Handle(T message);
    }
}