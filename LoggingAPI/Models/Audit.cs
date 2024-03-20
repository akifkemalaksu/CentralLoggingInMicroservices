using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoggingAPI.Models
{
    public class Audit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Table { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; } = null!;
    }
}
