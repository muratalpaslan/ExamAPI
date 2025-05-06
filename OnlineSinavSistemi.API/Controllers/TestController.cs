using Microsoft.AspNetCore.Mvc;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;
using System;
using System.Collections.Generic;

namespace OnlineSinavSistemi.API.Controllers
{
    /// <summary>
    /// Sınav cevaplama işlemlerini yöneten API controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestEngine _testEngine;

        public TestController(ITestEngine testEngine)
        {
            _testEngine = testEngine;
        }

        /// <summary>
        /// Sistemdeki mevcut sınavları listeler
        /// </summary>
        [HttpGet("sinavlar")]
        public ActionResult<IEnumerable<Sinav>> GetSinavlar()
        {
            return Ok(_testEngine.GetSinavlar());
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
        [HttpGet("sonuclar")]
        public ActionResult<IEnumerable<Sonuc>> GetSonuclar()
        {
            return Ok(_testEngine.GetSonuclar());
        }
    }
} 