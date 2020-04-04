using System;
using Factory;
using Model.Space;
using Network.DataTransfer.StarSystem;
using Service.Space;
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

        private async void LoadAndRenderSystem()
        {
            try
            {
                GameController.StarSystem = await StarSystemService.GetSystem(2);
                preloaderOverlay.SetActive(false);
            }
            catch (Exception e)
            {
                if (e.Message == "DomainObjectNotFoundException")
                {
                    GameController.StarSystem = StarSystem.CreateFromDTO(new StarSystemResponse());
                }
                else
                {
                    GameController.Error = "OOPs!.\n" +
                                           "Something went wrong. \n" +
                                           $"System could not be loaded {GameController.CurrentSystemId}\n" +
                                           e.Message;

                    throw;
                }
            }

            Debug.Log(GameController.StarSystem.IsNew());
            Debug.Log(GameController.StarSystem.PublicName);
            Debug.Log(GameController.StarSystem.Id);

            GameController.StarSystem.Objects.ForEach(spaceObject =>
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