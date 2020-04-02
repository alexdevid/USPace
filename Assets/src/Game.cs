using Data.Storage;
using Data.Storage.LocalStorage;
using Model;
using Model.Space;
using Network;
using UnityEngine;

public class Game
{
    private const string TokenStorageKey = "_user_token";
    private static Game _app = new Game();

    public LevelManager LevelManager { get; } = LevelManager.Load();
    public Player Player { get; private set; }
    public StarSystem CurrentStarSystem { get; set; }
    public IStorage Storage { get; }
    public Proxy Client { get; } = new Proxy();
    public string Token { get; private set; }

    public static Game App
    {
        get
        {
            _app = _app ?? new Game();
            return _app;
        }
    }

    public void SetPlayer(Player player)
    {
        Player = player;
        Token = player.Token;
        PlayerPrefs.SetString(TokenStorageKey, Token);
    }

    public static void Init()
    {
        _app = _app ?? new Game();
    }
    
    private Game()
    {
        Player = new Player();
        Storage = new LocalStorage();
        Client.SetupServer();
        Token = PlayerPrefs.GetString(TokenStorageKey);
    }
}