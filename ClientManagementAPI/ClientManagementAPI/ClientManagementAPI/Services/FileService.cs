using AutoMapper;
using ClientManagementAPI.Data;
using ClientManagementAPI.DTO.FileDTO;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientManagementAPI.Services
{
    public class FileService: IFileService
    {
        private readonly string _uploadsFolder;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FileService(IWebHostEnvironment environment, ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _uploadsFolder = Path.Combine(environment.ContentRootPath, "UploadedFiles");
            Directory.CreateDirectory(_uploadsFolder);

        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}{extension}";
            string filePath = GetFilePath(fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public bool DeleteFile (string fileName)
        {
            string filePath = GetFilePath(fileName);
            if (!File.Exists(filePath))
            {
                return false;
            }
            File.Delete(filePath);
            return true;
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(_uploadsFolder, fileName);
        }
    }
    }
