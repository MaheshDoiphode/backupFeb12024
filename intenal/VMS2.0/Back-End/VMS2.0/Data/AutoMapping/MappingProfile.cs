using AutoMapper;
using VMS2._0.DTO;
using VMS2._0.Models;

namespace VMS2._0.Data.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Visit, VisitDTO>().ReverseMap();
            CreateMap<Visitor, VisitorDTO>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();
            CreateMap<SecondaryInfo, SecondaryInfoDTO>().ReverseMap();
            CreateMap<URLManagement, URLManagementDTO>().ReverseMap();
        }
    }
}
