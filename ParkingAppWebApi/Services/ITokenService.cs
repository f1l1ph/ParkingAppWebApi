namespace ParkingAppWebApi.Services
{
    public interface ITokenService
    {
        string CreateToken(string userName);
    }
}
