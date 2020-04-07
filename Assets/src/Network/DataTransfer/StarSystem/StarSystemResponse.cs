using System;

namespace Network.DataTransfer.StarSystem
{
    [Serializable]
    public class StarSystemResponse : Response
    {
        public int id;
        public string name;
        public string publicName;
        public float position_x;
        public float position_y;
        public float speed;

        public int seed;
        public int sector;
        public string size;
        public int created_at;
        public int discovered_at;

        public string owner;
        public string discovered_by;

        public Game.Model.StarSystem CreateModel()
        {
            return Game.Model.StarSystem.CreateFromDTO(this);
        }
    }
}