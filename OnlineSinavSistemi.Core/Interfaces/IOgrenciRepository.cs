using OnlineSinavSistemi.Core.Models;
using System.Collections.Generic;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Öğrenci repository arayüzü
    /// </summary>
    public interface IOgrenciRepository : IRepository<Ogrenci>
    {
        /// <summary>
        /// Belirli bir sınava katılan öğrencileri getirir
        /// </summary>
        IEnumerable<Ogrenci> GetOgrencilerBySinavId(int sinavId);
        
        /// <summary>
        /// Email'e göre öğrenci bulur
        /// </summary>
        Ogrenci GetByEmail(string email);
    }
} 