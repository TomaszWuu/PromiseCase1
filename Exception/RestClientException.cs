namespace RestLibrary.Exception
{
    public class RestClientException : IOException
    {
        public System.Net.HttpStatusCode StatusCode { get; }

        public RestClientException(string message, System.Net.HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
