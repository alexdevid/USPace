﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Component.Scene.WorldSelect;
using Model;
using Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.Scene
{
    public class WorldSelectController : AbstractSceneController
    {
        public Button backButton;
        public Button playButton;
        public Transform worldSelectorsContainer;
        public GameObject worldSelector;
        public Text username;
        public Button logoutButton;

        private int _lastPlayedWorldId;
        private World _selectedWorld;
        private readonly List<WorldSelector> _worldSelectors = new List<WorldSelector>();

        private void Start()
        {
            username.text = GameController.Player.Username;
            
            backButton.onClick.AddListener(OnBackClick);
            playButton.onClick.AddListener(OnPlayClick);
            logoutButton.onClick.AddListener(OnLogout);

            _lastPlayedWorldId = GameController.LastPlayedWorldId;
            
            CreateWorldSelectors();
        }

        private void OnGUI()
        {
            playButton.interactable = _selectedWorld != null;
        }

        private void OnWorldSelected(WorldSelector selector)
        {
            _worldSelectors.ForEach(worldSelectorsComponent => { worldSelectorsComponent.SetSelected(false); });

            selector.SetSelected(true);
            _selectedWorld = selector.World;
        }

        private void OnPlayClick()
        {
            GameController.World = _selectedWorld;
            GameController.LastPlayedWorldId = _selectedWorld.Id;
            if (GameController.Player.HomeSystemId == 0)
            {
                SceneManager.LoadScene(SceneSectorSelect);
                return;
            }

            GameController.CurrentSystemId = GameController.Player.HomeSystemId;
            SceneManager.LoadScene(SceneStarSystem);
        }
        
        private static void OnBackClick()
        {
            SceneManager.LoadScene(SceneMainMenu);
        }

        private static void OnLogout()
        {
            GameController.Logout();
            SceneManager.LoadScene(SceneMainMenu);
        }

        private void CreateWorldSelectors()
        {
            WorldService.GetAll(worlds =>
            {
                foreach (World world in worlds)
                {
                    CreateSelector(world);
                }

                SelectActiveWorld();
            }, e => throw e);
        }

        private void CreateSelector(World world)
        {
            GameObject worldObject = Instantiate(worldSelector, worldSelectorsContainer);
            WorldSelector worldSelectorComponent = worldObject.GetComponent<WorldSelector>();
            worldSelectorComponent.World = world;
            worldSelectorComponent.MouseClickEvent.AddListener(() => OnWorldSelected(worldSelectorComponent));
            worldSelectorComponent.MouseDoubleClickEvent.AddListener(() =>
            {
                OnWorldSelected(worldSelectorComponent);
                OnPlayClick();
            });

            _worldSelectors.Add(worldSelectorComponent);
        }

        private void SelectActiveWorld()
        {
            WorldSelector selected = _worldSelectors.Find(selector => selector.World.Id == _lastPlayedWorldId);
            if (selected == null) return;
            
            //TODO re-render after sort
            selected.SetSelected(true);
            _selectedWorld = selected.World;
        }
    }
}