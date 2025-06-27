namespace Exception
{
    public class InvalidEmailFormatException : System.Exception
    {
        public InvalidEmailFormatException(string message) : base(message) { }
    }
}
