using RestaurantApi.Core.Application.DTOs.Email;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest emailRequest);
    }
}
