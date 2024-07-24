using testNewAPI.DTOS;
using testNewAPI.Models;
using testNewAPI.ServicesResponse;

namespace testNewAPI.Services.Contract
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<StudentDto>>> GetAllStudentAsyn();
        Task<ServiceResponse<StudentDto>> GetStudentByIdAsync(int Id);
        Task<ServiceResponse<StudentDto>> CreateStudentAsync(CreateStudentDto newStudent);
        Task<ServiceResponse<StudentDto>> UpdateStudentAsync(StudentDto updateStudent);
        Task<ServiceResponse<StudentDto>> DeleteStudentAsync(int Id);
 
    }
}
