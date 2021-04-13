namespace Domain.Settings
{
    public class DatabaseConnectionString
    {
        public LocalDb LocalDb { get; set; }
        public SqlServer SqlServer { get; set; }
        public SQLite SQLite { get; set; }


    }

    public class LocalDb
    {
        public string InitialCatalog { get; set; }
        public string AttachDbFilename { get; set; }
    }

    public class SqlServer : BaseConnectionString { }
    public class SQLite : BaseConnectionString { }


    public class BaseConnectionString
    {
        public string ApplicationDbContextConnection { get; set; }
    }


}
