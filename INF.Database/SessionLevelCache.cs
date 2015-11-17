using System;
using System.Collections.Generic;

namespace INF.Database
{
    public class SessionLevelCache
    {
        private readonly Dictionary<Type, Dictionary<string, object>> _cache = new Dictionary<Type, Dictionary<string, object>>();

        public object TryToFind(Type type, object id)
        {
            if (!_cache.ContainsKey(type)) return null;

            string idAsString = id.ToString();
            if (!_cache[type].ContainsKey(idAsString)) return null;

            return _cache[type][idAsString];
        }

        public void Store(Type type, object id, object entity)
        {
            if (!_cache.ContainsKey(type)) _cache.Add(type, new Dictionary<string, object>());

            _cache[type][id.ToString()] = entity;
        }

        public void ClearAll()
        {
            _cache.Clear();
        }

        public void RemoveAllInstancesOf(Type type)
        {
            if (_cache.ContainsKey(type))
            {
                _cache.Remove(type);
            }
        }

        public void Remove(object entity)
        {
            var type = entity.GetType();

            if (!_cache.ContainsKey(type)) return;

            string keyToRemove = null;

            foreach (var pair in _cache[type])
            {
                if (pair.Value == entity)
                {
                    keyToRemove = pair.Key;
                }
            }

            if (keyToRemove != null)
            {
                _cache[type].Remove(keyToRemove);
            }
        }
    }
}