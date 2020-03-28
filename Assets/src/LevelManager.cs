using System;
using System.Collections.Generic;
using System.Reflection;
using Data.Repository;
using Model;
using Random = UnityEngine.Random;

public class LevelManager
{
    public List<Level> Levels => Game.App.Storage.GetAll<Level>();

    public void SaveLevel(Level level)
    {
        Game.App.Storage.Store(level);
    }

    public void DeleteLevel(Level level)
    {
        LevelRepository.Delete(level);
    }
    
    public Level CreateLevel(int seed, string name)
    {
        int id = GetRandomLevelId();
        
        Level model = (Level) Activator.CreateInstance(typeof(Level), new object[] { });
            
        model.StorageIndex = id;
        
        var nameProp = model.GetType().GetField("name", BindingFlags.NonPublic | BindingFlags.Instance);
        if (nameProp != null)
        {
            nameProp.SetValue(model, name);
        }
        
        var seedProp = model.GetType().GetField("seed", BindingFlags.NonPublic | BindingFlags.Instance);
        if (seedProp != null)
        {
            seedProp.SetValue(model, seed);
        }
        
        var startProp = model.GetType().GetField("startTime", BindingFlags.NonPublic | BindingFlags.Instance);
        if (startProp != null)
        {
            startProp.SetValue(model, (Int32) DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        }

        return model;
    }

    private int GetRandomLevelId()
    {
        int id = Random.Range(0, int.MaxValue);
        return Game.App.Storage.Has<Level>(id) == false ? id : GetRandomLevelId();
    }

    public static LevelManager Load()
    {
        return new LevelManager();
    }

    private LevelManager()
    {
    }
}