namespace LoggingAPI.OptionModels
{
    public class LogDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string BloggingAPILogsCollection { get; set; }
        public string ContentAPILogsCollection { get; set; }
    }
}
