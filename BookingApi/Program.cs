using BookingApi.Middlewares;
using BookingApi.Repositories;
using BookingApi.Repositories.Abstractions;
using BookingApi.Services;
using BookingApi.Services.Abstractions;

namespace BookingApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IHomeRepository, HomeRepository>();
        builder.Services.AddScoped<IHomeService, HomeService>();

        var app = builder.Build();

        // Middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}