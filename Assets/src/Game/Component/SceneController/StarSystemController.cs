using System;
using Factory;
using Model.Space;
using Game.Service.Space;
using Network.DataTransfer.StarSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.SceneController
{
    public class StarSystemController : AbstractSceneController
    {
        public Button menuButton;
        public Button closeMenuButton;
        public Button exitButton;
        public GameObject menuPanel;
        public GameObject preloaderOverlay;

        public const float GameScreenSize = 10000;
        private CameraDrag _cameraDrag;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) ToggleMenuOverlay();
        }

        private void Start()
        {
            preloaderOverlay.SetActive(true);
            if (Camera.main == null) throw new Exception("Add `MainCamera` tag to main camera");
            _cameraDrag = Camera.main.GetComponent<CameraDrag>();

            menuButton.onClick.AddListener(ToggleMenuOverlay);
            closeMenuButton.onClick.AddListener(ToggleMenuOverlay);
            exitButton.onClick.AddListener(OnExitButtonClick);

            LoadAndRenderSystem();
        }

        private void LoadAndRenderSystem()
        {
            StarSystemService.Get(2, system =>
            {
                GameController.StarSystem = system ?? StarSystem.CreateFromDTO(new StarSystemResponse());
                preloaderOverlay.SetActive(false);
                GameController.StarSystem.Objects.ForEach(spaceObject =>
                {
                    //GameObject go = SpaceObjectFactory.Generate(spaceObject);
                });
            }, e =>
            {
                GameController.Error = "OOPs!.\n" +
                                       "Something went wrong. \n" +
                                       $"System could not be loaded {GameController.CurrentSystemId}\n" +
                                       e.Message;

                throw e;
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