using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VideoPlatform.BLL.Models.Enums;

namespace VideoPlatform.BLL.Interfaces;

public interface IFileOperationManager
{
    Task<(string filePath, string thumbnailPath, string logoPath)> UploadFileToStorageAsync(IFormFile file,
        CancellationToken cancellationToken = default);

    Task<string> UploadFileByTypeToStorageAsync(IFormFile file, FileTypeEnum fileType,
        CancellationToken cancellationToken = default);

    Task<bool> RemoveFileByTypeFromStorageAsync(string filePath, FileTypeEnum fileType,
        CancellationToken cancellationToken = default);
}