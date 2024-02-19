using Microsoft.EntityFrameworkCore;
using AutoWrapper;
using Xsport.API.Authentication;
using Xsport.Core;
using Xsport.Db;
using Asup.Api.AutoWrapperCustomClasses;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xsport.DB.Entities;
using FirebaseAdmin;
using Microsoft.AspNetCore;
using Xsport.DB;

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

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Path.Combine(Directory.GetCurrentDirectory(), "Firebase", "xsports-a951a-firebase-adminsdk-9t65q-203c04501e.json"));
builder.Services.AddSingleton(FirebaseApp.Create());

var app = builder.Build();

//seed database

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        DbInitializer.Seed(context);
    }
    catch (Exception ex)
    {
        // var logger = services.GetRequiredService<ILogger<Program>>();
        // logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
// Configure the HTTP request pipeline.
app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}
    app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
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
