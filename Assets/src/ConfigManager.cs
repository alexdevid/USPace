using UnityEngine;

public static class ConfigManager
{
    public enum Name
    {
        SettingsDisplayFps
    }
    
    public static bool GetBool(ConfigManager.Name key)
    {
        return PlayerPrefs.GetInt(key.ToString()) == 1;
    }
    public static int GetInt(ConfigManager.Name key)
    {
        return PlayerPrefs.GetInt(key.ToString());
    }
    
    public static void Store(ConfigManager.Name key, int value)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
    }

    public static void Store(ConfigManager.Name key, bool value)
    {
        PlayerPrefs.SetInt(key.ToString(), value ? 1 : 0);
    }
    
}