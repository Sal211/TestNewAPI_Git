using testNewAPI.DTOS;

namespace testNewAPI.Repository.Contract
{
    public interface IUserLoginRepository
    {
        Task<bool> GetUserLoginAsync(UsersDto user);
    }
}
