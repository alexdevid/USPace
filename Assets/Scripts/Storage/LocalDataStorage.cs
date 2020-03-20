using UnityEngine;

namespace Storage
{
    public class LocalDataStorage : IDataStorage
    {
        public void Store(StorageKey key, int value)
        {
            PlayerPrefs.SetInt(key.ToString(), value);
        }

        public void Store(StorageKey key, bool value)
        {
            PlayerPrefs.SetInt(key.ToString(), value ? 1 : 0);
        }

        public void Store(StorageKey key, float value)
        {
            PlayerPrefs.SetFloat(key.ToString(), value);
        }

        public void Store(StorageKey key, string value)
        {
            PlayerPrefs.SetString(key.ToString(), value);
        }

        public int GetInt(StorageKey key)
        {
            return PlayerPrefs.GetInt(key.ToString());
        }

        public bool GetBool(StorageKey key)
        {
            return PlayerPrefs.GetInt(key.ToString()) == 1;
        }

        public float GetFloat(StorageKey key)
        {
            return PlayerPrefs.GetFloat(key.ToString());
        }

        public string GetString(StorageKey key)
        {
            return PlayerPrefs.GetString(key.ToString());
        }
    }
}