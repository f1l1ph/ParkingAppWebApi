using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Models;
using ParkingAppWebApi.Services;
using System;

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
                return Ok(car);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetCarByPlate")]
        public async Task<IActionResult> GetCarByPlate(string plate)
        {
            var cars = await _carService.GetAllCarsAsync();
            if (cars == null) { return NotFound(); }
            if (plate == null) { return NotFound(); }
            List<String> carPlates = new List<string>();
            for (int i = 0; i < cars.Count; i++)
            {
                carPlates.Add(cars[i].PlateNumber);
                if (carPlates.Contains(plate)) 
                {
                    return Ok(cars[i]);

                }
            }
            return NotFound();
        }

        [HttpGet("GetAllCars")]
        public async Task<IEnumerable<Car>> GetAllCars() 
        {
            return await _carService.GetAllCarsAsync();
        }

        [HttpGet("CheckForExistingCar")]
        public async Task<IActionResult> CheckForExistingCar(string num)
        {
            var cars = await _carService.GetAllCarsAsync();
            if(cars == null) { return NotFound(); }
            if(num == null) { return NotFound(); }
            List<String> carPlates = new List<string>();
            for(int i = 0; i < cars.Count; i++) 
            {
                carPlates.Add(cars[i].PlateNumber);
            }
            if (carPlates.Contains(num))
            {
                return Ok("car exists in db");
            }
            else
            {
                return NotFound();
            }
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
            if(!await _carService.DeleteCar(id))
            {
                return BadRequest();
            }
            return Ok("car successfully deleted");
        }
    }
}
