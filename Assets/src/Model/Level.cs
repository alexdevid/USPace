using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Level
    {
        [SerializeField] private int id;
        [SerializeField] private int seed;
        [SerializeField] private long startTime;
        
        public int Id => id;
        public int Seed => seed;
        public long StartTime => startTime;
        public string Name { get; set; }

        public Level(int id, int seed)
        {
            this.id = id;
            this.seed = seed;
            startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public long GetLevelAge()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() - startTime;
        }
    }
}