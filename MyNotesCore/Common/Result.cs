namespace MyNotesCore.Common
{
    public class Result<T> 
    {
        public bool Success { get; set; }
        public T ReturnValue { get; set; }
        public string Message { get; set; }

        public Result()
        {

        }

        public Result(bool success, string message, T returnValue)
        {
            Success = success;
            ReturnValue = returnValue;
            Message = message;
        }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Result(bool success, T returnValue)
        {
            Success = success;
            ReturnValue = returnValue;
        }
    }
}
