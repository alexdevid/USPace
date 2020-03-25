using System;
using UnityEngine;

namespace Model.Space
{
    [Serializable]
    public abstract class SpaceObject
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private Vector2 position;

        public int Id => id;
        public string Name => name;
        public Vector2 Position => position;

        public SpaceObject(int id, string name, Vector2 position)
        {
            this.id = id;
            this.name = name;
            this.position = position;
        }
    }
}