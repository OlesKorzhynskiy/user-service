using System.Threading.Tasks;

namespace Mediator.Handler
{
    public interface IHandleMessages<T>
    {
        Task Handle(T message);
    }
}