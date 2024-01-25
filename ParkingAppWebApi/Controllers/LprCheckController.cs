using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ParkingAppWebApi.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LprCheckController(LprCheckService service) : ControllerBase
    {
        [HttpPost("/CheckLicensePlate/")]
        public async Task<IActionResult> GetOneCar([FromForm] IFormFile image)
        {
            try
            {
                var plateNum = await service.CheckLicensePlateAsync(image);
                return Ok(plateNum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}