using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoPortifolio.API.Controllers
{
    /// <summary>
    /// essa classe é para testes local apenas...
    /// </summary>
    public class UsuarioController : BaseController
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IConfiguration configuration;

        public UsuarioController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 IUsuarioRepository usuarioRepository,
                                 IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.usuarioRepository = usuarioRepository;
            this.configuration = configuration;
        }

        [HttpPost("criar")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            await usuarioRepository.Insert(usuario);

            await _userManager.CreateAsync(new IdentityUser() { UserName = usuario.Email, Email = usuario.Email }, usuario.Senha);

            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, false, true);
            if (result.Succeeded)
            {
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                DateTime expiration = DateTime.Now.AddMinutes(120);

                Usuario usuarioSalvo = await usuarioRepository.GetByEmail(usuario.Email);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, usuarioSalvo.Email),
                    new Claim(ClaimTypes.Name, usuarioSalvo.Nome),
                    new Claim(ClaimTypes.Sid, usuarioSalvo.CodigoUsuario.ToString())
                };

                JwtSecurityToken Sectoken = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Issuer"],
                  claims,
                  expires: expiration,
                  signingCredentials: credentials);

                string token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return Ok(new { token, expiration });
            }
            return Unauthorized();
        }
    }
}
