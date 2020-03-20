using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class Game : MonoBehaviour
    {
        public Camera gameCamera;
        public Button menuButton;
        public Button closeMenuButton;
        public Button exitButton;
        public GameObject menuPanel;
        
        public const float GameScreenSize = 1000;

        private void Start()
        {
            ToggleMenuOverlay(false);

            gameObject.AddComponent<CameraDrag>().mainCamera = gameCamera;
            
            menuButton.onClick.AddListener(() => ToggleMenuOverlay(true));
            closeMenuButton.onClick.AddListener(() => ToggleMenuOverlay(false));
            exitButton.onClick.AddListener(OnExitButtonClick);

            if (ConfigManager.GetBool(ConfigName.SettingsDisplayFps))
            {
                gameObject.AddComponent<DebugOverlay>();
            }
        }

        private static void OnExitButtonClick()
        {
            SceneManager.LoadSceneAsync("MenuMainMenu");
        }
        
        private void ToggleMenuOverlay(bool visible)
        {
            menuPanel.SetActive(visible);
        }
    }
}