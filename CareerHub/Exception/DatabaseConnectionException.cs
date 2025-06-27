namespace Exception
{
    public class DatabaseConnectionException : System.Exception
    {
        public DatabaseConnectionException(string message) : base(message) { }
        public DatabaseConnectionException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
