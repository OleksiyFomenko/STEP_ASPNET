using System.ComponentModel.DataAnnotations;

namespace ClientManagementAPI.DTOs;

public class CreateOrderDto
{
    [Required]
    public int ClientId { get; set; }

    [Range(0.01, 1000000)]
    public decimal TotalAmount { get; set; }
}
