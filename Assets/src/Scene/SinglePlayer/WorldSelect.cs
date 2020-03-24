using System.Collections.Generic;
using Generator;
using Model;
using UI.General;
using UI.SinglePlayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityPackages;

namespace Scene.SinglePlayer
{
    public class WorldSelect : SceneController
    {
        public Button createButton;
        public Button backButton;
        public Button playButton;
        public Button deleteButton;
        public Transform levelContainer;
        public Preloader preloader;
        public GameObject levelSelector;
        public GameObject loadingScreen;

        private Level _selectedLevel;
        private readonly List<LevelSelector> _levelSelectors = new List<LevelSelector>();
        
        private void Start()
        {
            createButton.onClick.AddListener(OnCreateClick);
            backButton.onClick.AddListener(OnBackClick);
            playButton.onClick.AddListener(OnPlayClick);
            deleteButton.onClick.AddListener(OnDeleteClick);

            loadingScreen.SetActive(false);
            CreateLevelSelectors();
        }

        private void OnGUI()
        {
            playButton.interactable = _selectedLevel != null;
            deleteButton.interactable = _selectedLevel != null;
            preloader.value = WorldGenerator.GenerationProgress;
        }

        private void OnCreateClick()
        {
            Level level = Game.App.LevelManager.CreateLevel(WorldGenerator.WorldSeed);
            level.Name = $"New Universe {level.Id}";
            Game.App.LevelManager.Store();
            
            WorldGenerator generator = new WorldGenerator();
            Promise<bool> generatorOperation = generator.Generate();
            loadingScreen.SetActive(true);
            preloader.max = WorldGenerator.StarSystemsCount;
            
            generatorOperation.Then(generated =>
            {
                SceneManager.LoadScene(GameSystem);
            });
        }

        private void OnLevelSelected(LevelSelector selector)
        {
            _levelSelectors.ForEach(levelSelectorComponent =>
            {
                levelSelectorComponent.SetSelected(false);
            });
            
            selector.SetSelected(true);
            _selectedLevel = selector.GetLevel();
            
        }

        private void OnDeleteClick()
        {
            Game.App.LevelManager.DeleteLevel(_selectedLevel);
            Game.App.LevelManager.Store();
            
            LevelSelector levelSelectorToDelete = GetLevelSelectorByLevelId(_selectedLevel.Id);
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
            return _levelSelectors.Find(levelSelectorComponent => levelSelectorComponent.GetLevel().Id == id);
        }
        
        private void CreateLevelSelectors()
        {
            foreach (Level level in Game.App.LevelManager.Levels)
            {
                GameObject levelObject = Instantiate(levelSelector, levelContainer);
                LevelSelector levelSelectorComponent = levelObject.GetComponent<LevelSelector>();
                levelSelectorComponent.MouseClickEvent.AddListener(() => OnLevelSelected(levelSelectorComponent));
                levelSelectorComponent.MouseDoubleClickEvent.AddListener(() =>
                {
                    OnLevelSelected(levelSelectorComponent);
                    OnPlayClick();
                });
                levelSelectorComponent.SetLevel(level);
                
                _levelSelectors.Add(levelSelectorComponent);
            }
        }
    }
}