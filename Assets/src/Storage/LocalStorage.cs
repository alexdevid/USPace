using UnityEngine;

namespace Storage
{
    public static class LocalStorage
    {
        public enum Key
        {
            Levels
        }
        
        public static void Store(LocalStorage.Key key, int value)
        {
            PlayerPrefs.SetInt(key.ToString(), value);
        }

        public static void Store(LocalStorage.Key key, bool value)
        {
            PlayerPrefs.SetInt(key.ToString(), value ? 1 : 0);
        }

        public static void Store(LocalStorage.Key key, float value)
        {
            PlayerPrefs.SetFloat(key.ToString(), value);
        }

        public static void Store(LocalStorage.Key key, string value)
        {
            PlayerPrefs.SetString(key.ToString(), value);
        }

        public static int GetInt(LocalStorage.Key key)
        {
            return PlayerPrefs.GetInt(key.ToString());
        }

        public static bool GetBool(LocalStorage.Key key)
        {
            return PlayerPrefs.GetInt(key.ToString()) == 1;
        }

        public static float GetFloat(LocalStorage.Key key)
        {
            return PlayerPrefs.GetFloat(key.ToString());
        }

        public static string GetString(LocalStorage.Key key)
        {
            return PlayerPrefs.GetString(key.ToString());
        }
    }
}