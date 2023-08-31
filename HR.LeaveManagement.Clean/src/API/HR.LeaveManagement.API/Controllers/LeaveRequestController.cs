using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.ChangeLeaveRequestApprovalCommand;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.leaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.leaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        // GET: api/<VLeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
        {
            return await _mediator.Send(new GetLeaveRequestQuery());
        }

        // GET api/<VLeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        {
            return  await _mediator.Send(new LeaveRequestDetailsQuery() { Id = id}) ;
        }

        // POST api/<VLeaveRequestController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand command)
        {
            var response =await _mediator.Send(command);
            return CreatedAtAction(nameof(Get) ,  new {Id = response});
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("CancelLeaveRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CancelLeaveRequest(CancelLeaveRequestCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("ChangeLeaveRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangeLeaveRequest(ChangeLeaveRequestApprovalCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


        // DELETE api/<VLeaveRequestController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand { Id = id});
            return NoContent();
        }
    }
}
