using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ParkingAppWebApi.Models;
using System.Data;


namespace ParkingAppWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //string connectionString = "Server=tcp:parkingappsqlserver.database.windows.net,1433;Initial Catalog=parkingappsqldb;Persist Security Info=False;User ID=FilipMasarik;Password=Fifo2004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        string connectionString = "Server=localhost;Initial Catalog=ParkingAppDb;MultipleActiveResultSets=False;Connection Timeout=30;Trusted_Connection=True;TrustServerCertificate=True";
        /*
        [HttpGet(Name ="TestUser")]
        public async Task Get()
        {
            using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();

            var command = new SqlCommand(
            "SELECT * FROM [User]",
            conn);
            using SqlDataReader reader = await command.ExecuteReaderAsync();


            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(ReadOneRow((IDataRecord)reader));
            }
            await reader.CloseAsync();
        }

        private static User ReadOneRow(IDataRecord dataRecord)
        {
            String id = dataRecord[0].ToString();
            int id_Int = int.Parse(id);
            String userName = dataRecord[1].ToString();
            String password = dataRecord[2].ToString();
            User user = new User
            {
                ID = id_Int,
                UserName = userName,
                Password = password
            };
            return user;
        }
    */
    
    }
}