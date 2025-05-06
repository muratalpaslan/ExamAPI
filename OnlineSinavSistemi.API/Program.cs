using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.DataAccess;
using OnlineSinavSistemi.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Online Sınav Sistemi API",
        Version = "v1",
        Description = "Online sınav sistemi için RESTful API"
    });
});

// LocalStorage Service
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

// Register Repositories and Services with LocalStorage
builder.Services.AddScoped<IUnitOfWork, LocalStorageUnitOfWork>();
builder.Services.AddScoped<ITestEngine, TestEngineService>();
builder.Services.AddScoped<ISinavService, SinavService>();
builder.Services.AddScoped<IOgrenciService, OgrenciService>();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Sınav Sistemi API V1");
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
