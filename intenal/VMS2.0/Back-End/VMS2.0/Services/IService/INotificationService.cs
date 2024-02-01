using VMS2._0.DTO;

namespace VMS2._0.Services.IService
{
    public interface INotificationService
    {
        Task HostSendVisitNotificationToAdminForApprovalAsync<T>(string email, string subject, string message, T entity) where T : class;
        Task SendApprovalOrRejectionMailToHostOrVisitor<T>(string email, string subject, string message, T entity) where T : class;

    }
}
