using System;
using System.Collections.Generic;
using Model;
using Storage;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Service
{
    [Serializable]
    public class LevelManager
    {
        private static LevelManager _manager = LevelManager.Create();
        public List<Level> levels = new List<Level>();

        public static Level CreateLevel(int seed)
        {
            Level level = new Level(_manager.GenerateLevelId(), seed);
            Debug.Log(level.GetId());
            return level;
        }

        public static void DeleteLevel(Level level)
        {
            _manager.levels.Remove(level);
            Store();
        }

        public static Level GetCurrentLevel()
        {
            return Runtime.Level;
        }

        public static Level GetLevelById(int id)
        {
            return _manager.levels.Find(level => level.GetId() == id);
        }
        
        public static void SaveLevel(Level level)
        {
            if (level.GetId() == 0)
            {
                throw new Exception("Level id should be set");
            }
            
            _manager.levels.Add(level);
        }

        public static void Store()
        {
            LocalStorage.Store(LocalStorage.Key.Levels, JsonUtility.ToJson(_manager));
        }

        public static List<Level> GetLevels()
        {
            return _manager.levels;
        }

        public static int GetLevelsCount()
        {
            return _manager.levels.Count;
        }

        public static LevelManager Create()
        {
            string json = LocalStorage.GetString(LocalStorage.Key.Levels);
            
            return json.Length > 0 ? JsonUtility.FromJson<LevelManager>(json) : new LevelManager();
        }

        private int GenerateLevelId()
        {
            int id = Random.Range(0, int.MaxValue);
            if (_manager.levels.Find(level => level.GetId() == id) != null) GenerateLevelId();

            return id;
        }
    }
}