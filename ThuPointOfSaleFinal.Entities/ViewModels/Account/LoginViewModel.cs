using System.ComponentModel.DataAnnotations;

namespace ThuPointOfSaleFinal.Entities.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name ="User name")]
        [StringLength(256)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
