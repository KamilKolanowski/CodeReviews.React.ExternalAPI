using ImageFetcherAPI.Data;
using ImageFetcherAPI.Models;
using ImageFetcherAPI.Repositories;
using ImageFetcherAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ImageFetcherAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ImageFetcherDbContext>(options =>
            options.UseSqlite(connectionString)
        );
        
        builder.Configuration.AddUserSecrets<Program>();
        builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

        builder.Services.AddControllers();
        builder.Services.AddScoped<ICatsRepository, CatsRepository>();
        builder.Services.AddScoped<ICatsApi, CatsApi>();
        builder.Services.AddScoped<CatSyncService>();
        builder.Services.AddScoped<ExternalCatsApi>();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
            });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ImageFetcherDbContext>();
            db.Database.EnsureCreated();
        }

        using (var scope = app.Services.CreateScope())
        {
            var syncService = scope.ServiceProvider.GetRequiredService<CatSyncService>();
            await syncService.SyncCatsAsync();
        }

        app.UseRouting();
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
