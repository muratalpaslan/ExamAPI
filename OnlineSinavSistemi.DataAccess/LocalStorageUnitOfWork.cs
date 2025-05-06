using System;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.DataAccess.Repositories;

namespace OnlineSinavSistemi.DataAccess
{
    /// <summary>
    /// UnitOfWork deseni için LocalStorage implementasyonu
    /// </summary>
    public class LocalStorageUnitOfWork : IUnitOfWork
    {
        private readonly ILocalStorageService _localStorage;
        
        private ISinavRepository _sinavRepository;
        private IOgrenciRepository _ogrenciRepository;
        private ISonucRepository _sonucRepository;
        
        public LocalStorageUnitOfWork(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public ISinavRepository Sinavlar => _sinavRepository ??= new LocalStorageSinavRepository(_localStorage);

        public IOgrenciRepository Ogrenciler => _ogrenciRepository ??= new LocalStorageOgrenciRepository(_localStorage);
        
        public ISonucRepository Sonuclar => _sonucRepository ??= new LocalStorageSonucRepository(_localStorage);

        public int Complete()
        {
            // LocalStorage zaten güncellendi, gerçek bir transaction olmadığından bir şey yapmamız gerekmiyor
            return 1; // Başarılı değişiklik sayısı
        }

        public void Dispose()
        {
            // LocalStorage durumunda bir kaynak temizliği gerekmez
        }
    }
} 