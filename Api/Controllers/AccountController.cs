using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Rgister(RegisterDto regUser)
        {
            if (await IsUserExists(regUser.Username.ToLower())) return BadRequest("Please select different user name");

            using var hmac = new HMACSHA512();

            var user = new AppUser();
            user.UserName = regUser.Username.ToLower();
            user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(regUser.Password));
            user.passwordSalt = hmac.Key;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto()
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };


        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginuser)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginuser.Username.ToLower());

            if (user == null) return Unauthorized("user name not exist");

            using var hmac = new HMACSHA512(user.passwordSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginuser.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.passwordHash[i]) return Unauthorized("Password not matched");
            }
            return new UserDto()
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };

        }


        public async Task<bool> IsUserExists(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName);
        }

    }
}