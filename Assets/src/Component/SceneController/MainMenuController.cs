using Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Component.SceneController
{
    public class MainMenuController : AbstractSceneController
    {
        public Button playButton;
        public Button optionsButton;
        public Button exitButton;
        public Button logoutButton;
        public Text username;
        public GameObject userBlock;
        public Transform preloader;

        private void Start()
        {
            RenderUserBlock();
            
            playButton.onClick.AddListener(OnStartClick);
            optionsButton.onClick.AddListener(OnSettingsClick);
            exitButton.onClick.AddListener(OnExitClick);
            logoutButton.onClick.AddListener(OnLogoutClick);
        }

        private void FixedUpdate()
        {
            if (preloader.gameObject.activeSelf) preloader.Rotate(new Vector3(0, 1, 0), 2.5f);
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

        private void OnLogoutClick()
        {
            userBlock.SetActive(false);
            Game.App.Logout();
        }
        
        private static void OnStartClick()
        {
            SceneManager.LoadScene(Game.App.IsLogged ? SceneWorldSelect : SceneLogin);
        }

        private static void OnSettingsClick()
        {
            SceneManager.LoadScene(SceneOptions);
        }

        private static void OnExitClick()
        {
            Game.App.Quit();
        }
    }
}