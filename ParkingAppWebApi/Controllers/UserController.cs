using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Models;
using ParkingAppWebApi.Services;

namespace ParkingAppWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(UserService service, ITokenService tokenService) : ControllerBase
    {
        [HttpPost ("Register")]
        public async Task<IActionResult> Register(UserRegisterModelDTO user)
        {
            if (!await service.CreateUser(user)) 
            {
                return BadRequest();
            }

            var userDto = new UserDTO
            {
                UserName = user.UserName,
                Token = tokenService.CreateToken(user.UserName)
            };
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserLoginModelDTO login)
        {
            var user = await service.LoginUser(login);

            var userDto = new UserDTO
            {
                UserName = user.UserName,
                Token = tokenService.CreateToken(user.UserName)
            };
            return userDto;
        }

        [HttpGet("selectAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await service.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("selectUserById")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var user = await service.GetById(id);
            return Ok(user);
        }
        [Authorize]
        [HttpDelete("DeleteUserById")]
        public async Task<IActionResult> DeleteUserByID(int id)
        {
            if(!await service.DeleteUser(id))
            {
                return BadRequest();
            }
            return Ok("user deleted successfully");
        }
    }
}