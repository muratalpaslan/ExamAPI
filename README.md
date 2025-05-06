# Online Sınav Sistemi (N-Tier Mimari ile localStorage)

Bu proje, online sınav sisteminin N-Tier (katmanlı) mimari ile yeniden yapılandırılmış ve veritabanı yerine localStorage kullanacak şekilde basitleştirilmiş halidir.

## Proje Yapısı

Proje, SOLID prensiplerine uygun şekilde aşağıdaki katmanlardan oluşmaktadır:

- **Core Katmanı**: Tüm projelerde kullanılan ortak model ve arayüzleri içerir.
- **DataAccess Katmanı**: Veri erişim işlemlerini LocalStorage üzerinden yürütür, Repository ve UnitOfWork desenlerini kullanır.
- **Business Katmanı**: İş mantığı ve servislerin bulunduğu katmandır.
- **API Katmanı**: RESTful API uç noktalarını sağlar.
- **Web Katmanı**: MVC yapısı ile kullanıcı arayüzünü sağlar.

## Teknolojiler

- .NET Core 9.0
- ASP.NET Core MVC
- ASP.NET Core Web API
- LocalStorage / JavaScript

## Kurulum

1. Projeyi klonlayın:
   ```
   git clone https://github.com/your-username/OnlineSinavSistemi.git
   ```

2. Proje dizinine gidin:
   ```
   cd OnlineSinavSistemi
   ```

3. Gerekli paketleri yükleyin:
   ```
   dotnet restore
   ```

4. Projeyi çalıştırın:
   ```
   dotnet run --project OnlineSinavSistemi.API
   ```

   Başka bir terminal penceresi açın ve Web projesini de çalıştırın:
   ```
   dotnet run --project OnlineSinavSistemi.Web
   ```

5. Web tarayıcısında uygulamaya erişin ve "Örnek Verileri Yükle" butonuna tıklayarak test verilerini oluşturun.

## Veri Depolama

Bu projede SQL Server veya başka bir veritabanı yerine basitlik için localStorage kullanılmaktadır:

- **Sunucu tarafında**: In-memory veri depolama için statik `Dictionary` kullanılmaktadır.
- **İstemci tarafında**: Tarayıcının localStorage API'si kullanılmaktadır.

Bu sayede:
- Veritabanı kurulumuna gerek yoktur
- Deployment süreci basitleştirilmiştir
- Geliştirme ortamında hızlı test yapılabilir

> **Not:** Gerçek bir üretim ortamında, kalıcı veri depolama için SQL Server, PostgreSQL gibi bir veritabanı kullanılması önerilir.

## API Uç Noktaları

### Sınav İşlemleri

- `GET /api/sinav` - Tüm sınavları listeler
- `GET /api/sinav/{id}` - Belirtilen ID'ye sahip sınavı getirir
- `POST /api/sinav` - Yeni sınav ekler
- `PUT /api/sinav/{id}` - Belirtilen sınavı günceller
- `DELETE /api/sinav/{id}` - Belirtilen sınavı siler

### Test İşlemleri

- `GET /api/test/sinavlar` - Mevcut sınavları listeler
- `POST /api/test/cevapla` - Sınav cevaplarını gönderir ve sonucu döndürür
- `GET /api/test/sonuclar` - Tüm sınav sonuçlarını listeler

### Öğrenci İşlemleri

- `GET /api/ogrenci` - Tüm öğrencileri listeler
- `GET /api/ogrenci/{id}` - Belirtilen ID'ye sahip öğrenciyi getirir
- `GET /api/ogrenci/email/{email}` - Belirtilen email'e sahip öğrenciyi getirir
- `POST /api/ogrenci` - Yeni öğrenci ekler
- `PUT /api/ogrenci/{id}` - Belirtilen öğrenciyi günceller
- `DELETE /api/ogrenci/{id}` - Belirtilen öğrenciyi siler
- `GET /api/ogrenci/sinav/{sinavId}` - Belirli bir sınava katılan öğrencileri listeler

## Mimari

### Repository ve UnitOfWork Deseni

Projede, veri işlemlerini yönetmek için Repository ve UnitOfWork desenleri kullanılmıştır:

- **Repository Deseni**: Her entity için CRUD işlemlerini gerçekleştirir.
- **UnitOfWork Deseni**: Tüm repository'leri yöneten merkezi bir yapı sunar.

### Dependency Injection

Bağımlılıklar, Constructor Injection yöntemi ile yönetilmiştir. Tüm bağımlılıklar, Program.cs dosyasında kayıt edilmiştir.

### SOLID Prensipleri

- **Tek Sorumluluk Prensibi (SRP)**: Her sınıf tek bir sorumluluğa sahiptir.
- **Açık/Kapalı Prensibi (OCP)**: Sınıflar genişletilmeye açık, değiştirilmeye kapalıdır.
- **Liskov Yerine Geçme Prensibi (LSP)**: Alt sınıflar, üst sınıfların yerini alabilir.
- **Arayüz Ayrımı Prensibi (ISP)**: İstemcilerin kullanmadıkları arayüzlere bağımlı olmaları önlenmiştir.
- **Bağımlılık Tersine Çevirme Prensibi (DIP)**: Yüksek seviyeli modüller, düşük seviyeli modüllere bağlı değildir. Her ikisi de soyutlamalara bağlıdır.

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylı bilgi için [LICENSE](LICENSE) dosyasına bakabilirsiniz. 