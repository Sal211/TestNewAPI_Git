using System.Data.SqlClient;
using testNewAPI.DTOS;
using testNewAPI.Models.Connection;
using testNewAPI.Repository.Contract;

namespace testNewAPI.Repository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        public  async Task<bool> GetUserLoginAsync(UsersDto user)
        {
            ClsConnection con = new ClsConnection();
            if (con._Errcode == 0)
            {
                try
                {
                    string query = $"EXEC sp_GetUserLogin {user.UserName}, {user.Password}";
                    con._cmd = new SqlCommand(query, con._con);
                    int count = (int)await con._cmd.ExecuteScalarAsync();
                    return count > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    Console.Write($"Exception: {ex.Message}");
                    return false;
                }
                finally
                {
                    await con._con.CloseAsync();
                }
            }
            else
            {
                Console.Write("Database connection error.");
                throw new ArgumentException("Database connection error.");
            }
        }
    }
}
