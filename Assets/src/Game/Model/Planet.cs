using Game.Model.Space.Dictionary;
using UnityEngine;

namespace Game.Model.Space
{
    public class Planet : SpaceObject
    {
        private PlanetType type;
        
        public Planet(int id, string name, Vector2 position, PlanetType type)
        {
            this.type = type;
        }
    }
}