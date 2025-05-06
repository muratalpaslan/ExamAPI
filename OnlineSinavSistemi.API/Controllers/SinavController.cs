using Microsoft.AspNetCore.Mvc;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;
using System;
using System.Collections.Generic;

namespace OnlineSinavSistemi.API.Controllers
{
    /// <summary>
    /// Sınav işlemlerini yöneten API controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SinavController : ControllerBase
    {
        private readonly ISinavService _sinavService;

        public SinavController(ISinavService sinavService)
        {
            _sinavService = sinavService;
        }

        /// <summary>
        /// Sistemdeki mevcut sınavları listeler
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Sinav>> GetAllSinavlar()
        {
            return Ok(_sinavService.GetAllSinavlar());
        }

        /// <summary>
        /// ID'ye göre sınav detaylarını getirir
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Sinav> GetSinavById(int id)
        {
            var sinav = _sinavService.GetSinavById(id);
            if (sinav == null)
                return NotFound();

            return Ok(sinav);
        }

        /// <summary>
        /// Yeni sınav oluşturur
        /// </summary>
        [HttpPost]
        public ActionResult<Sinav> CreateSinav([FromBody] Sinav sinav)
        {
            try
            {
                _sinavService.CreateSinav(sinav);
                return CreatedAtAction(nameof(GetSinavById), new { id = sinav.SinavId }, sinav);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Mevcut bir sınavı günceller
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateSinav(int id, [FromBody] Sinav sinav)
        {
            if (id != sinav.SinavId)
                return BadRequest("ID uyuşmazlığı");

            try
            {
                _sinavService.UpdateSinav(sinav);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Bir sınavı siler
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteSinav(int id)
        {
            try
            {
                _sinavService.DeleteSinav(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 