using CMS.Business.ClientResponseModels;
using CMS.Business.Models;
using CMS.Data;
using CMS.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly CMSDbContext _cmsDbContext;
        private readonly JwtOptions _options;

        public AuthController(CMSDbContext cmsDbContext, IOptions<JwtOptions> options, IConfiguration configuration)
        {
            this._cmsDbContext = cmsDbContext;
            _options = options.Value;
            _connectionString = configuration.GetConnectionString("SQLConnection") ?? throw new InvalidOperationException("Connection string cannot be null or whitespace");
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginCredentials loginCredentials)
        {
            LoginComponent loginImplementation = new LoginComponent();
            Users user = loginImplementation.IsValidUser(_cmsDbContext, loginCredentials);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Token = GetJWTToken(loginCredentials.EmailId);
            loginResponse.UserId = user.UserId;
            loginResponse.UserType = user.UserType;
            loginResponse.FirstName = user.FirstName;
            loginResponse.LastName = user.LastName;
            loginResponse.EmailId = user.EmailId;
            loginResponse.Mobile = user.Mobile;

            return Ok(loginResponse);
        }

        private string GetJWTToken(string email)
        {
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var crendential = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Email",email)
            };
            var sToken = new JwtSecurityToken(_options.Key, _options.Issuer, claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: crendential);
            var token = new JwtSecurityTokenHandler().WriteToken(sToken);
            return token;
        }
    }
}
