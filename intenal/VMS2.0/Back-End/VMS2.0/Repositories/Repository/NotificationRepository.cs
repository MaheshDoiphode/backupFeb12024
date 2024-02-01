using AutoMapper;
using VMS2._0.Data;
using VMS2._0.DTO;
using VMS2._0.Models;
using VMS2._0.Repositories.IRepository;

namespace VMS2._0.Repositories.Repository
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly VMSDbContext _context;
        private readonly IMapper _mapper;
        public NotificationRepository(VMSDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddNotificationToTheDB(NotificationDTO notificaitonDTO) 
        {
            var notificationEntity = _mapper.Map<Notification>(notificaitonDTO);
            _context.Add(notificationEntity);
            await _context.SaveChangesAsync();
        }//- AddNotificationToTheDB


    }//- NotificationRepository
}
