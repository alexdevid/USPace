using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Storage;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LevelManager
{
    [SerializeField] private List<Level> levels = new List<Level>();
    public List<Level> Levels => levels;

    public Level CreateLevel(int seed)
    {
        Level level = new Level(GetRandomLevelId(), seed);
        levels.Add(level);

        return level;
    }

    public void DeleteLevel(Level level)
    {
        levels.Remove(level);
    }

    public Level GetCurrentLevel()
    {
        return levels.First();
    }

    public void Store()
    {
        LocalStorage.Store(LocalStorage.Key.Levels, JsonUtility.ToJson(this));
    }

    public Level GetLevelById(int id)
    {
        return levels.Find(level => level.Id == id);
    }

    private int GetRandomLevelId()
    {
        int id = Random.Range(0, int.MaxValue);

        return GetLevelById(id) == null ? id : GetRandomLevelId();
    }

    public static LevelManager Load()
    {
        string json = LocalStorage.GetString(LocalStorage.Key.Levels);

        return json.Length > 0 ? JsonUtility.FromJson<LevelManager>(json) : new LevelManager();
    }

    private LevelManager()
    {
    }
}