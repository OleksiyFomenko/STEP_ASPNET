using System.ComponentModel.DataAnnotations;

namespace ClientManagementAPI.Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    = new List<Order>();

    }


}
