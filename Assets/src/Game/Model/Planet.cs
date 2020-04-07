using Game.Model.Dictionary;
using UnityEngine;

namespace Game.Model
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