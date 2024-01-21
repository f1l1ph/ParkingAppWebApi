using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Models;
using ParkingAppWebApi.Services;
using ParkingAppWebApi.Validation;

namespace ParkingAppWebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CarController(ICarService carService) : ControllerBase
    {
        [HttpGet ("GetByID/{id}")]
        public async Task<IActionResult> GetOneCar(int id)
        {
            var car = await carService.GetCarById(id);

            return Ok(car);
        }

        [HttpGet("GetByPlate/{plate}")]
        public async Task<IActionResult> GetCarByPlate(string plate)
        {
            var car = await carService.GetCarByPlate(plate);

            return car != null ? Ok(car) : NotFound();
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Car>> GetAllCars() 
        {
            return await carService.GetAllCarsAsync();
        }

        [HttpGet("CheckForExisting/{plate}")]
        public async Task<IActionResult> CheckForExistingCar(string plate)
        {
            var car = await carService.GetCarByPlate(plate);

            return Ok(car != null);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCar(Car car)
        {
            var validator = new CarValidator();
            var valResult = await validator.ValidateAsync(car);
            if (!valResult.IsValid) { return NotFound("wrong car format"); }

            await carService.CreateCar(car);
            return Ok("Car successfully created");
        }

        [HttpPost("CreateBulk")]
        public async Task<IActionResult> CreateCars(List<Car> cars)
        {
            var validator = new CarValidator();
            foreach (var car in cars)
            {
                var valResult = await validator.ValidateAsync(car);
                if (!valResult.IsValid) { return NotFound("wrong format"); }

            }
            await carService.CreateCars(cars);
            return Ok("cars added to db");
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> EditCar(int id, Car car)
        {
            await carService.UpdateCar(id, car);
            return Ok(await carService.GetCarById(id));
        }

        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if(!await carService.DeleteCar(id))
            {
                return BadRequest();
            }
            return Ok("car successfully deleted");
        }
    }
}
