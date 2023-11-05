
namespace ParkingAppWebApi.Models
{
    public class UserRegisterModelDTO
    {
        public required string UserName { get; set; }

        public required string Password { get; set; }

        public required string Email { get; set; }
    }
}
