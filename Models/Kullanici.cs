namespace OnlineSinavSistemi.Models
{
    public abstract class Kullanici
    {
        public int Id { get; set; }
        public required string Isim { get; set; }
        public required string Email { get; set; }
        public DateTime KayitTarihi { get; set; }

        protected Kullanici()
        {
            KayitTarihi = DateTime.Now;
        }
    }
} 