namespace OneDay.Core
{
    public class Result
    {
        public string Error { get; }
        public int ErrorCode { get; }

        public bool HasError() => Error != null;
        
        public static Result WithError(string error) => new(error);
        public static Result WithError(int errorCode, string error) => new(error);
        public static Result WithOk() => new();

        protected Result(string error) => Error = error;

        protected Result(int errorCode, string error)
        {
            ErrorCode = errorCode;
            Error = error;
        }

        protected Result() { }
    }
    
    public class DataResult<T> : Result
    {
        public T Data { get; private set; }

        private DataResult(string error) : base(error)
        { }
        
        private DataResult(int errorCode, string error) : base(errorCode, error)
        { }
        
        private DataResult() : base()
        { }

        public static DataResult<T> WithData(T data)
        {
            var result = new DataResult<T>
            {
                Data = data
            };
            return result;
        }

        public new static DataResult<T> WithError(int errorCode, string error) => new(errorCode, error);
        public new static DataResult<T> WithError(string error) => new(error);
    }
}