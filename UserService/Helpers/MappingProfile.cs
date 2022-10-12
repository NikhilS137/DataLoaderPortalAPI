using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using UserService.DBContext;
using UserService.Dtos;

namespace UserService.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginMaster, LoginMasterDtos.LoginMaster>();
            CreateMap<LoginMasterDtos.LoginMaster, LoginMaster>();
        }
    }
}
