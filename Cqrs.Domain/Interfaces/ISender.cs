
namespace Cqrs.Domain.Interfaces
{
    public interface ISender<in Message>
    {
        void SendMessage(Message message);
    }
}
