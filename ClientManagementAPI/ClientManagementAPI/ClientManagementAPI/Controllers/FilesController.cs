using ClientManagementAPI.DTO.FileDTO;
using ClientManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {

        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            IFormFile file)
        {
            if (!ValidateFile(file))
            {
                return BadRequest("Файл не пройшов перевірку.");
            }
            string fileName = await _fileService.SaveFileAsync(file);

            string downloadUrl = Url.Action(
                nameof(Download),
                "Files",
                new { fileName },
                Request.Scheme
            );

            return Ok(new
            {
                FileName = fileName,
                DownloadUrl = downloadUrl
            });
        }


        [HttpGet("download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            string filePath = _fileService.GetFilePath(fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Файл не знайдено.");
            }

            string contentType = "application/octet-stream";

            return PhysicalFile(
                filePath,
                contentType,
                fileName
            );
        }


        private bool ValidateFile (IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            const long maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize) 
            {
                return false;
            }

            string[] allowedExtensions =
            {
                ".jpg",
                ".jpeg",
                ".png"
            };

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if(!allowedExtensions.Contains(extension))
            {
                return false;
            }

            string[] allowedContentTypes =
            {
                "image/jpeg",
                "image/png"
            };

            if(!allowedContentTypes.Contains(file.ContentType))
            {
                return false;
            }

            return true;
        }
    }
}
