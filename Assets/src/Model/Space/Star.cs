using System;
using Model.Space.Dictionary;
using Object;
using UnityEngine;

namespace Model.Space
{
    [Serializable]
    public class Star : SpaceObject
    {
        [SerializeField]
        private StarType type;
        
        public Star(int id, string name, Vector2 position, StarType type) : base(id, name, position)
        {
            this.type = type;
        }
    }
}