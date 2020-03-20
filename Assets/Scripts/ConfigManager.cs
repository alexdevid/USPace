using UnityEngine;

public static class ConfigManager
{
    public static bool GetBool(ConfigName key)
    {
        return PlayerPrefs.GetInt(key.ToString()) == 1;
    }
    public static int GetInt(ConfigName key)
    {
        return PlayerPrefs.GetInt(key.ToString());
    }
    
    public static void Store(ConfigName key, int value)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
    }

    public static void Store(ConfigName key, bool value)
    {
        PlayerPrefs.SetInt(key.ToString(), value ? 1 : 0);
    }
    
}