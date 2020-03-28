using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model;
using UnityEngine;

namespace Data.Storage
{
    public class LocalStorage : IStorage
    {
        private const char ResourceKeyDelimiter = '|';

        public void Delete(StorageObject model)
        {
            Delete(model.StorageIndex);
        }

        public void Delete(int id)
        {
            StorageObject model = Get<StorageObject>(id);
            if (model == null) return;
            
            DeleteResourceKey(model);
            DeleteFields(model);
        }
        
        public bool Has<T>(int id) where T : StorageObject
        {
            StorageObject instance = (T) Activator.CreateInstance(typeof(T), new object[] { });

            return ResourceHasKey(instance.ResourceName, id);
        }
        
        public void Store(StorageObject model)
        {
            FieldInfo[] fields = model.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fields)
            {
                var value = fieldInfo.GetValue(model);
                if (value != null)
                {
                    PlayerPrefs.SetString($"{GetKey(model)}.{fieldInfo.Name}", fieldInfo.GetValue(model).ToString());
                }
            }
            
            SaveResourceKey(model);
        }

        public List<T> GetAll<T>() where T : StorageObject
        {
            List<T> models = new List<T>();
            StorageObject instance = (T) Activator.CreateInstance(typeof(T), new object[] { });

            string[] keys = GetResourceKeys(instance.ResourceName);
            foreach (string key in keys)
            {
                T model = Get<T>(Convert.ToInt32(key));
                if (model == null) continue;
                
                models.Add(model);
            }
            
            return models;
        }
        
        public T Get<T>(int key) where T : StorageObject
        {
            StorageObject model = (T) Activator.CreateInstance(typeof(T), new object[] { });
            if (!ResourceHasKey(model.ResourceName, key)) return null;
            
            model.StorageIndex = key;
            
            FieldInfo[] fields = model.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fields)
            {
                string value = PlayerPrefs.GetString($"{GetKey(model)}.{fieldInfo.Name}");
                FieldInfo field = model.GetType().GetField(fieldInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance);

                Debug.Log(value);
                if (field != null && value != null && value.Length > 0)
                {
                    field.SetValue(model, Convert.ChangeType(value, fieldInfo.FieldType));
                }
            }

            return (T) model;
        }

        private void DeleteFields(StorageObject model)
        {
            FieldInfo[] fields = model.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fields)
            {
                PlayerPrefs.DeleteKey($"{GetKey(model)}.{fieldInfo.Name}");
            }    
        }
        
        private void DeleteResourceKey(StorageObject model)
        {
            string[] keys = GetResourceKeys(model.ResourceName);
            
            Array.Resize(ref keys, keys.Length + 1);
            keys[keys.GetUpperBound(0)] = model.StorageIndex.ToString();
            
            PlayerPrefs.SetString(model.ResourceName, string.Join(ResourceKeyDelimiter.ToString(), keys));
            Debug.Log(string.Join(ResourceKeyDelimiter.ToString(), keys));    
        }
        
        private void SaveResourceKey(StorageObject model)
        {
            string[] keys = GetResourceKeys(model.ResourceName);
            
            Array.Resize(ref keys, keys.Length + 1);
            keys[keys.GetUpperBound(0)] = model.StorageIndex.ToString();
            
            PlayerPrefs.SetString(model.ResourceName, string.Join(ResourceKeyDelimiter.ToString(), keys));
            Debug.Log(string.Join(ResourceKeyDelimiter.ToString(), keys));
        }

        private string[] GetResourceKeys(string resource)
        {
            string keys = PlayerPrefs.GetString(resource);
            
            return keys.Split(ResourceKeyDelimiter);
        }

        private bool ResourceHasKey(string resource, int key)
        {
            return GetResourceKeys(resource).Contains(key.ToString());
        }

        private string GetKey(StorageObject model)
        {
            return GetKey(model.ResourceName, model.StorageIndex);
        }

        private string GetKey(string resource, int key)
        {
            return $"{resource}.{key}";
        }
    }
}