namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Basit veri depolama işlemleri için arayüz
    /// </summary>
    public interface ILocalStorageService
    {
        /// <summary>
        /// Belirtilen anahtara göre verileri getirir
        /// </summary>
        T GetItem<T>(string key);
        
        /// <summary>
        /// Belirtilen anahtarı ve değeri depolar
        /// </summary>
        void SetItem<T>(string key, T value);
        
        /// <summary>
        /// Belirtilen anahtarı kaldırır
        /// </summary>
        void RemoveItem(string key);
        
        /// <summary>
        /// Belirtilen anahtarın var olup olmadığını kontrol eder
        /// </summary>
        bool ContainsKey(string key);
        
        /// <summary>
        /// Tüm depolanmış verileri temizler
        /// </summary>
        void Clear();
    }
} 