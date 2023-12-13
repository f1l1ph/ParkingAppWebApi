using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace ParkingAppWebApi.Services
{
    public class UserService(AppDbContext context)
    {
        public async Task<List<User>> GetAllUsers()
        {
            var users = await context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            return await context.Users.FindAsync(id) ?? throw new ArgumentOutOfRangeException(nameof(id));
        }

        public async Task<bool> CreateUser(UserRegisterModelDTO user)
        {
            if (await UserExists(user.UserName, user.Email))
            {
                return false;//BadRequest("Username or email Is Already Taken");
            }

            var hmac = new HMACSHA256();

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            var newUser = new User
            {
                UserName = user.UserName,
                Password = hash,
                Salt = hmac.Key,
                UserEmail = user.Email
            };

            context.Users.Add(newUser);
            return await context.SaveChangesAsync() > 0;
        }

        private async Task<bool> UserExists(string username, string email)
        {
            var sameName = await context.Users.AnyAsync(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            var sameEmail = await context.Users.AnyAsync(x => x.UserEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            return sameName || sameEmail;
        }

        public async Task<User?> LoginUser(UserLoginModelDTO login)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(x => x.UserName == login.Name);
            

            if (user == null) return null;//Unauthorized("Invalid UserName or Email");

            var hmac = new HMACSHA256(user.Salt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.Password[i]) return null;//Unauthorized("Invalid Password");
            }
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetById(id);

            context.Remove(user);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
