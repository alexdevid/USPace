using System;

namespace Network.DataTransfer.StarSystem
{
    [Serializable]
    public class StarSystemCreateRequest
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

        public static StarSystemCreateRequest FromModel(Game.Model.StarSystem system)
        {
            return new StarSystemCreateRequest
            {
                name = system.Name,
                publicName = system.PublicName,
                position_x = system.Location.x,
                position_y = system.Location.y,
                sector = system.Sector,
                size = system.Size.ToString(),
                created_at = Convert.ToInt32(system.CreatedAt),
                discovered_at = Convert.ToInt32(system.DiscoveredAt),
                owner = system.Owner,
                discovered_by = system.DiscoveredBy
            };
        }
    }
}