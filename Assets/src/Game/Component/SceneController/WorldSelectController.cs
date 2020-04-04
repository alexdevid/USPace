using System.Collections.Generic;
using Model;
using Network.Service;
using UI.General;
using UI.SinglePlayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.SceneController
{
    public class WorldSelectController : AbstractSceneController
    {
        public Button backButton;
        public Button playButton;
        public Transform levelContainer;
        public Preloader preloader;
        public GameObject levelSelector;
        public GameObject loadingScreen;
        public GameObject createOverlay;
        public InputField levelNameInput;

        private Level _selectedLevel;
        private readonly List<LevelSelector> _levelSelectors = new List<LevelSelector>();

        private void Start()
        {
            levelNameInput.text = Level.LevelNameDefault;
            preloader.max = 10000;

            backButton.onClick.AddListener(OnBackClick);
            playButton.onClick.AddListener(OnPlayClick);
            playButton.onClick.AddListener(OnPlayClick);

            loadingScreen.SetActive(false);
            createOverlay.SetActive(false);

            CreateLevelSelectors();
        }

        private void OnGUI()
        {
            playButton.interactable = _selectedLevel != null;
        }

        private void OnLevelSelected(LevelSelector selector)
        {
            _levelSelectors.ForEach(levelSelectorComponent => { levelSelectorComponent.SetSelected(false); });

            selector.SetSelected(true);
            _selectedLevel = selector.Level;
        }

        private void OnPlayClick()
        {
            GameController.Level = _selectedLevel;
            GameController.CurrentSystemId = GameController.Player.HomeSystemId;

            SceneManager.LoadScene(SceneStarSystem);
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(SceneMainMenu);
        }

        private async void CreateLevelSelectors()
        {
            List<Level> levels = await World.GetLevels();

            foreach (Level level in levels)
            {
                GameObject levelObject = Instantiate(levelSelector, levelContainer);
                LevelSelector levelSelectorComponent = levelObject.GetComponent<LevelSelector>();
                levelSelectorComponent.MouseClickEvent.AddListener(() => OnLevelSelected(levelSelectorComponent));
                levelSelectorComponent.MouseDoubleClickEvent.AddListener(() =>
                {
                    OnLevelSelected(levelSelectorComponent);
                    OnPlayClick();
                });
                levelSelectorComponent.Level = level;

                _levelSelectors.Add(levelSelectorComponent);
            }
        }
    }
}