using Microsoft.AspNetCore.Mvc;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace OnlineSinavSistemi.Web.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly IOgrenciService _ogrenciService;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<OgrenciController> _logger;

        public OgrenciController(IOgrenciService ogrenciService, IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<OgrenciController> logger)
        {
            _ogrenciService = ogrenciService;
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl") ?? throw new ArgumentNullException(nameof(configuration), "API Base URL is not configured");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _logger = logger;
        }

        // GET: Ogrenci
        public async Task<IActionResult> Index()
        {
            try
            {
                // Tüm sınavları getir
                var sinavlarResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav");
                if (sinavlarResponse.IsSuccessStatusCode)
                {
                    var sinavlarJson = await sinavlarResponse.Content.ReadAsStringAsync();
                    var sinavlar = JsonSerializer.Deserialize<List<Sinav>>(sinavlarJson, _jsonOptions);
                    
                    if (sinavlar != null)
                    {
                        // Sınav ID'lerini ve başlıklarını bir sözlükte sakla
                        ViewBag.Sinavlar = sinavlar.ToDictionary(
                            s => s.SinavId,
                            s => s.Baslik.Split(' ')[0] // Sınav başlığının ilk kelimesini al
                        );
                    }
                }

                // Öğrencileri getir
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/ogrenci");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var ogrenciler = JsonSerializer.Deserialize<IEnumerable<Ogrenci>>(json, _jsonOptions);
                    return View(ogrenciler ?? new List<Ogrenci>());
                }

                return View(new List<Ogrenci>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Öğrenci listesi alınırken hata oluştu");
                return View(new List<Ogrenci>());
            }
        }

        // GET: Ogrenci/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/ogrenci/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ogrenci = JsonSerializer.Deserialize<Ogrenci>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(ogrenci);
            }
            
            return NotFound();
        }

        // GET: Ogrenci/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ogrenci/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(ogrenci);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/ogrenci", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(ogrenci);
        }

        // GET: Ogrenci/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/ogrenci/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ogrenci = JsonSerializer.Deserialize<Ogrenci>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(ogrenci);
            }
            
            return NotFound();
        }

        // POST: Ogrenci/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogrenci ogrenci)
        {
            if (id != ogrenci.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonSerializer.Serialize(ogrenci);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = await _httpClient.PutAsync($"{_apiBaseUrl}/api/ogrenci/{id}", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    if (!OgrenciExists(ogrenci.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(ogrenci);
        }

        // GET: Ogrenci/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/ogrenci/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ogrenci = JsonSerializer.Deserialize<Ogrenci>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(ogrenci);
            }
            
            return NotFound();
        }

        // POST: Ogrenci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/ogrenci/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return NotFound();
        }

        // GET: Ogrenci/SinavSonuclari/5
        public async Task<IActionResult> SinavSonuclari(int id)
        {
            // Önce öğrenciyi alıyoruz
            var ogrenciResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/ogrenci/{id}");
            if (!ogrenciResponse.IsSuccessStatusCode)
            {
                return NotFound("Öğrenci bulunamadı.");
            }
            
            var ogrenciContent = await ogrenciResponse.Content.ReadAsStringAsync();
            var ogrenci = JsonSerializer.Deserialize<Ogrenci>(ogrenciContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            // Tüm sınavları alıyoruz
            var sinavResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav");
            var sinavlar = new List<Sinav>();
            if (sinavResponse.IsSuccessStatusCode)
            {
                var sinavContent = await sinavResponse.Content.ReadAsStringAsync();
                sinavlar = JsonSerializer.Deserialize<List<Sinav>>(sinavContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            
            // Tüm sonuçları alıyoruz
            var sonucResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/test/sonuclar");
            
            if (sonucResponse.IsSuccessStatusCode)
            {
                var sonucContent = await sonucResponse.Content.ReadAsStringAsync();
                var tumSonuclar = JsonSerializer.Deserialize<List<Sonuc>>(sonucContent, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                // Öğrencinin sınav sonuçlarını filtreliyoruz
                var ogrenciSonuclari = tumSonuclar
                    .Where(s => s.OgrenciId == id)
                    .ToList();

                // Öğrenci ve sınav bilgilerini her sonuca ekle
                foreach (var sonuc in ogrenciSonuclari)
                {
                    sonuc.Ogrenci = ogrenci;
                    var sinav = sinavlar.FirstOrDefault(s => s.SinavId == sonuc.SinavId);
                    if (sinav != null)
                    {
                        sonuc.SinavBaslik = sinav.Baslik;
                    }
                }
                
                ViewBag.Ogrenci = ogrenci;
                return View(ogrenciSonuclari);
            }
            
            ViewBag.Ogrenci = ogrenci;
            return View(new List<Sonuc>());
        }

        private bool OgrenciExists(int id)
        {
            var response = _httpClient.GetAsync($"{_apiBaseUrl}/api/ogrenci/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
} 