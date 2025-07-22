using Microsoft.AspNetCore.Http;

namespace EXE202_BE.Service.Interface;

public interface IFireBaseStorageService
{
    Task<string> UploadImageAsync(IFormFile file, string fileName);

    Task FixAllAppChaoImagesAsync();
}