using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.SceneController
{
    public class OptionsController : AbstractSceneController
    {
        public Button exitButton;
        public Toggle displayFpsToggle;
        public Toggle fullScreenToggle;
        public Dropdown resolutionDropdown;

        private void Start()
        {
            InitSettingsGui();

            exitButton.onClick.AddListener(OnExitClick);
            displayFpsToggle.onValueChanged.AddListener(OnDisplayFpsToggle);
            fullScreenToggle.onValueChanged.AddListener(OnFullScreenToggle);
            resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
        }

        private void InitSettingsGui()
        {
            fullScreenToggle.isOn = Screen.fullScreen;
            displayFpsToggle.isOn = ConfigManager.GetBool(ConfigManager.Name.SettingsDisplayFps);

            foreach (Resolution resolution in Screen.resolutions)
            {
                Dropdown.OptionData option = new Dropdown.OptionData();
                option.text = resolution.ToString();

                resolutionDropdown.options.Add(option);
            }
        }

        private static void OnResolutionChange(int value)
        {
            Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, true);
        }

        private static void OnFullScreenToggle(bool value)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, value);
        }

        private static void OnDisplayFpsToggle(bool value)
        {
            ConfigManager.Store(ConfigManager.Name.SettingsDisplayFps, value);
        }

        private static void OnExitClick()
        {
            SceneManager.LoadScene(SceneMainMenu);
        }
    }
}