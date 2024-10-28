namespace Flim.API.Common
{
    /// <summary>
    /// Genric Api response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; } 
        public string Message { get; set; }
        public T Result { get; set; }
        public bool IsSuccess { get; set; }

        public static ApiResponse<T> Success(T result, string message = "Success", int statusCode = 200)
        {
            return new ApiResponse<T> { StatusCode = statusCode, Message = message, Result = result, IsSuccess = true };
        }

        public static ApiResponse<T> Failure(string message, int statusCode = 500)
        {
            return new ApiResponse<T> { StatusCode = statusCode, Message = message, IsSuccess = false };
        }
    }
}
