using UnityEngine;

namespace Scene
{
    public abstract class SceneController : MonoBehaviour
    {
        protected const string MainMenu = "general_main_menu";
        protected const string Options = "general_options";
        protected const string SceneLogin = "general_login";
        protected const string WorldSelect = "single_world_select";
        protected const string GameSystem = "game_system";
    }
}