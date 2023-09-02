using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Hr.LeaveManagement.BlazorUI;
using Hr.LeaveManagement.BlazorUI.Shared;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Contracts;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index
    {

        [Inject]
        public NavigationManager NavigationManager  { get; set; }
        [Inject]
        public ILeaveTypeService LeaveTypeService { get; set; }

        [Inject]
        public  IJSRuntime jSRuntime { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; set; }
        public string Message { get; set; } = string.Empty;

        private async Task ReloadPageAsync()
        {
            await jSRuntime.InvokeVoidAsync("window.location.reload");
        }


        protected override async Task OnInitializedAsync()
        {
           LeaveTypes =  await LeaveTypeService.GetAll();
        }
        public void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");
        }

        public void AllocateLeaveType(int id)
        {

        }

        public void LeaveTypeDetails(int id)
        {

        }

        public void EditLeaveType(int id)
        {

        }

        public async Task DeleteLeaveType(int id)
        {
            var Response = await LeaveTypeService.DeleteLeaveType(id);
            if (Response.Success)
            {
                StateHasChanged();    // to rerender the component
                ReloadPageAsync();
            }
            else
            {
                Message = Response.Message;
            }
        }


    }
}