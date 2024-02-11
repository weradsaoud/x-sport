using Microsoft.EntityFrameworkCore;
using Xsport.API.Authentication;
using Xsport.Core;
using Xsport.Db;

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

builder.Services.AddScoped<IUserServices, UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.MapControllers();
app.Run();
