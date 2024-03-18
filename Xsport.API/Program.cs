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
using Microsoft.OpenApi.Models;
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Xsport.Common.Configurations;
using Xsport.Core.EmailServices.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Xsport.Core.EmailServices;
using System.Net;
using Xsport.Core.SportServices;
using Xsport.Core.CommonServices;
using Xsport.Core.AcademyServices;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AWSConnection");
string issuer = builder.Configuration.GetValue<string>("JwtConfig:Issuer") ?? string.Empty;
string signingKey = builder.Configuration.GetValue<string>("JwtConfig:Secret") ?? string.Empty;
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = issuer,
    ValidateAudience = true,
    ValidAudience = issuer,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ClockSkew = System.TimeSpan.Zero,
    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
};
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the BearerAuth scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
    options.HttpsPort = 443; // Default HTTPS port
});

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly("Xsport.API")
                ).EnableSensitiveDataLogging()
            );
builder.Services.AddIdentity<XsportUser, XsportRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers();
//builder.Services.AddAuthentication(options =>
//{
//    options.AddScheme<AuthenticationHandler>("Firebase", "FireBaseAuth");
//    options.DefaultScheme = "Firebase";
//});
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = tokenValidationParameters;
});
builder.Services.AddAuthorization();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.Configure<GeneralConfig>(builder.Configuration.GetSection("GeneralConfig"));

builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISportServices, SportServices>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<ICommonServices, CommonServices>();
builder.Services.AddScoped<IAcademyServices, AcademyServices>();


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
app.UseHsts();
app.UseHttpsRedirection(); // Enable HTTPS redirection
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
