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
        [SerializeField] private string name;

        public int Id => id;
        public int Seed => seed;
        public long StartTime => startTime;
        public string Name => name;

        private string _ageString;
        private string _startDateString;

        public Level(int id, int seed, string name)
        {
            this.id = id;
            this.seed = seed;
            this.name = name;
            startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public string GetLevelAgeString()
        {
            if (_ageString != null)
                return _ageString;
            
            long offset = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - startTime;
            TimeSpan time = TimeSpan.FromSeconds(offset);
            _ageString = $"{((int) time.TotalDays).ToString()} days";

            return _ageString;
        }

        public string GetStartDateString()
        {
            if (_startDateString != null)
                return _startDateString;
            
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(startTime).ToLocalTime();
            _startDateString = dateTime.ToShortDateString();
            
            return _startDateString;
        }
    }
}