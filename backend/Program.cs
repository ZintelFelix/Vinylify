using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Vinylify.Backend.Data;
using Vinylify.Backend.Models;
using Vinylify.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) CORS für den Vue-Client
builder.Services.AddCors(o => o.AddPolicy("AllowVueClient", p =>
    p.WithOrigins("http://localhost:5173")
     .AllowAnyHeader()
     .AllowAnyMethod()));

// 2) EF Core DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlite("Data Source=vinylify.db"));

// 3) Discogs-Settings aus appsettings.json binden
builder.Services.Configure<DiscogsSettings>(
    builder.Configuration.GetSection("Discogs"));

// 4) Services registrieren
builder.Services.AddScoped<IDiscogsAuthService, DiscogsAuthService>();
builder.Services.AddScoped<IDiscogsClientService, DiscogsClientService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IDiscogsSearchService, DiscogsSearchService>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IDiscogsReleaseService, DiscogsReleaseService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();

// 5) MVC & Swagger/OpenAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "Vinylify API",
        Version     = "v1",
        Description = "Backend mit Discogs-Integration, Collection, Wishlist & Release-Details"
    });
});

var app = builder.Build();

// 6) Swagger UI im Development-Modus
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vinylify API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowVueClient");
app.UseAuthorization();
app.MapControllers();

app.Run();
