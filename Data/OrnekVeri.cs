using OnlineSinavSistemi.Models;

namespace OnlineSinavSistemi.Data
{
    public static class OrnekVeri
    {
        public static List<Sinav> Sinavlar { get; } = new List<Sinav>
        {
            new Sinav
            {
                SinavId = 1,
                Sorular = new List<Soru>
                {
                    new Soru
                    {
                        SoruId = 1,
                        Metin = "Türkiye'nin başkenti neresidir?",
                        Secenekler = new List<string> { "İstanbul", "Ankara", "İzmir", "Bursa" },
                        DogruCevap = "Ankara"
                    },
                    new Soru
                    {
                        SoruId = 2,
                        Metin = "2 + 2 kaç eder?",
                        Secenekler = new List<string> { "3", "4", "5", "6" },
                        DogruCevap = "4"
                    }
                }
            }
        };

        public static List<Ogrenci> Ogrenciler { get; } = new List<Ogrenci>
        {
            new Ogrenci 
            { 
                Id = 1, 
                Isim = "Ali Yılmaz",
                Email = "ali@example.com",
                Sinif = "12-A",
                KatildigiSinavlar = new List<int> { 1 }
            },
            new Ogrenci 
            { 
                Id = 2, 
                Isim = "Ayşe Demir",
                Email = "ayse@example.com",
                Sinif = "11-B",
                KatildigiSinavlar = new List<int>()
            }
        };

        public static List<Ogretmen> Ogretmenler { get; } = new List<Ogretmen>
        {
            new Ogretmen
            {
                Id = 1,
                Isim = "Mehmet Hoca",
                Email = "mehmet@example.com",
                Brans = "Matematik",
                HazirladigiSinavlar = new List<int> { 1 }
            }
        };
    }
} 