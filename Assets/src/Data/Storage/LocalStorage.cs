using System;
using Model;
using UnityEngine;

namespace Data.Storage
{
    public class LocalStorage : IStorage
    {
        public void Store(StorageObject model)
        {
            model.Serialize();
            PlayerPrefs.SetString(GetKey(model.ResourceName, model.StorageIndex), model.GetString());
        }

        public StorageObject Get<T>(int key) where T : StorageObject
        {
            StorageObject model = (T) Activator.CreateInstance(typeof(T), new object[] { });

            return model;
        }

        private string Load(string resource, int key)
        {
            return PlayerPrefs.GetString(GetKey(resource, key));
        }

        private string GetKey(string resource, int key)
        {
            return $"{resource}.{key}";
        }
    }
}