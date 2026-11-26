namespace ClientManagementAPI.Model
{
    public class FileMetadata
    {
        public int Id { get; set; }

        public string OriginalFileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; }

        public string FilePath { get; set; } = string.Empty;

    }
}
