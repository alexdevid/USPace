using System;
using System.Collections.Generic;
using Model;
using Service;
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

            Level level = LevelManager.CreateLevel(seed);
            level.SetName($"New Universe {level.GetId()}");

            LevelManager.SaveLevel(level);
            LevelManager.Store();
            
            Runtime.Level = level;
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
            LevelManager.DeleteLevel(_selectedLevel);
            LevelSelector levelSelectorToDelete = GetLevelSelectorByLevelId(_selectedLevel.GetId());
            _levelSelectors.Remove(levelSelectorToDelete);
            
            _selectedLevel = null;
            Destroy(levelSelectorToDelete.gameObject);
        }

        private void OnPlayClick()
        {
            Runtime.Level = _selectedLevel;
            SceneManager.LoadScene(GameSystem);
        }
        
        private static void OnBackClick()
        {
            SceneManager.LoadScene(MainMenu);
        }

        private LevelSelector GetLevelSelectorByLevelId(int id)
        {
            return _levelSelectors.Find(levelSelectorComponent => levelSelectorComponent.GetLevel().GetId() == id);
        }
        
        private void CreateLevelSelectors()
        {
            foreach (Level level in LevelManager.GetLevels())
            {
                GameObject levelObject = Instantiate(levelSelector, levelContainer);
                LevelSelector levelSelectorComponent = levelObject.GetComponent<LevelSelector>();
                levelSelectorComponent.MouseDownEvent.AddListener(() => OnLevelSelected(levelSelectorComponent));
                levelSelectorComponent.SetLevel(level);
                
                _levelSelectors.Add(levelSelectorComponent);
            }
        }
    }
}