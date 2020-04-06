using System.Collections.Generic;
using Model;
using Game.Service;
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

        private int _lastPlayedLevelId;
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

            _lastPlayedLevelId = GameController.LastPlayedLevelId;
            
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
            GameController.LastPlayedLevelId = _selectedLevel.Id;
            Debug.Log(_selectedLevel.Id);
            GameController.CurrentSystemId = GameController.Player.HomeSystemId;

            SceneManager.LoadScene(SceneStarSystem);
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(SceneMainMenu);
        }

        private void CreateLevelSelectors()
        {
            LevelService.GetAll(levels =>
            {
                foreach (Level level in levels)
                {
                    CreateSelector(level);
                }

                SelectActiveLevel();
            }, e => throw e);
        }

        private void CreateSelector(Level level)
        {
            GameObject levelObject = Instantiate(levelSelector, levelContainer);
            LevelSelector levelSelectorComponent = levelObject.GetComponent<LevelSelector>();
            levelSelectorComponent.Level = level;
            levelSelectorComponent.MouseClickEvent.AddListener(() => OnLevelSelected(levelSelectorComponent));
            levelSelectorComponent.MouseDoubleClickEvent.AddListener(() =>
            {
                OnLevelSelected(levelSelectorComponent);
                OnPlayClick();
            });

            _levelSelectors.Add(levelSelectorComponent);
        }

        private void SelectActiveLevel()
        {
            LevelSelector selected = _levelSelectors.Find(selector => selector.Level.Id == _lastPlayedLevelId);
            if (selected != null)
            {
                //TODO render after sort
                selected.SetSelected(true);
                _selectedLevel = selected.Level;
            }
        }
    }
}