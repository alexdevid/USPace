using System.Collections.Generic;
using Model;
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
        public GameObject levelSelector;
        public Transform levelContainer;

        private Level _selectedLevel;
        private readonly List<LevelSelector> _levelSelectors = new List<LevelSelector>();
        
        private void Start()
        {
            createButton.onClick.AddListener(OnCreateClick);
            backButton.onClick.AddListener(OnBackClick);
            playButton.onClick.AddListener(OnPlayClick);
            deleteButton.onClick.AddListener(OnDeleteClick);

            CreateLevelSelectors();
        }

        private void OnGUI()
        {
            playButton.interactable = _selectedLevel != null;
            deleteButton.interactable = _selectedLevel != null;
        }

        private void OnCreateClick()
        {
            System.Random rand = new System.Random();
            int seed = rand.Next(int.MinValue, int.MaxValue);

            Level level = Game.App.LevelManager.CreateLevel(seed);
            level.Name = $"New Universe {level.Id}";
            Game.App.LevelManager.Store();
            
            SceneManager.LoadScene(GameSystem);
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

        private void OnPlayClick()
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