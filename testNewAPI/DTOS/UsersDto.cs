using System.ComponentModel.DataAnnotations;

namespace testNewAPI.DTOS
{
    public class UsersDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
