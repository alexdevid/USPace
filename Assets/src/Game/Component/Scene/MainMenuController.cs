using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.Scene
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
            playButton.gameObject.SetActive(true);
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