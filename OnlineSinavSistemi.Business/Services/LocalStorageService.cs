using System;
using System.Collections.Generic;
using System.Text.Json;
using OnlineSinavSistemi.Core.Interfaces;
using OnlineSinavSistemi.Core.Models;

namespace OnlineSinavSistemi.Business.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        // In-memory storage for server-side testing and development
        private static Dictionary<string, string> _storage = new Dictionary<string, string>();

        public T GetItem<T>(string key)
        {
            if (_storage.TryGetValue(key, out string value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public void SetItem<T>(string key, T value)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            _storage[key] = serializedValue;
        }

        public void RemoveItem(string key)
        {
            if (_storage.ContainsKey(key))
            {
                _storage.Remove(key);
            }
        }

        public bool ContainsKey(string key)
        {
            return _storage.ContainsKey(key);
        }

        public void Clear()
        {
            _storage.Clear();
        }
    }
} 