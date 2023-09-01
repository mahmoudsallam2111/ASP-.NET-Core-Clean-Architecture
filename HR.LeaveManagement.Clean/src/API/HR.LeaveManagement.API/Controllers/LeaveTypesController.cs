using HR.LeaveManagement.Application.Features.leaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.leaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.leaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.leaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<List<LeaveTypeDto>> Get()
        {
            var LeaveTypesList = await _mediator.Send(new GetLeaveTypeQuery());
            return LeaveTypesList;
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
        {
            var LeaveTypeResult = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
            
            return Ok(LeaveTypeResult);
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(CreateLeaveTypeCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new {id = response});
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(UpdateLeaveTypeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand() { Id = id};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
