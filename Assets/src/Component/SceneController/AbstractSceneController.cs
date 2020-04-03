using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Component.SceneController
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        protected const string SceneMainMenu = "main_menu";
        protected const string SceneOptions = "options";
        protected const string SceneLogin = "login";
        protected const string SceneWorldSelect = "world_select";
        protected const string SceneStarSystem = "star_system";

        private ArrayList SecureScenes { get; } = new ArrayList
        {
            SceneWorldSelect, 
            SceneStarSystem
        };

        private void Awake()
        {
            Game.Init();
            
            if (SecureScenes.Contains(SceneManager.GetActiveScene().name) && !Game.App.IsLogged)
            {
                SceneManager.LoadScene(SceneLogin);
            }
        }
    }
}