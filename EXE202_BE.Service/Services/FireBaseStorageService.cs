using EXE202_BE.Service.Interface;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace EXE202_BE.Service.Services;

public class FireBaseStorageService : IFireBaseStorageService
{
    private readonly GoogleCredential _googleCredential;
    private readonly string _bucketName = "moviemanagement-362f6.appspot.com"; // Find this in Firebase console
    
    public FireBaseStorageService(FirebaseCredentialProvider provider)
    {
        _googleCredential = provider.StorageCredential;
    }
    
    public async Task<string> UploadImageAsync(IFormFile file, string fileName)
    {
        var storage = await StorageClient.CreateAsync(_googleCredential);

        using var stream = file.OpenReadStream();

        // 🔥 Store inside the "pictures/" folder
        string objectName = $"appchao/{fileName}";

        var obj = await storage.UploadObjectAsync(
            _bucketName,         // e.g., moviemanagement-362616.appspot.com
            objectName,          // e.g., pictures/photo1.jpg
            file.ContentType,
            stream
        );
        
        // 🔒 Fix: Set public read access (if you want public access)
        await storage.UpdateObjectAsync(new Google.Apis.Storage.v1.Data.Object
        {
            Bucket = _bucketName,
            Name = objectName,
            Acl = new List<Google.Apis.Storage.v1.Data.ObjectAccessControl>
            {
                new Google.Apis.Storage.v1.Data.ObjectAccessControl
                {
                    Entity = "allUsers",
                    Role = "READER"
                }
            }
        });

        return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
    }
    
    public async Task FixAllAppChaoImagesAsync()
    {
        var storage = await StorageClient.CreateAsync(_googleCredential);
        var objects = storage.ListObjects("moviemanagement-362f6.appspot.com", "appchao/");

        foreach (var obj in objects)
        {
            await storage.UpdateObjectAsync(new Google.Apis.Storage.v1.Data.Object
            {
                Bucket = obj.Bucket,
                Name = obj.Name,
                Acl = new List<Google.Apis.Storage.v1.Data.ObjectAccessControl>
                {
                    new Google.Apis.Storage.v1.Data.ObjectAccessControl
                    {
                        Entity = "allUsers",
                        Role = "READER"
                    }
                }
            });

            Console.WriteLine($"Updated ACL for {obj.Name}");
        }
    }
    
}