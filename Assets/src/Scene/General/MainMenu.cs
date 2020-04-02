using System;
using System.Threading.Tasks;
using Network;
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
        public Button logoutButton;
        public Text username;
        public GameObject userBlock;
        
        public Transform preloader;

        private async void Awake()
        {
            Game.Init();
            RenderUserBlock();
        }

        private async void RenderUserBlock()
        {
            userBlock.SetActive(false);
            preloader.gameObject.SetActive(true);
            Authenticator.AuthStatus status = await Authenticator.Auth();
            if (status == Authenticator.AuthStatus.Success)
            {
                username.text = Game.App.Player.Username;
                userBlock.SetActive(true);
            }
            
            preloader.gameObject.SetActive(false);
        }

        private void Start()
        {
            startButton.onClick.AddListener(OnStartClick);
            optionsButton.onClick.AddListener(OnSettingsClick);
            exitButton.onClick.AddListener(OnExitClick);
            multiplayerButton.onClick.AddListener(OnMultiplayerClick);
            logoutButton.onClick.AddListener(OnLogoutClick);
        }

        private void Update()
        {
            if (preloader.gameObject.activeSelf) preloader.Rotate(new Vector3(0, 1, 0), 2.5f);
        }

        private void OnLogoutClick()
        {
            userBlock.SetActive(false);
            Game.App.Logout();
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
            Game.App.Quit();
        }

        private async void OnMultiplayerClick()
        {
            SceneManager.LoadScene(SceneLogin);
        }
    }
}