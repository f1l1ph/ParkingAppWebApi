using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace ParkingAppWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //string connectionString = "Server=tcp:parkingappsqlserver.database.windows.net,1433;Initial Catalog=parkingappsqldb;Persist Security Info=False;User ID=FilipMasarik;Password=Fifo2004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        string connectionString = "Server=localhost;Initial Catalog=ParkingAppDb;MultipleActiveResultSets=False;Connection Timeout=30;Trusted_Connection=True;TrustServerCertificate=True";

        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost ("Register")]
        public async Task<IActionResult> Register(UserRegisterModelDTO user)
        {
            if(await UserExists(user.UserName, user.Email))
            {
                return BadRequest("Username or email Is Already Taken");
            }
            var hmac = new HMACSHA256();
            
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            var _user = new User
            {
                UserName = user.UserName,
                Password = hash,
                Salt = hmac.Key,
                UserEmail = user.Email
            };

            _context.Users.Add(_user);
            await _context.SaveChangesAsync();
            return Ok(_user);
        }

        private async Task<bool> UserExists(string username, string email)
        {
            bool sameName = await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
            bool sameEmail = await _context.Users.AnyAsync(x => x.UserEmail.ToLower() == email.ToLower());
            if (sameName || sameEmail)
            {
                return true;
            }
            else return false;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginModelDTO login)
        {
            User? user = null;
            

            if(login.Name == null)
            {
                await _context.Users
                    .SingleOrDefaultAsync(x => x.UserEmail == login.Email);
            }
            else if(login.Email == null)
            {
                await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == login.Name);
            }
            else
            {
               return Unauthorized("Invalid UserName or Email");
            }

            if (user == null) return Unauthorized("Invalid UserName or Email");

            var hmac = new HMACSHA256(user.Salt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i]) return Unauthorized("Invalid Password");
            }

            return user;
        }

        [HttpGet("select all users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}