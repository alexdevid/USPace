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
        public const int StarSystemsCount = 2;

        private const int SeedModifier = 3;

        public static void Generate()
        {
            Level level = Game.App.LevelManager.GetCurrentLevel();
            Random.InitState(level.Seed);

            StarSystemGenerator.Generate(1 * SeedModifier);
            
            // for (int i = 0; i < StarSystemsCount; i++)
            // {
            //     StarSystemGenerator.Generate(i * SeedModifier);
            // }
        }
    }
}