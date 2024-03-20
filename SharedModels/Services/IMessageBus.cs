using MassTransit.Initializers;

namespace SharedModels.Services
{
    public interface IMessageBus
    {
        public Task Send<T>(T message, string queue);
    }
}
