using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Services;

public interface ICarService
{
    Task<List<Car>> GetAllCarsAsync();
    Task<Car> GetCarById(int id);
    Task<Car?> GetCarByPlate(string plate);
    Task CreateCar(Car car);
    Task CreateCars(List<Car> cars);
    Task UpdateCar(int id, Car car);
    Task<bool> DeleteCar(int id);
}