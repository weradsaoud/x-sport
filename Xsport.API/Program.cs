using Microsoft.EntityFrameworkCore;
using AutoWrapper;
using Xsport.API.Authentication;
using Xsport.Core;
using Xsport.Db;
using Asup.Api.AutoWrapperCustomClasses;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly("Xsport.API")
                ).EnableSensitiveDataLogging()
            );

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.AddScheme<AuthenticationHandler>("Firebase", "FireBaseAuth");
    options.DefaultScheme = "Firebase";
});
builder.Services.AddAuthorization();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IUserServices, UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseApiResponseAndExceptionWrapper<MapResponseObject>(new AutoWrapperOptions()
{
    IgnoreWrapForOkRequests = false,
    ShowIsErrorFlagForSuccessfulResponse = true,
    ShowStatusCode = true,
    WrapWhenApiPathStartsWith = "/api",
    LogRequestDataOnException = true,
    EnableExceptionLogging = true,
    IsApiOnly = true
});
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
