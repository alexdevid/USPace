using Model;

namespace Data.Repository
{
    public class LevelRepository : AbstractRepository
    {
        public static Level GetLevel(int id)
        {
            return new Level();
        }
    }
}