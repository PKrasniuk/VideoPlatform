using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.BLL.Models.Enums;

namespace VideoPlatform.BLL.Managers
{
    public class FileOperationManager : IFileOperationManager
    {
        public async Task<(string filePath, string thumbnailPath, string logoPath)> UploadFileToStorageAsync(IFormFile file, CancellationToken cancellationToken)
        {
            var result = (filePath: default(string), thumbnailPath: default(string), logoPath: default(string));

            //TODO: Need to implementation upload file to file storage
            //TODO: and generate thumbnailPath and logoPath

            await Task.Delay(10, cancellationToken);

            return result;
        }

        public async Task<string> UploadFileByTypeToStorageAsync(IFormFile file, FileTypeEnum fileType, CancellationToken cancellationToken)
        {
            //TODO: Need to implementation upload file to files storage
            //TODO: by file type

            await Task.Delay(10, cancellationToken);

            return string.Empty;
        }

        public async Task<bool> RemoveFileByTypeFromStorageAsync(string filePath, FileTypeEnum fileType, CancellationToken cancellationToken)
        {
            //TODO: Need to implementation remove file from files storage
            //TODO: by file type

            await Task.Delay(10, cancellationToken);

            return true;
        }
    }
}