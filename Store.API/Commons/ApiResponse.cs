using System.Diagnostics.Eventing.Reader;

namespace Store.API.Commons
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }


        public static ApiResponse<T> Ok(int statusCode, T? data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Error(int statusCode, string message, IEnumerable<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
