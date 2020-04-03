using Model;
using Model.Space;
using Network;
using UnityEngine;

public class Game
{
    private const string TokenStorageKey = "_user_token";
    private static Game _app = new Game();

    public Player Player { get; private set; }
    public Level World { get; set; }
    public StarSystem StarSystem { get; set; }
    public Proxy Client { get; } = new Proxy();
    public string Token { get; private set; }
    public bool IsLogged { get; private set; }
    public string AuthError { get; set; }
    
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
        IsLogged = true;
        Player = player;
        Token = player.Token;
        PlayerPrefs.SetString(TokenStorageKey, Token);
    }
    
    public void Logout()
    {
        IsLogged = false;
        Token = null;
        PlayerPrefs.DeleteKey(TokenStorageKey);
        Player = new Player();
    }

    public void Quit()
    {
        if (Client.IsConnected()) App.Client.Disconnect();
        Application.Quit();
    }

    public static void Init()
    {
        _app = _app ?? new Game();
    }
    
    private Game()
    {
        Player = new Player();
        Token = PlayerPrefs.GetString(TokenStorageKey);
    }
}