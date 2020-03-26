using System;
using UnityEngine;

namespace Model
{
    public class Level : StorageObject
    {
        public override int StorageIndex { get; }
        public override string ResourceName { get; }

        public int Id { get; }
        public int Seed { get; }
        public long StartTime { get; }
        public string Name { get; }

        private string _ageString;
        private string _startDateString;

        public Level()
        {
            Debug.Log("LEVEL CREATED!");
        }

        // public Level(int seed, string name, long startTime)
        // {
        //     Id = seed;
        //     Seed = seed;
        //     StartTime = startTime;
        //     Name = name;
        //
        //     ResourceName = "level";
        //     StorageIndex = Id;
        // }

        public override void Serialize()
        {
            AddField("name", Name);
            AddField("id", Id);
            AddField("seed", Seed);
            AddField("start_time", StartTime);
        }

        public string GetLevelAgeString()
        {
            if (_ageString != null)
                return _ageString;

            long offset = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - StartTime;
            TimeSpan time = TimeSpan.FromSeconds(offset);
            _ageString = $"{((int) time.TotalDays).ToString()} days";

            return _ageString;
        }

        public string GetStartDateString()
        {
            if (_startDateString != null)
                return _startDateString;

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(StartTime).ToLocalTime();
            _startDateString = dateTime.ToShortDateString();

            return _startDateString;
        }
    }
}