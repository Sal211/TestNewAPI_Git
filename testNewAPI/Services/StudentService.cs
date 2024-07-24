using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using testNewAPI.DTOS;
using testNewAPI.Models;
using testNewAPI.Repository;
using testNewAPI.Repository.Contract;
using testNewAPI.Services.Contract;
using testNewAPI.ServicesResponse;

namespace testNewAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepo,IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }
       
        private ServiceResponse<StudentDto> HandleError (ServiceResponse<StudentDto> response , string Msg)
        {
            response.Message = Msg;
            response.Success = false;
            response.Data = null;
            return response;
        }
        private void HandleException<T>(Exception ex, ServiceResponse<T> response)
        {         
            response.Message = "Error";
            response.Error = ex.Message;
            response.Success = false;
            response.Data = default(T);        
        }
        private void HandleArgumentException<T>(ArgumentException ex, ServiceResponse<T> response )
        {                    
            response.Message = ex.Message;
            response.Success = false;
            response.Data = default(T);         
        }
        public async Task<ServiceResponse<StudentDto>> CreateStudentAsync(CreateStudentDto newStudent)
        {
            ServiceResponse<StudentDto> response = new();
            try
            {
                ClsStudent students = new()
                {
                    Inactive = false,
                    Age = newStudent.Age,
                    Name = newStudent.Name,
                };
                if (!await _studentRepo.CreateStudentAsync(students)) return HandleError(response, "Respo Error");
                response.Success = true;
                response.Message = "Created";
                response.Data = _mapper.Map<StudentDto>(students);
            }
            catch (ArgumentException ex)
            {
                HandleArgumentException(ex, response);
            }
            catch (Exception ex)
            {
                HandleException(ex, response);
            }
            return response;
        }

        public async Task<ServiceResponse<StudentDto>> DeleteStudentAsync(int Id)
        {
            ServiceResponse<StudentDto> response = new();
            try
            {
                if (!await _studentRepo.SearchStudentAsync(Id)) return HandleError(response, "Not Found");
                if (!await _studentRepo.DeleteStudentAsync(Id)) return HandleError(response, "Respo Error");
                response.Message = "Deleted";
                response.Success = true;
            }
            catch (ArgumentException ex)
            {
                HandleArgumentException(ex, response);
            }
            catch (Exception ex)
            {
                HandleException(ex, response);
            }
            return response;
        }

        public async Task<ServiceResponse<List<StudentDto>>> GetAllStudentAsyn()
        {
            ServiceResponse<List<StudentDto>> response = new();
            try
            {
                var StudentList = await _studentRepo.GetAllStudentAsync();
                response.Success = true;
                response.Message = "OK";
                response.Data = StudentList;
            }
            catch (ArgumentException ex)
            {
                HandleArgumentException(ex, response);
            }
            catch (Exception ex)
            {
                HandleException (ex, response);
            }
            return response;
        }

        public async Task<ServiceResponse<StudentDto>> GetStudentByIdAsync(int Id)
        {
            ServiceResponse<StudentDto> response = new();
            try
            {
                if (!await _studentRepo.SearchStudentAsync(Id)) return  HandleError(response, "Not Found");
                var Student = await _studentRepo.GetStudentByIdAsync(Id);
                response.Success = true;
                response.Message = "OK";
                response.Data = Student[0];
            }
            catch (ArgumentException ex)
            {
                HandleArgumentException(ex, response);
            }
            catch (Exception ex)
            {
                HandleException(ex, response);
            }
            return response;
        }

        public async Task<ServiceResponse<StudentDto>> UpdateStudentAsync(StudentDto updateStudent)
        {
            ServiceResponse<StudentDto> response = new();
            try
            {
                if (!await _studentRepo.SearchStudentAsync(updateStudent.ID)) return HandleError(response, "Not Found");
                ClsStudent students = new()
                {
                    Inactive = false,
                    Age = updateStudent.Age,
                    Name = updateStudent.Name,
                    ID = updateStudent.ID
                };
                if (!await _studentRepo.UpdateStudentAsync(students)) return HandleError(response, "Respo Error");
                response.Success = true;
                response.Message = "Updated";
                response.Data = _mapper.Map<StudentDto>(updateStudent);
            }
            catch (ArgumentException ex)
            {
                HandleArgumentException(ex, response);
            }
            catch (Exception ex)
            {
                HandleException(ex, response);
            }
            return response;
        }
    }
}
