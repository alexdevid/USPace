using System;
using System.Collections.Generic;
using Network.DataTransfer.StarSystem;
using UnityEngine;

namespace Game.Model
{
    public enum StarSystemSize
    {
        Tiny, Small, Medium, Large, ExtraLarge     
    }
    
    public enum StarSystemType
    {
        Single,
        Double,
        Triple,
        BlackHole,
        Empty,
        Special
    }
    
    public class StarSystem
    {
        public string PublicName { get; set; }
        public string Owner { get; set; }
        public string DiscoveredBy { get; set; }
        
        public float Speed { get; set; }
        public long DiscoveredAt { get; set; }
        
        public int Id { get; private set; }
        public int Seed { get; private set; }
        public int Sector { get; private set; }
        public long CreatedAt { get; private set; }
        public string Name { get; private set; }
        public Vector2 Location { get; private set; } = Vector2.zero;
        public StarSystemType Type { get; private set; } = StarSystemType.Single;
        public StarSystemSize Size { get; private set; } = StarSystemSize.Medium;
        
        public List<SpaceObject> Objects { get; } = new List<SpaceObject>();

        public StarSystem(int sector, Vector2 location, StarSystemType type, StarSystemSize size)
        {
            Name = GenerateName(location);
            Sector = sector;
            Location = location;
            Type = type;
            Size = size;
            Owner = GameController.Player.Username;
            CreatedAt = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            DiscoveredAt = CreatedAt;
            DiscoveredBy = Owner;
        }

        public StarSystem()
        {
            CreatedAt = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public bool IsNew()
        {
            return Id == 0;
        }

        public void AddObject(SpaceObject spaceObject)
        {
            Objects.Add(spaceObject);
        }

        private static string GenerateName(Vector2 coords)
        {
            string name = "USC-";
            name += Math.Abs(coords.x) + (coords.x < 0 ? "W" : "E") + ".";
            name += Math.Abs(coords.y) + (coords.x < 0 ? "N" : "S");

            return name;
        }

        public static StarSystem CreateFromDTO(StarSystemResponse response)
        {
            StarSystemType GetType()
            {
                return StarSystemType.Double;
            }

            StarSystemSize GetSize()
            {
                return StarSystemSize.Medium;
            }
            
            StarSystem system = new StarSystem()
            {
                PublicName = response.publicName ?? response.name,
                Owner = response.owner,
                DiscoveredBy = response.discovered_by,
                
                Speed = response.speed,
                DiscoveredAt = response.discovered_at,
                
                Id = response.id,
                Seed = response.seed,
                Sector = response.sector,
                CreatedAt = response.created_at,
                Name = response.name,
                Location = new Vector2(response.position_x, response.position_y),
                Type = GetType(),
                Size = GetSize()
            };

            return system;
        }
    }
}