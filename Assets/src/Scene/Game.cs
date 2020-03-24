using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class Game : SceneController
    {
        public Button menuButton;
        public Button closeMenuButton;
        public Button exitButton;
        public GameObject menuPanel;
        
        public const float GameScreenSize = 10000;
        private CameraDrag _cameraDrag;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) ToggleMenuOverlay();
        }

        private void Start()
        {
            if (Camera.main == null) throw new Exception("Add `MainCamera` tag to main camera");
            
            _cameraDrag = Camera.main.GetComponent<CameraDrag>();
            
            menuButton.onClick.AddListener(ToggleMenuOverlay);
            closeMenuButton.onClick.AddListener(ToggleMenuOverlay);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private static void OnExitButtonClick()
        {
            SceneManager.LoadSceneAsync(MainMenu);
        }
        
        private void ToggleMenuOverlay()
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            _cameraDrag.interactive = !menuPanel.activeSelf;
        }
    }
}