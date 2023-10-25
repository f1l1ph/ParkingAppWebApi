using Microsoft.AspNetCore.Mvc;


namespace ParkingAppWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //string connectionString = "Server=tcp:parkingappsqlserver.database.windows.net,1433;Initial Catalog=parkingappsqldb;Persist Security Info=False;User ID=FilipMasarik;Password=Fifo2004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        string connectionString = "Server=localhost;Initial Catalog=ParkingAppDb;MultipleActiveResultSets=False;Connection Timeout=30;Trusted_Connection=True;TrustServerCertificate=True";
    
    }
}