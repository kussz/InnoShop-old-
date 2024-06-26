using AutoMapper;
using UMS.Shared.DataTransferObjects;
using UMS.Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Cryptography;
using System.Text;

namespace UMS;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Role, RoleDTO>().DisableCtorValidation();
        CreateMap<User, UserDTO>().DisableCtorValidation();
        CreateMap<RoleForPostDTO, Role>();
        CreateMap<RoleForUpdateDTO, Role>();
        CreateMap<UserForPostDTO, User>().ForMember(dest => dest.PasswordHash, src => src.MapFrom(x => Encoding.Default.GetString( SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(x.Password)))));
        CreateMap<UserForUpdateDTO, User>().ForMember(dest => dest.PasswordHash, src => src.MapFrom(x => Encoding.Default.GetString( SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(x.Password))))).ReverseMap();

    }
}
