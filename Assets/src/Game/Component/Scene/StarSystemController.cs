using System;
using Game.Component.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.Scene
{
    public class StarSystemController : AbstractSceneController
    {
        public Button menuButton;
        public Button closeMenuButton;
        public Button exitButton;
        public GameObject menuPanel;
        public PreloaderOverlay preloaderOverlay;

        public const float GameScreenSize = 10000;
        private CameraDrag _cameraDrag;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) ToggleMenuOverlay();
        }

        private void Start()
        {
            preloaderOverlay.Show();
            if (Camera.main == null) throw new Exception("Add `MainCamera` tag to main camera");
            _cameraDrag = Camera.main.GetComponent<CameraDrag>();

            menuButton.onClick.AddListener(ToggleMenuOverlay);
            closeMenuButton.onClick.AddListener(ToggleMenuOverlay);
            exitButton.onClick.AddListener(OnExitButtonClick);
            
            LoadAndRenderSystem();
        }

        private void LoadAndRenderSystem()
        {
            preloaderOverlay.Hide();
            
            Debug.Log(GameController.StarSystem.Id);
            Debug.Log(GameController.StarSystem.Name);
        }

        private static void OnExitButtonClick()
        {
            SceneManager.LoadSceneAsync(SceneMainMenu);
        }

        private void ToggleMenuOverlay()
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            _cameraDrag.interactive = !menuPanel.activeSelf;
        }
    }
}