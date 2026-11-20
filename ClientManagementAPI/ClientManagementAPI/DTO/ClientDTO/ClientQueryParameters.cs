namespace ClientManagementAPI.DTO.ClientDTO
{
    public class ClientQueryParameters
    {
        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;

        public string? Sort { get; set; } = "id";

        public string? Direction { get; set; } = "asc";

        public string? Filter { get; set; }
    }
}