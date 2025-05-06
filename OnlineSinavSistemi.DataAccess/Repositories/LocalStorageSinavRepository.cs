using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.DataAccess.Repositories
{
    /// <summary>
    /// Sınav repository'sinin LocalStorage implementasyonu
    /// </summary>
    public class LocalStorageSinavRepository : ISinavRepository
    {
        private readonly ILocalStorageService _localStorage;
        private const string SINAVLAR_KEY = "sinavlar";

        public LocalStorageSinavRepository(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            // İlk kullanımda boş liste oluştur
            if (!_localStorage.ContainsKey(SINAVLAR_KEY))
            {
                _localStorage.SetItem(SINAVLAR_KEY, new List<Sinav>());
            }
        }

        public void Add(Sinav entity)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            
            // Yeni ID oluştur
            if (entity.SinavId <= 0)
            {
                entity.SinavId = sinavlar.Any() ? sinavlar.Max(s => s.SinavId) + 1 : 1;
            }
            
            sinavlar.Add(entity);
            _localStorage.SetItem(SINAVLAR_KEY, sinavlar);
        }

        public void AddRange(IEnumerable<Sinav> entities)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            
            foreach (var entity in entities)
            {
                // Yeni ID oluştur
                if (entity.SinavId <= 0)
                {
                    entity.SinavId = sinavlar.Any() ? sinavlar.Max(s => s.SinavId) + 1 : 1;
                }
            }
            
            sinavlar.AddRange(entities);
            _localStorage.SetItem(SINAVLAR_KEY, sinavlar);
        }

        public IEnumerable<Sinav> Find(Expression<Func<Sinav, bool>> expression)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            return sinavlar.AsQueryable().Where(expression.Compile()).ToList();
        }

        public IEnumerable<Sinav> GetAll()
        {
            return _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
        }

        public Sinav GetById(int id)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            return sinavlar.FirstOrDefault(s => s.SinavId == id);
        }

        public IEnumerable<Sinav> GetSinavlarByOgretmenId(int ogretmenId)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            var ogretmenler = _localStorage.GetItem<List<Ogretmen>>("ogretmenler") ?? new List<Ogretmen>();
            
            var ogretmen = ogretmenler.FirstOrDefault(o => o.Id == ogretmenId);
            if (ogretmen == null) return new List<Sinav>();
            
            return sinavlar.Where(s => ogretmen.HazirladigiSinavlar.Contains(s.SinavId)).ToList();
        }

        public void Remove(Sinav entity)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            sinavlar.RemoveAll(s => s.SinavId == entity.SinavId);
            _localStorage.SetItem(SINAVLAR_KEY, sinavlar);
        }

        public void RemoveRange(IEnumerable<Sinav> entities)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            var idsToRemove = entities.Select(e => e.SinavId).ToList();
            sinavlar.RemoveAll(s => idsToRemove.Contains(s.SinavId));
            _localStorage.SetItem(SINAVLAR_KEY, sinavlar);
        }

        public void Update(Sinav entity)
        {
            var sinavlar = _localStorage.GetItem<List<Sinav>>(SINAVLAR_KEY);
            var index = sinavlar.FindIndex(s => s.SinavId == entity.SinavId);
            
            if (index >= 0)
            {
                sinavlar[index] = entity;
                _localStorage.SetItem(SINAVLAR_KEY, sinavlar);
            }
        }
    }
} 