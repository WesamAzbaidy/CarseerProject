using carseer.Mapping;
using carseer.Repositories.VehicleRepository;
using carseer.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Carseer API",
        Version = "v1"
    });
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallback(async context =>
{
    context.Response.StatusCode = 404;
    context.Response.ContentType = "application/json";
    var response = new
    {
        Success = false,
        Message = "Endpoint not found.",
        Data = (object)null,
        StatusCode = 404
    };
    await context.Response.WriteAsJsonAsync(response);
});

app.Run();
