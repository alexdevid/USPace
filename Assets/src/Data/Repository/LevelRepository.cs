using System.Collections.Generic;
using Model;
using UnityPackages;

namespace Data.Repository
{
    public class LevelRepository : AbstractRepository
    {
        public static Level Find(int id)
        {
            return new Level();
        }

        public static List<Level> FindAll()
        {
            List<Level> levels = new List<Level>();
            levels.Sort((level, level1) => level1.StartTime.CompareTo(level.StartTime));

            return levels;
        }

        public static void Delete(Level level)
        {
            // Game.App.Storage.Delete<Level>(level);
        }

        // public static Promise<List<Level>> FindAsync()
        // {
        //     return AbstractRepository.FindAsync<Level>();
        // }
    }
}