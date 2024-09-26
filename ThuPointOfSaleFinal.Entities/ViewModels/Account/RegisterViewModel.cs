using System.ComponentModel.DataAnnotations;

namespace ThuPointOfSaleFinal.Entities.ViewModels.Account
{
    public class RegisterViewModel
    {
        [StringLength(256)]
        [Display(Name ="User name")]
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        [StringLength(256)]
        [Display(Name = "User Email")]
        [EmailAddress(ErrorMessage ="Email not in correct format")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirme password")]
        public string ConfirmePassword { get; set; }
    }
}
