using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VMS2._0.DTO;
using VMS2._0.Models;
using VMS2._0.Services.IService;

namespace VMS2._0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        
        [HttpPost("/newrequest")]
        public async Task<IActionResult> InitiateVisit([FromBody] InitiateVisitDTO initiateVisitDto)
        {
            var visitorId = await _visitService.InitiateVisitAsync(initiateVisitDto);
            return Ok(new { status = "success", VisitorID = visitorId });

        }//- InitiateVisit




        /* Next Task
         * 1. Done --------- Update the Visit service and notification repository to add the notifications to the DB 
          2. Done --------------  New API - Appove or reject the request - 
                   a. done - If rejected send the update to the host. 
                   b. If accepted Send update to host and visitor

         */
        [HttpPut("/approval")]
        public async Task<IActionResult> RequestApprovalByAdmin([FromBody] ApprovalDTO approvalDTO)
        {
            // call the service first with the name - AdminRequestApproval(ApprovalDTO approvalDTO)
            await _visitService.AdminRequestApprovalByAdmin(approvalDTO);
            return Ok(new { status = "success"});
        }











        // Update Visit Status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateVisitStatus(int id, [FromBody] string visitStatus)
        {
            return Ok();
        }

        // Get All Visits for a Host
        [HttpGet("host/{hostEmail}")]
        public async Task<IActionResult> GetAllVisitsByHost(string hostEmail)
        {
            return Ok();
        }

        // Delete a Visit
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            return Ok();
        }

        // Get Visit by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitById(int id)
        {
            return Ok();
        }
    }
}
