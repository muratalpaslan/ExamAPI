// Bu dosya sadece örnek amaçlıdır ve N-Tier mimariye geçiş sürecinde referans olarak eklenmiştir.
// Migration komutları:

// Initial Migration:
// dotnet ef migrations add InitialCreate --project OnlineSinavSistemi.DataAccess --startup-project OnlineSinavSistemi.API

// Update Database:
// dotnet ef database update --project OnlineSinavSistemi.DataAccess --startup-project OnlineSinavSistemi.API

// Diğer Migration İşlemleri:
// dotnet ef migrations list --project OnlineSinavSistemi.DataAccess --startup-project OnlineSinavSistemi.API
// dotnet ef migrations remove --project OnlineSinavSistemi.DataAccess --startup-project OnlineSinavSistemi.API

// Proje Çalıştırma:
// dotnet run --project OnlineSinavSistemi.API 