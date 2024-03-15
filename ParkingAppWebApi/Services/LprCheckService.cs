using Microsoft.EntityFrameworkCore.Migrations;
using Refit;

namespace ParkingAppWebApi.Services;

public class LprCheckService
{
    private readonly ILprAPI _api;

    public LprCheckService()
    {
        _api = RestService.For<ILprAPI>("http://127.0.0.1:8000");
    }

    public async Task<string> CheckLicensePlateAsync(StreamPart image)
    {
        var result = await _api.CheckPlateByImageTask(image);

        return result ?? "notFound";
    }
}