using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMS2._0.DTO;
using VMS2._0.Services.IService;

namespace VMS2._0.Testing
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {

/*        private readonly TestingService _service;
*/        private readonly INotificationService _notificationservice;


        public TestingController( INotificationService notificationservice)
        {
/*            _service = Service;
*/            _notificationservice = notificationservice;
        }


        [HttpPost]
        public async Task<IActionResult> SendEmail()
        {
           // await _notificationservice.SendEmailAsync("rojira9823@recutv.com", "Testing Subject", "Testing MEssage");
            return Ok("Mail sent!!");
        }//- 

        [HttpPost("/HTML")]
        public async Task<IActionResult> SendEmailHTML()
        {
            // Create a VisitDTO object with sample data
            VisitDTO visit = new VisitDTO
            {
                VisitID = "SampleVisitID123",
                ParentVisitID = "SampleParentVisitID456",
                VisitorID = "SampleVisitorID789",
                HostName = "SampleHostName",
                HostEmail = "samplehost@email.com",
                Purpose = "SamplePurpose",
                ExpectedArrival = DateTime.Now.AddHours(1), // For example, 1 hour from now
                ExpectedDepart = DateTime.Now.AddHours(2), // For example, 2 hours from now
                CheckIn = DateTime.Now, // Current time
                CheckOut = null, // Not checked out yet
                RequestStatus = "SampleRequestStatus",
                Feedback = "SampleFeedback",
                VisitStatus = "SampleVisitStatus"
            };


    await _notificationservice.HostSendVisitNotificationToAdminForApprovalAsync("rojira9823@recutv.com", "Testing Subject", "Testing message", visit);
            return Ok("Mail sent!!");
        }



    }
}
