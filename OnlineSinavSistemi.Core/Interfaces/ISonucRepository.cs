using OnlineSinavSistemi.Core.Models;
using System.Collections.Generic;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Sınav sonuçları repository arayüzü
    /// </summary>
    public interface ISonucRepository : IRepository<Sonuc>
    {
        /// <summary>
        /// Belirli bir öğrencinin sonuçlarını getirir
        /// </summary>
        IEnumerable<Sonuc> GetSonuclarByOgrenciId(int ogrenciId);
        
        /// <summary>
        /// Belirli bir sınavın sonuçlarını getirir
        /// </summary>
        IEnumerable<Sonuc> GetSonuclarBySinavId(int sinavId);
    }
} 