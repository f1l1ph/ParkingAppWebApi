using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Models;
using ParkingAppWebApi.Services;

namespace ParkingAppWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet ("GetCarByID/{id}")]
        public async Task<IActionResult> GetOneCar(int id)
        {
            var car = await _carService.GetCarById(id);

            if(car != null)
            {
                return Ok(await _carService.GetCarById(id));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllCars")]
        public async Task<IEnumerable<Car>> GetAllCars() 
        {
            return await _carService.GetAllCarsAsync();
        }

        [HttpPost("CreateCar")]
        public async Task<IActionResult> CreateCar(Car car)
        {
            await _carService.CreateCar(car);
            return Ok("Car successfully created");
        }

        [HttpPost("EditCar")]
        public async Task<IActionResult> EditCar(int id, Car car)
        {
            await _carService.UpdateCar(id, car);
            return Ok(await _carService.GetCarById(id));
        }

        [HttpDelete("DeleteCarById")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCar(id);
            return Ok("car successfully deleted");
        }
    }
}
