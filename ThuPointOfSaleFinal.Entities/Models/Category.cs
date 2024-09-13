using System.ComponentModel.DataAnnotations;

namespace ThuPointOfSaleFinal.Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name ="Category Name")]
        [Required(ErrorMessage = "Name Can not be empty")]
        public string CategoryName { get; set; }
        [StringLength(150)]
        public string? Description { get; set; }
    }
}
