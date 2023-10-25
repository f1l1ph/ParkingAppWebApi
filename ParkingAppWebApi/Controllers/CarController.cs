using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetOneCar/{id}")]
        public ActionResult GetOneCar(int id)
        {
            var cars = _context.Cars.FirstOrDefault(c => c.ID == id);

            return Ok(cars);
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            var cars = await _context.Cars.ToListAsync();

            return cars;
        }
    }
}
