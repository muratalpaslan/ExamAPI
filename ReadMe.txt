Bu uygulama, öğrencilerin çevrimiçi sınavlara katılabildiği, öğretmenlerin sınav oluşturabildiği ve sonuçları takip edebildiği bir online sınav sistemidir.


Uygulamanın Ana Özellikleri:

Sınav Yönetimi: Öğretmenler sınav oluşturabilir, düzenleyebilir ve silebilir
Öğrenci Yönetimi: Sisteme öğrenciler eklenebilir, silinebilir ve güncellenebilir
Sınav Çözme: Öğrenciler sınava katılabilir ve cevaplarını gönderebilir
Sonuç Görüntüleme: Sınav sonuçları ve istatistikleri görüntülenebilir
Nasıl Kullanılır:
Uygulamaya Erişim:
Web arayüzü: http://localhost:5009 adresinden erişilebilir
API: http://localhost:5194 adresinden erişilebilir


Örnek Verileri Yükleme:

Ana sayfada "Örnek Verileri Yükle" butonuna tıklayarak test verileri yükleyebilirsiniz
Bu işlem, örnek öğrenciler, öğretmenler, sınavlar ve sonuçlar oluşturur
Menü Kullanımı:
Ana Sayfa: Genel bilgiler ve hızlı erişim bağlantıları
Sınavlar: Mevcut sınavların listesi, yeni sınav oluşturma, düzenleme ve silme işlemleri
Öğrenciler: Öğrenci listesi, yeni öğrenci ekleme, düzenleme ve silme işlemleri
Sınav Ol: Öğrencilerin sınava katılabileceği alan


Sınav Çözme:

"Sınav Ol" menüsünden bir sınav seçin
Soruları cevaplayın ve "Gönder" butonuna tıklayın
Sonuçlarınızı görebilirsiniz
Sonuç Görüntüleme:
Öğrenci profilinden kendi sınav sonuçlarınızı görebilirsiniz
Sınav detaylarından tüm sonuçları görüntüleyebilirsiniz


Teknik Özellikler:

Uygulama N-Tier mimarisiyle geliştirilmiştir (Core, DataAccess, Business, API ve Web katmanları)
Veritabanı yerine basitlik için LocalStorage kullanılmaktadır (tarayıcıda ve sunucu tarafında in-memory depolama)
SOLID prensiplerine uygun olarak tasarlanmıştır
API ve Web uygulaması ayrı projeler olarak çalışmaktadır

API ve Web uygulamasını farklı portlarda başlattığınızda, API istekleri Web uygulaması tarafından doğru şekilde yapılabilmesi için OnlineSinavSistemi.Web/appsettings.json dosyasındaki ApiSettings:BaseUrl değerini http://localhost:5194 olarak ayarlamanız gerekmektedir.