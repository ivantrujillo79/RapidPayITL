using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RapidPayITL.Data.Entity;
using RapidPayITL.Model;
using RapidPayITL.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace RapidPayITL.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Credential _credentialDetail = new Credential();
        private readonly IConfiguration _configuration;
        private readonly AuthService _Auth;

        public AuthController(IConfiguration configuration, AuthService Auth) 
        {
            this._configuration = configuration;
            this._Auth = Auth;
        }


        [HttpPost("register")]
        public async Task<ActionResult<CredentialDTO>> Register(CredentialDTO registerRequest)
        {
            _Auth.CreatePasswordHash(registerRequest.Password, out Byte[] passwordHash, out byte[] passwordSalt);
            _credentialDetail.UserName = registerRequest.UserName;
            _credentialDetail.PasswordHash = passwordHash;
            _credentialDetail.PasswordSalt = passwordSalt;

            return Ok(_credentialDetail);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(CredentialDTO loginRequest)
        {
            if(_credentialDetail.UserName != loginRequest.UserName)
            {
                return BadRequest();
            }

            if(!_Auth.VerifyPasswordHash(loginRequest.Password, _credentialDetail.PasswordHash, _credentialDetail.PasswordSalt))
            {
                return BadRequest();
            }

            string token = _Auth.CreateToken(_credentialDetail);
            return Ok(token);
        }
    }
}
