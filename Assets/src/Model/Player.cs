namespace Model
{
    public class Player
    {
        private int id;
        private string name;

        public int Id => id;
        public string Name => name;
        
        public Player()
        {
            id = 1;
            name = "DeviD";
        }
    }
}