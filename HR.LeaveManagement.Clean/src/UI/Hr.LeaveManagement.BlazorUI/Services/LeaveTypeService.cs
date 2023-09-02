using AutoMapper;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper mapper;

        public LeaveTypeService(IClient client, IMapper mapper) : base(client)
        {
            this.mapper = mapper;
        }

        public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypeVM)
        {
            try
            {
                var leaveTypeCommand = mapper.Map<CreateLeaveTypeCommand>(leaveTypeVM);
                await _client.LeaveTypesPOSTAsync(leaveTypeCommand);

                return new Response<Guid>
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(id);

                return new Response<Guid>
                {
                    Success = true,
                };

            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<List<LeaveTypeVM>> GetAll()
        {
            // get leavetypes from serviceClient
            var leaveTypes = await _client.LeaveTypesAllAsync();
            // convert the leavetypedto to leavetypeVM
            return mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            var leaveType = await _client.LeaveTypesGETAsync(id);

            return mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<Response<Guid>> UpdateLeaveType( LeaveTypeVM leaveTypeVM)
        {
            try
            {
                var leaveTypeCommand = mapper.Map<UpdateLeaveTypeCommand>(leaveTypeVM);
                await _client.LeaveTypesPUTAsync(leaveTypeCommand);

                return new Response<Guid>
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
