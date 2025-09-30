using AutoMapper;
using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;

namespace Company.Pro.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();
            //CreateMap<CreateDepartmentDto, Department>().ReverseMap();
        }
    }
}
