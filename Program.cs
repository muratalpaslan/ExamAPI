using OnlineSinavSistemi.Interfaces;
using OnlineSinavSistemi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Dependency Injection
builder.Services.AddSingleton<ITestEngine, TestEngine>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Sınav Sistemi API V1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();