using testNewAPI.DTOS;
using testNewAPI.Models;

namespace testNewAPI.Repository.Contract
{
    public interface IStudentRepository
    {
        Task<List<StudentDto>> GetAllStudentAsync();
        Task<List<StudentDto>> GetStudentByIdAsync(int Id);
        Task<bool> CreateStudentAsync(ClsStudent newStudent);
        Task<bool> UpdateStudentAsync(ClsStudent updateStudent);
        Task<bool> DeleteStudentAsync(int Id);
        Task<bool> SearchStudentAsync(int Id);
    }
}
