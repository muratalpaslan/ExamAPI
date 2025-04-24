using Microsoft.AspNetCore.Mvc;
using OnlineSinavSistemi.Models;
using OnlineSinavSistemi.Interfaces;
using OnlineSinavSistemi.Services;
using System;
using System.Collections.Generic;

namespace OnlineSinavSistemi.Controllers
{
    /// <summary>
    /// Sınav işlemlerini yöneten API controller
    /// </summary>
    [ApiController]
    [Route("api")]
    public class SinavController : ControllerBase
    {
        private readonly ITestEngine _testEngine;

        public SinavController(ITestEngine testEngine)
        {
            _testEngine = testEngine;
        }

        /// <summary>
        /// Sistemdeki mevcut sınavları listeler
        /// </summary>
        [HttpGet("sinav")]
        public ActionResult<List<Sinav>> GetSinavlar()
        {
            return Ok(((TestEngine)_testEngine).GetSinavlar());
        }

        /// <summary>
        /// Öğrencinin sınav cevaplarını alır ve değerlendirir
        /// </summary>
        [HttpPost("cevapla")]
        public ActionResult<Sonuc> CevapGonder([FromBody] SinavCevap cevaplar)
        {
            try
            {
                var sonuc = _testEngine.SinavOl(cevaplar.SinavId, cevaplar);
                return Ok(sonuc);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Tüm sınav sonuçlarını listeler
        /// </summary>
        [HttpGet("sonuc")]
        public ActionResult<List<Sonuc>> GetSonuclar()
        {
            return Ok(((TestEngine)_testEngine).GetSonuclar());
        }
    }
} 