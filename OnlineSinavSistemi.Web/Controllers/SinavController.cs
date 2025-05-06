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

namespace OnlineSinavSistemi.Web.Controllers
{
    public class SinavController : Controller
    {
        private readonly ISinavService _sinavService;
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public SinavController(ISinavService sinavService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _sinavService = sinavService;
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
        }

        // GET: Sinav
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sinavlar = JsonSerializer.Deserialize<IEnumerable<Sinav>>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(sinavlar);
            }
            
            return View(new List<Sinav>());
        }

        // GET: Sinav/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sinav = JsonSerializer.Deserialize<Sinav>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(sinav);
            }
            
            return NotFound();
        }

        // GET: Sinav/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sinav/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sinav sinav)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(sinav);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/sinav", content);
                
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(sinav);
        }

        // GET: Sinav/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sinav = JsonSerializer.Deserialize<Sinav>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(sinav);
            }
            
            return NotFound();
        }

        // POST: Sinav/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sinav sinav)
        {
            if (id != sinav.SinavId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonSerializer.Serialize(sinav);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = await _httpClient.PutAsync($"{_apiBaseUrl}/api/sinav/{id}", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    if (!SinavExists(sinav.SinavId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(sinav);
        }

        // GET: Sinav/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sinav = JsonSerializer.Deserialize<Sinav>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(sinav);
            }
            
            return NotFound();
        }

        // POST: Sinav/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/sinav/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return NotFound();
        }

        private bool SinavExists(int id)
        {
            var response = _httpClient.GetAsync($"{_apiBaseUrl}/api/sinav/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
} 