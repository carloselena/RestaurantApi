using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.DTOs.Account;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RestaurantApi.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Sistema de membresías")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Iniciar sesión",
            Description = "Autentica al usuario y retorna un JWT"
        )]
        public async Task<IActionResult> LogIn([FromBody] LoginRequest request)
        {
            return Ok(await _accountService.LoginAsync(request));
        }

        [HttpPost("register-mesero")]
        [SwaggerOperation(
            Summary = "Registrar un nuevo usuario MESERO",
            Description = "Crea una nueva cuenta de usuario con los datos proporcionados + rol MESERO y envía un correo de confirmación para validar la dirección de email antes de permitir el acceso al sistema"
        )]
        public async Task<IActionResult> RegisterMesero([FromBody] RegisterRequest request)
        {
            var origin = Request.Headers.Origin;
            request.Role = Roles.MESERO;
            return Ok(await _accountService.RegisterAsync(request, origin!));
        }
        
        [HttpPost("register-admin")]
        [SwaggerOperation(
            Summary = "Registrar un nuevo usuario ADMIN",
            Description = "Crea una nueva cuenta de usuario con los datos proporcionados + rol ADMIN y envía un correo de confirmación para validar la dirección de email antes de permitir el acceso al sistema. Solo puede ser usado por usuarios ADMIN"
        )]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
        {
            var origin = Request.Headers.Origin;
            request.Role = Roles.ADMIN;
            return Ok(await _accountService.RegisterAsync(request, origin!));
        }

        [HttpGet("confirm-email")]
        [SwaggerOperation(
            Summary = "Confirmar correo electrónico",
            Description = "Valida la dirección de correo electrónico del usuario utilizando el userId y el token enviados por email durante el proceso de registro. Una vez confirmado, el usuario podrá iniciar sesión."
        )]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }

        [HttpPost("forgot-password")]
        [SwaggerOperation(
            Summary = "Solicitar restablecimiento de contraseña",
            Description = "Envía un correo electrónico con un enlace seguro para restablecer la contraseña del usuario, siempre que el correo esté registrado en el sistema."
        )]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var origin = Request.Headers.Origin;
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }

        [HttpPost("reset-password")]
        [SwaggerOperation(
            Summary = "Restablecer contraseña",
            Description = "Permite establecer una nueva contraseña utilizando el token de seguridad enviado previamente por correo electrónico durante el proceso de recuperación."
        )]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }
    }
}
