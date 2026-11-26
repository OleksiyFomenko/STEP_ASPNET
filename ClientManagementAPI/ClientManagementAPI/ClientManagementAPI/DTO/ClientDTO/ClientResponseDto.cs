namespace ClientManagementAPI.DTO.ClientDTO
{
    public class ClientResponseDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public object Id { get; internal set; }
    }
}
