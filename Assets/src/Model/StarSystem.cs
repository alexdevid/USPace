using UnityEngine;

namespace Model
{
    public class StarSystem
    {
        public readonly string Name;
        public readonly Vector2 Location;
        private Sector _sector;

        public StarSystem(string name, Sector sector, Vector2 location)
        {
            Name = name;
            Location = location;
            
            _sector = sector;
        }

        public override string ToString()
        {
            return $"{Name} [{Location.x}, {Location.y}]";
        }
    }
}