using HR.LeaveManagement.API.MiddleWare;
using HR.LeaveManagement.Application;
using HR.LeaveManagement.Infrastructure;
using HR.LeaveManagement.Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Add services
builder.Services.AddApplicationService();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);
#endregion

#region Register Serilog
builder.Host.UseSerilog((context, config) => config.WriteTo.Console()
.ReadFrom.Configuration(context.Configuration)) ;
#endregion

#region Cors Configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});
#endregion

///builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region add Custom Middleware

app.UseMiddleware<ExceptionMiddleware>();

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// add serilog middleware
app.UseSerilogRequestLogging();

// add cors middleWare
app.UseCors("all");

app.UseAuthorization();

app.MapControllers();

app.Run();
