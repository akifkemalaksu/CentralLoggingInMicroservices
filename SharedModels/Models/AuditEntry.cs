using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedModels.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class AuditEntry
    {
        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public string Type { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public DateTime DateTime { get; set; } = DateTime.Now;

        public AuditLogCreated ToAudit()
        {
            var audit = new AuditLogCreated()
            {
                Type = Type,
                DateTime = this.DateTime,
                Table = TableName,
                PrimaryKey = KeyValues.Count > 0 ? JsonSerializer.Serialize(KeyValues) : null,
                OldValues = OldValues.Count > 0 ? JsonSerializer.Serialize(OldValues) : null,
                NewValues = NewValues.Count > 0 ? JsonSerializer.Serialize(NewValues) : null,
                AffectedColumns = ChangedColumns.Count > 0 ? JsonSerializer.Serialize(ChangedColumns) : null,
                UserId = UserId,
            };

            return audit;
        }
    }
}
