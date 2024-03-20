using LoggingAPI.Models;
using LoggingAPI.OptionModels;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace LoggingAPI.Services
{
    public class BloggingAPILogService : ILogService
    {
        private readonly IMongoCollection<Audit> _collection;

        public BloggingAPILogService(IOptions<LogDatabaseSettings> logDatabaseSettings)
        {
            var mongoClient = new MongoClient(logDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(logDatabaseSettings.Value.Database);

            _collection = mongoDatabase.GetCollection<Audit>(logDatabaseSettings.Value.BloggingAPILogsCollection);
        }

        public async Task<List<Audit>> GetAsync(Expression<Func<Audit, bool>> expression = null) =>
            expression == null ? await _collection.Find(_ => true).ToListAsync() :
                                await _collection.Find(expression).ToListAsync();

        public async Task<Audit?> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Audit newLog) =>
            await _collection.InsertOneAsync(newLog);
    }
}
