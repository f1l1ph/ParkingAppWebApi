using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Models;
using ParkingAppWebApi.Services;

namespace ParkingAppWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserService _service;
        readonly TokenService _tokenService;

        public UserController(UserService service, TokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [HttpPost ("Register")]
        public async Task<IActionResult> Register(UserRegisterModelDTO user)
        {
            if (!await _service.CreateUser(user)) 
            {
                return BadRequest();
            }

            var userDto = new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user.UserName)
            };
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserLoginModelDTO login)
        {
            var user = await _service.LoginUser(login);
            if(user == null) { return Unauthorized("Invalid Username, Email or password"); }

            var userDto = new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user.UserName)
            };
            return userDto;
        }

        [HttpGet("selectAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("selectUserById")]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var user = await _service.GetUserByID(id);
            return Ok(user);
        }
        [Authorize]
        [HttpDelete("DeleteUserById")]
        public async Task<IActionResult> DeleteUserByID(int id)
        {
            if(!await _service.DeleteUser(id))
            {
                return BadRequest();
            }
            return Ok("user deleted succesfuly");
        }
    }
}