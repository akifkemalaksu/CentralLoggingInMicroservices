using LoggingAPI.Models;
using LoggingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoggingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ILogService _logService;

        public ContentController([FromKeyedServices(nameof(ContentController))] ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var log = await _logService.GetAsync(id);

            if (log == null) return NotFound();

            return Ok(log);
        }

        [HttpGet("{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            var logs = await _logService.GetAsync(x => x.Type == type);

            if (logs == null) return NotFound();

            return Ok(logs);
        }

        [HttpGet("{primaryKey}")]
        public async Task<IActionResult> GetByPrimaryKey(string primaryKey)
        {
            var logs = await _logService.GetAsync(x => x.PrimaryKey == primaryKey);

            if (logs == null) return NotFound();

            return Ok(logs);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Audit log)
        {
            await _logService.CreateAsync(log);
            return CreatedAtAction(nameof(Get), new { id = log.Id }, log);
        }
    }
}
