using System;
using Network.DataTransfer.World;

namespace Model
{
    public class Level
    {
        public const string LevelNameDefault = "new universe";

        private int id;
        private int seed;
        private string name;
        private int startTime;
        private int systemsCount;

        public int Id => id;
        public int Seed => seed;
        public string Name => name;
        public long StartTime => startTime;

        private string _ageString;
        private string _startDateString;

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

        public static Level CreateFromDTO(WorldResponse dto)
        {
            Level level = new Level();
            level.name = "asdasd";
            
            return level;
        }
    }
}