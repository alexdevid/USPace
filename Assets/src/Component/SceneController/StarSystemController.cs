using System;
using Factory;
using Generator;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Component.SceneController
{
    public class StarSystemController : AbstractSceneController
    {
        public Button menuButton;
        public Button closeMenuButton;
        public Button exitButton;
        public GameObject menuPanel;

        public const float GameScreenSize = 10000;
        private CameraDrag _cameraDrag;
        private Model.Space.StarSystem _starSystem;

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

            Game.App.StarSystem = StarSystemGenerator.Generate(1);

            Game.App.StarSystem.Objects.ForEach(spaceObject =>
            {
                GameObject go = SpaceObjectFactory.Generate(spaceObject);
            });
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