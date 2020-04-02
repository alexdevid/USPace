using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.General
{
    public class MainMenu : SceneController
    {
        public Button startButton;
        public Button optionsButton;
        public Button exitButton;
        public Button multiplayerButton;
        
        public Transform preloader;

        private void Awake()
        {
            Game.Init();
        }

        private void Start()
        {
            startButton.onClick.AddListener(OnStartClick);
            optionsButton.onClick.AddListener(OnSettingsClick);
            exitButton.onClick.AddListener(OnExitClick);
            multiplayerButton.onClick.AddListener(OnMultiplayerClick);
        }

        private void Update()
        {
            preloader.Rotate(new Vector3(0, 1, 0), 2.5f);
        }

        private static void OnStartClick()
        {
            SceneManager.LoadScene(WorldSelect);
        }

        private static void OnSettingsClick()
        {
            SceneManager.LoadScene(Options);
        }

        private static void OnExitClick()
        {
            Debug.Log("BYE!");
            // Game.App.Client.Disconnect();
            Application.Quit();
        }

        private async void OnMultiplayerClick()
        {
            var something = await Fetch();
            
            Debug.Log(something);
        }

        async Task<string> Fetch()
        {
            return await Task.Run(() => Game.App.Client.SendMessage("hello world"));
        }
    }
}