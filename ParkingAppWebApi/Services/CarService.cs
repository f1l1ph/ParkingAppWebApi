using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Services
{
    public class CarService
    {
        readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars;
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task CreateCar(Car car)
        {
            if(car.Description == "string")
            {
                car.Description = null;
            }
            if(car.ExpirationDate == DateTime.Today)
            {
                car.ExpirationDate = null;
            }

            var _car = new Car { Name = car.Name, PlateNumber = car.PlateNumber, Description = car.Description, ExpirationDate = car.ExpirationDate };
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCar(int id, Car car)
        {
            var _car = _context.Cars.Find(id);
            if (car != null)
            {
                if (car.Name != null)
                {
                    _car.Name = car.Name;
                }
                if (car.PlateNumber != null)
                {
                    _car.PlateNumber = car.PlateNumber;
                }
                if(car.Description != null)
                {
                    _car.Description = car.Description;
                }
                if(car.ExpirationDate != null)
                {
                    _car.ExpirationDate = car.ExpirationDate;
                }

                _context.Entry(_car).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
