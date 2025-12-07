using System;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly LeadService _service;
        public LeadsController(LeadService service) => _service = service;

        [HttpGet("invited")]
        public async Task<IActionResult> GetInvited()
        {
            var res = await _service.GetNewLeadsAsync();
            return Ok(res);
        }

        [HttpGet("accepted")]
        public async Task<IActionResult> GetAccepted()
        {
            var res = await _service.GetAcceptedLeadsAsync();
            return Ok(res);
        }

        [HttpPost("{id}/accept")]
        public async Task<IActionResult> Accept(Guid id)
        {
            await _service.AcceptLeadAsync(id);
            return Ok(new { message = "Lead aceito com sucesso", id });
        }

        [HttpPost("{id}/decline")]
        public async Task<IActionResult> Decline(Guid id)
        {
            await _service.DeclineLeadAsync(id);
            return Ok(new { message = "Lead recusado com sucesso", id });
        }
    }
}
