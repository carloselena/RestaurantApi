namespace RestaurantApi.Core.Application.Wrappers
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }

        public Response(T data, string? message = null)
        {
            Data = data;
            Message = message;
        }
    }
}
