namespace OnlineSinavSistemi.Core.Models
{
    /// <summary>
    /// Temel kullanıcı varlık sınıfı
    /// </summary>
    public abstract class Kullanici
    {
        public int Id { get; set; }
        public string Isim { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
} 