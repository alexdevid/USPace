using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.Menu
{
    public class Settings : MonoBehaviour
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
            displayFpsToggle.isOn = ConfigManager.GetBool(ConfigName.SettingsDisplayFps);
            
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
            ConfigManager.Store(ConfigName.SettingsDisplayFps, value);
        }
        
        private static void OnExitClick()
        {
            SceneManager.LoadScene("MenuMainMenu");
        }
    }
}