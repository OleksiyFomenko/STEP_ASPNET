using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManagementAPI.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; } = null!;
    }

}
