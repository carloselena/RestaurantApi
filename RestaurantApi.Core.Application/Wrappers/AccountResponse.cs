namespace RestaurantApi.Core.Application.Wrappers
{
    public class AccountResponse
    {
        public bool HasError { get; init; }
        public string? Error { get; init; }

        protected AccountResponse() { }

        public static AccountResponse Success()
            => new() { HasError = false };

        public static AccountResponse Fail(string error)
            => new() { HasError = true, Error = error };
    }

    public class AccountResponse<T> : AccountResponse
    {
        public T? Data { get; init; }

        protected AccountResponse() { }

        public static AccountResponse<T> Success(T data)
            => new() { HasError = false, Data = data };

        public new static AccountResponse<T> Fail(string error)
            => new() { HasError = true, Error = error };
    }
}
