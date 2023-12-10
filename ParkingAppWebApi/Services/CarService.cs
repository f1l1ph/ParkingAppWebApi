using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Services
{
    public class CarService(AppDbContext context) : ICarService
    {
        public async Task<List<Car>> GetAllCarsAsync()
        {
            var cars = await context.Cars.ToListAsync();
            return cars;
        }

        public async Task<Car> GetCarById(int id)
        {
            return await context.Cars.FindAsync(id) ?? throw new ArgumentOutOfRangeException(nameof(id));
        }

        public async Task<Car?> GetCarByPlate(string plate)
        {
            return await context.Cars.FirstOrDefaultAsync(car => car.PlateNumber == plate);
        }

        public async Task CreateCar(Car car)
        {
            //ToDo validacia poli
            context.Cars.Add(car);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCar(int id, Car car)
        {
            var dbCar = await GetCarById(id);

            dbCar.Name = car.Name;
            dbCar.PlateNumber = car.PlateNumber;
            dbCar.Description = car.Description;
            dbCar.ExpirationDate = car.ExpirationDate;

            //TODO treba ? context.Entry(dbCar).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCar(int id)
        {
            var car = await GetCarById(id);

            context.Cars.Remove(car);

            return await context.SaveChangesAsync() > 0;
        }
    }
}
