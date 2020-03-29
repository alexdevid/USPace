using System.Collections.Generic;
using Model.Space.Dictionary;
using UnityEngine;

namespace Model.Space
{
    public class StarSystem
    {
        public readonly int Id;
        public readonly string Name;
        public readonly Vector2 Location;
        public readonly StarSystemType Type;
        
        public string PublicName;
        
        public List<SpaceObject> Objects { get; } = new List<SpaceObject>();
        

        public StarSystem(int id, string name, Vector2 location, StarSystemType type)
        {
            Id = id;
            Name = name;
            PublicName = name;
            Location = location;
            Type = type;
        }

        public void AddObject(SpaceObject spaceObject)
        {
            Objects.Add(spaceObject);
        }
    }
}