using System.Collections.Generic;
using Data.Repository;
using Generator;
using Model;
using UI.General;
using UI.SinglePlayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.SinglePlayer
{
    public class WorldSelect : SceneController
    {
        public Button createButton;
        public Button backButton;
        public Button playButton;
        public Button deleteButton;
        public Button showCreateOverlayButton;
        public Button hideCreateOverlayButton;
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

            backButton.onClick.AddListener(OnBackClick);
            playButton.onClick.AddListener(OnPlayClick);
            playButton.onClick.AddListener(OnPlayClick);
            deleteButton.onClick.AddListener(OnDeleteClick);
            createButton.onClick.AddListener(OnCreateClick);
            showCreateOverlayButton.onClick.AddListener(OnCreateOverlayClick);
            hideCreateOverlayButton.onClick.AddListener(OnHideOverlayClick);

            loadingScreen.SetActive(false);
            createOverlay.SetActive(false);
            CreateLevelSelectors();
        }

        private void OnGUI()
        {
            playButton.interactable = _selectedLevel != null;
            deleteButton.interactable = _selectedLevel != null;
        }

        private void OnHideOverlayClick()
        {
            createOverlay.SetActive(false);
        }

        private void OnCreateOverlayClick()
        {
            createOverlay.SetActive(true);
        }

        private void OnCreateClick()
        {
            preloader.max = WorldGenerator.StarSystemsCount;
            createOverlay.SetActive(false);
            // loadingScreen.SetActive(true);

            Level level = Game.App.LevelManager.CreateLevel(WorldGenerator.WorldSeed, levelNameInput.text);
            Game.App.LevelManager.SaveLevel(level);

            // Game.App.CurrentStarSystem = StarSystemGenerator.Generate(1);

            // SceneManager.LoadScene(GameSystem);
        }

        private void OnLevelSelected(LevelSelector selector)
        {
            _levelSelectors.ForEach(levelSelectorComponent => { levelSelectorComponent.SetSelected(false); });

            selector.SetSelected(true);
            _selectedLevel = selector.Level;
        }

        private void OnDeleteClick()
        {
            Game.App.LevelManager.DeleteLevel(_selectedLevel);

            LevelSelector levelSelectorToDelete = GetLevelSelectorByLevelId(_selectedLevel.StorageIndex);
            _levelSelectors.Remove(levelSelectorToDelete);

            _selectedLevel = null;
            Destroy(levelSelectorToDelete.gameObject);
        }

        private static void OnPlayClick()
        {
            SceneManager.LoadScene(GameSystem);
        }

        private static void OnBackClick()
        {
            SceneManager.LoadScene(MainMenu);
        }

        private LevelSelector GetLevelSelectorByLevelId(int id)
        {
            return _levelSelectors.Find(levelSelectorComponent => levelSelectorComponent.Level.StorageIndex == id);
        }

        private void CreateLevelSelectors()
        {
            foreach (Level level in LevelRepository.FindAll())
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