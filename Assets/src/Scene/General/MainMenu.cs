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

        private void Start()
        {
            startButton.onClick.AddListener(OnStartClick);
            optionsButton.onClick.AddListener(OnSettingsClick);
            exitButton.onClick.AddListener(OnExitClick);
            multiplayerButton.onClick.AddListener(OnMultiplayerClick);
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
            Application.Quit();
        }

        private static void OnMultiplayerClick()
        {
            Debug.Log("Multiplayer Not Enabled");
        }
    }
}