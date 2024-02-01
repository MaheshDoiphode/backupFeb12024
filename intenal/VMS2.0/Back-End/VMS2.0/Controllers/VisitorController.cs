using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VMS2._0.DTO;

namespace VMS2._0.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorController : ControllerBase
    {
        // Create Visitor
        [HttpPost]
        public async Task<IActionResult> CreateVisitor([FromBody] VisitorDTO visitorDto)
        {
            return Ok();
        }

        // Get Visitor by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitorById(int id)
        {
            return Ok();
        }

        // Update Visitor
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVisitor(int id, [FromBody] VisitorDTO visitorDto)
        {
            return Ok();
        }

        // Delete Visitor
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            return Ok();
        }

        // Get All Visits for a Visitor
        [HttpGet("{id}/visits")]
        public async Task<IActionResult> GetAllVisitsByVisitor(int id)
        {
            return Ok();
        }
    }
}
