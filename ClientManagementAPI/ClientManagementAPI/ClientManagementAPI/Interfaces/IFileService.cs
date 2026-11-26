using ClientManagementAPI.DTO.FileDTO;

namespace ClientManagementAPI.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);

        bool DeleteFile(string fileName);

        string GetFilePath(string fileName);
    }

}
