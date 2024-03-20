using LoggingAPI.Controllers;
using LoggingAPI.Models;
using LoggingAPI.Services;
using Mapster;
using MassTransit;
using SharedModels.Messages;

namespace LoggingAPI.Consumers
{
    public class BloggingAPIAuditLogCreatedConsumer : IConsumer<AuditLogCreated>
    {
        private readonly ILogService _logService;

        public BloggingAPIAuditLogCreatedConsumer([FromKeyedServices(nameof(BloggingController))] ILogService logService)
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
