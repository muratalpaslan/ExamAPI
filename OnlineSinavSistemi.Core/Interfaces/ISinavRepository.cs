using OnlineSinavSistemi.Core.Models;
using System.Collections.Generic;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Sınav repository arayüzü
    /// </summary>
    public interface ISinavRepository : IRepository<Sinav>
    {
        /// <summary>
        /// Belirli bir öğretmenin hazırladığı sınavları getirir
        /// </summary>
        IEnumerable<Sinav> GetSinavlarByOgretmenId(int ogretmenId);
    }
} 