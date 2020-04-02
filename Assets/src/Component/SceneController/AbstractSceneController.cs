using UnityEngine;

namespace Component.SceneController
{
    public abstract class AbstractSceneController : MonoBehaviour
    {
        protected const string SceneMainMenu = "general_main_menu";
        protected const string SceneOptions = "general_options";
        protected const string SceneLogin = "general_login";
        protected const string SceneWorldSelect = "single_world_select";
        protected const string SceneStarSystem = "game_system";
    }
}