using System;
using Model;
using Model.Space;
using UnityEngine;
using UnityPackages;
using Random = UnityEngine.Random;

namespace Generator
{
    public static class WorldGenerator
    {
        public const int WorldSeed = 393108462;
        public const int StarSystemsCount = 100000;
        public static int Counter = 0;

        // public static Promise<bool> Generate()
        // {
        //     return new Promise<bool>(GenerateSystems);
        // }

        public static void Generate()
        {
            Level level = Game.App.LevelManager.GetCurrentLevel();
            Random.InitState(level.Seed);

            for (int i = 0; i < StarSystemsCount; i++)
            {
                StarSystemGenerator
                    .Generate(GenerateSystemLocation())
                    .Then(system =>
                    {
                        Debug.Log(system.Name);
                        Counter++;
                    });
            }
        }

        private static void GenerateSystems(Action<bool> resolve, Action<string> reject)
        {
            Level level = Game.App.LevelManager.GetCurrentLevel();
            Random.InitState(level.Seed);

            for (int i = 0; i < StarSystemsCount; i++)
            {
                StarSystemGenerator
                    .Generate(GenerateSystemLocation())
                    .Then(system => { Debug.Log(system.Name); });
            }

            resolve(true);
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