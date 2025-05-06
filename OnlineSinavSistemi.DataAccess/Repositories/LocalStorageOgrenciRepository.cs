using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.DataAccess.Repositories
{
    /// <summary>
    /// Öğrenci repository'sinin LocalStorage implementasyonu
    /// </summary>
    public class LocalStorageOgrenciRepository : IOgrenciRepository
    {
        private readonly ILocalStorageService _localStorage;
        private const string OGRENCILER_KEY = "ogrenciler";

        public LocalStorageOgrenciRepository(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            // İlk kullanımda boş liste oluştur
            if (!_localStorage.ContainsKey(OGRENCILER_KEY))
            {
                _localStorage.SetItem(OGRENCILER_KEY, new List<Ogrenci>());
            }
        }

        public void Add(Ogrenci entity)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            
            // Yeni ID oluştur
            if (entity.Id <= 0)
            {
                entity.Id = ogrenciler.Any() ? ogrenciler.Max(s => s.Id) + 1 : 1;
            }
            
            ogrenciler.Add(entity);
            _localStorage.SetItem(OGRENCILER_KEY, ogrenciler);
        }

        public void AddRange(IEnumerable<Ogrenci> entities)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            
            foreach (var entity in entities)
            {
                // Yeni ID oluştur
                if (entity.Id <= 0)
                {
                    entity.Id = ogrenciler.Any() ? ogrenciler.Max(s => s.Id) + 1 : 1;
                }
            }
            
            ogrenciler.AddRange(entities);
            _localStorage.SetItem(OGRENCILER_KEY, ogrenciler);
        }

        public IEnumerable<Ogrenci> Find(Expression<Func<Ogrenci, bool>> expression)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            return ogrenciler.AsQueryable().Where(expression.Compile()).ToList();
        }

        public IEnumerable<Ogrenci> GetAll()
        {
            return _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
        }

        public Ogrenci GetById(int id)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            return ogrenciler.FirstOrDefault(s => s.Id == id);
        }

        public Ogrenci GetByEmail(string email)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            return ogrenciler.FirstOrDefault(o => o.Email == email);
        }

        public IEnumerable<Ogrenci> GetOgrencilerBySinavId(int sinavId)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            return ogrenciler.Where(o => o.KatildigiSinavlar.Contains(sinavId)).ToList();
        }

        public void Remove(Ogrenci entity)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            ogrenciler.RemoveAll(s => s.Id == entity.Id);
            _localStorage.SetItem(OGRENCILER_KEY, ogrenciler);
        }

        public void RemoveRange(IEnumerable<Ogrenci> entities)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            var idsToRemove = entities.Select(e => e.Id).ToList();
            ogrenciler.RemoveAll(s => idsToRemove.Contains(s.Id));
            _localStorage.SetItem(OGRENCILER_KEY, ogrenciler);
        }

        public void Update(Ogrenci entity)
        {
            var ogrenciler = _localStorage.GetItem<List<Ogrenci>>(OGRENCILER_KEY);
            var index = ogrenciler.FindIndex(s => s.Id == entity.Id);
            
            if (index >= 0)
            {
                ogrenciler[index] = entity;
                _localStorage.SetItem(OGRENCILER_KEY, ogrenciler);
            }
        }
    }
} 