using Microsoft.EntityFrameworkCore;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Services;

public class CarService(AppDbContext context, ValidationService validationService) : ICarService
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
        try
        {
            //if (!validationService.ValidateLicensePlate(plate)) { return null; }

            var car = await context.Cars.FirstOrDefaultAsync(car => car.PlateNumber == plate);
            return car;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException);
            return null;
        }
    }

    public async Task CreateCar(Car car)
    {
        //if(!await validationService.ValidateAndCheckLicensePlate(car.PlateNumber)) { return; }

        await context.Cars.AddAsync(car);
        await context.SaveChangesAsync();
    }

    public async Task CreateCars(List<Car> cars)
    {
        for(var i = 0; i < cars.Count; i++)
        {
            //if (await validationService.ValidateAndCheckLicensePlate(cars[i].PlateNumber))
            //{
                await context.Cars.AddAsync(cars[i]);
            //}
        }
        await context.SaveChangesAsync();
    }

    public async Task UpdateCar(int id, Car car)
    {
        var dbCar = await GetCarById(id);

        //if(!validationService.ValidateLicensePlate(car.PlateNumber)){ return; }

        dbCar.Name = car.Name;
        dbCar.PlateNumber = car.PlateNumber;
        dbCar.Description = car.Description;
        dbCar.ExpirationDate = car.ExpirationDate;

        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteCar(int id)
    {
        var car = await GetCarById(id);

        context.Cars.Remove(car);

        return await context.SaveChangesAsync() > 0;
    }
}
