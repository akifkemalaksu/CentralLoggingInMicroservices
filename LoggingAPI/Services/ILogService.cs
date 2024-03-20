using LoggingAPI.Models;
using System.Linq.Expressions;

namespace LoggingAPI.Services
{
    public interface ILogService
    {
        Task<List<Audit>> GetAsync(Expression<Func<Audit, bool>> expression = null);
        Task<Audit?> GetAsync(string id);
        Task CreateAsync(Audit newLog);
    }
}
