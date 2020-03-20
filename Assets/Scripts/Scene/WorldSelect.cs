using System.Collections.Generic;
using Generator;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene
{
    public class WorldSelect : MonoBehaviour
    {
        public Camera mainCamera;
        public GameObject canvas;
        public GameObject panelPrefab;

        private List<GameObject> _sectors = new List<GameObject>();
        
        private void Start()
        {
            WorldGenerator generator = new WorldGenerator();
            generator.Generate();


            float containerSize = mainCamera.aspect > 1 ? canvas.GetComponent<RectTransform>().sizeDelta.y : canvas.GetComponent<RectTransform>().sizeDelta.x;
            float sectorSize = containerSize / Mathf.Sqrt(WorldGenerator.SectorsCount);
            
            GameObject sectorContainer = Instantiate(panelPrefab, canvas.transform);
            sectorContainer.GetComponent<RectTransform>().sizeDelta = new Vector2( containerSize, containerSize);
            GridLayoutGroup grid = sectorContainer.AddComponent<GridLayoutGroup>();
            sectorContainer.name = "SectorContainer";
            grid.cellSize = new Vector2( sectorSize, sectorSize);
            
            Debug.Log(sectorSize);

            foreach (Sector sector in generator.GetSectors())
            {
                GameObject sectorGameObject = Instantiate(panelPrefab, sectorContainer.transform);
                Button button = sectorGameObject.AddComponent<Button>();
                sectorGameObject.name = sector.Name;
                button.onClick.AddListener(() => { OnPanelClick(sector);});
                _sectors.Add(sectorGameObject);
            }

            if (ConfigManager.GetBool(ConfigName.SettingsDisplayFps))
            {
                gameObject.AddComponent<DebugOverlay>();
            }
        }

        private void OnPanelClick(Sector sector)
        {
            Debug.Log(sector.Name);
            SceneManager.LoadScene("Game");
        }
    }

}