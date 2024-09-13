using System.ComponentModel.DataAnnotations;

namespace ThuPointOfSaleFinal.Entities.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string StoreName { get; set; }
        [StringLength(250)]
        public string? Address { get; set; }
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
