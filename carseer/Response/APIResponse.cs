namespace migration_postgress.Response
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }

        public APIResponse(bool success, string message, T data, int statusCode)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        // Helper methods for common scenarios
        public static APIResponse<T> SuccessResponse(T data, string message = "Request succeeded", int statusCode = 200)
        {
            return new APIResponse<T>(true, message, data, statusCode);
        }

        public static APIResponse<T> ErrorResponse(string message, int statusCode = 400)
        {
            return new APIResponse<T>(false, message, default(T), statusCode);
        }
    }
}
