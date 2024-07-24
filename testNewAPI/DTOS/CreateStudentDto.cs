using System.ComponentModel.DataAnnotations;

namespace testNewAPI.DTOS
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Student Age is required")]
        [Range(1, 200, ErrorMessage = "Age must be more than 0 ")]
        public int Age { get; set; }

    }
}
