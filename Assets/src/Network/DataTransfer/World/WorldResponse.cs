using System;

namespace Network.DataTransfer.World
{
    [Serializable]
    public class WorldResponse : Response
    {
        public int id;
        public int name;
        public int created_at;
        public int status;
        public int seed;
        public int systems_total;
        public int max_players;
        public int current_players;
    }
}