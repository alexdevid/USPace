using Model.Space.Dictionary;
using UnityEngine;

namespace Model.Space
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