using LibraryManagmentAPI.Common.Configurations;
using LibraryManagmentAPI.Common.IServices;
using LibraryManagmentAPI.Common.MappingHelper;
using LibraryManagmentAPI.Common.Services;
using LibraryManagmentAPI.Domain.Entities;
using LibraryManagmentAPI.Infrastructure.Data;
using LibraryManagmentAPI.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(path: "G:\\3Pillars\\LibraryManagmentAPI\\logs\\log-.txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information).CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(option =>
option.SerializerSettings.ReferenceLoopHandling =
Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddAutoMapper(typeof(MapperInit));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICheckoutService, ChrckoutService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddCors(a =>
{
    a.AddPolicy("allowAll", newBuilder =>
    newBuilder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Host.UseSerilog();

try
{
    Log.Information("Application is starting");
    var app = builder.Build();
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("allowAll");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception)
{
    Log.Fatal("Application is failed to start");
}
finally
{
    Log.CloseAndFlush();
}


