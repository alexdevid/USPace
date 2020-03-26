using System;
using UnityEngine;

namespace Model.Space
{
    public abstract class SpaceObject
    {
        private int id;
        private string name;
        private Vector2 position;
        private Player _owner;

        public int Id => id;
        public string Name => name;
        public Vector2 Position => position;

        public SpaceObject(int id, string name, Vector2 position)
        {
            this.id = id;
            this.name = name;
            this.position = position;
        }

        public void SetOwner(Player owner)
        {
            this._owner = owner;
        }
    }
}