using VMS2._0.DTO;

namespace VMS2._0.Services.IService
{
    public interface IVisitService
    {
        Task<string> InitiateVisitAsync(InitiateVisitDTO initiateVisitDto);
        Task<string> AdminRequestApprovalByAdmin(ApprovalDTO approvalDTO);

        }
    }
