using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantApi.Core.Application.DTOs.Account;
using RestaurantApi.Core.Application.DTOs.Email;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.Wrappers;
using RestaurantApi.Core.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantApi.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailService emailService,
            IOptions<JWTSettings> jwtSettings
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AccountResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AccountResponse<LoginResponse>.Fail("Datos de acceso incorrectos");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return AccountResponse<LoginResponse>.Fail("Datos de acceso incorrectos");

            if (!user.EmailConfirmed)
                return AccountResponse<LoginResponse>.Fail("Debe confirmar su correo electrónico antes de iniciar sesión");

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            LoginResponse response = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsVerified = user.EmailConfirmed
            };

            var roleList = await _userManager.GetRolesAsync(user);
            response.Roles = roleList.ToList();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return AccountResponse<LoginResponse>.Success(response);
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AccountResponse> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
                return AccountResponse.Fail("Ese nombre de usuario ya está en uso");

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
                return AccountResponse.Fail("Ese correo ya está en uso");

            IdentityUser user = new()
            {
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                if (result.Errors.Any(e => e.Code == "PasswordTooShort"))
                    errors.Add("La contraseña debe tener al menos 6 caracteres");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresUpper"))
                    errors.Add("La contraseña debe tener al menos una mayúscula");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresLower"))
                    errors.Add("La contraseña debe tener al menos una minúscula");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresDigit"))
                    errors.Add("La contraseña debe tener al menos un número");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresNonAlphanumeric"))
                    errors.Add("La contraseña debe tener al menos un caracter especial");

                if (errors.Any())
                    return AccountResponse.Fail(string.Join("\n", errors));
            }

            await _userManager.AddToRoleAsync(user, request.Role.ToString());
            var verificationUri = await SendVerificationEmailUri(user, origin);
            await _emailService.SendAsync(new EmailRequest
            {
                To = user.Email,
                Subject = "Confirma tu registro en Restaurant API by Carlos Elena",
                Body = $"<h1>Restaurant API by Carlos Elena</h1> <p>Por favor verifica tu cuenta haciendo clic en el siguiente enlace:</p> <p>{verificationUri}</p>"
            });

            return AccountResponse.Success();
        }

        public async Task<AccountResponse<string>> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return AccountResponse<string>.Fail("No se encontró la cuenta");

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return AccountResponse<string>.Fail("Ocurrió un error al confirmar la cuenta, intente de nuevo");

            return AccountResponse<string>.Success("Cuenta verificada correctamente. Ahora puedes acceder a la api.");
        }

        public async Task<AccountResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AccountResponse.Fail("No se encontró una cuenta para ese correo");

            var verificationUri = await SendForgotPasswordUri(user, origin);
            await _emailService.SendAsync(new EmailRequest
            {
                To = user.Email,
                Subject = "Restablece tu contraseña",
                Body = $"<h1>Restaurant API by Carlos Elena</h1> <p>Por favor restablece tu contraseña haciendo clic en el siguiente enlace:</p> <p>{verificationUri}</p>"
            });

            return AccountResponse.Success();
        }

        public async Task<AccountResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AccountResponse.Fail("No se encontró una cuenta para ese correo");

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                var errors = new List<string>();

                if (result.Errors.Any(e => e.Code == "PasswordTooShort"))
                    errors.Add("La contraseña debe tener al menos 6 caracteres");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresUpper"))
                    errors.Add("La contraseña debe tener al menos una mayúscula");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresLower"))
                    errors.Add("La contraseña debe tener al menos una minúscula");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresDigit"))
                    errors.Add("La contraseña debe tener al menos un número");

                if (result.Errors.Any(e => e.Code == "PasswordRequiresNonAlphanumeric"))
                    errors.Add("La contraseña debe tener al menos un caracter especial");

                if (errors.Any())
                    return AccountResponse.Fail(string.Join("\n", errors));

                else return AccountResponse.Fail("Ocurrió un error al restablecer la contraseña, intente de nuevo");
            }

            return AccountResponse.Success();
        }

        #region Private Methods
        private async Task<JwtSecurityToken> GenerateJWToken(IdentityUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;

        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<string> SendVerificationEmailUri(IdentityUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }

        private async Task<string> SendForgotPasswordUri(IdentityUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(uri.ToString(), "token", code);

            return verificationUri;
        }
        #endregion
    }
}
