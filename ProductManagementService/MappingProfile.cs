using AutoMapper;
using PMS.Shared.DataTransferObjects;
using PMS.Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Cryptography;
using System.Text;

namespace UMS;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDTO>().DisableCtorValidation();
        CreateMap<ProductForPostDTO, Product>();
        CreateMap<ProductForUpdateDTO, Product>();
    }
}
