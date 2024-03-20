using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Messages
{
    public class AuditLogCreated
    {
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
