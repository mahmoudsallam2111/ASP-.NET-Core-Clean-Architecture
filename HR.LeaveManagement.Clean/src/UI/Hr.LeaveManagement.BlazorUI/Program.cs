using Hr.LeaveManagement.BlazorUI;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Services;
using Hr.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// here i configure the app for the api   ==> add Microsoft.Extensions.Http
builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7028"));

// register the services here

builder.Services.AddScoped<ILeaveTypeService , LeaveTypeService>();  
builder.Services.AddScoped<ILeaveRequestService , LeaveRequestService>();  
builder.Services.AddScoped<ILeaveAllocationService , LeaveAllocationService>();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
