using System.Collections.Generic;
using Game.Component.Scene.SectorSelect;
using Game.Generator;
using Game.Model;
using Game.Service;
using Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Component.Scene
{
    public class SectorSelectController : AbstractSceneController
    {
        public Button backButton;
        public Button selectButton;
        public Transform worldContainer;
        public Text username;
        public Button logoutButton;
        public Sector sectorPrefab;
        public GameObject overlay;
        public GameObject preloader;

        private int _selectedSector;
        private readonly List<Sector> _sectors = new List<Sector>();
        
        private void Start()
        {
            backButton.onClick.AddListener(OnBackClick);
            selectButton.onClick.AddListener(OnSelectClick);
            logoutButton.onClick.AddListener(OnLogout);
            
            username.text = GameController.Player.Username;
            overlay.SetActive(true);
            RenderSectors();
        }

        private void OnGUI()
        {
            selectButton.interactable = _selectedSector != 0;
        }

        private void OnSectorSelected(Sector sector)
        {
            _sectors.ForEach(s => { s.SetSelected(false); });
            _selectedSector = sector.Id;
            sector.SetSelected(true);
        }

        private void OnSelectClick()
        {
            StarSystemService.Create(_selectedSector, system =>
            {
                GameController.Player.HomeSystemId = system.Id;
                GameController.StarSystem = system;
                SceneManager.LoadScene(SceneStarSystem);
            }, e => Debug.Log(e.Message));
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

        private void RenderSectors()
        {
            WorldService.GetPopulationDensity(response =>
            {
               for (int index = 0; index < response.density.Length; index++)
               {
                   int population = response.density[index];
                   Sector sector = Instantiate(sectorPrefab, worldContainer);
                   sector.Id = index + 1;
                   sector.SetText(population.ToString());
                   sector.MouseClickEvent.AddListener(() => OnSectorSelected(sector));
                   sector.MouseDoubleClickEvent.AddListener(() =>
                   {
                       OnSectorSelected(sector);
                       OnSelectClick();
                   });

                   _sectors.Add(sector);
               }
            }, Debug.Log);
                
            overlay.SetActive(false);
        }
    }
}