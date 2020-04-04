using Model;
using Model.Space;
using Network;
using UnityEngine;

public class GameController
{
    private const string TokenStorageKey = "_user_token";
    private static GameController _app = new GameController();

    private Player _player;
    private Level _level;
    private StarSystem _starSystem;
    private int _currentSystemId;
    private bool _isLogged;
    private string _token;
    private string _error;
    private string _systemError;
    private readonly Proxy _client;

    public static Proxy Client => _app._client;
    public static bool IsLogged => _app._isLogged;
    public static string Token => _app._token;

    public static Player Player
    {
        get => _app._player;
        set => _app._player = value;
    }

    public static Level Level
    {
        get => _app._level;
        set => _app._level = value;
    }

    public static StarSystem StarSystem
    {
        get => _app._starSystem;
        set => _app._starSystem = value;
    }

    public static int CurrentSystemId
    {
        get => _app._currentSystemId;
        set => _app._currentSystemId = value;
    }

    public static string SystemError
    {
        get => _app._systemError;
        set => _app._systemError = value;
    }

    public static string Error
    {
        get => _app._error;
        set => _app._error = value;
    }

    public static GameController App
    {
        get
        {
            _app = _app ?? new GameController();
            return _app;
        }
    }

    public static void SetPlayer(Player player)
    {
        _app._isLogged = true;
        _app._player = player;
        _app._token = player.Token;
        PlayerPrefs.SetString(TokenStorageKey, player.Token);
    }

    public static void Logout()
    {
        _app._isLogged = false;
        _app._token = null;
        PlayerPrefs.DeleteKey(TokenStorageKey);
        Player = new Player();
    }

    public static void Quit()
    {
        if (_app._client.IsConnected()) _app._client.Disconnect();

        Application.Quit();
    }

    public static void Init()
    {
        _app = _app ?? new GameController();
    }

    private GameController()
    {
        _token = PlayerPrefs.GetString(TokenStorageKey);
        _client = new Proxy();
    }
}