using System.ComponentModel.DataAnnotations;

namespace testNewAPI.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? Token { get; set; }
    }
}
