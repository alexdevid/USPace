using System;
using System.Collections.Generic;
using Model;
using Storage;
using UnityEngine;

namespace Service
{
    [System.Serializable]
    public class LevelManager
    {
        [SerializeField]
        private List<Level> levels = new List<Level>();

        public Level CreateLevel(int seed)
        {
            Level level = new Level(GenerateLevelId(), seed);
            
            return level;
        }

        public Level GetLevelById(int id)
        {
            return levels.Find(level => level.GetId() == id);
        }
        
        public LevelManager SaveLevel(Level level)
        {
            if (level.GetId() == 0)
            {
                throw new Exception("Level id should be set");
            }
            
            levels.Add(level);

            return this;
        }

        public void Store()
        {
            LocalStorage.Store(LocalStorage.Key.Levels, JsonUtility.ToJson(this));
        }

        public List<Level> GetLevels()
        {
            return levels;
        }

        public int GetLevelsCount()
        {
            return levels.Count;
        }

        public static LevelManager Create()
        {
            string json = LocalStorage.GetString(LocalStorage.Key.Levels);
            
            return json.Length > 0 ? JsonUtility.FromJson<LevelManager>(json) : new LevelManager();
        }

        private int GenerateLevelId()
        {
            int i = 1;
            while (GetLevelById(i) != null)
            {
                i++;
            }

            return i;
        }
    }
}