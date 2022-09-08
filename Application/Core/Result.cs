namespace Application.Core
{
    public class Result<T>
    {
        public T? Value { get; set; }
        
        public bool IsSuccess { get; set; }
        
        public Exception? Error { get; set; }
        
        public Result(T? value, bool isSuccess, Exception? error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Success(T value) => new (value, true, null);
        public static Result<T> Failure(Exception error) => new (default, false, error);
    }
}