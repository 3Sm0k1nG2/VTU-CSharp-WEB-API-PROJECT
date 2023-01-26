namespace WorkOrders_DAL.Errors
{
    public class ExceptionDetails : Exception
    {
        public ExceptionDetails() : base() { }

        public ExceptionDetails(string? message) : base(message) { }

        public ExceptionDetails(string? message, Exception? innerException) : base(message, innerException) { }

        public IDictionary<string, object?> Extensions  = new Dictionary<string, object?>();
    }
}
