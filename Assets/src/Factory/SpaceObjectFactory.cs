using System;
using Game.Model.Space;
using UnityEngine;

namespace Factory
{
    public class SpaceObjectFactory : AbstractFactory
    {
        private const string StarSprite = "obj";
        
        private const string StarPrefix = "_star_";
        private const string PlanetPrefix = "_planet_";
        private const string UnknownPrefix = "_unknown_";

        public static GameObject Generate(SpaceObject spaceObject)
        {
            GameObject go = new GameObject(GetGameObjectUniqueName(spaceObject));

            go.AddComponent<Game.Component.Planet>().Model = spaceObject;
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            go.AddComponent<CircleCollider2D>();

            spriteRenderer.sprite = Resources.Load<Sprite>(StarSprite);

            return go;
        }

        private static string GetGameObjectUniqueName(SpaceObject spaceObject)
        {
            return GetPrefix(spaceObject.GetType());
        }
        
        private static string GetPrefix(Type type)
        {
            if (type.FullName == typeof(Star).FullName)
                return StarPrefix;
            else if (type.FullName == typeof(Planet).FullName)
                return PlanetPrefix;

            return UnknownPrefix;
        }
    }
}