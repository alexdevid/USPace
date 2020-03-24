using UnityEngine;

public class Game
{
    private static Game _app = new Game();

    public LevelManager LevelManager { get; } = LevelManager.Load();

    public static Game App
    {
        get
        {
            _app = _app ?? new Game();
            return _app;
        }
    }
    
    private Game()
    {
        Debug.Log("INST GAME");
    }
}