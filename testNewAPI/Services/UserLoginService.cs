using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using testNewAPI.DTOS;
using testNewAPI.Models;
using testNewAPI.Repository.Contract;
using testNewAPI.Services.Contract;
using testNewAPI.ServicesResponse;

namespace testNewAPI.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userRespo;
        public UserLoginService(IUserLoginRepository userRespo)
        {
            _userRespo = userRespo;
        }
      
        public async Task<ServiceResponse<Users>> GetUserLoginAsync(UsersDto user)
        {
            ServiceResponse<Users> response = new();
            try
            {
                if (!await _userRespo.GetUserLoginAsync(user))
                {
                    response.Message = "Invalid Username or Password!";
                    response.Success = false;
                    response.Data = null;
                    return response;
                }
                Users _user = new()
                {
                    Password = user.Password,
                    UserName = user.UserName,
                    Token =  GenarateToken()
                };
                response.Message = "Ok";
                response.Success = true;
                response.Data = _user;
            }
            catch (ArgumentException ex)
            {
                response.Message = ex.Message;
                response.Success = false;
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.Message = "Error";
                response.Error = ex.Message;
                response.Success = false;
                response.Data = null;
            }
            return response;
        }
        private string GenarateToken()
        {
            const string secretKey = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTcyMTM1MTM0NywiaWF0IjoxNzIxMzUxMzQ3fQ.5UgVI840kT-U8mxqbH_o8gn9normQHQrl9XgNM8x6qc";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }

}
