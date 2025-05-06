using Microsoft.AspNetCore.Mvc;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace OnlineSinavSistemi.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestEngine _testEngine;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public TestController(ITestEngine testEngine, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _testEngine = testEngine;
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        // GET: Test/Sinavlar
        public async Task<IActionResult> Sinavlar()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/test/sinavlar");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sinavlar = JsonSerializer.Deserialize<IEnumerable<Sinav>>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(sinavlar);
            }
            
            return View(new List<Sinav>());
        }

        // GET: Test/SinavOl/5
        public async Task<IActionResult> SinavOl(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sinav = JsonSerializer.Deserialize<Sinav>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                // Sınav cevap modelini oluştur
                var cevapModel = new SinavCevap
                {
                    SinavId = sinav.SinavId,
                    Cevaplar = new List<Cevap>()
                };
                
                return View(new Tuple<Sinav, SinavCevap>(sinav, cevapModel));
            }
            
            return NotFound();
        }

        // POST: Test/SinavOl/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SinavOl(int id, SinavCevap sinavCevap)
        {
            if (ModelState.IsValid)
            {
                // Öğrenci varsa mevcut ID'yi al, yoksa yeni öğrenci oluştur
                if (!string.IsNullOrEmpty(sinavCevap.OgrenciIsim) && !string.IsNullOrEmpty(sinavCevap.OgrenciEmail))
                {
                    // Öğrencilerin listesini al
                    var ogrenciResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/ogrenci");
                    if (ogrenciResponse.IsSuccessStatusCode)
                    {
                        var ogrenciContent = await ogrenciResponse.Content.ReadAsStringAsync();
                        var ogrenciler = JsonSerializer.Deserialize<List<Ogrenci>>(ogrenciContent, 
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            
                        // Öğrenci e-postasına göre kontrol et
                        var mevcutOgrenci = ogrenciler?.FirstOrDefault(o => o.Email.ToLower() == sinavCevap.OgrenciEmail.ToLower());
                        
                        if (mevcutOgrenci != null)
                        {
                            sinavCevap.OgrenciId = mevcutOgrenci.Id;
                        }
                        else
                        {
                            // Yeni öğrenci oluştur
                            var yeniOgrenci = new Ogrenci
                            {
                                Isim = sinavCevap.OgrenciIsim,
                                Email = sinavCevap.OgrenciEmail,
                                Sinif = "Belirtilmedi"
                            };
                            
                            var ogrenciJson = JsonSerializer.Serialize(yeniOgrenci);
                            var ogrenciRequestContent = new StringContent(ogrenciJson, Encoding.UTF8, "application/json");
                            var yeniOgrenciResponse = await _httpClient.PostAsync($"{_apiBaseUrl}/api/ogrenci", ogrenciRequestContent);
                            
                            if (yeniOgrenciResponse.IsSuccessStatusCode)
                            {
                                var yeniOgrenciContent = await yeniOgrenciResponse.Content.ReadAsStringAsync();
                                var eklenenOgrenci = JsonSerializer.Deserialize<Ogrenci>(yeniOgrenciContent, 
                                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                                
                                sinavCevap.OgrenciId = eklenenOgrenci?.Id ?? 0;
                            }
                        }
                    }
                }
                
                // Sınav ID'sini ayarla
                sinavCevap.SinavId = id;
                
                // API'ye cevapları gönder
                var json = JsonSerializer.Serialize(sinavCevap);
                var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/test/cevapla", requestContent);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var sonuc = JsonSerializer.Deserialize<Sonuc>(responseContent, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    return RedirectToAction(nameof(SinavSonuc), new { id = sonuc.SinavId });
                }
                else
                {
                    // API isteği başarısız olduğunda hata mesajını göster
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Sınav tamamlanamadı: {errorContent}");
                }
            }
            
            // Hata durumunda sınavı tekrar yükle
            var sinavResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav/{id}");
            if (sinavResponse.IsSuccessStatusCode)
            {
                var sinavContent = await sinavResponse.Content.ReadAsStringAsync();
                var sinav = JsonSerializer.Deserialize<Sinav>(sinavContent, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                return View(new Tuple<Sinav, SinavCevap>(sinav, sinavCevap));
            }
            
            return NotFound();
        }

        // GET: Test/SinavSonuc/5
        public async Task<IActionResult> SinavSonuc(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/test/sonuclar");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sonuclar = JsonSerializer.Deserialize<IEnumerable<Sonuc>>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                var sinavSonuclari = sonuclar?.Where(s => s.SinavId == id).ToList();
                return View(sinavSonuclari ?? new List<Sonuc>());
            }
            
            return View(new List<Sonuc>());
        }
    }
} 