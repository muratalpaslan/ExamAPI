using System.Collections.Generic;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Sınav işlemlerini yürüten motor için arayüz
    /// </summary>
    public interface ITestEngine
    {
        /// <summary>
        /// Öğrencinin sınav olmasını sağlar ve sonuç döndürür
        /// </summary>
        /// <param name="sinavId">Sınav ID</param>
        /// <param name="cevaplar">Öğrenci cevapları</param>
        /// <returns>Sınav sonucu</returns>
        Sonuc SinavOl(int sinavId, SinavCevap cevaplar);
        
        /// <summary>
        /// Mevcut sınavları listeler
        /// </summary>
        /// <returns>Sınav listesi</returns>
        List<Sinav> GetSinavlar();
        
        /// <summary>
        /// Mevcut sonuçları listeler
        /// </summary>
        /// <returns>Sonuç listesi</returns>
        List<Sonuc> GetSonuclar();
    }
} 