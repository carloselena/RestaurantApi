namespace RestaurantApi.Core.Application.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        protected Response() { }

        public static Response<T> Success(T data, string? message = null)
            => new()
            {
                Succeeded = true,
                Data = data,
                Message = message
            };

        public static Response<T> Fail(string error)
            => new()
            {
                Succeeded = false,
                Errors = new() { error }
            };

        public static Response<T> Fail(List<string> errors)
            => new()
            {
                Succeeded = false,
                Errors = errors
            };
    }
}
