using AutoMapper;
using testNewAPI.DTOS;
using testNewAPI.Models;

namespace testNewAPI.Config
{
    public class DtoMapping:Profile
    {
        public DtoMapping()
        {
            CreateMap<ClsStudent, StudentDto>().ReverseMap();
        }
    }
}
