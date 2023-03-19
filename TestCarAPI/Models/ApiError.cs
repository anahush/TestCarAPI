namespace TestCarAPI.Models
{
    public class ApiError
    {
        public string Message { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;

        public ApiError() {}

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(string message, string detail)
        {
            Message = message;
            Detail = detail;
        }
    }
}
