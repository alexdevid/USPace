using System;
using Network.DataTransfer.World;

namespace Model
{
    public class Level
    {
        public const string LevelNameDefault = "new universe";
        //{"id":1,"name":"Eva","created_at":1586079520,"status":"1","seed":393108462,"systems_total":50000,"max_players":10000,"current_players":1320}
        
        public int Id { get; private set; }
        public string Name { get; private set; }
        public long CreatedAt { get; private set; }
        public long Seed { get; private set; }
        public int TotalSystems { get; private set; }
        public int MaxPlayers { get; private set; }
        public int CurrentPlayers { get; private set; }
        
        private string _ageString;
        private string _startDateString;

        public string GetLevelAgeString()
        {
            if (_ageString != null)
                return _ageString;

            long offset = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - CreatedAt;
            TimeSpan time = TimeSpan.FromSeconds(offset);
            _ageString = $"{((int) time.TotalDays).ToString()} days";

            return _ageString;
        }

        public string GetStartDateString()
        {
            if (_startDateString != null)
                return _startDateString;

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(CreatedAt).ToLocalTime();
            _startDateString = dateTime.ToShortDateString();

            return _startDateString;
        }

        public static Level CreateFromDTO(WorldResponse response)
        {
            return new Level()
            {
                Id = response.id,
                Name = response.name,
                CreatedAt = response.created_at,
                Seed = response.seed,
                TotalSystems = response.systems_total,
                MaxPlayers = response.max_players,
                CurrentPlayers = response.current_players
            };
        }
    }
}