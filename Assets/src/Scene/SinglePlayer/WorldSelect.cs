using System;
using System.Collections.Generic;
using Model;
using Service;
using UI.SinglePlayer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene.SinglePlayer
{
    public class WorldSelect : SceneController
    {
        public Button createButton;
        public Button backButton;
        public GameObject levelSelector;
        public Transform levelContainer;
        
        private LevelManager _levelManager;

        private void Start()
        {
            _levelManager = LevelManager.Create();
            
            createButton.onClick.AddListener(OnCreateClick);
            backButton.onClick.AddListener(OnBackClick);

            CreateLevelSelectors();
        }

        private void CreateLevelSelectors()
        {
            foreach (Level level in _levelManager.GetLevels())
            {
                GameObject levelObject = Instantiate(levelSelector, levelContainer);
                levelObject.GetComponent<Button>().onClick.AddListener((() => OnLevelClick(level)));
                levelObject.GetComponent<LevelSelector>().levelName.text = level.GetName();
            }
        }
        
        private void OnCreateClick()
        {
            System.Random rand = new System.Random();
            int seed = rand.Next(int.MinValue, int.MaxValue);

            Level level = _levelManager.CreateLevel(seed);
            level.SetName($"New Universe {_levelManager.GetLevelsCount() + 1}");
            
            _levelManager.SaveLevel(level).Store();
        }

        private static void OnLevelClick(Level level)
        {
            Debug.Log("LOAD LEVEL " + level.GetName());
        }
        
        private static void OnBackClick()
        {
            SceneManager.LoadScene(MainMenu);
        }
    }
}