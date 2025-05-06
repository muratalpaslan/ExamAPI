using OnlineSinavSistemi.Core.Models;
using System.Collections.Generic;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Sınav işlemleri servis arayüzü
    /// </summary>
    public interface ISinavService
    {
        /// <summary>
        /// Tüm sınavları listeler
        /// </summary>
        List<Sinav> GetAllSinavlar();
        
        /// <summary>
        /// ID'ye göre sınav getirir
        /// </summary>
        Sinav GetSinavById(int sinavId);
        
        /// <summary>
        /// Yeni sınav oluşturur
        /// </summary>
        void CreateSinav(Sinav sinav);
        
        /// <summary>
        /// Sınav günceller
        /// </summary>
        void UpdateSinav(Sinav sinav);
        
        /// <summary>
        /// Sınav siler
        /// </summary>
        void DeleteSinav(int sinavId);
    }
} 