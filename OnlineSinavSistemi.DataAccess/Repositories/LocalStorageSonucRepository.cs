using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.DataAccess.Repositories
{
    /// <summary>
    /// Sonuç repository'sinin LocalStorage implementasyonu
    /// </summary>
    public class LocalStorageSonucRepository : ISonucRepository
    {
        private readonly ILocalStorageService _localStorage;
        private const string SONUCLAR_KEY = "sonuclar";

        public LocalStorageSonucRepository(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            // İlk kullanımda boş liste oluştur
            if (!_localStorage.ContainsKey(SONUCLAR_KEY))
            {
                _localStorage.SetItem(SONUCLAR_KEY, new List<Sonuc>());
            }
        }

        public void Add(Sonuc entity)
        {
            var sonuclar = _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
            sonuclar.Add(entity);
            _localStorage.SetItem(SONUCLAR_KEY, sonuclar);
        }

        public void AddRange(IEnumerable<Sonuc> entities)
        {
            var sonuclar = _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
            sonuclar.AddRange(entities);
            _localStorage.SetItem(SONUCLAR_KEY, sonuclar);
        }

        public IEnumerable<Sonuc> Find(Expression<Func<Sonuc, bool>> expression)
        {
            var sonuclar = _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
            return sonuclar.AsQueryable().Where(expression.Compile()).ToList();
        }

        public IEnumerable<Sonuc> GetAll()
        {
            return _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
        }

        public Sonuc GetById(int id)
        {
            // Sonuç sınıfının bir ID özelliği olmadığından, böyle bir metod kullanılamaz
            // Gerçek bir uygulama için Sonuc sınıfına bir ID alanı eklenmelidir
            throw new NotImplementedException("Sonuc sınıfına bir ID alanı eklenmeli");
        }

        public IEnumerable<Sonuc> GetSonuclarByOgrenciId(int ogrenciId)
        {
            var sonuclar = _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
            return sonuclar.Where(s => s.Ogrenci.Id == ogrenciId).ToList();
        }

        public IEnumerable<Sonuc> GetSonuclarBySinavId(int sinavId)
        {
            var sonuclar = _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
            return sonuclar.Where(s => s.SinavId == sinavId).ToList();
        }

        public void Remove(Sonuc entity)
        {
            var sonuclar = _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
            // Sonuç için doğrudan eşitlik kontrolü olmadığından, öğrenci ve sınav ID'leri ile eşleşenleri kaldıralım
            sonuclar.RemoveAll(s => s.Ogrenci.Id == entity.Ogrenci.Id && s.SinavId == entity.SinavId);
            _localStorage.SetItem(SONUCLAR_KEY, sonuclar);
        }

        public void RemoveRange(IEnumerable<Sonuc> entities)
        {
            // Sonuçları tek tek kaldırıyoruz
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        public void Update(Sonuc entity)
        {
            var sonuclar = _localStorage.GetItem<List<Sonuc>>(SONUCLAR_KEY);
            // Öğrenci ve sınav ID'lerine göre sonuç bul
            var index = sonuclar.FindIndex(s => s.Ogrenci.Id == entity.Ogrenci.Id && s.SinavId == entity.SinavId);
            
            if (index >= 0)
            {
                sonuclar[index] = entity;
                _localStorage.SetItem(SONUCLAR_KEY, sonuclar);
            }
        }
    }
} 