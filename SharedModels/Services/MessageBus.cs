using MassTransit;
using SharedModels.Services;

namespace SharedModels.Services
{
    public class MessageBus : IMessageBus
    {
        private readonly IBus _bus;

        public MessageBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task Send<T>(T message, string queue)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queue}"));
            endpoint.Send(message);
        }
    }
}
