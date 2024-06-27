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
        CreateMap<User, UserDTO>().DisableCtorValidation();
        CreateMap<UserForRegDTO, User>();
        CreateMap<UserForUpdateDTO, User>();
    }
}
