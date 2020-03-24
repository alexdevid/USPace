using System;
using Model.Space.Dictionary;
using UnityEngine;

namespace Model.Space
{
    [Serializable]
    public class StarSystem
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string publicName;
        [SerializeField] private Vector2 location;

        public string Name => name;
        public Vector2 Location => location;

        public StarSystem(string name, Vector2 location)
        {
            this.name = name;
            this.location = location;
            
            publicName = name;
            id = $"{location.x}{location.y}";
        }
    }
}