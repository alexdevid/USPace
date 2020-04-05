using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.SceneController
{
    public class MainMenuController : AbstractSceneController
    {
        public Button playButton;
        public Button optionsButton;
        public Button exitButton;

        private void Start()
        {
            playButton.onClick.AddListener(OnStartClick);
            optionsButton.onClick.AddListener(OnSettingsClick);
            exitButton.onClick.AddListener(OnExitClick);

            // if (!GameController.Client.IsConnected())
            // {
            //     playButton.GetComponent<Button>().interactable = false;
            // }
            //
            // GameController.Client.OnConnected.AddListener(() =>
            // {
            //     Debug.Log("CONNECTED...................");
            //     playButton.GetComponent<Button>().interactable = true;
            // });
        }

        private static void OnStartClick()
        {
            SceneManager.LoadScene(GameController.IsLogged ? SceneWorldSelect : SceneLogin);
        }

        private static void OnSettingsClick()
        {
            SceneManager.LoadScene(SceneOptions);
        }

        private static void OnExitClick()
        {
            GameController.Quit();
        }
    }
}