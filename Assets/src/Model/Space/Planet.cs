using Model.Space.Dictionary;
using UnityEngine;

namespace Model.Space
{
    public class Planet : SpaceObject
    {
        private PlanetType type;
        
        public Planet(int id, string name, Vector2 position, PlanetType type) : base(id, name, position)
        {
            this.type = type;
        }
    }
}