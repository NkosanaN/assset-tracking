using Application.UserPhotos;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Persistence;
public interface IPhotoAccessor
{
    Task<PhotoUploadResult> AddPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}
