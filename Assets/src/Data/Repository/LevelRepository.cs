using System.Collections.Generic;
using Model;

namespace Data.Repository
{
    public class LevelRepository : AbstractRepository
    {
        public static Level Find(int id)
        {
            return AbstractRepository.Find<Level>(id);
        }
        
        public static List<Level> FindAll()
        {
            return AbstractRepository.FindAll<Level>();
        }
    }
}