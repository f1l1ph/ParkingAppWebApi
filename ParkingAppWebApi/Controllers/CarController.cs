using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]

    class CarController : ControllerBase
    {
        /*string connectionString = "Server=localhost;Initial Catalog=ParkingAppDb;MultipleActiveResultSets=False;Connection Timeout=30;Trusted_Connection=True;TrustServerCertificate=True";

        [HttpGet(Name = "GetAllCars")]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            var command = new SqlCommand(
            "SELECT * FROM [Car]",
            conn);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<Car> cars = new List<Car>();
            int i = 0;
            while (reader.Read())
            {
                cars.Add(ReadOneRow((IDataRecord)reader));
                if (cars[i].Name == "0")
                {
                    return NotFound();
                }
                i++;
            }
            await reader.CloseAsync();
            if (cars.Count == 0)
            {
                return NotFound();
            }
            await conn.CloseAsync();

            return Ok(cars);
        }

        private static Car ReadOneRow(IDataRecord dataRecord)
        {
            String id = dataRecord[0].ToString();
            int id_Int = int.Parse(id);
            String? name = dataRecord[1].ToString();
            String? spz = dataRecord[2].ToString();
            String? delete_date = dataRecord[3].ToString();
            DateTime deleteTime = Convert.ToDateTime(delete_date);

            String? description = dataRecord[4].ToString();

            if(name == null || spz == null)
            {
                Car car1 = new Car
                {
                    ID = 0,
                    Name = "0",
                    SPZ = "0",
                    delete_Date = null,
                    Description = "0"
                };

                return car1;
            }

            Car car = new Car
            {
                ID = id_Int,
                Name = name,
                SPZ = spz,
                delete_Date = deleteTime,
                Description = description
            };
            return car;
        }

        [HttpDelete(Name = "DeleteCarById")]
        public async Task<IActionResult> DeleteOneCarsAsync(int id) 
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            var command = new SqlCommand(
            "DELETE FROM car\r\nWHERE id = @id;",
            conn);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            await conn.CloseAsync();
            return Ok("worked out goog job");
        }

        [HttpPost(Name = "CreateCar")]
        public async Task<IActionResult> CreateCarAsync(Car car)
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            var command = new SqlCommand(
                "INSERT INTO [Car] (Name, SPZ) VALUES (@Name, @Spz)", 
                conn);
            command.Parameters.AddWithValue("@Name", car.Name);
            command.Parameters.AddWithValue("@Spz", car.SPZ);


            using SqlDataReader reader = await command.ExecuteReaderAsync();

            await conn.CloseAsync();
            return Ok("Car probably added to DB");
        }*/

        private readonly AppDbContext _context;

        public CarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name ="GetAllCars")]
        public OkObjectResult Get()
        {
            var cars = _context.Cars.ToList();

            return Ok(cars);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetProducts()
        {
            return _context.Cars.ToList();
        }
    }
}
