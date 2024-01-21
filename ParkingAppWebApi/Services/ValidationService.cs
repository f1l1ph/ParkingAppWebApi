using System.Text.RegularExpressions;

namespace ParkingAppWebApi.Services;

public class ValidationService()
{
    public bool ValidateLicensePlate(string licensePlate)
    {
        var pattern = @"^[A-Z]{2}\d{3}[A-Z]{2}$";
        var regex = new Regex(pattern);

        return regex.IsMatch(licensePlate);

    }
}