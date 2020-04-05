using System;

namespace Network.DataTransfer.World
{
    [Serializable]
    public class WorldResponse : Response
    {
        public int id;
        public string name;
        public long created_at;
        public long seed;
        public int status;
        public int systems_total;
        public int max_players;
        public int current_players;
    }
}