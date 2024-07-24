 
using testNewAPI.DTOS;
using testNewAPI.Models;
using testNewAPI.ServicesResponse;

namespace testNewAPI.Services.Contract
{
    public interface IUserLoginService
    {
        Task<ServiceResponse<Users>> GetUserLoginAsync(UsersDto user);
    }
}
