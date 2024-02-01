using VMS2._0.DTO;
using VMS2._0.Models;
using VMS2._0.Repositories.IRepository;
using VMS2._0.Services.IService;
using System.Threading.Tasks;
using AutoMapper;

namespace VMS2._0.Services.Service
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;
        private readonly VisitorService _visitorService;

        public VisitService(IVisitRepository visitRepository, INotificationService notificationService, IMapper mapper, INotificationRepository notificationRepository, VisitorService visitorService)
        {
            _visitRepository = visitRepository;
            _notificationService = notificationService;
            _mapper = mapper;
            _notificationRepository = notificationRepository;
            _visitorService = visitorService;
        }//- Constructor


        // Step 1 -  Host initiated the visit request.
        public async Task<string> InitiateVisitAsync(InitiateVisitDTO initiateVisitDto)
        {
            var hostdetails = await _visitRepository.GetHostDetails(initiateVisitDto.UserID);
            var visitorDTO = await _visitRepository.GetVisitorDetails(initiateVisitDto);
            var visitID = GenerateGUID();
            var parentVisitID = visitID;
           /* // We have to handle the multi days visit
            TimeSpan visitDuration = initiateVisitDto.ExpectedDepart - initiateVisitDto.ExpectedArrival;
            if (visitDuration.Days > 1 && DateTime.Today.Date != initiateVisitDto.ExpectedArrival.Date) {
                parentVisitID = GenerateGUID();
            }*/

            var visitDto = new VisitDTO
            {
                VisitID = visitID,
                ParentVisitID = visitID,
                VisitorID = visitorDTO.VisitorID,
                HostName = hostdetails.UserName,
                HostEmail = hostdetails.UserEmail,
                Purpose = initiateVisitDto.Purpose,
                ExpectedArrival = initiateVisitDto.ExpectedArrival,
                ExpectedDepart = initiateVisitDto.ExpectedDepart,
                RequestStatus = "Pending",
                VisitStatus = "Under Review"
            };
            var visitEntity = _mapper.Map<Visit>(visitDto);
            await _visitRepository.AddAndSaveAsync(visitEntity);

            // Hard coded stuff - 
            var admin_mail = "je.f.freyee.llis.3.49@gmail.com";
            var subject_admin = $"{hostdetails.UserName} wants approval to invite {initiateVisitDto.VisitorName}";
            // TO the admin
            await _notificationService.HostSendVisitNotificationToAdminForApprovalAsync(admin_mail,subject_admin, initiateVisitDto.message, visitorDTO);
            
            await _notificationRepository.AddNotificationToTheDB(new NotificationDTO { 
                NotificationID = GenerateGUID(),
                VisitID = visitDto.VisitID,
                Message = initiateVisitDto.message,
                NotificationGenerated = DateTime.Now,
                NotificationType = "Approval Request",
                NotificationStatus = "OK",
            });
            // TO the Host
            var message_host = $"Hello {hostdetails.UserName}, \n Your request has been successfully sent to the admin for inviting {initiateVisitDto.VisitorName} has be";
            await _notificationService.HostSendVisitNotificationToAdminForApprovalAsync(hostdetails.UserEmail, $"Request for approval for {initiateVisitDto.VisitorName} sent to the admin.", message_host, visitorDTO);
            await _notificationRepository.AddNotificationToTheDB(new NotificationDTO {
                NotificationID = GenerateGUID(),
                VisitID = visitDto.VisitID,
                Message = initiateVisitDto.message,
                NotificationGenerated = DateTime.Now,
                NotificationType = "Approval Request",
                NotificationStatus= "OK",
            });
            return visitorDTO.VisitorID;
        }//- InitiateVisitAsync



        // --------------------------------------------------------------------

        // Step 2 - Admin Approval or rejection request.

        public async Task<string> AdminRequestApprovalByAdmin(ApprovalDTO approvalDTO) {
            var visitDetails = await _visitRepository.GetVisitDetailsByID(approvalDTO.visitID);
            var visitorDetails = await _visitRepository.GetVisitorDetailsByVisitID(approvalDTO.visitID);
            // Rejection Part ----------------------
            if (approvalDTO.decision.ToLower().CompareTo("reject") == 0) {
                // We need visit details by the visit id.
                visitDetails.RequestStatus = "Rejected";
                visitDetails.VisitStatus = "Rejected";
                await _visitRepository.UpdateAndSaveAsync(_mapper.Map<Visit>(visitDetails));
                // adding the method to the notification service to send mail
                var subject = $"Update on the request for your invite {visitorDetails.VisitorName}";
                var message = $"Hello {visitDetails.HostName},<br><br>"
                            + $"I hope this email finds you well. We regret to inform you that your request to invite {visitorDetails.VisitorName} has been rejected due to the following reason/s:<br><br>"
                            + $"{approvalDTO.reason}<br><br>"
                            + "Regards,<br>"
                            + "NCS Admin team";
                await _notificationService.SendApprovalOrRejectionMailToHostOrVisitor(visitDetails.HostEmail, subject, message, visitDetails);
                var notification = new NotificationDTO
                {
                    VisitID = approvalDTO.visitID,
                    Message = $"Your request to invite {visitorDetails.VisitorName} has been rejected.",
                    NotificationType = "Rejected",
                    NotificationGenerated = DateTime.Now,
                    NotificationStatus = "Sent"
                };

                await _notificationRepository.AddNotificationToTheDB(notification);
                return "Rejected";
            }//- if end


            // Approval part ---------------------------
            await _visitorService.ApprovedRequestByAdmin(visitDetails, visitorDetails);
            
            return "Approved";
        }//- AdminRequestApproval






        // Helper Methods 
        private string GenerateGUID() {
            Guid guidPart = Guid.NewGuid();
            string randomPart = new Random().Next(0, 10000).ToString("D4");
            string timestampPart = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"{guidPart}{timestampPart}{randomPart}";
        }//- GenerateGUID

    }//- Service end
}
