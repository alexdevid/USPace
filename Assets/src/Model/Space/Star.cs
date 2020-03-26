using Model.Space.Dictionary;
using UnityEngine;

namespace Model.Space
{
    public class Star : SpaceObject
    {
        private StarType type;

        public Star(int id, string name, Vector2 position, StarType type) : base(id, name, position)
        {
            this.type = type;
        }
    }
}