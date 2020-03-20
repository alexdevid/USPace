using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public Button startButton;
        public Button settingsButton;
        public Button exitButton;

        private void Start()
        {
            startButton.onClick.AddListener(OnStartClick);
            settingsButton.onClick.AddListener(OnSettingsClick);
            exitButton.onClick.AddListener(OnExitClick);
        }

        private static void OnStartClick()
        {
            SceneManager.LoadScene("WorldSelect");
        }

        private static void OnSettingsClick()
        {
            SceneManager.LoadScene("MenuSettings");
        }

        private static void OnExitClick()
        {
            Debug.Log("BYE!");
            Application.Quit();
        }
    }
}