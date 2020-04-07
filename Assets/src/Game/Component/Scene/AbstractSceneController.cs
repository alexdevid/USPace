using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Game.Component.Scene
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        protected const string SceneMainMenu = "main_menu";
        protected const string SceneOptions = "options";
        protected const string SceneLogin = "login";
        protected const string SceneWorldSelect = "world_select";
        protected const string SceneStarSystem = "star_system";
        protected const string SceneSectorSelect = "sector_select";
        protected const string SceneError = "error";

        private ArrayList SecureScenes { get; } = new ArrayList
        {
            SceneWorldSelect,
            SceneStarSystem
        };

        private void Awake()
        {
            Application.logMessageReceived += HandleException;

            if (SecureScenes.Contains(SceneManager.GetActiveScene().name) && !GameController.IsLogged)
            {
                SceneManager.LoadScene(SceneLogin);
            }
        }

        private static void HandleException(string condition, string stackTrace, LogType type)
        {
            if (type != LogType.Exception) return;

            GameController.SystemError = $"{type}: {condition}\n";
            if (GameController.GameMode == GameMode.Dev)
            {
                GameController.SystemError += stackTrace;
            }
            
            SceneManager.LoadScene(SceneError);
        }
    }
}