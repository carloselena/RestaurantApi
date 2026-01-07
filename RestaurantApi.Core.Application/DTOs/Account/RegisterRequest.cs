using RestaurantApi.Core.Application.Enums;
using System.Text.Json.Serialization;

namespace RestaurantApi.Core.Application.DTOs.Account
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public Roles Role { get; set; }
    }
}
