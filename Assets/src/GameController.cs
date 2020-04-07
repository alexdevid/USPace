using System;
using Game.Component;
using Model;
using Game.Model;
using Network;
using UnityEngine;

public enum GameMode
{
    Dev, Prod
}
public class GameController
{
    private const string TokenStorageKey = "_user_token";
    private const string LastPlayedWorldStorageKey = "_user_last_played_world";
    private static GameController _app = new GameController();
    private MainThreadWorker _worker;

    private Player _player;
    private World _world;
    private StarSystem _starSystem;
    private int _currentSystemId;
    private bool _isLogged;
    private string _token;
    private string _error;
    private string _systemError;
    private readonly Proxy _client;

    public static Proxy Client => _app._client;
    public static Player Player => _app._player;
    public static string Token => _app._token;
    public static bool IsLogged => _app._isLogged;
    public static GameMode GameMode => GameMode.Dev;

    public static World World
    {
        get => App._world;
        set => App._world = value;
    }

    public static StarSystem StarSystem
    {
        get => App._starSystem;
        set => App._starSystem = value;
    }

    public static int CurrentSystemId
    {
        get => App._currentSystemId;
        set => App._currentSystemId = value;
    }

    public static string SystemError
    {
        get => App._systemError;
        set => App._systemError = value;
    }

    public static string Error
    {
        get => App._error;
        set => App._error = value;
    }

    public static int LastPlayedWorldId
    {
        get => PlayerPrefs.GetInt(LastPlayedWorldStorageKey);
        set => PlayerPrefs.SetInt(LastPlayedWorldStorageKey, value);
    }

    public static GameController App
    {
        get
        {
            _app = _app ?? new GameController();
            return _app;
        }
    }

    public static MainThreadWorker AddTask(Action action)
    {
        if (App._worker == null)
        {
            App.CreateWorker();
        }
        
        App._worker.AddTask(action);
            
        return App._worker;
    }

    public static void SetPlayer(Player player)
    {
        App._isLogged = true;
        App._player = player;
        App._token = player.Token;
        PlayerPrefs.SetString(TokenStorageKey, player.Token);
    }

    public static void Logout()
    {
        App._isLogged = false;
        App._token = null;
        App._player = new Player();
        PlayerPrefs.DeleteKey(TokenStorageKey);
    }

    public static void Quit()
    {
        if (App._client.IsConnected()) App._client.Disconnect();

        Application.Quit();
    }

    private void CreateWorker()
    {
        GameObject worker = new GameObject("_worker");
        _worker = worker.AddComponent<MainThreadWorker>();
        GameObject.DontDestroyOnLoad(worker);
    }
    
    private GameController()
    {
        _token = PlayerPrefs.GetString(TokenStorageKey);
        _client = new Proxy();
    }
}