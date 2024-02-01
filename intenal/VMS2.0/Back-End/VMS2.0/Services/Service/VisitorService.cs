using AutoMapper;
using System.Runtime.CompilerServices;
using VMS2._0.DTO;
using VMS2._0.Models;
using VMS2._0.Repositories.IRepository;
using VMS2._0.Services.IService;

namespace VMS2._0.Services.Service
{
    public class VisitorService : IVisitorService
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IMapper _mapper;
        public VisitorService(INotificationService notificationService, INotificationRepository notificationRepository, IMapper mapper, IVisitRepository visitRepository) {
            _notificationService = notificationService;
            _notificationRepository = _notificationRepository;
            _visitRepository = visitRepository;
            _mapper = mapper;
        }
        public async Task ApprovedRequestByAdmin(VisitDTO visitDetails, VisitorDTO visitorDetails) 
        {
            visitDetails.RequestStatus = "Approved";
            visitDetails.VisitStatus = "Scheduled";
            await _visitRepository.UpdateAndSaveAsync(_mapper.Map<Visit>(visitDetails));

            // Send notification email to the host about the approval
            var subjectApproved = $"Approval for your invite to {visitorDetails.VisitorName}";
            var messageApproved = $"Hello {visitDetails.HostName},<br><br>"
                        + $"Good news! Your request to invite {visitorDetails.VisitorName} has been approved.<br><br>"
                        + "Regards,<br>"
                        + "NCS Admin team";
            await _notificationService.SendApprovalOrRejectionMailToHostOrVisitor(visitDetails.HostEmail, subjectApproved, messageApproved, visitDetails);

            // Add host notification to the database
            var notificationApprovedForHost = new NotificationDTO
            {
                VisitID = visitDetails.VisitID,
                Message = $"Your request to invite {visitorDetails.VisitorName} has been approved.",
                NotificationType = "Approved",
                NotificationGenerated = DateTime.Now,
                NotificationStatus = "Sent"
            };
            await _notificationRepository.AddNotificationToTheDB(notificationApprovedForHost);

            // Send notification email to the visitor about the approval
            var subjectVisitor = $"Your visit to NCS has been approved!";
            var messageVisitor = $"Hello {visitorDetails.VisitorName},<br><br>"
                        + $"We're pleased to inform you that your visit to NCS has been approved by {visitDetails.HostName}.<br><br>"
                        + "Regards,<br>"
                        + "NCS Admin team";
            await _notificationService.SendApprovalOrRejectionMailToHostOrVisitor(visitorDetails.VisitorEmail, subjectVisitor, messageVisitor, visitDetails);

            // Add visitor notification to the database
            var notificationApprovedForVisitor = new NotificationDTO
            {
                VisitID = visitDetails.VisitID,
                Message = $"Your visit to NCS, hosted by {visitDetails.HostName}, has been approved.",
                NotificationType = "Approved",
                NotificationGenerated = DateTime.Now,
                NotificationStatus = "Sent"
            };
            await _notificationRepository.AddNotificationToTheDB(notificationApprovedForVisitor);



            // -------------- URL part 
            // Generate a unique URL for the user
           




        }

    }
}
