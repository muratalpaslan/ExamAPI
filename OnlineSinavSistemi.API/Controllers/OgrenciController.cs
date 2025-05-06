using Microsoft.AspNetCore.Mvc;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;
using System;
using System.Collections.Generic;

namespace OnlineSinavSistemi.API.Controllers
{
    /// <summary>
    /// Öğrenci işlemlerini yöneten API controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OgrenciController : ControllerBase
    {
        private readonly IOgrenciService _ogrenciService;

        public OgrenciController(IOgrenciService ogrenciService)
        {
            _ogrenciService = ogrenciService;
        }

        /// <summary>
        /// Tüm öğrencileri listeler
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Ogrenci>> GetAllOgrenciler()
        {
            return Ok(_ogrenciService.GetAllOgrenciler());
        }

        /// <summary>
        /// ID'ye göre öğrenci detaylarını getirir
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Ogrenci> GetOgrenciById(int id)
        {
            var ogrenci = _ogrenciService.GetOgrenciById(id);
            if (ogrenci == null)
                return NotFound();

            return Ok(ogrenci);
        }

        /// <summary>
        /// Email'e göre öğrenciyi bulur
        /// </summary>
        [HttpGet("email/{email}")]
        public ActionResult<Ogrenci> GetOgrenciByEmail(string email)
        {
            var ogrenci = _ogrenciService.GetOgrenciByEmail(email);
            if (ogrenci == null)
                return NotFound();

            return Ok(ogrenci);
        }

        /// <summary>
        /// Yeni öğrenci oluşturur
        /// </summary>
        [HttpPost]
        public ActionResult<Ogrenci> CreateOgrenci([FromBody] Ogrenci ogrenci)
        {
            try
            {
                _ogrenciService.CreateOgrenci(ogrenci);
                return CreatedAtAction(nameof(GetOgrenciById), new { id = ogrenci.Id }, ogrenci);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Mevcut bir öğrenciyi günceller
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateOgrenci(int id, [FromBody] Ogrenci ogrenci)
        {
            if (id != ogrenci.Id)
                return BadRequest("ID uyuşmazlığı");

            try
            {
                _ogrenciService.UpdateOgrenci(ogrenci);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Bir öğrenciyi siler
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteOgrenci(int id)
        {
            try
            {
                _ogrenciService.DeleteOgrenci(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Belirli bir sınava katılan öğrencileri listeler
        /// </summary>
        [HttpGet("sinav/{sinavId}")]
        public ActionResult<IEnumerable<Ogrenci>> GetOgrencilerBySinavId(int sinavId)
        {
            return Ok(_ogrenciService.GetOgrencilerBySinavId(sinavId));
        }
    }
} 