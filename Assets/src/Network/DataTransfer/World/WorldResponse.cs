using System;

namespace Network.DataTransfer.World
{
    [Serializable]
    public class WorldResponse : Response
    {
        public int id;
        public string name;
        public long created_at;
        public int seed;
        public int status;
        public int systems_total;
        public int max_players;
        public int current_players;
        
        public int total_players;
        public int star_systems_count;
    }
}