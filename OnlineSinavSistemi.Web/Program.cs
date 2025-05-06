using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Business.Services;
using OnlineSinavSistemi.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Register HttpClient
builder.Services.AddHttpClient();

// LocalStorage Service
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

// Register Repositories and Services with LocalStorage
builder.Services.AddScoped<IUnitOfWork, LocalStorageUnitOfWork>();
builder.Services.AddScoped<ITestEngine, TestEngineService>();
builder.Services.AddScoped<ISinavService, SinavService>();
builder.Services.AddScoped<IOgrenciService, OgrenciService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
