using System;
using Model;
using Model.Space;
using Model.Space.Dictionary;
using UnityEngine;
using UnityPackages;
using Random = UnityEngine.Random;

namespace Generator
{
    public static class WorldGenerator
    {
        public const int WorldSeed = 393108462;
        public const int StarSystemsCount = 500000;

        private const string StarSystemPrefix = "USC";

        public static Promise<bool> Generate()
        {
            Level level = Game.App.LevelManager.GetCurrentLevel();
            Random.InitState(level.Seed);

            return new Promise<bool>(GenerateSystems);
        }

        private static void GenerateSystems(Action<bool> resolve, Action<string> reject)
        {
            for (int i = 0; i < StarSystemsCount; i++)
            {
                GenerateSystem();
            }

            resolve(true);
        }

        private static StarSystem GenerateSystem()
        {
            Vector2 location = GenerateSystemLocation();
            StarSystem system = new StarSystem(GenerateSystemName(location), location);

            return system;
        }

        private static string GenerateSystemName(Vector2 location)
        {
            return $"{StarSystemPrefix}-{location.x}.{location.y}";
        }

        private static string GenerateSystemType(Vector2 location)
        {
            return "";
        }

        private static Vector2 GenerateSystemLocation()
        {
            float halfSize = GetUniverseSize() / 2;
            float x = Random.Range(-halfSize, halfSize);
            float y = Random.Range(-halfSize, halfSize);

            return new Vector2(x, y);
        }

        private static float GetUniverseSize()
        {
            return 50000 * 2.387f;
        }
    }
}