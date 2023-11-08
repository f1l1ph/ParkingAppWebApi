using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace ParkingAppWebApi.Services
{
    public class UserService
    {
        readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUserByID(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> CreateUser(UserRegisterModelDTO user)
        {
            if(user == null) { return false; }

            if (await UserExists(user.UserName, user.Email))
            {
                return false;//BadRequest("Username or email Is Already Taken");
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
            bool succes = await _context.SaveChangesAsync() > 0;
            return succes;
        }

        private async Task<bool> UserExists(string username, string email)
        {
            if(username == null || email == null)
            {
                return false;
            }
            bool sameName = await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
            bool sameEmail = await _context.Users.AnyAsync(x => x.UserEmail.ToLower() == email.ToLower());
            if (sameName || sameEmail)
            {
                return true;
            }
            else return false;
        }

        public async Task<User> LoginUser(UserLoginModelDTO login)
        {
            User? user = null;

            if (login.Name == null)
            {
                user = await _context.Users
                    .SingleOrDefaultAsync(x => x.UserEmail == login.Email);
            }
            else if (login.Email == null)
            {
                user = await _context.Users
                    .SingleOrDefaultAsync(x => x.UserName == login.Name);
            }
            else if(login.Email == null && login.Name == null)
            {
                return null;//Unauthorized("Invalid UserName or Email");
            }
            else
            {
                user = await _context.Users
                    .SingleOrDefaultAsync(x => x.UserName == login.Name);
            }

            if (user == null) return null;//Unauthorized("Invalid UserName or Email");

            var hmac = new HMACSHA256(user.Salt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i]) return null;//Unauthorized("Invalid Password");
            }

            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null) return false;
            _context.Remove(user);
            if( await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            
            return false;
        }
    }
}
