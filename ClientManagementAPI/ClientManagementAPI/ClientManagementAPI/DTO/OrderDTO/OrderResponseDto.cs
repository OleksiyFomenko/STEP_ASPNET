namespace ClientManagementAPI.DTO.OrderDTO
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; } = string.Empty;
    }

}
