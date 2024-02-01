using VMS2._0.DTO;

namespace VMS2._0.Services.IService
{
    public interface IVisitorService
    {
        Task ApprovedRequestByAdmin(VisitDTO visitDetails, VisitorDTO visitorDetails);

    }
}
