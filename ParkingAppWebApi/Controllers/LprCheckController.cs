using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Services;
using Refit;

namespace ParkingAppWebApi.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LprCheckController(LprCheckService service) : ControllerBase
    {
        [HttpPost("/CheckLicensePlate/")]
        public async Task<IActionResult> GetOneCar(ByteArrayPart image)
        {
            try
            {
                Stream str = new MemoryStream(image.Value);

                var plateNum = await service.CheckLicensePlateAsync(new StreamPart(str, image.FileName, image.ContentType));
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