using OnlineSinavSistemi.Core.Models;
using System.Collections.Generic;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Öğrenci işlemleri servis arayüzü
    /// </summary>
    public interface IOgrenciService
    {
        /// <summary>
        /// Tüm öğrencileri listeler
        /// </summary>
        List<Ogrenci> GetAllOgrenciler();
        
        /// <summary>
        /// ID'ye göre öğrenci getirir
        /// </summary>
        Ogrenci GetOgrenciById(int ogrenciId);
        
        /// <summary>
        /// Email'e göre öğrenci getirir
        /// </summary>
        Ogrenci GetOgrenciByEmail(string email);
        
        /// <summary>
        /// Yeni öğrenci oluşturur
        /// </summary>
        void CreateOgrenci(Ogrenci ogrenci);
        
        /// <summary>
        /// Öğrenci günceller
        /// </summary>
        void UpdateOgrenci(Ogrenci ogrenci);
        
        /// <summary>
        /// Öğrenci siler
        /// </summary>
        void DeleteOgrenci(int ogrenciId);
        
        /// <summary>
        /// Belirli bir sınava katılan öğrencileri listeler
        /// </summary>
        List<Ogrenci> GetOgrencilerBySinavId(int sinavId);
    }
} 