using VMS2._0.DTO;

namespace VMS2._0.Repositories.IRepository
{
    public interface INotificationRepository
    {
        Task AddNotificationToTheDB(NotificationDTO notificaitonDTO);
    }
}
