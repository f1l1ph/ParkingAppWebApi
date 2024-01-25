using Refit;

namespace ParkingAppWebApi.Services;

public interface ILprAPI
{
    [Multipart]
    [Post("/upload-image/")]
    Task<string> CheckPlateByImageTask([AliasAs("image")] IFormFile image);

}