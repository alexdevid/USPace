using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Data.Storage.LocalStorage.Persistence;
using Model;
using UnityEngine;

namespace Data.Storage.LocalStorage
{

    public class LocalStorage : IStorage
    {
        private const char ResourceKeyDelimiter = '|';
        private const string DatabasePrefix = "uspace_db_";

        private readonly Collection _persisted = new Collection();

        public void Persist(object model)
        {
            _persisted.Add(new Entity(model));
        }

        public void Flush()
        {
            _persisted.ForEach(entity =>
            {
                foreach (KeyValuePair<string,string> keyValuePair in entity.Fields)
                {
                    Set(keyValuePair.Key, keyValuePair.Value);
                }
            });
            
            SaveIndexes();
            _persisted.Clear();
        }

        private void SaveIndexes()
        {
            Dictionary<string, List<int>> keys = _persisted.GetKeys();
            
            foreach (KeyValuePair<string,List<int>> keyValuePair in keys)
            {
                int[] indexArray = keyValuePair.Value.ToArray();
                int[] storedIndexes = GetKeysForResource(keyValuePair.Key).ToArray();
                int[] merged = storedIndexes.Concat(indexArray).ToArray();
                
                Set(keyValuePair.Key, string.Join(ResourceKeyDelimiter.ToString(), merged));
            }
        }
        
        private static IEnumerable<int> GetKeysForResource(string resource)
        {
            string keys = Get(resource);
            
            int[] ids = Array.ConvertAll(keys.Split(ResourceKeyDelimiter), int.Parse);
            
            return keys.Length == 0 ? new List<int>() : new List<int>(ids);
        }
        
        private static bool ResourceHasKey(string resource, int key)
        {
            return GetKeysForResource(resource).Contains(key);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }

        public T Get<T>(int index)
        {
            T model = (T) Activator.CreateInstance(typeof(T), new object[] { });
            Entity entity = new Entity(model, index);

            if (!ResourceHasKey(entity.Resource, index)) return default;
            
            Dictionary<string, string> fields = new Dictionary<string, string>();
            foreach (KeyValuePair<string,string> keyValuePair in entity.Fields)
            {
                string[] keyExploded = keyValuePair.Key.Split('.');
                string keyName = keyExploded[keyExploded.Length - 1];
                
                fields[keyName] = Get(keyValuePair.Key);
            }
            
            entity.UpdateModel(fields, model);

            return (T) entity.Model;
        }

        private static void Set(string key, string value)
        {
            PlayerPrefs.SetString(DatabasePrefix + key, value);
        }

        private static string Get(string key)
        {
            return PlayerPrefs.GetString(DatabasePrefix + key);
        }

        // public void Delete<T>(StorageObject model)
        // {
        //     DeleteResourceKey(model);
        //     DeleteFields(model);
        // }
        //
        // public bool Has<T>(T obj)
        // {
        //     StorageObject attribute = obj.GetType().GetCustomAttribute<StorageObject>();
        //     Debug.Log(attribute.IndexField);
        //     Debug.Log(attribute.ResourceName);
        //
        //     // var instance = (T) Activator.CreateInstance(typeof(T), new object[] { });
        //     // Debug.Log(instance);
        //
        //     // return ResourceHasKey(instance.ResourceName, id);
        //     return false;
        // }
        //
        // public List<T> GetAll<T>()
        // {
        //     List<T> models = new List<T>();
        //     var instance = (T) Activator.CreateInstance(typeof(T), new object[] { });
        //
        //     string[] keys = GetResourceKeys("level");
        //     // string[] keys = GetResourceKeys(instance.ResourceName);
        //     if (keys.Length == 0) return models;
        //
        //     foreach (string key in keys)
        //     {
        //         T model = Get<T>(Convert.ToInt32(key));
        //         if (model == null) continue;
        //
        //         models.Add(model);
        //     }
        //
        //     return models;
        // }
        //
        // public T Get<T>(int key)
        // {
        //     var model = (T) Activator.CreateInstance(typeof(T), new object[] { });
        //     // if (!ResourceHasKey("level", key)) return null;
        //
        //     // model.StorageIndex = key;
        //
        //     FieldInfo[] fields = model.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        //     foreach (FieldInfo fieldInfo in fields)
        //     {
        //         StorageField attribute = fieldInfo.GetCustomAttribute<StorageField>();
        //         if (attribute == null) continue;
        //
        //         // string value = PlayerPrefs.GetString($"{GetKey(model)}.{attribute.name ?? fieldInfo.Name}");
        //         // if (!string.IsNullOrEmpty(value)) fieldInfo.SetValue(model, Convert.ChangeType(value, fieldInfo.FieldType));
        //     }
        //
        //     return (T) model;
        // }
        //
        // private void DeleteFields(StorageObject model)
        // {
        //     FieldInfo[] fields = model.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        //     foreach (FieldInfo fieldInfo in fields)
        //     {
        //         StorageField attribute = fieldInfo.GetCustomAttribute<StorageField>();
        //         if (attribute == null) continue;
        //
        //         PlayerPrefs.DeleteKey($"{GetKey(model)}.{attribute.name ?? fieldInfo.Name}");
        //     }
        // }
        //
        // private void DeleteResourceKey(StorageObject model)
        // {
        //     var keys = new List<string>(GetResourceKeys(model.ResourceName));
        //     // keys.Remove(model.StorageIndex.ToString());
        //
        //     PlayerPrefs.SetString(model.ResourceName, string.Join(ResourceKeyDelimiter.ToString(), keys.ToArray()));
        // }
        //
        // private void SaveResourceKey(StorageObject model)
        // {
        //     string[] keys = GetResourceKeys(model.ResourceName);
        //
        //     Array.Resize(ref keys, keys.Length + 1);
        //     // keys[keys.GetUpperBound(0)] = model.StorageIndex.ToString();
        //
        //     PlayerPrefs.SetString(model.ResourceName, string.Join(ResourceKeyDelimiter.ToString(), keys));
        // }
        //
        // private List<string> GetResourceKeys(string resource)
        // {
        //     string keys = PlayerPrefs.GetString(resource);
        //
        //     return keys.Length == 0 ? new List<string>() : keys.Split(ResourceKeyDelimiter);
        // }
        
        // private bool ResourceHasKey(string resource, int key)
        // {
        //     return GetResourceKeys(resource).Contains(key.ToString());
        // }
    }
}