using Microsoft.EntityFrameworkCore;
using AutoWrapper;
using Xsport.API.Authentication;
using Xsport.Core;
using Xsport.Db;
using Asup.Api.AutoWrapperCustomClasses;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xsport.DB.Entities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AWSConnection");

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

//seed database

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        if (!context.Languages.Any())
        {
            context.Languages.AddRange(
                new Language { LanguageId = 1, Code = "en", Name = "English" },
                new Language { LanguageId = 2, Code = "ar", Name = "العربية" }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }

    // Configure the HTTP request pipeline.
    app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
app.UseStaticFiles();
//if (app.Environment.IsDevelopment())
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
