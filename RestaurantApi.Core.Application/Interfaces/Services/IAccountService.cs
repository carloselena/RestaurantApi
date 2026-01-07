using RestaurantApi.Core.Application.DTOs.Account;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AccountResponse<string>> ConfirmAccountAsync(string userId, string token);
        Task<AccountResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<AccountResponse<LoginResponse>> LoginAsync(LoginRequest request);
        Task LogOutAsync();
        Task<AccountResponse> RegisterAsync(RegisterRequest request, string origin);
        Task<AccountResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
