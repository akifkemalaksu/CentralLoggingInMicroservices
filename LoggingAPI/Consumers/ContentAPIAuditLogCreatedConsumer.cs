using LoggingAPI.Controllers;
using LoggingAPI.Models;
using LoggingAPI.Services;
using Mapster;
using MassTransit;
using SharedModels.Messages;

namespace LoggingAPI.Consumers
{
    public class ContentAPIAuditLogCreatedConsumer : IConsumer<AuditLogCreated>
    {
        private readonly ILogService _logService;

        public ContentAPIAuditLogCreatedConsumer([FromKeyedServices(nameof(ContentController))] ILogService logService)
        {
            _logService = logService;
        }

        public Task Consume(ConsumeContext<AuditLogCreated> context)
        {
            var audit = context.Message.Adapt<Audit>();
            _logService.CreateAsync(audit);
            return Task.CompletedTask;
        }
    }
}
