using Model;
using Service;

public class Game
{
    private static Game _game;

    private LevelManager _levelManager;
    
    private Game()
    {
    }

    public LevelManager GetLevelManager()
    {
        _levelManager = _levelManager ?? LevelManager.Create();

        return _levelManager;
    }
    
    public static Game App()
    {
        _game = _game ?? new Game();

        return _game;
    }
}