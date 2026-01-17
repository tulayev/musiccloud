namespace Application.Helpers
{
    public class ApiResponse<T>
    {
        public T? Value { get; set; }
        public bool IsSuccess { get; set; }
        public Exception? Error { get; set; }
        
        public ApiResponse(T? value, bool isSuccess, Exception? error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static ApiResponse<T> Success(T value) => new (value, true, null);

        public static ApiResponse<T> Failure(Exception error) => new (default, false, error);
    }
}