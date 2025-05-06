/**
 * LocalStorage servisini client tarafında yönetmek için yardımcı sınıf
 */
const LocalStorageManager = {
    /**
     * Verileri localStorage'dan getirir
     * @param {string} key - Veri anahtarı
     * @returns {object|null} - JSON parse edilmiş veri veya null
     */
    getItem: function(key) {
        const item = localStorage.getItem(key);
        if (!item) return null;
        
        try {
            return JSON.parse(item);
        } catch (e) {
            console.error('LocalStorage parse hatası:', e);
            return null;
        }
    },
    
    /**
     * Verileri localStorage'a kaydeder
     * @param {string} key - Veri anahtarı
     * @param {object} value - Kaydedilecek veri
     */
    setItem: function(key, value) {
        try {
            const serializedValue = JSON.stringify(value);
            localStorage.setItem(key, serializedValue);
        } catch (e) {
            console.error('LocalStorage kayıt hatası:', e);
        }
    },
    
    /**
     * Belirtilen anahtarı localStorage'dan kaldırır
     * @param {string} key - Silinecek veri anahtarı
     */
    removeItem: function(key) {
        localStorage.removeItem(key);
    },
    
    /**
     * LocalStorage'ı tamamen temizler
     */
    clear: function() {
        localStorage.clear();
    },
    
    /**
     * LocalStorage'da belirtilen anahtar var mı kontrol eder
     * @param {string} key - Kontrol edilecek anahtar
     * @returns {boolean} - Anahtarın var olup olmadığı
     */
    containsKey: function(key) {
        return localStorage.getItem(key) !== null;
    },
    
    /**
     * Örnek verileri localStorage'a yükler (geliştirme amaçlı)
     */
    loadSampleData: function() {
        // Örnek öğrenciler
        const ogrenciler = [
            { id: 1, isim: "Ahmet Yılmaz", email: "ahmet@example.com", sinif: "10-A", katildigiSinavlar: [1, 2] },
            { id: 2, isim: "Ayşe Demir", email: "ayse@example.com", sinif: "10-B", katildigiSinavlar: [1] },
            { id: 3, isim: "Mehmet Kaya", email: "mehmet@example.com", sinif: "11-A", katildigiSinavlar: [2] }
        ];
        
        // Örnek öğretmenler
        const ogretmenler = [
            { id: 1, isim: "Zeynep Öztürk", email: "zeynep@example.com", brans: "Matematik", hazirladigiSinavlar: [1] },
            { id: 2, isim: "Ali Can", email: "ali@example.com", brans: "Fizik", hazirladigiSinavlar: [2] }
        ];
        
        // Örnek sınavlar
        const sinavlar = [
            { 
                sinavId: 1, 
                baslik: "Matematik Sınavı", 
                olusturulmaTarihi: new Date(2023, 9, 15),
                sorular: [
                    { soruId: 1, metin: "2+2=?", secenekler: ["3", "4", "5", "6"], dogruCevap: "4" },
                    { soruId: 2, metin: "3x3=?", secenekler: ["6", "7", "8", "9"], dogruCevap: "9" }
                ]
            },
            { 
                sinavId: 2, 
                baslik: "Fizik Sınavı", 
                olusturulmaTarihi: new Date(2023, 9, 20),
                sorular: [
                    { soruId: 3, metin: "Newton'un kaç hareket yasası vardır?", secenekler: ["1", "2", "3", "4"], dogruCevap: "3" },
                    { soruId: 4, metin: "Işık hızı yaklaşık kaç km/s'dir?", secenekler: ["200,000", "300,000", "400,000", "500,000"], dogruCevap: "300,000" }
                ]
            }
        ];
        
        // Örnek sonuçlar
        const sonuclar = [
            { 
                ogrenci: ogrenciler[0], 
                sinavId: 1, 
                dogruSayisi: 2, 
                yanlisSayisi: 0, 
                puan: 100, 
                tamamlanmaTarihi: new Date(2023, 9, 16)
            },
            { 
                ogrenci: ogrenciler[1], 
                sinavId: 1, 
                dogruSayisi: 1, 
                yanlisSayisi: 1, 
                puan: 50, 
                tamamlanmaTarihi: new Date(2023, 9, 16)
            },
            { 
                ogrenci: ogrenciler[0], 
                sinavId: 2, 
                dogruSayisi: 1, 
                yanlisSayisi: 1, 
                puan: 50, 
                tamamlanmaTarihi: new Date(2023, 9, 21)
            }
        ];
        
        this.setItem("ogrenciler", ogrenciler);
        this.setItem("ogretmenler", ogretmenler);
        this.setItem("sinavlar", sinavlar);
        this.setItem("sonuclar", sonuclar);
        
        console.log("Örnek veriler yüklendi!");
    }
}; 