namespace Sprinterly.Models
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                Data = data,
                IsSuccessful = true
            };
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>
            {
                IsSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }

}
