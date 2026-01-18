namespace Application.Helpers
{
    public sealed class ApiResponse<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public Exception Error { get; }

        private ApiResponse(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null!;
        }

        private ApiResponse(Exception error)
        {
            IsSuccess = false;
            Error = error;
            Value = default!;
        }

        public static ApiResponse<T> Success(T value) => new(value);

        public static ApiResponse<T> Failure(Exception error) => new(error);
    }
}
