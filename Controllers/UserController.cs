using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 

    public class UserController: ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        public UserController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> login(AuthInfo info){

            var results = from users in _context.users select users;
            var user = await results.Where((x) => x.name == info.name && x.password == info.password).SingleAsync();
            if(user == null){
                return NotFound();
            }
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.name));

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(4),
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            string token =  tokenHandler.WriteToken(createdToken);

            return Ok(token);


        }

    }
}