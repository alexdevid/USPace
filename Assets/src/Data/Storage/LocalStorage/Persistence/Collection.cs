using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.LocalStorage.Persistence
{
    public class Collection
    {
        private readonly Dictionary<string, Entity> _collection = new Dictionary<string, Entity>();

        public void Add(Entity entity)
        {
            _collection.Add(entity.Key, entity);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public void ForEach(Action<Entity> callback)
        {
            foreach (KeyValuePair<string,Entity> pair in _collection)
            {
                callback(pair.Value);
            }
        }
        
        
        public Dictionary<string, List<int>> GetKeys()
        {
            Dictionary<string, List<int>> keys = new Dictionary<string, List<int>>();
            
            ForEach(entity =>
            {
                if (keys.ContainsKey(entity.Resource))
                {
                    keys[entity.Resource].Add(entity.Index);
                }
                else
                {
                    keys[entity.Resource] = new List<int> {entity.Index};
                }
            });
        
            return keys;
        }
    }
}