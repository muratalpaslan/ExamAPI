using System;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// UnitOfWork deseni için arayüz
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ISinavRepository Sinavlar { get; }
        IOgrenciRepository Ogrenciler { get; }
        ISonucRepository Sonuclar { get; }
        
        /// <summary>
        /// Yapılan tüm değişiklikleri veritabanına kaydeder
        /// </summary>
        int Complete();
    }
} 