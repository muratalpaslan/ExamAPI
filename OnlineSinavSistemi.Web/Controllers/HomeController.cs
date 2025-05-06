using Microsoft.AspNetCore.Mvc;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace OnlineSinavSistemi.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocalStorageService _localStorage;

        public HomeController(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Örnek verileri yükler
        /// </summary>
        [HttpPost]
        public IActionResult LoadSampleData()
        {
            try
            {
                // Örnek öğrenciler
                var ogrenciler = new List<Ogrenci>
                {
                    new Ogrenci { Id = 1, Isim = "Ahmet Yılmaz", Email = "ahmet@example.com", Sinif = "10-A", KatildigiSinavlar = new List<int> { 1, 2 } },
                    new Ogrenci { Id = 2, Isim = "Ayşe Demir", Email = "ayse@example.com", Sinif = "10-B", KatildigiSinavlar = new List<int> { 1 } },
                    new Ogrenci { Id = 3, Isim = "Mehmet Kaya", Email = "mehmet@example.com", Sinif = "11-A", KatildigiSinavlar = new List<int> { 2 } }
                };
                
                // Örnek öğretmenler
                var ogretmenler = new List<Ogretmen>
                {
                    new Ogretmen { Id = 1, Isim = "Zeynep Öztürk", Email = "zeynep@example.com", Brans = "Matematik", HazirladigiSinavlar = new List<int> { 1 } },
                    new Ogretmen { Id = 2, Isim = "Ali Can", Email = "ali@example.com", Brans = "Fizik", HazirladigiSinavlar = new List<int> { 2 } }
                };
                
                // Örnek sınavlar
                var sinavlar = new List<Sinav>
                {
                    new Sinav
                    { 
                        SinavId = 1, 
                        Baslik = "Matematik Sınavı", 
                        OlusturulmaTarihi = new DateTime(2023, 10, 15),
                        Sorular = new List<Soru>
                        {
                            new Soru { SoruId = 1, Metin = "2+2=?", Secenekler = new List<string> { "3", "4", "5", "6" }, DogruCevap = "4" },
                            new Soru { SoruId = 2, Metin = "3x3=?", Secenekler = new List<string> { "6", "7", "8", "9" }, DogruCevap = "9" }
                        }
                    },
                    new Sinav 
                    { 
                        SinavId = 2, 
                        Baslik = "Fizik Sınavı", 
                        OlusturulmaTarihi = new DateTime(2023, 10, 20),
                        Sorular = new List<Soru>
                        {
                            new Soru { SoruId = 3, Metin = "Newton'un kaç hareket yasası vardır?", Secenekler = new List<string> { "1", "2", "3", "4" }, DogruCevap = "3" },
                            new Soru { SoruId = 4, Metin = "Işık hızı yaklaşık kaç km/s'dir?", Secenekler = new List<string> { "200,000", "300,000", "400,000", "500,000" }, DogruCevap = "300,000" }
                        }
                    }
                };
                
                // Örnek sonuçlar
                var sonuclar = new List<Sonuc>
                {
                    new Sonuc
                    { 
                        Ogrenci = ogrenciler[0], 
                        SinavId = 1, 
                        DogruSayisi = 2, 
                        YanlisSayisi = 0, 
                        Puan = 100, 
                        TamamlanmaTarihi = new DateTime(2023, 10, 16)
                    },
                    new Sonuc
                    { 
                        Ogrenci = ogrenciler[1], 
                        SinavId = 1, 
                        DogruSayisi = 1, 
                        YanlisSayisi = 1, 
                        Puan = 50, 
                        TamamlanmaTarihi = new DateTime(2023, 10, 16)
                    },
                    new Sonuc
                    { 
                        Ogrenci = ogrenciler[0], 
                        SinavId = 2, 
                        DogruSayisi = 1, 
                        YanlisSayisi = 1, 
                        Puan = 50, 
                        TamamlanmaTarihi = new DateTime(2023, 10, 21)
                    }
                };

                // Veri yükleme
                _localStorage.SetItem("ogrenciler", ogrenciler);
                _localStorage.SetItem("ogretmenler", ogretmenler);
                _localStorage.SetItem("sinavlar", sinavlar);
                _localStorage.SetItem("sonuclar", sonuclar);
                
                return Json(new { success = true, message = "Örnek veriler başarıyla yüklendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Hata oluştu: {ex.Message}" });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
