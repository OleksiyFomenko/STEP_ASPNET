using ClientManagementAPI.Model;

namespace ClientManagementAPI.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ICollection<Order> Order { get; set; }
        = new List<Order>();
}
