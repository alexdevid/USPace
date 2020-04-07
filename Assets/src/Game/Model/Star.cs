using Game.Model.Dictionary;
using UnityEngine;

namespace Game.Model
{
    public class Star : SpaceObject
    {
        private StarType type;

        public Star(int id, string name, Vector2 position, StarType type)
        {
            this.type = type;
        }
    }
}