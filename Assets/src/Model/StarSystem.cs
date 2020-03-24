using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class StarSystem
    {
        [SerializeField] private string name;
        [SerializeField] private Vector2 location;

        public string Name => name;
        public Vector2 Location => location;

        public StarSystem(string name, Vector2 location)
        {
            this.name = name;
            this.location = location;
        }
    }
}